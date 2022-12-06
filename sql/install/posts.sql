DROP TABLE IF EXISTS `posts`;
CREATE TABLE `posts` (
  `post_id` int NOT NULL DEFAULT 0,
  `post_owner_name` VARCHAR(255) NOT NULL DEFAULT '',
  `post_ownerid` int NOT NULL DEFAULT 0,
  `post_date` bigint NOT NULL DEFAULT 0,
  `post_topic_id` int NOT NULL DEFAULT 0,
  `post_forum_id` int NOT NULL DEFAULT 0,
  `post_txt` text NOT NULL,
  KEY `post_forum_id` (`post_forum_id`,`post_topic_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;