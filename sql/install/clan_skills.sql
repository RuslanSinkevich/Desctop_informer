DROP TABLE IF EXISTS `clan_skills`;
CREATE TABLE `clan_skills` (
  `clan_id` int NOT NULL DEFAULT 0,
  `skill_id` smallint unsigned NOT NULL DEFAULT 0,
  `skill_level` tinyint unsigned NOT NULL DEFAULT 0,
  `skill_name` VARCHAR(100) DEFAULT NULL,
  `squad_index` smallint(6) NOT NULL DEFAULT '-1',
  PRIMARY KEY (`clan_id`,`skill_id`,`squad_index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;