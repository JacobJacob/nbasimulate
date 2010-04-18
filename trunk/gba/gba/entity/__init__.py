#!/usr/bin/python
# -*- coding: utf-8 -*-


__all__ = ['Player', 'PlayerBetchLog', 'YouthFreePlayer', 'SysConfig'
           , 'YouthPlayer', 'Message', 'Team', 'ProfessionPlayer', 'Matchs',
           'League', 'LeagueTeams', 'Names', 'UserInfo', 'YouthFreeplayerAuctionLog',
           'PlayerAuctionLog', 'ClientRunningLog', 'AttentionPlayer', 'LeagueConfig',
           'ActionDesc', 'TrainingCenter', 'TeamTacticalDetail', 'TeamTactical',
           'MatchNotInPlayer', 'LeagueMatchs', 'RuntimeData', 'ProPlayerCareerStatTotal',
           'ProPlayerSeasonStatTotal', 'ErrorLog', 'TeamStaff', 'MatchNodosityMain',
           'MatchNodosityDetail', 'MatchNodosityTacticalDetail', 'TeamArena',
           'RoundUpdateLog', 'SeasonFinance', 'AllFinance', 'TeamAd', 'ChatMessage',
           'TacticalGrade', 'TrainingRemain', 'Friends', 'OutMessage', 'TeamHonor',
           'Cup']

from gba.common.bottle.persistable import Persistable

class ProfessionPlayer(Persistable):
    '''职业球员'''
    CACHE_KEY = 'profession_player:no'
     
class FreePlayer(Persistable):
    '''职业自由球员'''
    CACHE_KEY = 'free_player:no'
    
class YouthFreePlayer(Persistable):
    '''街头自由球员'''
    CACHE_KEY = 'youth_free_player:no'

class YouthPlayer(Persistable):
    '''街头球员'''
    CACHE_KEY = 'youth_player:no'
    
class PlayerBetchLog(Persistable):
    '''球员生成日志'''
    CACHE_KEY = 'player_betch_log:betch_no'
    
class SysConfig(Persistable):
    '''系统信息'''
    CACHE_KEY = 'sys_config:key'
    
class Message(Persistable):
    '''消息'''
    CACHE_KEY = 'message:id'
    
class OutMessage(Persistable):
    '''发件箱'''
    CACHE_KEY = 'out_message:id'
    
class Team(Persistable):
    '''球队'''
    CACHE_KEY = 'team:username'
    
class Matchs(Persistable):
    '''球队'''
    CACHE_KEY = 'matchs:id'
 
class League(Persistable):
    '''联赛'''
    CACHE_KEY = 'league:id'
    
class LeagueMatchs(Persistable):
    '''联赛赛程'''
    CACHE_KEY = 'league_matchs:id'
 
class LeagueTeams(Persistable):
    '''联赛虚拟球队，通过映射来映射到真正的球队'''
    CACHE_KEY = 'league_teams:id'
    
class LeagueTasks(Persistable):
    CACHE_KEY = 'league_tasks:id'
    
class Names(Persistable):
    '''名字库'''
    CACHE_KEY = 'names:id'
    
class UserInfo(Persistable):
    '''用户表'''
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
    
class MatchNodosityTacticalDetail(Persistable):
    '''比赛每节战术详细'''
    CACHE_KEY = 'match_nodosity_tactical_detail:id'
    
class MatchNodosityMain(Persistable):
    '''比赛每节概要'''
    CACHE_KEY = 'match_nodosity_main:id'
    
class MatchNodosityDetail(Persistable):
    '''比赛每节详细'''
    CACHE_KEY = 'match_nodosity_detail:id'
    
class ProPlayerCareerStatTotal(Persistable):
    '''职业球员生涯总的统计'''
    CACHE_KEY = 'pro_player_career_stat_total:player_no'
    
class ProPlayerSeasonStatTotal(Persistable):
    '''职业球员赛季总的统计，每赛季完要清空'''
    CACHE_KEY = 'pro_player_season_stat_total:player_no'
    
class ErrorLog(Persistable):
    '''错误日志'''
    CACHE_KEY = 'error_log:id'
    
class TeamStaff(Persistable):
    '''球队职员'''
    CACHE_KEY = 'team_staff:team_id,type,is_youth'
    
class TeamArena(Persistable):
    '''创建球馆'''
    CACHE_KEY = 'team_arena:id'
    
class RoundUpdateLog(Persistable):
    '''每轮更新日志'''
    CACHE_KEY = 'round_update_log:id'
    
class SeasonFinance(Persistable):
    '''赛季球队收支明细'''
    CACHE_KEY = 'season_finance:id'
    
class AllFinance(Persistable):
    '''球队每赛季收支明细'''
    CACHE_KEY = 'all_finance:id'
    
class TeamAd(Persistable):
    '''球队广告'''
    CACHE_KEY = 'team_ad:id'
    
class ChatMessage(Persistable):
    '''联天信息'''
    CACHE_KEY = 'chat_message:id'
    
class TacticalGrade(Persistable):
    '''战术等级'''
    CACHE_KEY = 'tactical_grade:id'

class TrainingRemain(Persistable):
    '''训练赛剩余次数'''
    CACHE_KEY = 'training_remain:team_id'
    
class Friends(Persistable):
    '''友好'''
    CACHE_KEY = 'friends:team_id,friend_team_id'
    
class TeamHonor(Persistable):
    '''球队荣誉'''
    CACHE_KEY = 'team_honor:id'
    
class Cup(Persistable):
    '''奖杯'''
    CACHE_KEY = 'cup:id'