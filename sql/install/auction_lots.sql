DROP TABLE IF EXISTS `auction_lots`;
CREATE TABLE `auction_lots` (
  `lotId` int(10) NOT NULL AUTO_INCREMENT,
  `ownerId` int(10) NOT NULL,
  `itemId` int(6) NOT NULL,
  `objectId` int(10) NOT NULL,
  `count` bigint(20) NOT NULL,
  `enchantLevel` int(6) NOT NULL,
  `currency` int(11) NOT NULL,
  `startingBid` bigint(20) NOT NULL,
  `bidIncrement` bigint(20) NOT NULL,
  `buyNow` bigint(20) NOT NULL,
  `endDate` bigint(20) NOT NULL,
  `processed` VARCHAR(5) NOT NULL DEFAULT 'false',
  PRIMARY KEY (`lotId`),
  KEY `lotId` (`ownerId`)
) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;