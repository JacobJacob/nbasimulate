// WandDunClient.h : Declaration of the CWandDunClient

#pragma once
#include "resource.h"       // main symbols

#include "WangDunProxy.h"
#include <shlguid.h>     // IID_IWebBrowser2, DIID_DWebBrowserEvents2, etc.
#include <exdispid.h> // DISPID_DOCUMENTCOMPLETE, etc.

#include "KwsBlockCallback.h"
#include "KsWebshieldNotify.h"

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif



// CWandDunClient

class ATL_NO_VTABLE CWandDunClient :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CWandDunClient, &CLSID_WandDunClient>,
	public IObjectWithSiteImpl<CWandDunClient>,
	public IDispatchImpl<IWandDunClient, &IID_IWandDunClient, &LIBID_WangDunProxyLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IDispEventImpl<1, CWandDunClient, &DIID_DWebBrowserEvents2, &LIBID_SHDocVw, 1, 1>

{
public:
	CWandDunClient()
	{ 
		
	}

DECLARE_REGISTRY_RESOURCEID(IDR_WANDDUNCLIENT)

DECLARE_NOT_AGGREGATABLE(CWandDunClient)

BEGIN_COM_MAP(CWandDunClient)
	COM_INTERFACE_ENTRY(IWandDunClient)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IObjectWithSite)
END_COM_MAP()

BEGIN_SINK_MAP(CWandDunClient)
	SINK_ENTRY_EX(1, DIID_DWebBrowserEvents2, DISPID_BEFORENAVIGATE2, OnBeforeNavigate2)
END_SINK_MAP()

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	void STDMETHODCALLTYPE OnBeforeNavigate2(IDispatch *pDisp,
		VARIANT *url,
		VARIANT *Flags,
		VARIANT *TargetFrameName,
		VARIANT *PostData,
		VARIANT *Headers,
		VARIANT_BOOL *Cancel); 

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:
	STDMETHOD(SetSite)(IUnknown *pUnkSite);

private:
	CComPtr<IWebBrowser2>  m_spWebBrowser;

	IKXWebShieldNotify* m_piNotify;
	IKwsBlockCallback* m_pIKwsBlockCallback;


	BOOL m_fAdvised; 

};

OBJECT_ENTRY_AUTO(__uuidof(WandDunClient), CWandDunClient)
