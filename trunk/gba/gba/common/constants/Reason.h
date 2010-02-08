/************************************************************************
* @file      : Reason.h
* @author    : ChenZhiQiang <chenzhiqiang@kingsoft.com>
* @date      : 16/12/2008 AM 9:36:22
* @brief     : 
*
* $Id: $
/************************************************************************/
#ifndef __REASON_H__
#define __REASON_H__

// -------------------------------------------------------------------------
// urlindex.dat 版本号
// #define RUL_INDEX_VERSON                            L"20090113"

//
// 20090421版本描述
//
// 1> Reason格式
//   
// +-------------------------------------------+
// | top bit |             reason              |           
// +-------------------------------------------+
//    1 bit               31 bits
//
// 32位无符号整数被分为两个部分：
// 最高位置1表示TOP，否则表示LOCAL
// 剩下的31位表示被拦截的原因
//
// 可以使用KWS_TOP_URL_FLAG来测试该Reason表示的是TOP还是LOCAL
// 可以用Reason和KWS_LOCAL_URL_MASK进行与运算获取被拦截的原因
//
// 注意下面对BLOCK_JS_LOCAL和BLOCK_JS_TOP的定义
//

//
// 20090527版稍候发布，目前还是用20090421版
//
#define RUL_INDEX_VERSON                            L"20090421"

//
// 20090527版本描述
// 
// 31                                                      0
// +-------------------------------------------------------+
// | 1 bit | 1 bit |   4 bit   |    18 bit    |    8 bit   |
// +-------------------------------------------------------+
//   top   user/def  URL class    reserved     block reason
//
//
// top
//      如果该位被置1，说明被拦截的URL是TOP，否则是LOCAL。这个位在kwsui的
//      DoNotify函数中被设置，其他地方不应该设置此位。
//
// user/def
//      如果该位被置1，说明被拦截的URL是用户加如黑名单的，否则该URL是被网盾的
//      规则拦截的。
//
// URL class
//      被拦截的URL的分类
//
// block reason
//      URL被拦截的原因
//
//
// 

//
// 20090421版中添加
//

#define KWS_TOP_URL_FLAG                            0x80000000
#define KWS_LOCAL_URL_MASK                          0x7FFFFFFF

//
// 20090527版中添加
//

#define KWS_BLOCK_BY_DEFAULT_RULE   0
#define KWS_BLOCK_BY_USER_RULE      1

#define KWS_URL_CLASS_MALICIOUS     1 // URL是网马地址
#define KWS_URL_CLASS_TROJAN        2 // URL是木马地址
#define KWS_URL_CLASS_PHISHING      3 // URL是钓鱼网址
#define KWS_URL_CLASS_AD            4 // URL是广告地址

#define KWS_BLOCK_REASON_MASK       0x000000FF
#define KWS_USER_DEFINE_FLAG        0x80000000

//
// Parameters
//   u - [in] User defined rule or default rule.
//   c - [in] URL class.
//   b - [in] Block reason.
//
#define MAKE_REASON_INNER(u, c, b) \
    ( ((u) << 30) | ((c) << 26) | ((b) & KWS_BLOCK_REASON_MASK) )

//
// Parameters
//   pr - [out] Pointer to an output buffer.
//   c  - [in]  URL class. usually returned by MatchingUrl function.
//   b  - [in]  Block reason.
//
#define MAKE_REASON(pr, c, b) do {          \
    DWORD ud = KWS_BLOCK_BY_DEFAULT_RULE;   \
    DWORD uc = (c) & ~KWS_USER_DEFINE_FLAG; \
    DWORD br = (b) & KWS_BLOCK_REASON_MASK; \
    if ((c) & KWS_USER_DEFINE_FLAG)         \
        ud = KWS_BLOCK_BY_USER_RULE;        \
    *pr = MAKE_REASON_INNER(ud, uc, br);    \
} while (0)

#define GET_URL_CLASS( reason )  ((reason >> 26) & 0x0F)

//////////////////////////////////////////////////////////////////////////

//ie7被丁
#define BLOCK_REASON_IE7_PATCH                         0x01

//占坑
#define BLOCK_REASON_TAKEUP                            0x02

//ms06014
#define BLOCK_REASON_MS06014                           0x03                            

//检查到利用HeapSpray 的恶意代码
#define BLOCK_FUNCTION_RETURN_ADDRESS_HEAPSPRAY        0x04

//检测到其返回地址是在栈空间
#define BLOCK_FUNCTION_RETURN_ADDRESS_IN_STACK         0x05

//根据返回地址的页属性不符合要求
#define BLOCK_FUNCTION_RETURN_ADDRESS_PAGEATTRIBUTECHECK 0x06

//命令行解析
#define BLOCK_REASON_CREATEPROCESS                      0x07


//根据返回地址的页属性不符合要求
#define BLOCK_FUNCTION_RETURN_ADDRESS_PAGEATTRIBUTECHECK_FLASH 0x08

//恶意网址库MD5
#define BLOCK_NET_ADRESS_SPITE                      0x09

//恶意网址库模糊匹配
#define BLOCK_NET_ADRESS_SPITE_REGEX_MATCH          0xA

//恶意网址库MD5 _Send
#define BLOCK_NET_ADRESS_SPITE_MATCH_SEND           0xB

//恶意网址库模糊匹配_send
#define BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND     0xC

//静态脚本地栏
//#define BLOCK_JS_TOP                                0xD

//静态脚本地址
#define BLOCK_JS_LOCAL                              0xE

#define BLOCK_JS_TOP                                (BLOCK_JS_LOCAL | KWS_TOP_URL_FLAG)

#define BLOCK_MALICIOUS_SWF                         0xF

#define NET_PHISHING								0x10

#define THIRD_PARTY_ARG_CHECK						0x11

#define PPLIVE_ARG_CHECK							0x12

//URL中有6个或者6个以上的‘.’
#define CRITICAL_DOT_COUNT_IN_URL                   0x13

// 拦截到广告
#define BLOCK_ADV                                   0x14

// 脚本测试特征
#define BLOCK_JS_TEST                               0x15

// 拦截到5173的钓鱼
#define BLOCK_5173                                  0x16

#define BLOCK_MPEG2_EXP                             0x17

#define BLOCK_COLLECT_PHISHING_NET                  0x18

#define BLOCK_COLLECT_HEAP_SPRAY                    0x19

#define BLOCK_BY_BHO                                0x1A
// -------------------------------------------------------------------------
// $Log: $

#endif /* __REASON_H__ */
