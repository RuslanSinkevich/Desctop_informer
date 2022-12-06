DROP TABLE IF EXISTS `siege_territory_members`;
CREATE TABLE `siege_territory_members` (
  `obj_Id` int(11) NOT NULL DEFAULT '0',
  `side` int(11) NOT NULL DEFAULT '0',
  `type` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY  (`obj_Id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;