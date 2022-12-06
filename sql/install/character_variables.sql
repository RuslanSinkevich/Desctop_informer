DROP TABLE IF EXISTS `character_variables`;
CREATE TABLE `character_variables` (
  `obj_id` int NOT NULL DEFAULT 0,
  `type` VARCHAR(86) NOT NULL DEFAULT 0,
  `name` VARCHAR(86) NOT NULL DEFAULT 0,
  `value` VARCHAR(255) NOT NULL DEFAULT 0,
  `expire_time` int NOT NULL DEFAULT 0,
  UNIQUE KEY `prim` (`obj_id`,`type`,`name`),
  KEY `obj_id` (`obj_id`),
  KEY `type` (`type`),
  KEY `name` (`name`),
  KEY `value` (`value`),
  KEY `expire_time` (`expire_time`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;