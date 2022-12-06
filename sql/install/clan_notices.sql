DROP TABLE IF EXISTS `clan_notices`;
CREATE TABLE `clan_notices` (
  `clanID` int NOT NULL,
  `notice` VARCHAR(512) NOT NULL,
  `enabled` VARCHAR(5) NOT NULL,
  PRIMARY KEY  (`clanID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;