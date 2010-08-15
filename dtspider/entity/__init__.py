from dtspider.common.bottle.persistable import Persistable

class SpiderEmail(Persistable):
    CACHE_KEY = 'spider_email:email_md5'
 
class Email(Persistable):
    CACHE_KEY = 'email:email_md5'

class SearchUrl(Persistable):
    CACHE_KEY = 'search_url:url_md5' 
    
class SpiderDomain(Persistable):
    CACHE_KEY = 'spider_domain:domain_md5'
    
class SpiderHistory(Persistable):
    CACHE_KEY = 'spider_history:url_md5'
      
class InjectionHistory(Persistable):
    CACHE_KEY = 'injection_history:url_md5'      
    
class InjectUrlSpiderTask(Persistable):
    CACHE_KEY = 'inject_url_spider_task:keyword_md5'  

class InjectionUrl(Persistable):
    CACHE_KEY = 'injection_url:url_md5'
    
class InjectionResult(Persistable):
    CACHE_KEY = 'injection_result:url_md5'
    
class Md5s(Persistable):
    CACHE_KEY = 'md5s:md532'
    
class SanyaInfo(Persistable):
    CACHE_KEY = 'san_info:url_md5'
    
class RuntimeData(Persistable):
    CACHE_KEY = 'runtime_data:program;value_key'
    
class AskTypeMapping(Persistable):
    CACHE_KEY = 'ask_type_mapping:type'
    
class SanyaTypeMapping(Persistable):
    CACHE_KEY = 'sanya_type_mapping:type'
    
class AskInfo(Persistable):
    CACHE_KEY = 'ask_info:id'
    
class AnswerInfo(Persistable):
    CACHE_KEY = 'answer_info:id'