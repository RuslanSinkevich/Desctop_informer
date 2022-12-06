DROP TABLE IF EXISTS `pets`;
CREATE TABLE `pets` (
  `item_obj_id` int NOT NULL DEFAULT 0,
  `objId` int,
  `name` VARCHAR(12) DEFAULT NULL,
  `level` tinyint unsigned,
  `curHp` mediumint unsigned,
  `curMp` mediumint unsigned,
  `exp` bigint,
  `sp` int unsigned,
  `fed` smallint unsigned,
  `max_fed` smallint unsigned,
  PRIMARY KEY (item_obj_id)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;