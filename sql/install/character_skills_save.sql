DROP TABLE IF EXISTS `character_skills_save`;
CREATE TABLE `character_skills_save` (
  `char_obj_id` int NOT NULL DEFAULT 0,
  `skill_id` smallint unsigned NOT NULL DEFAULT 0,
  `class_index` smallint NOT NULL DEFAULT 0,
  `end_time` bigint NOT NULL DEFAULT 0,
  `reuse_delay_org` int NOT NULL DEFAULT 0,
  PRIMARY KEY  (`char_obj_id`,`skill_id`,`class_index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;