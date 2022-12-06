DROP TABLE IF EXISTS `forums`;
CREATE TABLE `forums` (
  `forum_id` int NOT NULL DEFAULT 0,
  `forum_name` VARCHAR(255) NOT NULL DEFAULT '',
  `forum_parent` int NOT NULL DEFAULT 0,
  `forum_post` int NOT NULL DEFAULT 0,
  `forum_type` int NOT NULL DEFAULT 0,
  `forum_perm` int NOT NULL DEFAULT 0,
  `forum_owner_id` int NOT NULL DEFAULT 0,
  UNIQUE KEY `forum_id` (`forum_id`),
  KEY `forum_id_parent` (`forum_id`, `forum_parent`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT IGNORE INTO `forums` VALUES (1, 'NormalRoot', 0, 0, 0, 1, 0);
INSERT IGNORE INTO `forums` VALUES (2, 'ClanRoot', 0, 0, 0, 0, 0);
INSERT IGNORE INTO `forums` VALUES (3, 'MemoRoot', 0, 0, 0, 0, 0);
INSERT IGNORE INTO `forums` VALUES (4, 'MailRoot', 0, 0, 0, 0, 0);