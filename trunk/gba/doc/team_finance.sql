/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE */;
/*!40101 SET SQL_MODE='' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES */;
/*!40103 SET SQL_NOTES='ON' */;

CREATE TABLE `team_finance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` smallint(6) DEFAULT NULL,
  `round` tinyint(3) DEFAULT NULL,
  `type` tinyint(3) DEFAULT NULL,
  `info` varchar(255) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `team_id` int(11) DEFAULT NULL,
  `created_time` datetime DEFAULT NULL,
  `updated_time` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
