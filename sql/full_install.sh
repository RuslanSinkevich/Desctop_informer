if [ -f mysql_pass.sh ]; then
		. mysql_pass.sh
else
		. mysql_pass.sh.default
fi

mysqldump --ignore-table=${DBNAME}.game_log --ignore-table=${DBNAME}.loginserv_log --ignore-table=${DBNAME}.petitions --add-drop-table -h $DBHOST -u $USER --password=$PASS $DBNAME > l2rt_full_backup.sql

for tab in \
		install/ally_data.sql \
		install/auction.sql \
		install/auction_bids.sql \
		install/auction_lots.sql \
		install/auction_bid.sql \
		install/bans.sql \
		install/bonus.sql \
		install/castle.sql \
		install/castle_manor_procure.sql \
		install/castle_manor_production.sql \
		install/character_blocklist.sql \
		install/character_contacts.sql \
		install/character_tpbookmark.sql \
		install/character_effects_save.sql \
		install/character_friends.sql \
		install/character_hennas.sql \
		install/character_l2top_votes.sql \
		install/character_macroses.sql \
		install/character_mail.sql \
		install/character_mmotop_votes.sql \
		install/character_quests.sql \
		install/character_recipebook.sql \
		install/character_shortcuts.sql \
		install/character_skills.sql \
		install/character_skills_save.sql \
		install/character_subclasses.sql \
		install/character_variables.sql \
		install/characters.sql \
		install/clan_data.sql \
		install/clan_notices.sql \
		install/clan_privs.sql \
		install/clan_skills.sql \
		install/clan_subpledges.sql \
		install/clan_wars.sql \
		install/clanhall.sql \
		install/community_skillsave.sql \
		install/communitybuff.sql \
		install/comteleport.sql \
		install/couples.sql \
		install/craftcount.sql \
		install/cursed_weapons.sql \
		install/dropcount.sql \
		install/epic_boss_spawn.sql \
		install/fishing_championship.sql \
		install/fishing_championship_date.sql \
		install/forts.sql \
		install/forums.sql \
		install/game_log.sql \
		install/games.sql \
		install/global_tasks.sql \
		install/hellbound.sql \
		install/heroes.sql \
		install/item_attributes.sql \
		install/items.sql \
		install/item_auction.sql \
		install/item_auction_bid.sql \
		install/items_delayed.sql \
		install/killcount.sql \
		install/mail.sql \
		install/mail_attachments.sql \
		install/multisell_log.sql \
		install/olympiad_nobles.sql \
		install/petitions.sql \
		install/pets.sql \
		install/posts.sql \
		install/raidboss_points.sql \
		install/raidboss_status.sql \
		install/residence_functions.sql \
		install/server_variables.sql \
		install/seven_signs.sql \
		install/seven_signs_festival.sql \
		install/seven_signs_status.sql \
		install/siege_clans.sql \
		install/siege_doorupgrade.sql \
		install/siege_guards.sql \
		install/siege_territory_members.sql \
		install/summon_effects_save.sql \
		install/topic.sql \
		install/tournament_table.sql \
		install/tournament_teams.sql \
		install/tournament_variables.sql \
		install/vote.sql \
		install/mercenaries_kills.sql \
		install/mercenaries_orders.sql \
		install/mercenaries_rewards.sql \
	; do
		echo Loading $tab ...
		mysql -h $DBHOST -u $USER --password=$PASS -D $DBNAME < $tab
done
./upgrade.sh