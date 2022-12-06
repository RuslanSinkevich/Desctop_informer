DROP TABLE IF EXISTS `auction_bids`;
CREATE TABLE `auction_bids` (
  `lotId` int(11) DEFAULT NULL,
  `bidderId` int(11) DEFAULT NULL,
  `bidAmount` bigint(20) DEFAULT NULL,
  `bidDate` decimal(20,0) DEFAULT NULL,
  `bidId` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;