

__all__ = ['Player', ]

from gba.common.bottle.persistable import Persistable

class FreePlayer(Persistable):
    CACHE_KEY = 'free_player:no'
 