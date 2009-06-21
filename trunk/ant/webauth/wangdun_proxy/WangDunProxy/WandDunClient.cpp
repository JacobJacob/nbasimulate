// WandDunClient.cpp : Implementation of CWandDunClient

#include "stdafx.h"
#include "WandDunClient.h"

#include "KwsBlockCallback.h"
#include "KwsBlockCallbackImpl.h"
#include <comutil.h.>
#include <algorithm>
#include <atlstr.h>

typedef HRESULT(__stdcall *GetClassObjectType)(	
	/*const*/ REFIID riid,
	void  **ppvObject
	);

VOID
TRACE_PRINT(
			PCHAR szFormat,
			...
			);
// CWandDunClient

STDMETHODIMP CWandDunClient::SetSite(IUnknown* pUnkSite)
{
	if (pUnkSite != NULL)
	{
		// Cache the pointer to IWebBrowser2.
		HRESULT hr = pUnkSite->QueryInterface(IID_IWebBrowser2, (void **)&m_spWebBrowser);
		if (SUCCEEDED(hr))
		{
			// Register to sink events from DWebBrowserEvents2.
			hr = DispEventAdvise(m_spWebBrowser);
			if (SUCCEEDED(hr))
			{
				m_fAdvised = TRUE;
			}

			{
				GetClassObjectType pfnGetClassObject = NULL;
				HMODULE hModule = GetModuleHandleA("kwsui.dll");
				if (NULL == hModule)
				{
					TRACE_PRINT("Register: Unable to Get handle of module kwsui.dll.\n");
				}
				else{

					//查找当前路径,生成日志文件路径
					char tmpCurPath[MAX_PATH] ;
					GetModuleFileNameA(hModule,tmpCurPath,MAX_PATH);

					string strCurPath = tmpCurPath;

					int iPos = (int)strCurPath.rfind('\\');
					string strUpPath = strCurPath.substr(0,iPos + 1);

					strUpPath += "urlindex.dat";
					

					pfnGetClassObject = (GetClassObjectType)GetProcAddress(hModule, "GetClassObject");
					if (NULL == pfnGetClassObject)
					{
						TRACE_PRINT("Register: Unable to get address of function GetClassObject.\n");
					}
					else{

						HRESULT hr = pfnGetClassObject(__uuidof(IKXWebShieldNotify), (VOID **)&m_piNotify);
						if (FAILED(hr) || NULL == m_piNotify)
						{
							TRACE_PRINT("Register: Unable to get interface IKXWebShieldNotify.\n");

						}
						else{
							m_pIKwsBlockCallback = CKwsBlockCallbackImpl::GetInstance();
							((CKwsBlockCallbackImpl*)m_pIKwsBlockCallback)->setLogFile(strUpPath);

							m_piNotify->RegisterBlockCallback(m_pIKwsBlockCallback);
						}
					}
				}

			}
		}
	}
	else
	{
		// Unregister event sink.
		if (m_fAdvised)
		{
			DispEventUnadvise(m_spWebBrowser);
			m_fAdvised = FALSE;
		}


		if (m_piNotify != NULL)
			m_piNotify->Release();

		// Release cached pointers and other resources here.
		m_spWebBrowser.Release();
	}

	// Return the base class implementation
	return IObjectWithSiteImpl<CWandDunClient>::SetSite(pUnkSite);
}

void STDMETHODCALLTYPE CWandDunClient::OnBeforeNavigate2(IDispatch *pDisp,
													   VARIANT *url,
													   VARIANT *Flags,
													   VARIANT *TargetFrameName,
													   VARIANT *PostData,
													   VARIANT *Headers,
													   VARIANT_BOOL *Cancel)
{
	
	if (m_spWebBrowser.IsEqualObject(pDisp))
	{
		USES_CONVERSION;  
//		char*       csStr   =   CW2A(url->bstrVal);
//		MessageBoxA(NULL,csStr,NULL,MB_OK);
		
		//wstring wstrU = url->bstrVal;
		//MessageBoxW(NULL,wstrU.c_str(),L"TTT",MB_OK);
		//const wchar_t* pstr = wstrU.c_str();

		CAtlString strUrlT = url->bstrVal;

		string strUrl = string(_com_util::ConvertBSTRToString(url->bstrVal));
		transform(strUrl.begin(), strUrl.end(), strUrl.begin(), tolower);

		((CKwsBlockCallbackImpl*)m_pIKwsBlockCallback)->setCurUrl(strUrl);

	}
}