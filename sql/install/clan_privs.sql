DROP TABLE IF EXISTS `clan_privs`;
CREATE TABLE `clan_privs` (
  `clan_id` int NOT NULL DEFAULT 0,
  `rank` int NOT NULL DEFAULT 0,
  `privilleges` int NOT NULL DEFAULT 0,
  PRIMARY KEY  (`clan_id`,`rank`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;