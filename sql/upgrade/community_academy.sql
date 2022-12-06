DROP TABLE IF EXISTS `community_academy`;
CREATE TABLE `community_academy` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `objId` int(11) NOT NULL DEFAULT '-1',
  `name` varchar(16) DEFAULT NULL,
  `param` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`,`objId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;