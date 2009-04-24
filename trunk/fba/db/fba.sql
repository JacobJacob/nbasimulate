/*
MySQL Data Transfer
Source Host: localhost
Source Database: fba
Target Host: localhost
Target Database: fba
Date: 2009-4-24 16:06:32
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for player
-- ----------------------------
DROP TABLE IF EXISTS `player`;
CREATE TABLE `player` (
  `id` bigint(15) NOT NULL auto_increment,
  `name` varchar(25) default NULL,
  `no` int(15) default NULL,
  `age` int(15) default NULL,
  `position` varchar(25) default NULL,
  `stature` int(15) default NULL,
  `avoirdupois` int(15) default NULL,
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
  `shooting_max` int(15) default NULL,
  `speed_max` int(15) default NULL,
  `strength_max` int(15) default NULL,
  `bounce_max` int(15) default NULL,
  `stamina_max` int(15) default NULL,
  `trisection_max` int(15) default NULL,
  `dribble_max` int(15) default NULL,
  `pass_max` int(15) default NULL,
  `backboard_max` int(15) default NULL,
  `steal_max` int(15) default NULL,
  `blocked_max` int(15) default NULL,
  `teamid` int(15) default NULL,
  `them` int(15) default NULL,
  `status` int(15) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `player` VALUES ('1', '姚明', '11', '27', 'C', '213', '140', '64', '50', '45', '67', '29', '60', '12', '46', '48', '70', '40', '66', null, null, null, null, null, null, null, null, null, null, null, '1', '97', '90');
INSERT INTO `player` VALUES ('2', '阿泰斯特', '44', '29', 'SG', '199', '100', '66', '77', '76', '90', '66', '80', '66', '77', '66', '66', '70', '68', null, null, null, null, null, null, null, null, null, null, null, '1', '98', '88');
INSERT INTO `player` VALUES ('3', '斯科拉', '12', '28', 'PF', '207', '120', '61', '70', '69', '78', '76', '70', '39', '66', '69', '76', '55', '59', null, null, null, null, null, null, null, null, null, null, null, '1', '100', '99');
INSERT INTO `player` VALUES ('4', '科比布莱恩特', '34', '30', 'SG', '198', '98', '73', '94', '83', '88', '80', '85', '74', '90', '78', '59', '78', '60', null, null, null, null, null, null, null, null, null, null, null, '1', '100', '100');
