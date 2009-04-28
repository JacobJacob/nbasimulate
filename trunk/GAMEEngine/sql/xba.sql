/*
MySQL Data Transfer
Source Host: localhost
Source Database: xba
Target Host: localhost
Target Database: xba
Date: 2009/4/27 18:10:23
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
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=1899 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for match_main
-- ----------------------------
DROP TABLE IF EXISTS `match_main`;
CREATE TABLE `match_main` (
  `id` bigint(20) NOT NULL auto_increment,
  `home_Team_Id` bigint(20) default NULL,
  `visiting_Team_Id` bigint(20) default NULL,
  `home_Team_Name` varchar(100) default NULL,
  `visiting_Team_Name` varchar(100) default NULL,
  `start_Time` datetime default NULL,
  `point` varchar(20) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

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
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=21083 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for team
-- ----------------------------
DROP TABLE IF EXISTS `team`;
CREATE TABLE `team` (
  `id` bigint(20) NOT NULL auto_increment,
  `name` varchar(30) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `action_desc` VALUES ('1', null, null, 'action_name1', null, '0');
INSERT INTO `action_desc` VALUES ('2', null, 'action_desc', 'action_name2', null, '0');
INSERT INTO `action_desc` VALUES ('3', 'result', 'action_desc', 'action_name3', null, '0');
INSERT INTO `action_desc` VALUES ('4', 'result', 'action_desc', 'action_name4', 'block', '0');
INSERT INTO `action_desc` VALUES ('5', 'result', 'action_desc', 'action_name5', 'block', '0');
INSERT INTO `action_desc` VALUES ('6', 'result', 'action_desc', 'action_name6', 'block', '0');
INSERT INTO `action_desc` VALUES ('7', 'result', 'action_desc', 'action_name7', 'block', '0');
INSERT INTO `action_desc` VALUES ('8', 'result', 'action_desc', 'action_name8', 'block', '0');
INSERT INTO `action_desc` VALUES ('9', 'result', 'action_desc', 'action_name9', 'block', '0');
INSERT INTO `action_desc` VALUES ('10', 'result', 'action_desc', 'action_name10', 'block', '0');
INSERT INTO `action_desc` VALUES ('11', 'result', 'action_desc', 'action_name11', 'block', '0');
INSERT INTO `action_desc` VALUES ('12', 'result', 'action_desc', 'action_name12', 'block', '0');
INSERT INTO `action_desc` VALUES ('13', 'result', 'action_desc', 'action_name13', 'block', '0');
INSERT INTO `action_desc` VALUES ('14', 'result', 'action_desc', 'action_name14', 'block', '0');
INSERT INTO `action_desc` VALUES ('15', 'result', 'action_desc', 'action_name15', 'block', '0');
INSERT INTO `action_desc` VALUES ('16', 'result', 'action_desc', 'action_name16', 'block', '0');
INSERT INTO `action_desc` VALUES ('17', 'result', 'action_desc', 'action_name17', 'block', '0');
INSERT INTO `action_desc` VALUES ('18', 'result', 'action_desc', 'action_name18', 'block', '0');
INSERT INTO `action_desc` VALUES ('19', 'result', 'action_desc', 'action_name19', 'block', '0');
INSERT INTO `action_desc` VALUES ('20', 'result', 'action_desc', 'action_name20', 'block', '0');
INSERT INTO `action_desc` VALUES ('21', 'result', 'action_desc', 'action_name21', 'block', '0');
INSERT INTO `action_desc` VALUES ('22', 'result', 'action_desc', 'action_name22', 'block', '0');
INSERT INTO `action_desc` VALUES ('23', 'result', 'action_desc', 'action_name23', 'block', '0');
INSERT INTO `action_desc` VALUES ('24', 'result', 'action_desc', 'action_name24', 'block', '0');
INSERT INTO `action_desc` VALUES ('25', 'result', 'action_desc', 'action_name25', 'block', '0');
INSERT INTO `action_desc` VALUES ('26', 'result', 'action_desc', 'action_name26', 'block', '0');
INSERT INTO `action_desc` VALUES ('27', 'result', 'action_desc', 'action_name27', 'block', '0');
INSERT INTO `action_desc` VALUES ('28', 'result', 'action_desc', 'action_name28', 'block', '0');
INSERT INTO `action_desc` VALUES ('29', 'success', 'action_desc', 'CatchSlamDunk', 'not_foul', '7');
INSERT INTO `action_desc` VALUES ('30', 'success', 'action_desc', 'CatchSlamDunk', 'not_foul', '4');
INSERT INTO `player` VALUES ('21056', 'Jacky', '9', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('21058', 'Jacky', '9', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('21059', 'Jacky', '9', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('21060', 'Jacky', '9', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('21061', 'Jacky', '9', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('21062', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
INSERT INTO `player` VALUES ('21063', '??    ', '0', 'C', '0', '212', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21064', '???  ', '0', 'PF', '0', '210', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21065', '????', '0', 'SF', '0', '200', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21066', '??    ', '0', 'SG', '0', '190', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21067', '????', '0', 'PG', '0', '180', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21068', '???  ', '0', 'C', '0', '210', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21069', '????', '0', 'PF', '0', '200', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21070', '???  ', '0', 'SF', '0', '190', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21071', '??    ', '0', 'SG', '0', '180', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21072', '??    ', '0', 'PG', '0', '180', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21073', '??    ', '0', 'C', '0', '212', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21074', '???  ', '0', 'PF', '0', '210', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21075', '????', '0', 'SF', '0', '200', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21076', '??    ', '0', 'SG', '0', '190', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21077', '????', '0', 'PG', '0', '180', '2', '0', '80', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21078', '???  ', '0', 'C', '0', '210', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21079', '????', '0', 'PF', '0', '200', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21080', '???  ', '0', 'SF', '0', '190', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21081', '??    ', '0', 'SG', '0', '180', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `player` VALUES ('21082', '??    ', '0', 'PG', '0', '180', '1', '0', '70', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0');
INSERT INTO `team` VALUES ('1', 'ME');
INSERT INTO `team` VALUES ('2', 'NOT ME');
