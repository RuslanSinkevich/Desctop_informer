DROP TABLE IF EXISTS `clan_subpledges`;
CREATE TABLE `clan_subpledges` (
  `clan_id` int unsigned NOT NULL DEFAULT 0,
  `type` smallint NOT NULL DEFAULT 0,
  `name` VARCHAR(45) NOT NULL DEFAULT '',
  `leader_id` int unsigned NOT NULL DEFAULT 0,
  PRIMARY KEY  (`clan_id`,`type`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;