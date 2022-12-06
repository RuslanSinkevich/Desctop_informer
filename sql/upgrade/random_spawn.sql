DROP TABLE IF EXISTS `random_spawn`;
CREATE TABLE `random_spawn` (
  `groupId` int(11) NOT NULL DEFAULT '0',
  `npcId` int(11) NOT NULL DEFAULT '0',
  `count` int(11) NOT NULL DEFAULT '0',
  `initialDelay` bigint(20) NOT NULL DEFAULT '-1',
  `respawnDelay` bigint(20) NOT NULL DEFAULT '-1',
  `despawnDelay` bigint(20) NOT NULL DEFAULT '-1',
  `broadcastSpawn` enum('false','true') NOT NULL DEFAULT 'false',
  `randomSpawn` enum('false','true') NOT NULL DEFAULT 'true',
  PRIMARY KEY (`groupId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT INTO `random_spawn` VALUES ('2', '31111', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('3', '31112', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('4', '31113', '1', '-1', '-1', '-1', 'true', 'true');
INSERT INTO `random_spawn` VALUES ('5', '31126', '1', '-1', '-1', '-1', 'true', 'true');
INSERT INTO `random_spawn` VALUES ('6', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('7', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('8', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('9', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('10', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('11', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('12', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('13', '31094', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('14', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('15', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('16', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('17', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('18', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('19', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('20', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('21', '31093', '1', '-1', '300000', '300000', 'false', 'true');
INSERT INTO `random_spawn` VALUES ('22', '25283', '1', '-1', '86400', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('23', '25286', '1', '-1', '86400', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('103', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('104', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('105', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('106', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('107', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('108', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('109', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('110', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('111', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('112', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('113', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('114', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('115', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('116', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('117', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('118', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('119', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('120', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('121', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('122', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('123', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('124', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('125', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('126', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('127', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('128', '31171', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('129', '31170', '1', '-1', '60', '0', 'false', 'false');
INSERT INTO `random_spawn` VALUES ('130', '31171', '1', '-1', '60', '0', 'false', 'false');