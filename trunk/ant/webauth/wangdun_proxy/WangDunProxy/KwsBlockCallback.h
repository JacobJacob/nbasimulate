#ifndef __KWSBLOCKCALLBACK__
#define __KWSBLOCKCALLBACK__

class __declspec(uuid("73761028-B13A-47e0-AFFA-D99EF856BF24"))
IKwsBlockCallback
{
public:

    //
    // Routine description
    //
    //  此回调函数在恶意URL被拦截的时候调用.
    //
    // Parameters
    //
    //  lpNotifyMessage
    //      拦截到恶意URL时要显示的消息. 如"发现xxx进程正在访问xxx网页, 已成功
    //      阻止".
    //
    //  lpLocalUrl
    //      拦截到的恶意链接, 比如, 网页引用的一个JS脚本的URL. 注意, 该参数不是
    //      浏览器地址栏上的URL.
    //
    //  nReason
    //      URL被拦截的原因, 该参数的取值及其意义请参考Reason.h.
    //

    virtual VOID __stdcall BlockNotifyRoutine(
        LPCWSTR lpNotifyMessage, 
        LPCWSTR lpLocalUrl, 
        UINT nReason) = 0;
};

#endif
