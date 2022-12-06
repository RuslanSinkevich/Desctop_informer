DROP TABLE IF EXISTS `residence_functions`;
CREATE TABLE `residence_functions` (
  `id` tinyint unsigned NOT NULL DEFAULT 0,
  `type` tinyint unsigned NOT NULL DEFAULT 0,
  `lvl` smallint unsigned NOT NULL DEFAULT 0,
  `lease` int NOT NULL DEFAULT 0,
  `rate` bigint NOT NULL DEFAULT 0,
  `endTime` int NOT NULL DEFAULT 0,
  `inDebt` tinyint NOT NULL DEFAULT 0,
  PRIMARY KEY  (`id`,`type`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
