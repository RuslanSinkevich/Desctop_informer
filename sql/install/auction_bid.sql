DROP TABLE IF EXISTS `auction_bid`;
CREATE TABLE `auction_bid` (
  `id` int NOT NULL DEFAULT 0,
  `auctionId` int NOT NULL DEFAULT 0,
  `bidderId` int NOT NULL DEFAULT 0,
  `bidderName` VARCHAR(50) NOT NULL,
  `clan_name` VARCHAR(50) NOT NULL,
  `maxBid` bigint(20) NOT NULL DEFAULT '0',
  `time_bid` bigint NOT NULL DEFAULT 0,
  PRIMARY KEY  (`auctionId`, `bidderId`),
  KEY `id` (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;