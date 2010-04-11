INSERT INTO `league_config` (`Id`,`season`,`round`,`created_time`,`updated_time`) VALUES (1,1,1,'2010-03-19 21:17:58','2010-03-19 21:18:09');

update league_matchs set status = 0;
update league_teams set win = 0, lose=0, net_points=0 ,status=0, team_id=-1;
truncate user_info;
truncate team;
truncate team_arena;
truncate team_tactical;
truncate team_tactical_detail;
truncate youth_player;
truncate profession_player;