DROP TABLE IF EXISTS `mail`;
CREATE TABLE `mail` (
  `messageId` INT UNSIGNED NOT NULL auto_increment,
  `sender` INT UNSIGNED NOT NULL,
  `receiver` INT UNSIGNED NOT NULL,
  `expire` TIMESTAMP NOT NULL DEFAULT '0000-00-00 00:00:00',
  `topic` VARCHAR(30) NOT NULL,
  `body` TEXT NOT NULL,
  `attachments` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `needsPayment` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `price` BIGINT UNSIGNED NOT NULL DEFAULT 0,
  `system` TINYINT UNSIGNED NOT NULL DEFAULT 0,
  `unread` TINYINT UNSIGNED NOT NULL DEFAULT 1,
  PRIMARY KEY (`messageId`),
  KEY `sender` (`sender`),
  KEY `receiver` (`receiver`),
  KEY `attachments` (`attachments`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;