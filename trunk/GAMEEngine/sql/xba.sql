/*
MySQL Data Transfer
Source Host: localhost
Source Database: xba
Target Host: localhost
Target Database: xba
Date: 2009/4/30 18:04:08
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for action_desc
-- ----------------------------
DROP TABLE IF EXISTS `action_desc`;
CREATE TABLE `action_desc` (
  `id` bigint(20) NOT NULL auto_increment,
  `result` varchar(255) default NULL,
  `ACTION_DESC` varchar(255) character set utf8 default NULL,
  `ACTION_NAME` varchar(255) default NULL,
  `flg` varchar(255) default NULL,
  `percent` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=130498 DEFAULT CHARSET=utf8;

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
) ENGINE=InnoDB AUTO_INCREMENT=4731 DEFAULT CHARSET=utf8;

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
) ENGINE=InnoDB AUTO_INCREMENT=8985 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for player
-- ----------------------------
DROP TABLE IF EXISTS `player`;
CREATE TABLE `player` (
  `id` bigint(20) NOT NULL auto_increment,
  `name` varchar(255) default NULL,
  `age` int(11) default NULL,
  `position` varchar(2) character set latin1 default NULL,
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
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21923 DEFAULT CHARSET=utf8;

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
-- Table structure for test
-- ----------------------------
DROP TABLE IF EXISTS `test`;
CREATE TABLE `test` (
  `id` bigint(20) NOT NULL auto_increment,
  `value` varchar(100) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=278058 DEFAULT CHARSET=utf8;
-- ----------------------------
-- Table structure for test
-- ----------------------------
DROP TABLE IF EXISTS `match_req`;
CREATE TABLE match_req ( 
    id              	bigint(20) AUTO_INCREMENT NOT NULL,
    home_team_id    	bigint(20) NULL,
    visiting_team_id	bigint(20) NULL,
    flag            	char(1) NULL,
    PRIMARY KEY(id)
)
-- ----------------------------
-- Table structure for match_nodosity_detail
-- ----------------------------
DROP TABLE IF EXISTS `match_nodosity_detail`;
CREATE TABLE `match_nodosity_detail` (
  `id` bigint(20) NOT NULL auto_increment,
  `match_nodosity_main_id` bigint(20) default NULL,
  `position` varchar(3) default NULL,
  `player_id` bigint(20) default NULL,
  `player_name` varchar(255) default NULL,
  `colligate` float default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=UTF8;

-- ----------------------------
-- Table structure for match_nodosity_main
-- ----------------------------
DROP TABLE IF EXISTS `match_nodosity_main`;
CREATE TABLE `match_nodosity_main` (
  `id` bigint(20) NOT NULL auto_increment,
  `seq` smallint(6) default NULL,
  `match_id` bigint(20) default NULL,
  `home_tactic_id` bigint(20) default NULL,
  `visiting_tactic_id` bigint(20) default NULL,
  `point` varchar(255) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=UTF8;