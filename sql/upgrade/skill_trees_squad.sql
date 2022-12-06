DROP TABLE IF EXISTS `skill_trees_squad`;
CREATE TABLE `skill_trees_squad` (
  `skill_id` mediumint(8) unsigned NOT NULL,
  `level` smallint(3) unsigned NOT NULL,
  `name` varchar(25) NOT NULL DEFAULT 'Clan Skill',
  `clan_lvl` tinyint(2) unsigned NOT NULL,
  `repCost` int(10) unsigned NOT NULL,
  `itemId` mediumint(8) unsigned NOT NULL,
  `itemCount` bigint(20) unsigned NOT NULL,
  PRIMARY KEY (`skill_id`,`level`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;


INSERT INTO `skill_trees_squad` VALUES ('611', '1', 'Fire Squad', '8', '12000', '9910', '8');
INSERT INTO `skill_trees_squad` VALUES ('611', '2', 'Fire Squad', '9', '17400', '9910', '11');
INSERT INTO `skill_trees_squad` VALUES ('611', '3', 'Fire Squad', '10', '24000', '9911', '2');
INSERT INTO `skill_trees_squad` VALUES ('612', '1', 'Water Squad', '7', '10400', '9910', '4');
INSERT INTO `skill_trees_squad` VALUES ('612', '2', 'Water Squad', '8', '12000', '9910', '8');
INSERT INTO `skill_trees_squad` VALUES ('612', '3', 'Water Squad', '9', '17400', '9910', '11');
INSERT INTO `skill_trees_squad` VALUES ('613', '1', 'Wind Squad', '8', '12000', '9910', '8');
INSERT INTO `skill_trees_squad` VALUES ('613', '2', 'Wind Squad', '9', '17400', '9910', '11');
INSERT INTO `skill_trees_squad` VALUES ('613', '3', 'Wind Squad', '10', '24000', '9911', '2');
INSERT INTO `skill_trees_squad` VALUES ('614', '1', 'Earth Squad', '8', '12000', '9910', '8');
INSERT INTO `skill_trees_squad` VALUES ('614', '2', 'Earth Squad', '9', '17400', '9910', '11');
INSERT INTO `skill_trees_squad` VALUES ('614', '3', 'Earth Squad', '10', '24000', '9911', '2');
INSERT INTO `skill_trees_squad` VALUES ('615', '1', 'Holy Squad', '7', '10400', '9910', '4');
INSERT INTO `skill_trees_squad` VALUES ('615', '2', 'Holy Squad', '8', '12000', '9910', '8');
INSERT INTO `skill_trees_squad` VALUES ('615', '3', 'Holy Squad', '9', '17400', '9910', '11');
INSERT INTO `skill_trees_squad` VALUES ('616', '1', 'Dark Squad', '8', '12000', '9910', '8');
INSERT INTO `skill_trees_squad` VALUES ('616', '2', 'Dark Squad', '9', '17400', '9910', '11');
INSERT INTO `skill_trees_squad` VALUES ('616', '3', 'Dark Squad', '10', '24000', '9911', '2');