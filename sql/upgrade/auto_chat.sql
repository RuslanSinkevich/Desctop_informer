DROP TABLE IF EXISTS `auto_chat`;
CREATE TABLE `auto_chat` (
  `groupId` int(11) NOT NULL DEFAULT '0',
  `npcId` int(11) NOT NULL DEFAULT '0',
  `chatDelay` int(11) NOT NULL DEFAULT '-1',
  PRIMARY KEY (`groupId`,`npcId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT INTO `auto_chat` VALUES ('1', '31093', '-1');
INSERT INTO `auto_chat` VALUES ('2', '31094', '-1');
INSERT INTO `auto_chat` VALUES ('3', '22134', '-1');
INSERT INTO `auto_chat` VALUES ('4', '31126', '1800');
INSERT INTO `auto_chat` VALUES ('5', '32347', '1800');
INSERT INTO `auto_chat` VALUES ('6', '36481', '600');
INSERT INTO `auto_chat` VALUES ('6', '36482', '600');
INSERT INTO `auto_chat` VALUES ('6', '36483', '600');
INSERT INTO `auto_chat` VALUES ('6', '36484', '600');
INSERT INTO `auto_chat` VALUES ('6', '36485', '600');
INSERT INTO `auto_chat` VALUES ('6', '36486', '600');
INSERT INTO `auto_chat` VALUES ('6', '36487', '600');
INSERT INTO `auto_chat` VALUES ('6', '36488', '600');
INSERT INTO `auto_chat` VALUES ('6', '36489', '600');