if [ -f mysql_pass.sh ]; then
		. mysql_pass.sh
else
		. mysql_pass.sh.default
fi

mysqldump --ignore-table=${DBNAME}.game_log --ignore-table=${DBNAME}.loginserv_log --ignore-table=${DBNAME}.petitions --add-drop-table -h $DBHOST -u $USER --password=$PASS $DBNAME > l2rt_full_backup.sql

for tab in \
		login/accounts.sql \
		login/gameservers.sql \
		login/banned_ips.sql \
		login/loginserv_log.sql \
	; do
		echo Loading $tab ...
		mysql -h $DBHOST -u $USER --password=$PASS -D $DBNAME < $tab
done