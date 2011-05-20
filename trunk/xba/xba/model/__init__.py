
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, mapper
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, Integer, String

Base = declarative_base()

class Player5(Base):
    
    __tablename__ = 'btp_player5'
    playerid = Column(Integer, primary_key=True)
    clubid = Column(Integer)
    name = Column(String)
    category = Column(Integer)
    status = Column(Integer)
    age = Column(Integer)
    pos = Column(Integer)
    salary = Column(Integer)
    power = Column(Integer)
    height = Column(Integer)
    weight = Column(Integer)
    number = Column(Integer)
    
    
class Game(Base):
    
    __tablename__ = 'btp_game'
    gameid = Column(Integer, primary_key=True)
    devlevelsum = Column(Integer)
    status = Column(Integer)
    season = Column(Integer)
    turn = Column(Integer)
    ischoose = Column(Integer)
    days = Column(Integer)
    
class Club(Base):
    
    __tablename__ = 'btp_club'
    clubid = Column(Integer, primary_key=True)
    userid = Column(Integer)
    name = Column(String)
    category = Column(Integer)
    mainxml = Column(String)
