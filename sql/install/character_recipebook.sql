DROP TABLE IF EXISTS `character_recipebook`;
CREATE TABLE `character_recipebook` (
  `char_id` int NOT NULL DEFAULT 0,
  `id` smallint unsigned NOT NULL DEFAULT 0,
  PRIMARY KEY  (`id`,`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;