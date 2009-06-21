#ifndef _KSWEBSHIELDNOTIFY_H
#define _KSWEBSHIELDNOTIFY_H

class IKwsBlockCallback;

class __declspec(uuid("447283B9-03F0-4baf-B998-97FC9B4E4904"))
IKXWebShieldNotify : public IUnknown
{
public:

    //
    // Routine description
    //
    //  注册一个回调接口。该回调接口的BlockNotifyRoutine方法会在每次拦截到恶意
    //  链接的时候被调用。
    //

    virtual BOOL __stdcall RegisterBlockCallback(
        IKwsBlockCallback *pCallback
        ) = 0;
};

#endif