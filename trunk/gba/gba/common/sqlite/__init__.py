from connection import Connection

connection = Connection('spiderproxy.s3db')

def create_connection():
    connection = Connection('spiderproxy.s3db')
    return connection

#del Connection

__all__ = ['connection', ]