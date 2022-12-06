DROP TABLE IF EXISTS `accounts`;
CREATE TABLE IF NOT EXISTS `accounts` (
  `login` varchar(45) NOT NULL default '',
  `password` varchar(256) default '',
  `lastactive` int(15) unsigned NOT NULL default '0',
  `access_level` tinyint(6) NOT NULL default '0',
  `lastIP` varchar(15) default '',
  `lastServer` int(4) default '1',
  `comments` text default NULL,
  `email` varchar(45) NOT NULL default 'null@null',
  `pay_stat` tinyint(1) NOT NULL default '1',
  `bonus` FLOAT NOT NULL default '1',
  `bonus_expire` int NOT NULL default '0',
  `banExpires` int(11) NOT NULL default '0',
  `AllowIPs` varchar(256) NOT NULL default '*',
  `points` int(11) NOT NULL default '0',
  `lock_expire` INT(11) NOT NULL DEFAULT '604800',
  `activated` tinyint(10) UNSIGNED NOT NULL DEFAULT '1',
  PRIMARY KEY  (`login`),
  KEY `bonus` (`bonus`),
  KEY `access_level` (`access_level`),
  KEY `pay_stat` (`pay_stat`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;