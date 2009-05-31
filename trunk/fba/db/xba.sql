/*
MySQL Data Transfer
Source Host: localhost
Source Database: xba
Target Host: localhost
Target Database: xba
Date: 2009/5/12 16:29:24
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for action_desc
-- ----------------------------
DROP TABLE IF EXISTS `action_desc`;
CREATE TABLE `action_desc` (
  `id` bigint(20) NOT NULL auto_increment,
  `result` varchar(255) default NULL,
  `ACTION_DESC` varchar(255) default NULL,
  `ACTION_NAME` varchar(255) default NULL,
  `flg` varchar(255) default NULL,
  `percent` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for free_player
-- ----------------------------
DROP TABLE IF EXISTS `free_player`;
CREATE TABLE `free_player` (
  `id` bigint(20) NOT NULL auto_increment,
  `name` varchar(255) default NULL,
  `age` int(11) default NULL,
  `position` varchar(2) default NULL,
  `stature` int(11) default NULL,
  `avoirdupois` int(11) default NULL,
  `team_Id` bigint(20) default NULL,
  `ability` double default NULL,
  `shooting` smallint(6) default NULL,
  `speed` smallint(6) default NULL,
  `strength` smallint(6) default NULL,
  `bounce` smallint(6) default NULL,
  `stamina` smallint(6) default NULL,
  `trisection` smallint(6) default NULL,
  `dribble` smallint(6) default NULL,
  `pass` smallint(6) default NULL,
  `backboard` smallint(6) default NULL,
  `steal` smallint(6) default NULL,
  `blocked` smallint(6) default NULL,
  `shooting_max` smallint(6) default NULL,
  `speed_max` smallint(6) default NULL,
  `strength_max` smallint(6) default NULL,
  `bounce_max` smallint(6) default NULL,
  `stamina_max` smallint(6) default NULL,
  `trisection_max` smallint(6) default NULL,
  `dribble_max` smallint(6) default NULL,
  `pass_max` smallint(6) default NULL,
  `backboard_max` smallint(6) default NULL,
  `steal_max` smallint(6) default NULL,
  `blocked_max` smallint(6) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for match_detail
-- ----------------------------
DROP TABLE IF EXISTS `match_detail`;
CREATE TABLE `match_detail` (
  `id` bigint(20) NOT NULL auto_increment,
  `match_Id` bigint(20) default NULL,
  `seq` bigint(20) default NULL,
  `description` varchar(1024) default NULL,
  `time_Msg` varchar(40) default NULL,
  `point_Msg` varchar(40) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3967 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for match_main
-- ----------------------------
DROP TABLE IF EXISTS `match_main`;
CREATE TABLE `match_main` (
  `id` bigint(20) NOT NULL auto_increment,
  `home_Team_Id` bigint(20) default NULL,
  `visiting_Team_Id` bigint(20) default NULL,
  `home_Team_Name` varchar(100) character set latin1 default NULL,
  `visiting_Team_Name` varchar(100) character set latin1 default NULL,
  `start_Time` datetime default NULL,
  `point` varchar(20) character set latin1 default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for match_stat
-- ----------------------------
DROP TABLE IF EXISTS `match_stat`;
CREATE TABLE `match_stat` (
  `id` bigint(20) NOT NULL auto_increment,
  `team_Id` bigint(20) default NULL,
  `player_Id` bigint(20) default NULL,
  `match_Id` bigint(20) default NULL,
  `point2_Shoot_Times` smallint(6) default NULL,
  `point2_Doom_Times` smallint(6) default NULL,
  `point3_Shoot_Times` smallint(6) default NULL,
  `point3_Doom_Times` smallint(6) default NULL,
  `point1_Shoot_Times` smallint(6) default NULL,
  `point1_Doom_Times` smallint(6) default NULL,
  `offensive_Rebound` smallint(6) default NULL,
  `defensive_Rebound` smallint(6) default NULL,
  `foul` smallint(6) default NULL,
  `lapsus` smallint(6) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=299 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for player
-- ----------------------------
DROP TABLE IF EXISTS `player`;
CREATE TABLE `player` (
  `id` bigint(20) NOT NULL auto_increment,
  `name` varchar(255) default NULL,
  `age` int(11) default NULL,
  `position` varchar(2) default NULL,
  `stature` int(11) default NULL,
  `avoirdupois` int(11) default NULL,
  `teamId` bigint(20) default NULL,
  `ability` double default NULL,
  `shooting` smallint(6) default NULL,
  `speed` smallint(6) default NULL,
  `strength` smallint(6) default NULL,
  `bounce` smallint(6) default NULL,
  `stamina` smallint(6) default NULL,
  `trisection` smallint(6) default NULL,
  `dribble` smallint(6) default NULL,
  `pass` smallint(6) default NULL,
  `backboard` smallint(6) default NULL,
  `steal` smallint(6) default NULL,
  `blocked` smallint(6) default NULL,
  `shooting_max` smallint(6) default NULL,
  `speed_max` smallint(6) default NULL,
  `strength_max` smallint(6) default NULL,
  `bounce_max` smallint(6) default NULL,
  `stamina_max` smallint(6) default NULL,
  `trisection_max` smallint(6) default NULL,
  `dribble_max` smallint(6) default NULL,
  `pass_max` smallint(6) default NULL,
  `backboard_max` smallint(6) default NULL,
  `steal_max` smallint(6) default NULL,
  `blocked_max` smallint(6) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for team
-- ----------------------------
DROP TABLE IF EXISTS `team`;
CREATE TABLE `team` (
  `id` bigint(20) NOT NULL auto_increment,
  `name` varchar(30) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for team_tactics
-- ----------------------------
DROP TABLE IF EXISTS `team_tactics`;
CREATE TABLE `team_tactics` (
  `id` bigint(20) NOT NULL auto_increment,
  `team_id` bigint(20) default NULL,
  `team_name` varchar(50) default NULL,
  `flg` varchar(5) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for team_tactics_detail
-- ----------------------------
DROP TABLE IF EXISTS `team_tactics_detail`;
CREATE TABLE `team_tactics_detail` (
  `id` bigint(20) NOT NULL auto_increment,
  `team_tactics_id` bigint(20) default NULL,
  `tactics_id` smallint(6) default NULL,
  `seq` smallint(6) default NULL,
  `pgid` bigint(20) default NULL,
  `sgid` bigint(20) default NULL,
  `sfid` bigint(20) default NULL,
  `pfid` bigint(20) default NULL,
  `cid` bigint(20) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for test
-- ----------------------------
DROP TABLE IF EXISTS `test`;
CREATE TABLE `test` (
  `id` bigint(20) NOT NULL auto_increment,
  `value` varchar(100) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=278058 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `action_desc` VALUES ('29', 'success', '~1~??~3~???,??~2~??????', 'CatchSlamDunk', 'not_foul', '7');
INSERT INTO `action_desc` VALUES ('30', 'success', '~1~??~3~???,???????,?????????,????????????', 'CatchSlamDunk', 'not_foul', '4');
INSERT INTO `action_desc` VALUES ('31', 'success', '~3~????,?????????,~2~????,~1~????,~3~??????????,~1~??????????', 'CatchSlamDunk', 'not_foul', '5');
INSERT INTO `player` VALUES ('1', '姚明    ', '0', 'C', '0', '212', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('2', '期科拉  ', '0', 'PF', '0', '210', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('3', '阿泰斯特', '0', 'SF', '0', '200', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('4', '麦迪    ', '0', 'SG', '0', '190', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('5', '阿尔斯通', '0', 'PG', '0', '180', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('6', '布泽尔  ', '0', 'C', '0', '210', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('7', '小奥尼尔', '0', 'PF', '0', '200', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('8', '安东尼  ', '0', 'SF', '0', '190', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('9', '科比    ', '0', 'SG', '0', '180', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('10', '保罗    ', '0', 'PG', '0', '180', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `team` VALUES ('1', 'ME');
INSERT INTO `team` VALUES ('2', 'NOT ME');
