DROP TABLE IF EXISTS `raidboss_status`;
CREATE TABLE `raidboss_status` (
  `id` int NOT NULL,
  `current_hp` int DEFAULT NULL,
  `current_mp` int DEFAULT NULL,
  `respawn_delay` int NOT NULL DEFAULT 0,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;