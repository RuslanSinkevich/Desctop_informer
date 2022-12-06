DROP TABLE IF EXISTS `raidboss_points`;
CREATE TABLE `raidboss_points` (
  `owner_id` int NOT NULL,
  `boss_id` smallint unsigned NOT NULL,
  `points` int NOT NULL DEFAULT 0,
  KEY `owner_id` (`owner_id`),
  KEY `boss_id` (`boss_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;