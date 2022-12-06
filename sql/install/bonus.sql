DROP TABLE IF EXISTS `bonus`;
CREATE TABLE `bonus` (
  `obj_id` int NOT NULL DEFAULT 0,
  `bonus_name` VARCHAR(30) NOT NULL DEFAULT '',
  `bonus_value` int NOT NULL DEFAULT 0,
  `bonus_expire_time` int NOT NULL DEFAULT 0,
  PRIMARY KEY  (`obj_id`,`bonus_name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;