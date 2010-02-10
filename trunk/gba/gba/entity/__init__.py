

__all__ = ['Player', 'PlayerBetchLog']

from gba.common.bottle.persistable import Persistable

class FreePlayer(Persistable):
    CACHE_KEY = 'free_player:no'
    
class PlayerBetchLog(Persistable):
    CACHE_KEY = 'player_betch_log:betch_no'
 
 