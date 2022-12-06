DROP TABLE IF EXISTS `siege_clans`;
CREATE TABLE `siege_clans` (
  `unit_id` int NOT NULL DEFAULT 0,
  `clan_id` int NOT NULL DEFAULT 0,
  `type` int DEFAULT NULL,
  PRIMARY KEY  (`clan_id`,`unit_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;