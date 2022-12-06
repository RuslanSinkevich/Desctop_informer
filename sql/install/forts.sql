DROP TABLE IF EXISTS `forts`;
CREATE TABLE `forts` (
  `id` tinyint unsigned NOT NULL DEFAULT 0,
  `name` VARCHAR(45) NOT NULL,
  `lastSiegeDate` int NOT NULL DEFAULT 0,
  `siegeDate` int NOT NULL DEFAULT 0,
  `skills` VARCHAR(32) NOT NULL DEFAULT '0;0',
  `ownDate` int NOT NULL DEFAULT 0,
  `state` tinyint unsigned NOT NULL DEFAULT '0',
  `castleId` tinyint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT INTO `forts` VALUES
('101', 'Shanty Fortress', '0', '0', '590;1;603;1', '0', '0', '0'),
('102', 'Southern Fortress', '0', '0', '602;1;604;1', '0', '0', '0'),
('103', 'Hive Fortress', '0', '0', '601;1;605;1', '0', '0', '0'),
('104', 'Valley Fortress', '0', '0', '595;1;606;1', '0', '0', '0'),
('105', 'Ivory Fortress', '0', '0', '607;1;594;1', '0', '0', '0'),
('106', 'Narsell Fortress', '0', '0', '608;1;593;1', '0', '0', '0'),
('107', 'Bayou Fortress', '0', '0', '596;1;598;1', '0', '0', '0'),
('108', 'White Sands Fortress', '0', '0', '592;1;599;1', '0', '0', '0'),
('109', 'Borderland Fortress', '0', '0', '591;1;610;1', '0', '0', '0'),
('110', 'Swamp Fortress', '0', '0', '597;1;601;1', '0', '0', '0'),
('111', 'Archaic Fortress', '0', '0', '590;1;608;1', '0', '0', '0'),
('112', 'Floran Fortress', '0', '0', '590;1;608;1', '0', '0', '0'),
('113', 'Cloud Mountain Fortress', '0', '0', '610;1;606;1', '0', '0', '0'),
('114', 'Tanor Fortress', '0', '0', '609;1;605;1', '0', '0', '0'),
('115', 'Dragonspine Fortress', '0', '0', '599;1;604;1', '0', '0', '0'),
('116', 'Antharas Fortress', '0', '0', '598;1;603;1', '0', '0', '0'),
('117', 'Western Fortress', '0', '0', '597;1;602;1;610;1', '0', '0', '0'),
('118', 'Hunters Fortress', '0', '0', '601;1;596;1', '0', '0', '0'),
('119', 'Aaru Fortress', '0', '0', '592;1;595;1', '0', '0', '0'),
('120', 'Demon Fortress', '0', '0', '591;1;594;1', '0', '0', '0'),
('121', 'Monastic Fortress', '0', '0', '590;1;593;1', '0', '0', '0');