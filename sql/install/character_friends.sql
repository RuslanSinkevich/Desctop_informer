DROP TABLE IF EXISTS `character_friends`;
CREATE TABLE `character_friends` (
  `char_id` int NOT NULL DEFAULT 0,
  `friend_id` int NOT NULL DEFAULT 0,
  `relation` INT UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY  (`char_id`,`friend_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;