DROP TABLE IF EXISTS `mercenaries_kills`;
CREATE TABLE `mercenaries_kills` (
  `target` int(11) NOT NULL,
  `killer` int(11) NOT NULL,
  `count` int(11) NOT NULL,
  PRIMARY KEY (`target`,`killer`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;