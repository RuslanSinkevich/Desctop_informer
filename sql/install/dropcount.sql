DROP TABLE IF EXISTS `dropcount`;
CREATE TABLE `dropcount` (
  `char_id` int unsigned NOT NULL,
  `item_id` smallint unsigned NOT NULL,
  `count` bigint unsigned DEFAULT 0,
  UNIQUE KEY `char_id` (`char_id`,`item_id`),
  KEY `char_id_2` (`char_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;