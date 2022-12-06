DROP TABLE IF EXISTS `heroes`;
CREATE TABLE `heroes` (
  `char_id` int NOT NULL DEFAULT 0,
  `count` tinyint unsigned NOT NULL DEFAULT 0,
  `played` tinyint NOT NULL DEFAULT 0,
  `active` tinyint NOT NULL DEFAULT 0,
  PRIMARY KEY  (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;