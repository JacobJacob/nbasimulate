/*
MySQL Data Transfer
Source Host: localhost
Source Database: fba
Target Host: localhost
Target Database: fba
Date: 2009-4-12 22:35:30
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for player
-- ----------------------------
DROP TABLE IF EXISTS `player`;
CREATE TABLE `player` (
  `id` bigint(15) NOT NULL auto_increment,
  `name` varchar(25) default NULL,
  `age` int(15) default NULL,
  `position` varchar(25) default NULL,
  `stature` int(15) default NULL,
  `avoirdupois` int(15) default NULL,
  `teamId` int(15) default NULL,
  `ability` int(15) default NULL,
  `shooting` int(15) default NULL,
  `speed` int(15) default NULL,
  `strength` int(15) default NULL,
  `bounce` int(15) default NULL,
  `stamina` int(15) default NULL,
  `trisection` int(15) default NULL,
  `dribble` int(15) default NULL,
  `pass` int(15) default NULL,
  `backboard` int(15) default NULL,
  `steal` int(15) default NULL,
  `blocked` int(15) default NULL,
  `no` int(15) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `player` VALUES (`1`, `姚明`, `27`, `PG`, null, null, `1`, null, null, null, null, null, null, null, null, null, null, null, null, `11`);
INSERT INTO `player` VALUES (`2`, `科比`, `29`, `SG`, null, null, `1`, null, null, null, null, null, null, null, null, null, null, null, null, `23`);
