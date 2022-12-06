DROP TABLE IF EXISTS `character_macroses`;
CREATE TABLE `character_macroses` (
  `char_obj_id` int NOT NULL DEFAULT 0,
  `id` smallint unsigned NOT NULL DEFAULT 0,
  `icon` tinyint unsigned,
  `name` VARCHAR(40) DEFAULT NULL,
  `descr` VARCHAR(80) DEFAULT NULL,
  `acronym` VARCHAR(4) DEFAULT NULL,
  `commands` VARCHAR(1024) DEFAULT NULL,
  PRIMARY KEY  (`char_obj_id`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;