-- MySQL dump 10.13  Distrib 5.1.36, for unknown-linux-gnu (x86_64)
--
-- Host: 192.168.1.152    Database: gba
-- ------------------------------------------------------
-- Server version	5.1.45-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `新表`
--

DROP TABLE IF EXISTS `新表`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `新表` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `success_union_id` int(11) DEFAULT NULL,
  `failure_union_id` int(11) DEFAULT NULL,
  `log` text,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `action_desc`
--

DROP TABLE IF EXISTS `action_desc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `action_desc` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `result` varchar(255) DEFAULT NULL,
  `action_desc` varchar(255) DEFAULT NULL,
  `action_name` varchar(255) DEFAULT NULL,
  `flg` varchar(255) DEFAULT NULL,
  `percent` int(11) NOT NULL,
  `is_assist` tinyint(3) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `not_stick` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `active_code`
--

DROP TABLE IF EXISTS `active_code`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `active_code` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `code` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `all_finance`
--

DROP TABLE IF EXISTS `all_finance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `all_finance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `season` smallint(6) DEFAULT NULL,
  `income` int(11) NOT NULL DEFAULT '0',
  `outlay` int(11) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=510 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `attention_player`
--

DROP TABLE IF EXISTS `attention_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attention_player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `no` char(32) NOT NULL DEFAULT '',
  `type` tinyint(3) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id_no` (`no`,`team_id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `captcha`
--

DROP TABLE IF EXISTS `captcha`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `captcha` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `captcha` varchar(255) DEFAULT NULL,
  `captcha_hash` char(32) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `challenge_all`
--

DROP TABLE IF EXISTS `challenge_all`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `challenge_all` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) DEFAULT NULL,
  `match_count` smallint(6) DEFAULT '0',
  `win_count` smallint(6) DEFAULT '0',
  `point` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `challenge_champion_history`
--

DROP TABLE IF EXISTS `challenge_champion_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `challenge_champion_history` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` int(11) DEFAULT NULL,
  `round` smallint(6) DEFAULT NULL,
  `seq` tinyint(3) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `point` smallint(6) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `challenge_history`
--

DROP TABLE IF EXISTS `challenge_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `challenge_history` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `home_team_id` int(11) DEFAULT NULL,
  `guest_team_id` int(11) DEFAULT NULL,
  `match_id` int(11) DEFAULT NULL,
  `finish` tinyint(1) DEFAULT '0',
  `point` varchar(50) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `match_id` (`match_id`),
  KEY `home_team_id` (`home_team_id`),
  KEY `guest_team_id` (`guest_team_id`),
  KEY `created_time` (`created_time`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `challenge_pool`
--

DROP TABLE IF EXISTS `challenge_pool`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `challenge_pool` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) DEFAULT NULL,
  `win_count` tinyint(3) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `match_id` int(11) DEFAULT NULL,
  `ability` float DEFAULT NULL,
  `start_wait_time` datetime DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `ability` (`ability`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `challenge_team`
--

DROP TABLE IF EXISTS `challenge_team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `challenge_team` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) DEFAULT NULL,
  `match_count` smallint(6) DEFAULT '0',
  `win_count` smallint(6) DEFAULT '0',
  `point` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `chat_message`
--

DROP TABLE IF EXISTS `chat_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `chat_message` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `content` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `created_time` (`created_time`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `client` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `client_id` varchar(50) DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `status` tinyint(255) DEFAULT NULL,
  `version` varchar(255) DEFAULT NULL,
  `cmd` varchar(255) DEFAULT NULL,
  `description` varchar(10000) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `params` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `client_id` (`client_id`,`ip`,`type`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `client_running_log`
--

DROP TABLE IF EXISTS `client_running_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `client_running_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `client_name` varchar(255) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `log_time` datetime DEFAULT NULL,
  `status` varchar(15) DEFAULT NULL,
  `log` varchar(2000) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `client_ip_time` (`client_name`,`ip`,`log_time`)
) ENGINE=InnoDB AUTO_INCREMENT=620 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cup`
--

DROP TABLE IF EXISTS `cup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cup` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cup_name` varchar(255) DEFAULT NULL,
  `cup_key` varchar(50) NOT NULL DEFAULT '',
  `cup_icon` varchar(255) DEFAULT NULL,
  `is_youth` tinyint(1) DEFAULT '0',
  `is_del` tinyint(1) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `cup_key` (`cup_key`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `draft_player`
--

DROP TABLE IF EXISTS `draft_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `draft_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `contract` smallint(6) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `current_team_id` int(11) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `match_power` tinyint(3) DEFAULT NULL,
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
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `training` varchar(30) DEFAULT 'speed',
  `last_inc` float DEFAULT '0',
  `training_locations` int(11) NOT NULL DEFAULT '0',
  `expired_time` datetime DEFAULT NULL,
  `bid_count` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`,`current_team_id`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `ability` (`ability`)
) ENGINE=InnoDB AUTO_INCREMENT=597 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `email_send_log`
--

DROP TABLE IF EXISTS `email_send_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `email_send_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `active_code` varchar(255) DEFAULT NULL,
  `success` tinyint(1) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `engine_status`
--

DROP TABLE IF EXISTS `engine_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `engine_status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `status` tinyint(3) DEFAULT '0',
  `cmd` varchar(255) DEFAULT NULL,
  `info` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `error_log`
--

DROP TABLE IF EXISTS `error_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `error_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(20) DEFAULT NULL,
  `log` text,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `error_match`
--

DROP TABLE IF EXISTS `error_match`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `error_match` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `match_id` int(11) DEFAULT NULL,
  `type` tinyint(3) DEFAULT '0',
  `remark` text,
  `client` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `match_id` (`match_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='异常比赛记录';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `free_player`
--

DROP TABLE IF EXISTS `free_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `free_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` tinyint(3) DEFAULT NULL,
  `picture` varchar(255) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `current_team_id` bigint(20) DEFAULT NULL,
  `current_price` int(11) DEFAULT '0',
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
  `expired_time` datetime DEFAULT NULL,
  `betch_no` varchar(100) DEFAULT NULL,
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `bid_count` smallint(6) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `worth` int(11) DEFAULT '0',
  `wage` int(11) DEFAULT '0',
  `power` smallint(6) DEFAULT '100',
  `auction_status` tinyint(1) DEFAULT '0',
  `delete_time` datetime DEFAULT NULL,
  `status` tinyint(1) DEFAULT '0',
  `team_id` int(11) DEFAULT NULL,
  `social_status` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `delete_time` (`delete_time`),
  KEY `expired_time` (`expired_time`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `friends`
--

DROP TABLE IF EXISTS `friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `friends` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) DEFAULT NULL,
  `friend_team_id` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `update_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id_friend_team_id` (`team_id`,`friend_team_id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `init_profession_player`
--

DROP TABLE IF EXISTS `init_profession_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `init_profession_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `contract` smallint(6) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `team_id` bigint(20) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `match_power` tinyint(3) DEFAULT NULL,
  `league_power` int(11) DEFAULT '0' COMMENT '联赛体力的一个中间值',
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
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `training` varchar(30) DEFAULT 'speed',
  `last_inc` float DEFAULT '0',
  `training_locations` int(11) NOT NULL DEFAULT '0',
  `is_draft` tinyint(1) DEFAULT '0',
  `in_tactical` tinyint(1) DEFAULT '0',
  `in_team_round` int(11) DEFAULT '0' COMMENT '在球队的轮数',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`,`team_id`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `ability` (`ability`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `init_youth_player`
--

DROP TABLE IF EXISTS `init_youth_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `init_youth_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `match_power` tinyint(3) DEFAULT NULL,
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
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `in_tactical` tinyint(1) DEFAULT '0',
  `in_team_round` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`,`team_id`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league`
--

DROP TABLE IF EXISTS `league`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `degree` smallint(6) DEFAULT NULL,
  `no` smallint(6) DEFAULT NULL,
  `team_count` smallint(6) DEFAULT '0',
  `status` tinyint(3) NOT NULL DEFAULT '0',
  `update_finish` tinyint(3) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `degree_no` (`degree`,`no`)
) ENGINE=InnoDB AUTO_INCREMENT=2508 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_config`
--

DROP TABLE IF EXISTS `league_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_config` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` smallint(6) DEFAULT NULL,
  `round` smallint(6) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_info`
--

DROP TABLE IF EXISTS `league_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `新字段` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_matchs`
--

DROP TABLE IF EXISTS `league_matchs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_matchs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `match_team_home_id` int(11) NOT NULL DEFAULT '0',
  `match_team_guest_id` int(11) NOT NULL DEFAULT '0',
  `round` smallint(6) DEFAULT NULL,
  `point` varchar(50) DEFAULT NULL,
  `status` tinyint(1) DEFAULT '0',
  `league_id` int(11) NOT NULL DEFAULT '0',
  `match_id` int(11) DEFAULT NULL,
  `match_data` date DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `match_id` (`match_id`)
) ENGINE=InnoDB AUTO_INCREMENT=456093 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_tasks`
--

DROP TABLE IF EXISTS `league_tasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_tasks` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `league_id` int(11) DEFAULT NULL,
  `status` tinyint(1) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `league_teams`
--

DROP TABLE IF EXISTS `league_teams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `league_teams` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `league_id` int(11) DEFAULT NULL,
  `team_id` int(11) DEFAULT '-1',
  `rank` tinyint(3) DEFAULT NULL,
  `win` smallint(6) DEFAULT '0',
  `lose` smallint(6) DEFAULT '0',
  `net_points` smallint(6) DEFAULT '0',
  `status` tinyint(3) DEFAULT '0',
  `seq` tinyint(3) DEFAULT NULL,
  `degrade` tinyint(3) DEFAULT '0',
  `upgrade` tinyint(3) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `league_id` (`league_id`),
  KEY `status` (`status`),
  KEY `team_id` (`team_id`),
  KEY `rank` (`rank`)
) ENGINE=InnoDB AUTO_INCREMENT=35090 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_nodosity_detail`
--

DROP TABLE IF EXISTS `match_nodosity_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_nodosity_detail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `match_nodosity_main_id` bigint(20) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `seq` smallint(6) DEFAULT NULL,
  `description` varchar(1024) DEFAULT NULL,
  `time_msg` varchar(40) DEFAULT NULL,
  `point_msg` varchar(40) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `is_new_line` tinyint(1) unsigned zerofill NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `main_seq` (`match_nodosity_main_id`,`seq`),
  KEY `match_nodosity_main_id` (`match_nodosity_main_id`),
  KEY `match_id` (`match_id`)
) ENGINE=InnoDB AUTO_INCREMENT=29886800 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_nodosity_main`
--

DROP TABLE IF EXISTS `match_nodosity_main`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_nodosity_main` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `seq` smallint(6) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `home_offensive_tactic` tinyint(3) DEFAULT NULL,
  `home_defend_tactic` tinyint(3) DEFAULT NULL,
  `guest_offensive_tactic` tinyint(3) DEFAULT NULL,
  `guest_defend_tactic` tinyint(3) DEFAULT NULL,
  `point` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `match_seq` (`seq`,`match_id`),
  KEY `match_id` (`match_id`)
) ENGINE=InnoDB AUTO_INCREMENT=144512 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_nodosity_tactical_detail`
--

DROP TABLE IF EXISTS `match_nodosity_tactical_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_nodosity_tactical_detail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `match_nodosity_main_id` bigint(20) DEFAULT NULL,
  `position` varchar(3) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `player_name` varchar(255) DEFAULT NULL,
  `power` smallint(6) DEFAULT NULL,
  `ability` float DEFAULT NULL,
  `age` smallint(6) DEFAULT '0',
  `stature` smallint(6) DEFAULT NULL COMMENT '身高',
  `avoirdupois` smallint(6) DEFAULT '0' COMMENT '体重',
  `no` smallint(6) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `match_nodosity_main_id` (`match_nodosity_main_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1445111 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_not_in_player`
--

DROP TABLE IF EXISTS `match_not_in_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_not_in_player` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `match_id` int(11) NOT NULL,
  `no` smallint(6) DEFAULT '0',
  `player_no` char(32) NOT NULL,
  `position` varchar(2) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `ability` float NOT NULL DEFAULT '0',
  `created_time` datetime NOT NULL,
  `updated_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_match_player` (`team_id`,`match_id`,`player_no`),
  KEY `team_match` (`match_id`,`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=223075 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `match_stat`
--

DROP TABLE IF EXISTS `match_stat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `match_stat` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `team_id` bigint(20) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `no` smallint(6) DEFAULT '0',
  `position` varchar(2) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `match_id` bigint(20) DEFAULT NULL,
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `is_main` tinyint(3) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_match` (`player_no`,`match_id`),
  KEY `match_id` (`match_id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=365796 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `matchs`
--

DROP TABLE IF EXISTS `matchs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `matchs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `home_team_id` int(11) DEFAULT NULL,
  `guest_team_id` int(11) DEFAULT NULL,
  `type` smallint(6) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `sub_status` tinyint(3) DEFAULT '0',
  `point` varchar(20) DEFAULT NULL,
  `send_time` datetime DEFAULT NULL,
  `start_time` datetime DEFAULT NULL,
  `is_youth` tinyint(1) DEFAULT '0',
  `expired_time` datetime DEFAULT NULL,
  `overtime` tinyint(3) NOT NULL DEFAULT '0',
  `show_status` tinyint(3) DEFAULT '15',
  `next_show_status` tinyint(3) DEFAULT NULL,
  `next_status_time` datetime DEFAULT NULL,
  `client` varchar(255) DEFAULT NULL,
  `created_time` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `home_team_id` (`home_team_id`),
  KEY `guest_team_id` (`guest_team_id`),
  KEY `type` (`type`),
  KEY `expired_time` (`expired_time`),
  KEY `status` (`status`),
  KEY `show_status` (`show_status`)
) ENGINE=InnoDB AUTO_INCREMENT=36000 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `message`
--

DROP TABLE IF EXISTS `message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `message` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `to_team_id` int(11) DEFAULT NULL,
  `from_team_id` int(11) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `content` text,
  `is_new` tinyint(1) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=60 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `names`
--

DROP TABLE IF EXISTS `names`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `names` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_type` (`name`,`type`)
) ENGINE=InnoDB AUTO_INCREMENT=844 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `out_message`
--

DROP TABLE IF EXISTS `out_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `out_message` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `to_team_id` int(11) DEFAULT NULL,
  `from_team_id` int(11) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  `content` text,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `player_auction_log`
--

DROP TABLE IF EXISTS `player_auction_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `player_auction_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` tinyint(1) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `content` varchar(2000) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `player_betch_log`
--

DROP TABLE IF EXISTS `player_betch_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `player_betch_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `betch_no` varchar(100) DEFAULT NULL,
  `is_success` tinyint(1) DEFAULT NULL,
  `info` text,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `betch_no` (`betch_no`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pro_player_career_stat_total`
--

DROP TABLE IF EXISTS `pro_player_career_stat_total`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pro_player_career_stat_total` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `player_no` char(32) DEFAULT NULL,
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `point_total` int(11) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `rebound_total` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `match_total` int(11) DEFAULT '0',
  `main_total` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_no` (`player_no`)
) ENGINE=InnoDB AUTO_INCREMENT=3120 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pro_player_season_stat_total`
--

DROP TABLE IF EXISTS `pro_player_season_stat_total`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pro_player_season_stat_total` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `player_no` char(32) NOT NULL DEFAULT '',
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `point_total` int(11) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `rebound_total` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `match_total` int(11) DEFAULT '0',
  `main_total` int(11) DEFAULT '0',
  `point_agv` float NOT NULL DEFAULT '0',
  `rebound_agv` float NOT NULL DEFAULT '0',
  `steals_agv` float NOT NULL DEFAULT '0',
  `assist_agv` float NOT NULL DEFAULT '0',
  `block_agv` float NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_no` (`player_no`),
  KEY `point_agv` (`point_agv`),
  KEY `rebound_agv` (`rebound_agv`),
  KEY `steals_agv` (`steals_agv`),
  KEY `assist_agv` (`assist_agv`),
  KEY `block_agv` (`block_agv`)
) ENGINE=InnoDB AUTO_INCREMENT=1362 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `profession_player`
--

DROP TABLE IF EXISTS `profession_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `profession_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `contract` smallint(6) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `team_id` bigint(20) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `match_power` tinyint(3) DEFAULT NULL,
  `league_power` int(11) DEFAULT '0' COMMENT '联赛体力的一个中间值',
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
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `training` varchar(30) DEFAULT 'speed',
  `last_inc` float DEFAULT '0',
  `training_locations` int(11) NOT NULL DEFAULT '0',
  `is_draft` tinyint(1) DEFAULT '0',
  `in_tactical` tinyint(1) DEFAULT '0',
  `in_team_round` int(11) DEFAULT '0' COMMENT '在球队的轮数',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`,`team_id`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `ability` (`ability`)
) ENGINE=InnoDB AUTO_INCREMENT=4097 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `round_update_log`
--

DROP TABLE IF EXISTS `round_update_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `round_update_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` tinyint(3) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `start_time` datetime DEFAULT NULL,
  `end_time` datetime DEFAULT NULL,
  `match_count` int(11) DEFAULT '0',
  `log` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `youth_player_count` int(11) DEFAULT NULL,
  `pro_player_count` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `season_round` (`season`,`round`)
) ENGINE=InnoDB AUTO_INCREMENT=134 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `runtime_data`
--

DROP TABLE IF EXISTS `runtime_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `runtime_data` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `programe` varchar(255) DEFAULT NULL,
  `value_key` varchar(50) DEFAULT NULL,
  `value` text,
  `created_at` datetime DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NewIndex` (`programe`,`value_key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `season_finance`
--

DROP TABLE IF EXISTS `season_finance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `season_finance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` varchar(255) DEFAULT NULL,
  `season` smallint(6) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `type` tinyint(1) DEFAULT NULL,
  `sub_type` tinyint(1) DEFAULT NULL,
  `info` varchar(500) DEFAULT NULL,
  `income` int(11) DEFAULT '0',
  `outlay` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3081 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `session`
--

DROP TABLE IF EXISTS `session`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `session` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `session_id` char(32) DEFAULT NULL,
  `active_time` datetime DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `table`
--

DROP TABLE IF EXISTS `table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `table` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `value` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tactical_grade`
--

DROP TABLE IF EXISTS `tactical_grade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tactical_grade` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tactical` tinyint(3) NOT NULL DEFAULT '0',
  `team_id` int(11) DEFAULT NULL,
  `point` int(11) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=145 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team`
--

DROP TABLE IF EXISTS `team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(30) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `micro` varchar(255) DEFAULT NULL,
  `youth_league` int(11) DEFAULT NULL,
  `profession_league_evel` smallint(6) DEFAULT NULL,
  `profession_league_class` smallint(6) DEFAULT NULL,
  `funds` int(11) DEFAULT '0',
  `hold_funds` int(11) DEFAULT '0',
  `agv_ability` float DEFAULT NULL,
  `agv_ability_rank` int(11) DEFAULT NULL,
  `last_active_time` datetime DEFAULT NULL,
  `union_id` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`name`),
  UNIQUE KEY `name` (`name`),
  KEY `last_active_time` (`last_active_time`),
  KEY `agv_ability` (`agv_ability`)
) ENGINE=InnoDB AUTO_INCREMENT=510 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_ad`
--

DROP TABLE IF EXISTS `team_ad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_ad` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `remain_round` tinyint(3) DEFAULT NULL,
  `amount` int(6) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_arena`
--

DROP TABLE IF EXISTS `team_arena`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_arena` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `level` tinyint(3) DEFAULT '1',
  `fare` tinyint(3) NOT NULL DEFAULT '0',
  `fan_count` smallint(6) NOT NULL DEFAULT '0',
  `status` tinyint(3) DEFAULT '0',
  `next_level_time` datetime DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=509 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_honor`
--

DROP TABLE IF EXISTS `team_honor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_honor` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` varchar(255) DEFAULT NULL,
  `honor_name` varchar(255) DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL,
  `ext_info` time DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `created_time` (`created_time`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_picks`
--

DROP TABLE IF EXISTS `team_picks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_picks` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `season` int(10) unsigned DEFAULT '0' COMMENT '赛季',
  `round` smallint(10) unsigned DEFAULT '0' COMMENT '轮次',
  `team_id` int(10) unsigned DEFAULT '0' COMMENT '球队ID',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='球队选秀权';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_staff`
--

DROP TABLE IF EXISTS `team_staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_staff` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `age` tinyint(3) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `is_youth` tinyint(3) DEFAULT '0',
  `team_id` int(11) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `wave` smallint(6) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `remain_round` tinyint(3) DEFAULT NULL,
  `level` tinyint(3) DEFAULT NULL,
  `hide_level` tinyint(3) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_youth_type` (`is_youth`,`type`,`team_id`),
  KEY `team_id` (`team_id`),
  KEY `status` (`status`)
) ENGINE=InnoDB AUTO_INCREMENT=8161 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_tactical`
--

DROP TABLE IF EXISTS `team_tactical`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_tactical` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `team_id` int(11) NOT NULL,
  `tactical_detail_1_id` int(11) DEFAULT NULL,
  `tactical_detail_2_id` int(11) DEFAULT NULL,
  `tactical_detail_3_id` int(11) DEFAULT NULL,
  `tactical_detail_4_id` int(11) DEFAULT NULL,
  `tactical_detail_5_id` int(11) DEFAULT NULL,
  `tactical_detail_6_id` int(11) DEFAULT NULL,
  `tactical_detail_7_id` int(11) DEFAULT NULL,
  `tactical_detail_8_id` int(11) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `is_youth` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id_type` (`team_id`,`type`,`is_youth`)
) ENGINE=InnoDB AUTO_INCREMENT=2037 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_tactical_detail`
--

DROP TABLE IF EXISTS `team_tactical_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_tactical_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `is_youth` tinyint(1) DEFAULT '0',
  `offensive_tactical_type` tinyint(3) DEFAULT '1',
  `defend_tactical_type` tinyint(3) DEFAULT '7',
  `seq` char(1) DEFAULT NULL,
  `cid` char(32) DEFAULT NULL,
  `pfid` char(32) DEFAULT NULL,
  `sfid` char(32) DEFAULT NULL,
  `sgid` char(32) DEFAULT NULL,
  `pgid` char(32) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`,`seq`,`is_youth`)
) ENGINE=InnoDB AUTO_INCREMENT=4069 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `team_ticket_history`
--

DROP TABLE IF EXISTS `team_ticket_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team_ticket_history` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(3) DEFAULT NULL,
  `price` smallint(3) DEFAULT NULL,
  `ticket_count` int(11) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `round` smallint(6) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=537 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `test`
--

DROP TABLE IF EXISTS `test`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `test` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `value` varchar(255) DEFAULT NULL,
  `b` tinyint(3) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `value` (`value`,`b`)
) ENGINE=InnoDB AUTO_INCREMENT=2102 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `training_center`
--

DROP TABLE IF EXISTS `training_center`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `training_center` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `finish_time` datetime DEFAULT NULL,
  `status` tinyint(3) NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `finish_time` (`finish_time`),
  KEY `team_id` (`team_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `training_remain`
--

DROP TABLE IF EXISTS `training_remain`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `training_remain` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `remain_times` tinyint(3) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `union_apply`
--

DROP TABLE IF EXISTS `union_apply`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `union_apply` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `union_id` int(11) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `remark` varchar(255) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `union_member`
--

DROP TABLE IF EXISTS `union_member`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `union_member` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `union_id` int(11) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `title` varchar(20) DEFAULT NULL,
  `is_manager` tinyint(3) DEFAULT '0',
  `is_leader` tinyint(1) DEFAULT '0',
  `prestige` int(11) DEFAULT '0',
  `contribution` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_id` (`team_id`),
  KEY `unino_id` (`union_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `union_war`
--

DROP TABLE IF EXISTS `union_war`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `union_war` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `home_union_id` int(11) NOT NULL DEFAULT '0',
  `home_team_id` int(11) NOT NULL DEFAULT '0' COMMENT '主队ID',
  `guest_union_id` int(11) NOT NULL DEFAULT '0',
  `guest_team_id` int(11) NOT NULL DEFAULT '0' COMMENT '客队ID',
  `prestige` int(11) NOT NULL DEFAULT '0' COMMENT '压威望',
  `start_time` date DEFAULT NULL COMMENT '开始时间',
  `status` tinyint(1) DEFAULT NULL COMMENT '状态',
  `match_id` int(11) DEFAULT NULL COMMENT '比赛ID',
  `created_time` date DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='盟战表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `unions`
--

DROP TABLE IF EXISTS `unions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `unions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `short_name` varchar(10) DEFAULT NULL,
  `leader` int(11) DEFAULT NULL,
  `prestige` int(11) DEFAULT '10',
  `member` int(11) DEFAULT '0',
  `qq_group` varchar(20) DEFAULT NULL,
  `forum` varchar(255) DEFAULT NULL,
  `announce` varchar(500) DEFAULT '',
  `union_desc` varchar(500) DEFAULT '',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user_info`
--

DROP TABLE IF EXISTS `user_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `password` char(32) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `session_id` varchar(255) DEFAULT NULL,
  `last_active_time` datetime DEFAULT NULL,
  `last_login_time` datetime DEFAULT NULL,
  `login_times` int(11) DEFAULT NULL,
  `birthday` date DEFAULT NULL,
  `city` varchar(255) DEFAULT NULL,
  `sex` char(1) DEFAULT NULL,
  `active` tinyint(1) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=521 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_free_player`
--

DROP TABLE IF EXISTS `youth_free_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_free_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` tinyint(3) DEFAULT NULL,
  `picture` varchar(255) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
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
  `betch_no` varchar(100) DEFAULT NULL,
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `bid_count` smallint(6) DEFAULT '0',
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `delete_time` datetime DEFAULT NULL,
  `expired_time` datetime DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `price` int(11) DEFAULT '0',
  `auction_status` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`),
  KEY `delete_time` (`delete_time`),
  KEY `expired_time` (`expired_time`)
) ENGINE=InnoDB AUTO_INCREMENT=629 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_freeplayer_auction_log`
--

DROP TABLE IF EXISTS `youth_freeplayer_auction_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_freeplayer_auction_log` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `player_no` char(32) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_player`
--

DROP TABLE IF EXISTS `youth_player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_player` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `no` varchar(32) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `team_id` int(11) NOT NULL DEFAULT '0',
  `name_base` varchar(255) DEFAULT NULL,
  `player_no` int(5) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` smallint(6) DEFAULT NULL,
  `position` varchar(2) DEFAULT NULL,
  `position_base` varchar(2) DEFAULT NULL,
  `stature` int(11) DEFAULT NULL,
  `wage` int(11) DEFAULT NULL,
  `avoirdupois` int(11) DEFAULT NULL,
  `status` tinyint(3) DEFAULT NULL,
  `power` tinyint(3) DEFAULT NULL,
  `match_power` tinyint(3) DEFAULT NULL,
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
  `defencons` float DEFAULT NULL,
  `offencons` float DEFAULT NULL,
  `buildupcons` float DEFAULT NULL,
  `leadcons` float DEFAULT NULL,
  `backbone` float DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `betch_no` varchar(100) DEFAULT NULL,
  `picture` smallint(6) DEFAULT NULL,
  `defencons_max` float DEFAULT NULL,
  `offencons_max` float DEFAULT NULL,
  `buildupcons_max` float DEFAULT NULL,
  `leadcons_max` float DEFAULT NULL,
  `backbone_max` float DEFAULT NULL,
  `in_tactical` tinyint(1) DEFAULT '0',
  `in_team_round` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `no` (`no`),
  UNIQUE KEY `team_player_no` (`player_no`,`team_id`),
  KEY `betch_no` (`betch_no`),
  KEY `position` (`position`)
) ENGINE=InnoDB AUTO_INCREMENT=4073 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_player_career_stat_total`
--

DROP TABLE IF EXISTS `youth_player_career_stat_total`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_player_career_stat_total` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `player_no` char(32) DEFAULT NULL,
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `point_total` int(11) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `rebound_total` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `match_total` int(11) DEFAULT '0',
  `main_total` int(11) DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_no` (`player_no`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `youth_player_season_stat_total`
--

DROP TABLE IF EXISTS `youth_player_season_stat_total`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `youth_player_season_stat_total` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `player_no` char(32) NOT NULL DEFAULT '',
  `point2_shoot_times` smallint(6) DEFAULT '0',
  `point2_doom_times` smallint(6) DEFAULT '0',
  `point3_shoot_times` smallint(6) DEFAULT '0',
  `point3_doom_times` smallint(6) DEFAULT '0',
  `point1_shoot_times` smallint(6) DEFAULT '0',
  `point1_doom_times` smallint(6) DEFAULT '0',
  `point_total` int(11) DEFAULT '0',
  `offensive_rebound` smallint(6) DEFAULT '0',
  `defensive_rebound` smallint(6) DEFAULT '0',
  `rebound_total` smallint(6) DEFAULT '0',
  `foul` smallint(6) DEFAULT '0',
  `lapsus` smallint(6) DEFAULT '0',
  `assist` smallint(6) DEFAULT '0',
  `block` smallint(6) DEFAULT '0',
  `steals` smallint(6) DEFAULT '0',
  `times` smallint(6) DEFAULT NULL,
  `match_total` int(11) DEFAULT '0',
  `main_total` int(11) DEFAULT '0',
  `point_agv` float NOT NULL DEFAULT '0',
  `rebound_agv` float NOT NULL DEFAULT '0',
  `steals_agv` float NOT NULL DEFAULT '0',
  `assist_agv` float NOT NULL DEFAULT '0',
  `block_agv` float NOT NULL DEFAULT '0',
  `created_time` datetime DEFAULT NULL,
  `updated_time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `player_no` (`player_no`),
  KEY `point_agv` (`point_agv`),
  KEY `rebound_agv` (`rebound_agv`),
  KEY `steals_agv` (`steals_agv`),
  KEY `assist_agv` (`assist_agv`),
  KEY `block_agv` (`block_agv`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2010-06-10 23:39:37
