DROP TABLE IF EXISTS `multisell_log`;
CREATE TABLE `multisell_log` (
  `id` int(11) unsigned NOT NULL DEFAULT '0',
  `date` VARCHAR(2048) NOT NULL DEFAULT '',
  `itemId` VARCHAR(2048) NOT NULL DEFAULT '0',
  `count` VARCHAR(2048) NOT NULL DEFAULT '0',
  `dItemId` VARCHAR(2048) NOT NULL DEFAULT '0',
  `dCount` VARCHAR(2048) NOT NULL DEFAULT '0',
  `name` VARCHAR(16) NOT NULL DEFAULT ''
) ENGINE=MyISAM DEFAULT CHARSET=utf8;