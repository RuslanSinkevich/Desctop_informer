DROP TABLE IF EXISTS `couples`;
CREATE TABLE `couples` (
  `id` int NOT NULL,
  `player1Id` int NOT NULL DEFAULT 0,
  `player2Id` int NOT NULL DEFAULT 0,
  `maried` VARCHAR(5) DEFAULT NULL,
  `affiancedDate` bigint DEFAULT 0,
  `weddingDate` bigint DEFAULT 0,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;