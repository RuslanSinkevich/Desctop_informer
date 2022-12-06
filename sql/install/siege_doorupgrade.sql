DROP TABLE IF EXISTS `siege_doorupgrade`;
CREATE TABLE `siege_doorupgrade` (
  `doorId` INT NOT NULL DEFAULT 0,
  `hp` INT NOT NULL DEFAULT 0,
  PRIMARY KEY  (`doorId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;