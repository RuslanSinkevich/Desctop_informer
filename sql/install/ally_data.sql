DROP TABLE IF EXISTS `ally_data`;
CREATE TABLE `ally_data` (
  `ally_id` int NOT NULL DEFAULT 0,
  `ally_name` VARCHAR(45) DEFAULT NULL,
  `leader_id` int NOT NULL DEFAULT 0,
  `expelled_member` int unsigned NOT NULL DEFAULT 0,
  `crest` VARBINARY(192) NULL DEFAULT NULL,
  PRIMARY KEY  (`ally_id`),
  KEY `leader_id` (`leader_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;