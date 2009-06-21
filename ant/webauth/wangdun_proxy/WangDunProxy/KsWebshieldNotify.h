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
    //  ע��һ���ص��ӿڡ��ûص��ӿڵ�BlockNotifyRoutine��������ÿ�����ص�����
    //  ���ӵ�ʱ�򱻵��á�
    //

    virtual BOOL __stdcall RegisterBlockCallback(
        IKwsBlockCallback *pCallback
        ) = 0;
};

#endif