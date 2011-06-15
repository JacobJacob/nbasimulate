
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, mapper
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, Integer, String, DateTime

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
    
class Player3(Base):
    
    __tablename__ = 'btp_player3'
    playerid = Column(Integer, primary_key=True)
    clubid = Column(Integer)
    name = Column(String)
    category = Column(Integer)
    status = Column(Integer)
    age = Column(Integer)
    pos = Column(Integer)
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
    
class Cup(Base):
    
    __tablename__ = 'btp_cup'
    cupid = Column("CupID", Integer, primary_key=True)
    setid = Column(Integer)
    category = Column(Integer)
    levels = Column(Integer)
    unionid = Column(Integer)
    name = Column(String)
    status = Column(Integer)
    money_cost = Column("MoneyCost", Integer)
    small_logo = Column("SmallLogo", String)
    big_logo = Column("BigLogo", String)
    requirement_xml = Column("RequirementXml", String)
    reward_xml = Column("RewardXml", String)
    round = Column(Integer)
    capacity = Column(Integer)
    ladder_url = Column("LadderUrl", String)
    create_time = Column("CreateTime", DateTime)
    end_reg_time = Column("EndRegTime", DateTime)
    matchtime = Column("MatchTime", DateTime)
    champion_userid = Column("ChampionUserid", Integer)
    champion_club_name = Column("ChampionClubName", String)
    regcount = Column("RegCount", Integer)
    coin = Column(Integer)
    ticket_category = Column("TicketCategory", Integer)
