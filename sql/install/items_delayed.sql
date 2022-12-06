DROP TABLE IF EXISTS `items_delayed`;
CREATE TABLE `items_delayed` (
  `payment_id` int NOT NULL auto_increment,
  `owner_id` int NOT NULL,
  `item_id` smallint unsigned NOT NULL,
  `count` int unsigned NOT NULL DEFAULT 1,
  `enchant_level` smallint unsigned NOT NULL DEFAULT 0,
  `attribute` smallint NOT NULL DEFAULT -1,
  `attribute_level` smallint NOT NULL DEFAULT -1,
  `elem0` smallint NOT NULL DEFAULT 0,
  `elem1` smallint NOT NULL DEFAULT 0,
  `elem2` smallint NOT NULL DEFAULT 0,
  `elem3` smallint NOT NULL DEFAULT 0,
  `elem4` smallint NOT NULL DEFAULT 0,
  `elem5` smallint NOT NULL DEFAULT 0,
  `flags` int NOT NULL DEFAULT 0,
  `payment_status` tinyint unsigned NOT NULL DEFAULT 0,
  `description` VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `key_owner_id` (`owner_id`),
  KEY `key_item_id` (`item_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;