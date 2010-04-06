#!/usr/bin/python
# -*- coding: utf-8 -*-


__all__ = ['Player', 'PlayerBetchLog', 'YouthFreePlayer', 'SysConfig'
           , 'YouthPlayer', 'Message', 'Team', 'ProfessionPlayer', 'Matchs',
           'League', 'LeagueTeams', 'Names', 'UserInfo', 'YouthFreeplayerAuctionLog',
           'PlayerAuctionLog', 'ClientRunningLog', 'AttentionPlayer', 'LeagueConfig',
           'ActionDesc', 'TrainingCenter', 'TeamTacticalDetail', 'TeamTactical',
           'MatchNotInPlayer', 'LeagueMatchs', 'RuntimeData', 'ProPlayerCareerStatTotal',
           'ProPlayerSeasonStatTotal', 'ErrorLog']

from gba.common.bottle.persistable import Persistable

class ProfessionPlayer(Persistable):
    CACHE_KEY = 'profession_player:no'
     
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
    
class Matchs(Persistable):
    CACHE_KEY = 'matchs:id'
 
class League(Persistable):
    CACHE_KEY = 'league:id'
    
class LeagueMatchs(Persistable):
    '''联赛赛程'''
    CACHE_KEY = 'league_matchs:id'
 
class LeagueTeams(Persistable):
    CACHE_KEY = 'league_teams:id'
    
class Names(Persistable):
    CACHE_KEY = 'names:id'
    
class UserInfo(Persistable):
    CACHE_KEY = 'user_info:username'
    
class YouthFreeplayerAuctionLog(Persistable):
    '''年轻球员出价表'''
    CACHE_KEY = 'youth_freeplayer_auction_log:username'
    

class PlayerAuctionLog(Persistable):
    '''球员成交记录'''
    CACHE_KEY = 'player_auction_log:player_no'
    
class ClientRunningLog(Persistable):
    '''客户端运行日志'''
    CACHE_KEY = 'client_running_log:id'
    
class AttentionPlayer(Persistable):
    '''关注球员'''
    CACHE_KEY = 'attention_player:id'
    
class LeagueConfig(Persistable):
    '''赛季，轮次信息'''
    CACHE_KEY = 'league_config:id'
    
class ActionDesc(Persistable):
    '''动作描述'''
    CACHE_KEY = 'action_desc:id'
    
class TrainingCenter(Persistable):
    '''训练中心球队'''
    CACHE_KEY = 'training_center:team_id'
    
class TeamTactical(Persistable):
    '''战术'''
    CACHE_KEY = 'team_tactical:team_id,type'

class TeamTacticalDetail(Persistable):
    '''战术详细'''
    CACHE_KEY = 'team_tactical_detail:id'

class MatchNotInPlayer(Persistable):
    '''比赛未上赛球员'''
    CACHE_KEY = 'match_not_in_player:id'
    
class RuntimeData(Persistable):
    '''运行时数据'''
    CACHE_KEY = 'runtime_data:programe,value_key'
    
class MatchStat(Persistable):
    '''比赛统计'''
    CACHE_KEY = 'match_stat:player_no,match_id'

class ProPlayerCareerStatTotal(Persistable):
    '''职业球员生涯总的统计'''
    CACHE_KEY = 'pro_player_career_stat_total:player_no'
    
class ProPlayerSeasonStatTotal(Persistable):
    '''职业球员赛季总的统计，每赛季完要清空'''
    CACHE_KEY = 'pro_player_season_stat_total:player_no'
    
class ErrorLog(Persistable):
    '''错误日志'''
    CACHE_KEY = 'error_log:id'
