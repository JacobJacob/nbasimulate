
from gba.business.user_roles import UserManager

class TeamInfoMiddleware(object):

    def __init__(self):
        pass

    def process_request(self, request):
        team = UserManager().get_team_info(request)
        setattr(request, 'team', team)