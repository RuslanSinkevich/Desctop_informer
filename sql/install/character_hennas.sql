DROP TABLE IF EXISTS `character_hennas`;
CREATE TABLE `character_hennas` (
  `char_obj_id` int NOT NULL DEFAULT 0,
  `symbol_id` tinyint unsigned NOT NULL DEFAULT 0,
  `slot` tinyint unsigned NOT NULL DEFAULT 0,
  `class_index` tinyint unsigned NOT NULL DEFAULT 0,
  INDEX (`char_obj_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;