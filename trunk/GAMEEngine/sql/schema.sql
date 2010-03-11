# MySQL-Front 5.1  (Build 4.2)

/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE */;
/*!40101 SET SQL_MODE='' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES */;
/*!40103 SET SQL_NOTES='ON' */;


# Host: 192.168.1.158    Database: gba
# ------------------------------------------------------
# Server version 5.1.36-log

#
# Source for table action_desc
#

DROP TABLE IF EXISTS `action_desc`;
CREATE TABLE `action_desc` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `result` varchar(255) DEFAULT NULL,
  `ACTION_DESC` varchar(255) DEFAULT NULL,
  `ACTION_NAME` varchar(255) DEFAULT NULL,
  `flg` varchar(255) DEFAULT NULL,
  `percent` int(11) NOT NULL,
  `created_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8;

#
# Source for table free_player
#

DROP TABLE IF EXISTS `free_player`;
CREATE TABLE `free_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `team_Id` bigint(20) DEFAULT NULL,
  `ability` double DEFAULT NULL,
  `shooting` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  `strength` float DEFAULT NULL,
  `bounce` float DEFAULT NULL,
  `stamina` float DEFAULT NULL,
  `trisection` float DEFAULT NULL,
  `dribble` float DEFAULT NULL,
  `pass` float DEFAULT NULL,
  `backboard` float DEFAULT NULL,
  `steal` float DEFAULT NULL,
  `blocked` float DEFAULT NULL,
  `shooting_max` float DEFAULT NULL,
  `speed_max` float DEFAULT NULL,
  `strength_max` float DEFAULT NULL,
  `bounce_max` float DEFAULT NULL,
  `stamina_max` float DEFAULT NULL,
  `trisection_max` float DEFAULT NULL,
  `dribble_max` float DEFAULT NULL,
  `pass_max` float DEFAULT NULL,
  `backboard_max` float DEFAULT NULL,
  `steal_max` float DEFAULT NULL,
  `blocked_max` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`)
) ENGINE=InnoDB AUTO_INCREMENT=597 DEFAULT CHARSET=utf8;

#
# Source for table match
#

DROP TABLE IF EXISTS `match`;
CREATE TABLE `match` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `home_team_id` int(11) DEFAULT NULL,
  `guest_team_id` int(11) DEFAULT NULL,
  `type` smallint(6) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `point` varchar(20) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `send_time` datetime DEFAULT NULL,
  `start_time` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `home_team_id` (`home_team_id`),
  KEY `guest_team_id` (`guest_team_id`),
  KEY `type` (`type`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

#
# Source for table match_detail
#

DROP TABLE IF EXISTS `match_detail`;
CREATE TABLE `match_detail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `match_Id` bigint(20) DEFAULT NULL,
  `seq` bigint(20) DEFAULT NULL,
  `description` varchar(1024) DEFAULT NULL,
  `time_Msg` varchar(40) DEFAULT NULL,
  `point_Msg` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Source for table match_nodosity_detail
#

DROP TABLE IF EXISTS `match_nodosity_detail`;
CREATE TABLE `match_nodosity_detail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `match_nodosity_main_id` bigint(20) DEFAULT NULL,
  `position` varchar(3) DEFAULT NULL,
  `player_id` bigint(20) DEFAULT NULL,
  `player_name` varchar(255) DEFAULT NULL,
  `colligate` float DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Source for table match_nodosity_main
#

DROP TABLE IF EXISTS `match_nodosity_main`;
CREATE TABLE `match_nodosity_main` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `seq` smallint(6) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `home_tactic_id` bigint(20) DEFAULT NULL,
  `visiting_tactic_id` bigint(20) DEFAULT NULL,
  `point` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Source for table match_req
#

DROP TABLE IF EXISTS `match_req`;
CREATE TABLE `match_req` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `home_team_id` bigint(20) DEFAULT NULL,
  `visiting_team_id` bigint(20) DEFAULT NULL,
  `flag` char(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

#
# Source for table match_stat
#

DROP TABLE IF EXISTS `match_stat`;
CREATE TABLE `match_stat` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `team_Id` bigint(20) DEFAULT NULL,
  `player_Id` bigint(20) DEFAULT NULL,
  `match_Id` bigint(20) DEFAULT NULL,
  `point2_Shoot_Times` smallint(6) DEFAULT NULL,
  `point2_Doom_Times` smallint(6) DEFAULT NULL,
  `point3_Shoot_Times` smallint(6) DEFAULT NULL,
  `point3_Doom_Times` smallint(6) DEFAULT NULL,
  `point1_Shoot_Times` smallint(6) DEFAULT NULL,
  `point1_Doom_Times` smallint(6) DEFAULT NULL,
  `offensive_Rebound` smallint(6) DEFAULT NULL,
  `defensive_Rebound` smallint(6) DEFAULT NULL,
  `foul` smallint(6) DEFAULT NULL,
  `lapsus` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Source for table player_betch_log
#

DROP TABLE IF EXISTS `player_betch_log`;
CREATE TABLE `player_betch_log` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `betch_no` varchar(100) DEFAULT NULL,
  `is_success` tinyint(1) DEFAULT NULL,
  `info` text,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `betch_no` (`betch_no`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

#
# Source for table profession_player
#

DROP TABLE IF EXISTS `profession_player`;
CREATE TABLE `profession_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `team_Id` bigint(20) DEFAULT NULL,
  `ability` double DEFAULT NULL,
  `shooting` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  `strength` float DEFAULT NULL,
  `bounce` float DEFAULT NULL,
  `stamina` float DEFAULT NULL,
  `trisection` float DEFAULT NULL,
  `dribble` float DEFAULT NULL,
  `pass` float DEFAULT NULL,
  `backboard` float DEFAULT NULL,
  `steal` float DEFAULT NULL,
  `blocked` float DEFAULT NULL,
  `shooting_max` float DEFAULT NULL,
  `speed_max` float DEFAULT NULL,
  `strength_max` float DEFAULT NULL,
  `bounce_max` float DEFAULT NULL,
  `stamina_max` float DEFAULT NULL,
  `trisection_max` float DEFAULT NULL,
  `dribble_max` float DEFAULT NULL,
  `pass_max` float DEFAULT NULL,
  `backboard_max` float DEFAULT NULL,
  `steal_max` float DEFAULT NULL,
  `blocked_max` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

#
# Source for table team
#

DROP TABLE IF EXISTS `team`;
CREATE TABLE `team` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

