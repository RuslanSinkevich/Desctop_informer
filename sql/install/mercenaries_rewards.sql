DROP TABLE IF EXISTS `mercenaries_rewards`;
CREATE TABLE `mercenaries_rewards` (
  `target` int(11) NOT NULL,
  `id` bigint(25) NOT NULL,
  `count` bigint(25) NOT NULL,
  PRIMARY KEY (`target`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;