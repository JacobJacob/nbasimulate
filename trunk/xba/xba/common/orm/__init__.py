
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

engine = create_engine('mssql+pymssql://BTPAdmin:BTPAdmin123@127.0.0.1:1433/NewBTP', echo=True)
connection = engine.connect()
Session = sessionmaker(bind=engine)
