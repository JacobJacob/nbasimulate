from connection import Connection

connection = Connection('spiderproxy.s3db')

del Connection

__all__ = ['connection', ]