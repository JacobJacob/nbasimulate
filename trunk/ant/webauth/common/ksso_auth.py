#--*-- coding:utf-8 --*--
# lvyi <lvyi@kingsoft.com>

"""
= 依赖与安装 =
依赖python-ldap;
$sudo apt-get install  python-ldap;

= 帮助 =
提供检查用户是否存在 Check_user_exist(userName) ，和验证用户 User_authentication(userAccount,userPassword) 两个公共方法.用户如果验证正确返回用户基本信息.

= 其他信息 =
* 如需修改密码请访问
 http://sso.rdev.kingsoft.net/chpasswd

* KSSO除python验证模块外，当前还提供java,php,webservice等验证方式。
"""


import ldap,ldap.filter,ldap.modlist

_ldap_server_addr = 'ldap://sso.rdev.kingsoft.net:389'
_migrate_target_bind_dn = 'cn=mercury_auth,ou=specialentity,ou=kss,ou=ZH,dc=kingsoft,dc=net'
_migrate_target_bind_pw = 'mercury_auth'
_search_base = 'ou=employees,ou=kss,ou=ZH,dc=kingsoft,dc=net'


class KSAuthenException(Exception):	
    "king soft authen with Ksso Exception"

def get_sys_ldap_link(server_addr=_ldap_server_addr,dn=_migrate_target_bind_dn,pw=_migrate_target_bind_pw):
    _sys_ldap_link = ldap.initialize(server_addr)
    _sys_ldap_link.timelimit = 30  #set timelimit to 30 sec
    _sys_ldap_link.simple_bind_s(dn,pw)
    return _sys_ldap_link


def get_search_filter_by_account(userAccount):
    """get search filter by user mail Account;input security check plus  """
    userAccount = userAccount.strip()
    userAccount = ldap.filter.escape_filter_chars(userAccount)  # escape bad characters
    return ldap.filter.filter_format('(&(objectClass=organizationalPerson)(cn=%s))',(userAccount,))


def get_userDN_by_account(userAccount,search_base=_search_base):
    """get userDN plus othe info by UserEmail account """
    l = get_sys_ldap_link()
    search_filter = get_search_filter_by_account(userAccount)
    res = l.search_s(search_base,ldap.SCOPE_SUBTREE,search_filter,('mail','displayName','cn','sn','description'),attrsonly=0)
    if len(res) != 1 :    # search result empty or not unique
        raise KSAuthenException , "your account not existence or make multi search result"
    return res[0][0] , res[0][1] #  res[0][1] is dic {'mail':'xxxx','sn':'xxx'.....}


def Check_user_exist(userName):
    """
    检查用户是否存在
    = 参数 =
    用户名
    = 返回值 =
    用户存在: True
    用户不存在: False
    """
    try:
        get_userDN_by_account(userName)
    except:
        return False
    else:
        return True
       
def User_authentication(userAccount,userPassword,ldap_server_addr = _ldap_server_addr ):
    """
    验证用户
    = 参数 =
    userAccount :  用户ksso用户名，除特殊帐号外 ksso帐户名与北京企业邮箱帐户名一致.
    userPassword:  用户密码
    ldap_server_addr(可选)， 用于链接测试ldap服务器进行验证。
    = 返回值 =
    == 验证失败 ==
    False
    == 验证成功 ==
    (True,userDict) ; userDict 为包含用户基本信息的字典。其中包含
    cn(用户帐号),description(简单描述,一般为其组织信息),displayName(一般为拼音名与中文名),mail(邮箱地址),sn(中文名)
    """

    try:
        userDN , userDict = get_userDN_by_account(userAccount)
        l = ldap.initialize(ldap_server_addr)
        l.timelimit = 30 #set time limit to 30 sec        
        l.simple_bind_s(userDN,userPassword)
#    except ldap.INVALID_CREDENTIALS,e:    #这有潜在的隐患        
    except Exception,e:
        return False
#	    raise KSAuthenException ,'password wrong.'
    else:
        return True , userDict


     
