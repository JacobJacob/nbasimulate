

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 6.00.0366 */
/* at Fri Jun 12 16:58:33 2009
 */
/* Compiler settings for .\WangDunProxy.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __WangDunProxy_h__
#define __WangDunProxy_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IWandDunClient_FWD_DEFINED__
#define __IWandDunClient_FWD_DEFINED__
typedef interface IWandDunClient IWandDunClient;
#endif 	/* __IWandDunClient_FWD_DEFINED__ */


#ifndef __WandDunClient_FWD_DEFINED__
#define __WandDunClient_FWD_DEFINED__

#ifdef __cplusplus
typedef class WandDunClient WandDunClient;
#else
typedef struct WandDunClient WandDunClient;
#endif /* __cplusplus */

#endif 	/* __WandDunClient_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 

void * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void * ); 

#ifndef __IWandDunClient_INTERFACE_DEFINED__
#define __IWandDunClient_INTERFACE_DEFINED__

/* interface IWandDunClient */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IWandDunClient;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FD16CD5D-6D89-4AF1-81D9-5F556BE47683")
    IWandDunClient : public IDispatch
    {
    public:
    };
    
#else 	/* C style interface */

    typedef struct IWandDunClientVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IWandDunClient * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IWandDunClient * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IWandDunClient * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IWandDunClient * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IWandDunClient * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IWandDunClient * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IWandDunClient * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } IWandDunClientVtbl;

    interface IWandDunClient
    {
        CONST_VTBL struct IWandDunClientVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IWandDunClient_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IWandDunClient_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IWandDunClient_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IWandDunClient_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IWandDunClient_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IWandDunClient_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IWandDunClient_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IWandDunClient_INTERFACE_DEFINED__ */



#ifndef __WangDunProxyLib_LIBRARY_DEFINED__
#define __WangDunProxyLib_LIBRARY_DEFINED__

/* library WangDunProxyLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_WangDunProxyLib;

EXTERN_C const CLSID CLSID_WandDunClient;

#ifdef __cplusplus

class DECLSPEC_UUID("7131E933-5B8B-4ED0-BD55-A055D246162A")
WandDunClient;
#endif
#endif /* __WangDunProxyLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


