DROP TABLE IF EXISTS `auto_chat_text`;
CREATE TABLE `auto_chat_text` (
  `groupId` int(11) NOT NULL DEFAULT '0',
  `chatText` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`groupId`,`chatText`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

INSERT INTO `auto_chat_text` VALUES ('1', '%player_cabal_loser%! All is lost! Prepare to meet the goddess of death!');
INSERT INTO `auto_chat_text` VALUES ('1', '%player_cabal_loser%! You bring an ill wind!');
INSERT INTO `auto_chat_text` VALUES ('1', '%player_cabal_loser%! You might as well give up!');
INSERT INTO `auto_chat_text` VALUES ('1', 'A curse upon you!');
INSERT INTO `auto_chat_text` VALUES ('1', 'All is lost! Prepare to meet the goddess of death!');
INSERT INTO `auto_chat_text` VALUES ('1', 'All is lost! The prophecy of destruction has been fulfilled!');
INSERT INTO `auto_chat_text` VALUES ('1', 'The prophecy of doom has awoken!');
INSERT INTO `auto_chat_text` VALUES ('1', 'This world will soon be annihilated!');
INSERT INTO `auto_chat_text` VALUES ('2', '%player_cabal_winner%! I bestow on you the authority of the abyss!');
INSERT INTO `auto_chat_text` VALUES ('2', '%player_cabal_winner%, Darkness shall be banished forever!');
INSERT INTO `auto_chat_text` VALUES ('2', '%player_cabal_winner%, the time for glory is at hand!');
INSERT INTO `auto_chat_text` VALUES ('2', 'All hail the eternal twilight!');
INSERT INTO `auto_chat_text` VALUES ('2', 'As foretold in the prophecy of darkness, the era of chaos has begun!');
INSERT INTO `auto_chat_text` VALUES ('2', 'The day of judgment is near!');
INSERT INTO `auto_chat_text` VALUES ('2', 'The prophecy of darkness has been fulfilled!');
INSERT INTO `auto_chat_text` VALUES ('2', 'The prophecy of darkness has come to pass!');
INSERT INTO `auto_chat_text` VALUES ('3', 'Dear %player_random%, may the blessing of Einhasad be with you always.');
INSERT INTO `auto_chat_text` VALUES ('4', '!I\'ve been so busy lately, in addition to planning my trip!');
INSERT INTO `auto_chat_text` VALUES ('4', '!Rulers of the seal! I bring you wondrous gifts!');
INSERT INTO `auto_chat_text` VALUES ('4', '!Rulers of the seal! I have some excellent weapons to show you!');
INSERT INTO `auto_chat_text` VALUES ('5', '!Who will be the lucky one tonight? Ha-ha! Curious, very curious!');
INSERT INTO `auto_chat_text` VALUES ('6', '!Courage! Ambition! Passion! Mercenaries who want to realize their dream of fighting in the territory war, come tome! Fortune and glory are waiting you!');