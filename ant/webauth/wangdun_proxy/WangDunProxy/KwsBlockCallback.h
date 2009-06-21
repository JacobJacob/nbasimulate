#ifndef __KWSBLOCKCALLBACK__
#define __KWSBLOCKCALLBACK__

class __declspec(uuid("73761028-B13A-47e0-AFFA-D99EF856BF24"))
IKwsBlockCallback
{
public:

    //
    // Routine description
    //
    //  �˻ص������ڶ���URL�����ص�ʱ�����.
    //
    // Parameters
    //
    //  lpNotifyMessage
    //      ���ص�����URLʱҪ��ʾ����Ϣ. ��"����xxx�������ڷ���xxx��ҳ, �ѳɹ�
    //      ��ֹ".
    //
    //  lpLocalUrl
    //      ���ص��Ķ�������, ����, ��ҳ���õ�һ��JS�ű���URL. ע��, �ò�������
    //      �������ַ���ϵ�URL.
    //
    //  nReason
    //      URL�����ص�ԭ��, �ò�����ȡֵ����������ο�Reason.h.
    //

    virtual VOID __stdcall BlockNotifyRoutine(
        LPCWSTR lpNotifyMessage, 
        LPCWSTR lpLocalUrl, 
        UINT nReason) = 0;
};

#endif
