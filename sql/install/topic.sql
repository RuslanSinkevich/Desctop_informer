DROP TABLE IF EXISTS `topic`;
CREATE TABLE `topic` (
  `topic_id` int NOT NULL DEFAULT 0,
  `topic_forum_id` int NOT NULL DEFAULT 0,
  `topic_name` VARCHAR(255) NOT NULL,
  `topic_date` bigint NOT NULL DEFAULT 0,
  `topic_ownername` VARCHAR(255) NOT NULL DEFAULT 0,
  `topic_ownerid` int NOT NULL DEFAULT 0,
  `topic_type` int NOT NULL DEFAULT 0,
  `topic_reply` int NOT NULL DEFAULT 0,
  KEY `topic_forum_id` (`topic_forum_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;