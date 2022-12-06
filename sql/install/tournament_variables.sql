DROP TABLE IF EXISTS `tournament_variables`;
CREATE TABLE `tournament_variables` (
  `name` VARCHAR(255) NOT NULL DEFAULT '',
  `value` VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY  (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT INTO `tournament_variables` VALUES ('start','0');