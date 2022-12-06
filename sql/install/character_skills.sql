DROP TABLE IF EXISTS `character_skills`;
CREATE TABLE `character_skills` (
  `char_obj_id` int NOT NULL DEFAULT 0,
  `skill_id` smallint unsigned NOT NULL DEFAULT 0,
  `skill_level` smallint unsigned NOT NULL DEFAULT 0,
  `skill_name` VARCHAR(100) DEFAULT NULL,
  `class_index` tinyint unsigned NOT NULL DEFAULT 0,
  PRIMARY KEY  (`char_obj_id`,`skill_id`,`class_index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;