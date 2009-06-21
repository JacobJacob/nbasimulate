
// http://svn.rdev.kingsoft.net/KXEngine/WebShield/trunk/Publish/Reason.h

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
//urlindex.dat �汾��
//#define RUL_INDEX_VERSON                            L"20090113"

//
// 20090421�汾����
//
// 1> Reason��ʽ
//   
// +-------------------------------------------+
// | top bit |             reason              |           
// +-------------------------------------------+
//    1 bit               31 bits
//
// 32λ�޷�����������Ϊ�������֣�
// ���λ��1��ʾTOP�������ʾLOCAL
// ʣ�µ�31λ��ʾ�����ص�ԭ��
//
// ����ʹ��KWS_TOP_URL_FLAG�����Ը�Reason��ʾ����TOP����LOCAL
// ������Reason��KWS_LOCAL_URL_MASK�����������ȡ�����ص�ԭ��
//
// ע�������BLOCK_JS_LOCAL��BLOCK_JS_TOP�Ķ���
//

//
// 20090527���Ժ򷢲���Ŀǰ������20090421��
//
#define RUL_INDEX_VERSON_A                            "20090114"
#define RUL_INDEX_VERSON                            L"20090114"

//
// 20090527�汾����
// 
// 31                                                      0
// +-------------------------------------------------------+
// | 1 bit | 1 bit |   4 bit   |    18 bit    |    8 bit   |
// +-------------------------------------------------------+
//   top   user/def  URL class    reserved     block reason
//
//
// top
//      �����λ����1��˵�������ص�URL��TOP��������LOCAL�����λ��kwsui��
//      DoNotify�����б����ã������ط���Ӧ�����ô�λ��
//
// user/def
//      �����λ����1��˵�������ص�URL���û�����������ģ������URL�Ǳ����ܵ�
//      �������صġ�
//
// URL class
//      �����ص�URL�ķ���
//
// block reason
//      URL�����ص�ԭ��
//
//
// 

//
// 20090421�������
//

#define KWS_TOP_URL_FLAG                            0x80000000
#define KWS_LOCAL_URL_MASK                          0x7FFFFFFF

//
// 20090527�������
//

#define KWS_BLOCK_BY_DEFAULT_RULE   0
#define KWS_BLOCK_BY_USER_RULE      1

#define KWS_URL_CLASS_MALICIOUS     1 // URL�������ַ
#define KWS_URL_CLASS_TROJAN        2 // URL��ľ���ַ
#define KWS_URL_CLASS_PHISHING      3 // URL�ǵ�����ַ
#define KWS_URL_CLASS_AD            4 // URL�ǹ���ַ

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

//ie7����
#define BLOCK_REASON_IE7_PATCH                         0x01

//ռ��
#define BLOCK_REASON_TAKEUP                            0x02

//ms06014
#define BLOCK_REASON_MS06014                           0x03                            

//��鵽����HeapSpray �Ķ������
#define BLOCK_FUNCTION_RETURN_ADDRESS_HEAPSPRAY        0x04

//��⵽�䷵�ص�ַ����ջ�ռ�
#define BLOCK_FUNCTION_RETURN_ADDRESS_IN_STACK         0x05

//���ݷ��ص�ַ��ҳ���Բ�����Ҫ��
#define BLOCK_FUNCTION_RETURN_ADDRESS_PAGEATTRIBUTECHECK 0x06

//�����н���
#define BLOCK_REASON_CREATEPROCESS                      0x07


//���ݷ��ص�ַ��ҳ���Բ�����Ҫ��
#define BLOCK_FUNCTION_RETURN_ADDRESS_PAGEATTRIBUTECHECK_FLASH 0x08

//������ַ��MD5
#define BLOCK_NET_ADRESS_SPITE                      0x09

//������ַ��ģ��ƥ��
#define BLOCK_NET_ADRESS_SPITE_REGEX_MATCH          0xA

//������ַ��MD5 _Send
#define BLOCK_NET_ADRESS_SPITE_MATCH_SEND           0xB

//������ַ��ģ��ƥ��_send
#define BLOCK_NET_ADRESS_SPITE_REGEX_MATCH_SEND     0xC

//��̬�ű�����
//#define BLOCK_JS_TOP                                0xD

//��̬�ű���ַ
#define BLOCK_JS_LOCAL                              0xE

#define BLOCK_JS_TOP                                (BLOCK_JS_LOCAL | KWS_TOP_URL_FLAG)

#define BLOCK_MALICIOUS_SWF                         0xF

#define NET_PHISHING								0x10

#define THIRD_PARTY_ARG_CHECK						0x11

#define PPLIVE_ARG_CHECK							0x12

//URL����6������6�����ϵġ�.��
#define CRITICAL_DOT_COUNT_IN_URL                   0x13

// ���ص����
#define BLOCK_ADV                                   0x14

// �ű���������
#define BLOCK_JS_TEST                               0x15
// -------------------------------------------------------------------------
// $Log: $

#endif /* __REASON_H__ */
