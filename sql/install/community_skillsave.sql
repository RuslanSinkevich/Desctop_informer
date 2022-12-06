DROP TABLE IF EXISTS `community_skillsave`;
CREATE TABLE `community_skillsave` (
  `id` int(2) DEFAULT '1',
  `charId` int(10) DEFAULT NULL,
  `skills` VARCHAR(250) DEFAULT '',
  `pet` VARCHAR(500) DEFAULT ''
) ENGINE=MyISAM DEFAULT CHARSET=utf8;