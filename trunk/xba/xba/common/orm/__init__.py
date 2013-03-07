
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from xba.config import DbSetting

url = 'mssql+pymssql://%s:%s@%s:1433/%s' % (DbSetting.DATABASE_USER, DbSetting.DATABASE_PASSWORD, DbSetting.DATABASE_HOST, DbSetting.DATABASE_NAME)
engine = create_engine(url, echo=True)
connection = engine.connect()
Session = sessionmaker(bind=engine)
