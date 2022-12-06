DROP TABLE IF EXISTS `mapregion`;
CREATE TABLE `mapregion` (
  `y10_plus` int(11) NOT NULL DEFAULT '0',
  `x11` int(2) NOT NULL DEFAULT '0',
  `x12` int(2) NOT NULL DEFAULT '0',
  `x13` int(2) NOT NULL DEFAULT '0',
  `x14` int(2) NOT NULL DEFAULT '0',
  `x15` int(2) NOT NULL DEFAULT '0',
  `x16` int(2) NOT NULL DEFAULT '0',
  `x17` int(2) NOT NULL DEFAULT '0',
  `x18` int(2) NOT NULL DEFAULT '0',
  `x19` int(2) NOT NULL DEFAULT '0',
  `x20` int(2) NOT NULL DEFAULT '0',
  `x21` int(2) NOT NULL DEFAULT '0',
  `x22` int(2) NOT NULL DEFAULT '0',
  `x23` int(2) NOT NULL DEFAULT '0',
  `x24` int(2) NOT NULL DEFAULT '0',
  `x25` int(2) NOT NULL DEFAULT '0',
  `x26` int(2) NOT NULL DEFAULT '0',
  PRIMARY KEY (`y10_plus`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT INTO `mapregion` VALUES ('0', '21', '21', '21', '21', '4', '4', '4', '8', '10', '12', '5', '5', '5', '5', '5', '5');
INSERT INTO `mapregion` VALUES ('1', '21', '21', '21', '21', '4', '4', '4', '4', '4', '4', '5', '5', '5', '5', '5', '5');
INSERT INTO `mapregion` VALUES ('2', '21', '21', '21', '21', '4', '4', '4', '4', '4', '4', '16', '16', '5', '5', '5', '5');
INSERT INTO `mapregion` VALUES ('3', '21', '21', '21', '21', '4', '4', '4', '4', '4', '4', '16', '16', '16', '15', '15', '15');
INSERT INTO `mapregion` VALUES ('4', '21', '21', '21', '21', '4', '4', '4', '4', '4', '4', '16', '16', '16', '15', '15', '15');
INSERT INTO `mapregion` VALUES ('5', '21', '21', '21', '21', '19', '19', '19', '4', '4', '4', '14', '14', '14', '15', '15', '15');
INSERT INTO `mapregion` VALUES ('6', '21', '21', '21', '21', '19', '19', '19', '19', '14', '14', '14', '14', '15', '15', '15', '15');
INSERT INTO `mapregion` VALUES ('7', '21', '21', '21', '21', '17', '17', '17', '3', '3', '18', '14', '10', '11', '11', '11', '11');
INSERT INTO `mapregion` VALUES ('8', '21', '21', '21', '21', '17', '17', '17', '3', '3', '3', '10', '10', '11', '11', '11', '11');
INSERT INTO `mapregion` VALUES ('9', '21', '21', '21', '21', '17', '17', '17', '3', '3', '2', '2', '10', '12', '11', '11', '11');
INSERT INTO `mapregion` VALUES ('10', '21', '21', '21', '21', '17', '17', '7', '3', '6', '2', '2', '10', '12', '12', '12', '12');
INSERT INTO `mapregion` VALUES ('11', '21', '21', '21', '21', '7', '7', '7', '6', '6', '8', '8', '12', '12', '12', '9', '9');
INSERT INTO `mapregion` VALUES ('12', '21', '21', '21', '21', '7', '7', '7', '7', '6', '8', '8', '9', '9', '9', '9', '9');
INSERT INTO `mapregion` VALUES ('13', '21', '21', '21', '21', '1', '1', '7', '7', '6', '8', '9', '13', '13', '13', '13', '13');
INSERT INTO `mapregion` VALUES ('14', '21', '21', '21', '21', '1', '1', '1', '7', '7', '8', '9', '13', '13', '13', '13', '13');
INSERT INTO `mapregion` VALUES ('15', '21', '21', '21', '21', '1', '1', '1', '1', '20', '20', '1', '13', '13', '13', '13', '13');
INSERT INTO `mapregion` VALUES ('16', '21', '21', '21', '21', '1', '1', '1', '1', '22', '22', '1', '1', '1', '1', '1', '1');