

__all__ = ['Player', 'PlayerBetchLog', 'YouthFreePlayer', 'SysConfig'
           , 'YouthPlayer', 'Message', 'Team']

from gba.common.bottle.persistable import Persistable

class FreePlayer(Persistable):
    CACHE_KEY = 'free_player:no'
    
class YouthFreePlayer(Persistable):
    CACHE_KEY = 'youth_free_player:no'

class YouthPlayer(Persistable):
    CACHE_KEY = 'youth_player:no'
    
class PlayerBetchLog(Persistable):
    CACHE_KEY = 'player_betch_log:betch_no'
    
class SysConfig(Persistable):
    CACHE_KEY = 'sys_config:key'
    
class Message(Persistable):
    CACHE_KEY = 'message:id'
    
class Team(Persistable):
    CACHE_KEY = 'team:username'
 
 