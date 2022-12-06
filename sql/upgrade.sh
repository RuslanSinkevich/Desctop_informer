if [ -f mysql_pass.sh ]; then
		. mysql_pass.sh
else
		. mysql_pass.sh.default
fi

mysqldump --ignore-table=${DBNAME}.game_log --ignore-table=${DBNAME}.loginserv_log --ignore-table=${DBNAME}.petitions --add-drop-table -h $DBHOST -u $USER --password=$PASS $DBNAME > ${DBNAME}_upgrade_`date +%Y-%m-%d_%H:%M:%S`.sql

for tab in \
		upgrade/ai_params.sql \
		upgrade/auto_chat.sql \
		upgrade/auto_chat_text.sql \
		upgrade/char_templates.sql \
		upgrade/class_list.sql \
		upgrade/community_academy.sql \
		upgrade/doors.sql \
		upgrade/droplist.sql \
		upgrade/four_sepulchers_spawnlist.sql \
		upgrade/henna.sql \
		upgrade/henna_trees.sql \
		upgrade/item_mall.sql \
		upgrade/itemall.sql \
		upgrade/lastimperialtomb_spawnlist.sql \
		upgrade/locations.sql \
		upgrade/lvlupgain.sql \
		upgrade/mapregion.sql \
		upgrade/merchant_areas_list.sql \
		upgrade/minions.sql \
		upgrade/npc.sql \
		upgrade/npcskills.sql \
		upgrade/pet_data.sql \
		upgrade/pets_skills.sql \
		upgrade/random_spawn.sql \
		upgrade/random_spawn_loc.sql \
		upgrade/recipes.sql \
		upgrade/recitems.sql \
		upgrade/siege_door.sql \
		upgrade/skills.sql \
		upgrade/skill_learn.sql \
		upgrade/skill_spellbooks.sql \
		upgrade/skill_trees.sql \
		upgrade/skill_trees_squad.sql \
		upgrade/spawnlist.sql \
		upgrade/tournament_class_list.sql \
	; do
		echo Loading $tab ...
		mysql -h $DBHOST -u $USER --password=$PASS -D $DBNAME < $tab
done