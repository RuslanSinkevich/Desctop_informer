DROP TABLE IF EXISTS `mail_attachments`;
CREATE TABLE `mail_attachments` (
  `messageId` INT UNSIGNED NOT NULL,
  `itemId` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`messageId`,`itemId`),
  KEY `messageId` (`messageId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;