DROP TABLE IF EXISTS `tournament_teams`;
CREATE TABLE `tournament_teams` (
  `obj_id` int NOT NULL DEFAULT 0,
  `type` int DEFAULT NULL,
  `team_id` int NOT NULL,
  `team_name` VARCHAR(255) DEFAULT NULL,
  `leader` int DEFAULT NULL,
  `category` int DEFAULT NULL,
  `wins` int NOT NULL DEFAULT 0,
  `losts` int NOT NULL DEFAULT 0,
  `status` int NOT NULL DEFAULT 0
) ENGINE=MyISAM DEFAULT CHARSET=utf8;