#-*- coding:utf-8 -*-

import time

from gba.web.render import render_to_response
from gba.business.user_roles import UserManager, login_required
from gba.entity import LeagueConfig, LeagueMatchs, LeagueTeams, TeamTicketHistory
from gba.common import commonutil

@login_required
def index(request):
    '''首页'''
    
    team = request.team
    league_config = LeagueConfig.load(id=1)
    start = False
    datas = {}
    if league_config.round == 1:
        start = True
    else:
        league_team = LeagueTeams.load(team_id=team.id)
        league_match = LeagueMatchs.query(condition='(match_team_home_id="%s" or match_team_guest_id="%s") and round="%s"' %\
                                           (league_team.id, league_team.id, league_config.round-1), limit=1)
        
        if league_match:
            league_match = league_match[0]
            timetuple = league_match.updated_time.timetuple()
            month = timetuple[1]
            date = timetuple[2]
    
            point_data = commonutil.change_point_to_score_card(league_match.point)
         
            datas.update({'league_match': league_match, 'start': start, 'month': month, 'date': date, 'league_team': league_team})
            datas.update(point_data)
            
            league_home_team = LeagueTeams.load(id=league_match.match_team_home_id)
        
            team_ticket_history = TeamTicketHistory.load(team_id=league_home_team.team_id)
            datas.update({'team_ticket_history': team_ticket_history})
            
    return render_to_response(request, "index.html", datas)

def development(request):
    return render_to_response(request, "development.html", locals())

