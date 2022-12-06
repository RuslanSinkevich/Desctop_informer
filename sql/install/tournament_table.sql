DROP TABLE IF EXISTS `tournament_table`;
CREATE TABLE `tournament_table` (
  `category` int DEFAULT NULL,
  `team1id` int DEFAULT NULL,
  `team1name` VARCHAR(255) DEFAULT NULL,
  `team2id` int DEFAULT NULL,
  `team2name` VARCHAR(255) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;