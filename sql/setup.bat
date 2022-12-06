@echo off
TITLE L2 Revolt Team Setup
REM ######################################## Automatic updater for L2 Revolt Team - Do not edit !!!
goto answer%ERRORLEVEL%
:answerTrue
set fastend=yes
goto upgrade_db
:answer0
set fastend=no

set user=root
set pass=
set DBname=nuboTeam
set DBHost=localhost

set Generaltables=accounts augmentations clanhall gameservers banned_ips loginserv_log character_friends character_hennas character_macroses character_quests character_recipebook character_shortcuts character_skills character_effects_save character_skills_save character_subclasses characters character_variables clanhall_bids clanhall_data clan_data clanhall_decorations_bids ally_data clan_wars items pets server_variables seven_signs seven_signs_festival siege_clans killcount dropcount craftcount game_log petitions seven_signs_status global_tasks raidboss_status manor_crop manor_seeds hellbound multisell_log character_vote
set Ignore=--ignore-table=%DBname%.game_log --ignore-table=%DBname%.loginserv_log --ignore-table=%DBname%.petitions

REM ########################################
mysql.exe -h %DBHost% -u %user% --password=%pass% --execute="CREATE DATABASE IF NOT EXISTS %DBname%"
if not exist backup (
mkdir backup
)

REM ######################################## :main_menu
:main_menu
cls
echo.L2 Revolt Team Setup
echo.
echo.### Main Menu ###
echo.
echo.(1) Install Login Server
echo.(2) Install Game Server
echo.(3) Upgrade DB
echo.(4) Backup DB
echo.(5) Restore DB
echo.(6) Lost data in DB
echo.(7) Install option data
echo.(q) Quit
echo.
set button=x
set /p button=What do you want ?:
if /i %button%==1 goto Install_Login_Server_menu
if /i %button%==2 goto Install_Game_Server_menu
if /i %button%==3 goto upgrade_menu
if /i %button%==4 goto backup_menu
if /i %button%==5 goto restore_menu
if /i %button%==6 goto lost_data_menu
if /i %button%==7 goto install_option_data
if /i %button%==q goto end
goto main_menu

REM ######################################## :Install_Login_Server_menu
:Install_Login_Server_menu
cls
echo.L2 Revolt Team Setup
echo.
echo.### Install Login Server ###
echo.
echo.(i) Install Login Server. Warning !!! accounts, gameservers, banned_ips will be deleted !!!
echo.(m) Main menu
echo.(q) Quit
echo.
set button=x
set /p button=Your choice ?:
if /i %button%==i goto Install_Login_Server
if /i %button%==m goto main_menu
if /i %button%==q goto end
goto Install_Login_Server_menu

REM ######################################## :Install_Game_Server_menu
:Install_Game_Server_menu
cls
echo.L2 Revolt Team Setup
echo.
echo.### Install Game Server ###
echo.
echo.(i) Install Game Server. Warning !!! All Game Server Database will be deleted !!!
echo.(m) Main menu
echo.(q) Quit
echo.
set button=x
set /p button=Your choice ?:
if /i %button%==i goto Install_Game_Server
if /i %button%==m goto main_menu
if /i %button%==q goto end
goto Install_Game_Server_menu

REM ######################################## :upgrade_menu
:upgrade_menu
cls
echo.L2 Revolt Team Setup
echo.
echo.### Upgrade Menu ###
echo.
echo.(u) Upgrade Database
echo.(m) Main menu
echo.(q) Quit
echo.
set button=x
set /p button=Your choice ?:
if /i %button%==u goto upgrade_db
if /i %button%==m goto main_menu
if /i %button%==q goto end
goto upgrade_menu

REM ######################################## :backup_menu
:backup_menu
cls
echo.L2 Revolt Team Setup
echo.
echo.### Backup Menu ###
echo.
echo.(1) Full backup
echo.(2) General tables backup only
echo.(m) Main menu
echo.(q) Quit
echo.
set button=x
set /p button=Select backup type ?:
if /i %button%==1 goto full_backup
if /i %button%==2 goto general_backup
if /i %button%==m goto setup
if /i %button%==q goto end
goto backup_menu

REM ######################################## :restore_menu
:restore_menu
cls
echo.List all files in dir "/backup" !
echo.
dir backup /B /P
echo.
echo.L2 Revolt Team Setup
echo.
echo.### Restore Menu ###
echo.
echo.Enter a full filename do you want to restore to the database !
echo.(m) Main menu
echo.(q) Quit
echo.
set filename=x
set /p filename=Enter filename ?:
if /i %filename%==m goto main_menu
if /i %filename%==q goto end
if /i %filename%==%filename% goto restore_DB
goto restore_menu

REM ######################################## :lost_data_menu
:lost_data_menu
cls
echo.L2 Revolt Team Setup
echo.
echo.### Lost Data Menu ###
echo.
echo.(1) Show lost data
echo.(2) Delete lost data
echo.(m) Main menu
echo.(q) Quit
echo.
set button=x
set /p button=Select backup type ?:
if /i %button%==1 goto show_lost_data
if /i %button%==2 goto delete_lost_data
if /i %button%==m goto main_menu
if /i %button%==q goto end
goto lost_data_menu

:show_lost_data
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < tools/maintenance/lost_data_show.sql
pause
goto lost_data_menu

:delete_lost_data
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < tools/maintenance/lost_data_del.sql
echo.
echo.All lost data deleted !!!
echo.
pause
goto lost_data_menu

REM ######################################## :install_option_data
:install_option_data
cls
echo.L2 Revolt Team Setup
echo.
echo.### Install_option_data ###
echo.
echo.(1) Install TeleToGH SQL patch
echo.(m) Main menu
echo.(q) Quit
echo.
set button=x
set /p button=Your choice ?:
if /i %button%==1 goto Install_TeleToGH
if /i %button%==m goto main_menu
if /i %button%==q goto end
goto Install_TeleToGH

REM ######################################## :Install_TeleToGH
:Install_TeleToGH
echo.Installing TeleToGH SQL patch !!!
echo.
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < optional/teletogh/patch.sql
echo.
echo.TeleToGH SQL patch Installed !!!
echo.
pause
goto main_menu

REM ######################################## :Install_Login_Server
:Install_Login_Server
set ctime=%TIME:~0,2%
if "%ctime:~0,1%" == " " (
set ctime=0%ctime:~1,1%
)
set ctime=%ctime%.%TIME:~3,2%.%TIME:~6,2%
echo.
echo Making a full backup into %DATE%-%ctime%_backup_full.sql
echo.
mysqldump.exe %Ignore% --add-drop-table -h %DBHost% -u %user% --password=%pass% %DBname% > backup/%DATE%-%ctime%_backup_full.sql
echo.
echo.Installing Login Server !!!
echo.
echo Loading Table: accounts
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < login/accounts.sql
echo Loading Table: gameservers
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < login/gameservers.sql
echo Loading Table: banned_ips
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < login/banned_ips.sql
echo Loading Table: loginserv_log
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < login/loginserv_log.sql
echo.
echo.Login Server Installed !!!
echo.
pause
goto main_menu

REM ######################################## :Install_Game_Server
:Install_Game_Server
set ctime=%TIME:~0,2%
if "%ctime:~0,1%" == " " (
set ctime=0%ctime:~1,1%
)
set ctime=%ctime%.%TIME:~3,2%.%TIME:~6,2%
echo.
echo Making a full backup into %DATE%-%ctime%_backup_full.sql
echo.
mysqldump.exe %Ignore% --add-drop-table -h %DBHost% -u %user% --password=%pass% %DBname% > backup/%DATE%-%ctime%_backup_full.sql
echo.
echo.Installing general tables !!!
echo.
echo Loading Table: ally_data
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/ally_data.sql
echo Loading Table: auction
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/auction.sql
echo Loading Table: auction_bid
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/auction_bid.sql
echo Loading Table: auction_bids
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/auction_bids.sql
echo Loading Table: auction_lots
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/auction_lots.sql
echo Loading Table: bans
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/bans.sql
echo Loading Table: bonus
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/bonus.sql
echo Loading Table: castle
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/castle.sql
echo Loading Table: castle_manor_procure
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/castle_manor_procure.sql
echo Loading Table: castle_manor_production
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/castle_manor_production.sql
echo Loading Table: character_blocklist
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_blocklist.sql
echo Loading Table: character_contacts
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_contacts.sql
echo Loading Table: character_tpbookmark
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_tpbookmark.sql
echo Loading Table: character_effects_save
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_effects_save.sql
echo Loading Table: character_friends
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_friends.sql
echo Loading Table: character_hennas
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_hennas.sql
echo Loading Table: character_l2top_votes
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_l2top_votes.sql
echo Loading Table: character_macroses
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_macroses.sql
echo Loading Table: character_mail
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_mail.sql
echo Loading Table: character_mmotop_votes
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_mmotop_votes.sql
echo Loading Table: character_quests
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_quests.sql
echo Loading Table: character_recipebook
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_recipebook.sql
echo Loading Table: character_shortcuts
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_shortcuts.sql
echo Loading Table: character_skills
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_skills.sql
echo Loading Table: character_skills_save
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_skills_save.sql
echo Loading Table: character_subclasses
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_subclasses.sql
echo Loading Table: character_variables
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/character_variables.sql
echo Loading Table: characters
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/characters.sql
echo Loading Table: clan_data
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clan_data.sql
echo Loading Table: clan_notices
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clan_notices.sql
echo Loading Table: clan_privs
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clan_privs.sql
echo Loading Table: clan_skills
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clan_skills.sql
echo Loading Table: clan_subpledges
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clan_subpledges.sql
echo Loading Table: clan_wars
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clan_wars.sql
echo Loading Table: clanhall
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/clanhall.sql
echo Loading Table: community_skillsave
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/community_skillsave.sql
echo Loading Table: communitybuff
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/communitybuff.sql
echo Loading Table: comteleport
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/comteleport.sql
echo Loading Table: couples
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/couples.sql
echo Loading Table: craftcount
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/craftcount.sql
echo Loading Table: cursed_weapons
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/cursed_weapons.sql
echo Loading Table: dropcount
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/dropcount.sql
echo Loading Table: epic_boss_spawn
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/epic_boss_spawn.sql
echo Loading Table: fishing_championship
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/fishing_championship.sql
echo Loading Table: fishing_championship_date
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/fishing_championship_date.sql
echo Loading Table: forts
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/forts.sql
echo Loading Table: forums
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/forums.sql
echo Loading Table: game_log
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/game_log.sql
echo Loading Table: games
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/games.sql
echo Loading Table: global_tasks
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/global_tasks.sql
echo Loading Table: hellbound
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/hellbound.sql
echo Loading Table: heroes
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/heroes.sql
echo Loading Table: item_attributes
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/item_attributes.sql
echo Loading Table: item_auction
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/item_auction.sql
echo Loading Table: item_auction_bid
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/item_auction_bid.sql
echo Loading Table: items
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/items.sql
echo Loading Table: items_delayed
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/items_delayed.sql
echo Loading Table: killcount
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/killcount.sql
echo Loading Table: mail
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/mail.sql
echo Loading Table: mail_attachments
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/mail_attachments.sql
echo Loading Table: multisell_log
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/multisell_log.sql
echo Loading Table: olympiad_nobles
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/olympiad_nobles.sql
echo Loading Table: petitions
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/petitions.sql
echo Loading Table: pets
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/pets.sql
echo Loading Table: posts
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/posts.sql
echo Loading Table: raidboss_points
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/raidboss_points.sql
echo Loading Table: raidboss_status
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/raidboss_status.sql
echo Loading Table: residence_functions
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/residence_functions.sql
echo Loading Table: server_variables
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/server_variables.sql
echo Loading Table: seven_signs
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/seven_signs.sql
echo Loading Table: seven_signs_festival
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/seven_signs_festival.sql
echo Loading Table: seven_signs_status
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/seven_signs_status.sql
echo Loading Table: siege_clans
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/siege_clans.sql
echo Loading Table: siege_doorupgrade
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/siege_doorupgrade.sql
echo Loading Table: siege_guards
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/siege_guards.sql
echo Loading Table: siege_territory_members
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/siege_territory_members.sql
echo Loading Table: summon_effects_save
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/summon_effects_save.sql
echo Loading Table: topic
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/topic.sql
echo Loading Table: tournament_table
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/tournament_table.sql
echo Loading Table: tournament_teams
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/tournament_teams.sql
echo Loading Table: tournament_variables
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/tournament_variables.sql
echo Loading Table: vote
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/vote.sql
echo Loading Table: mercenaries_kills
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/mercenaries_kills.sql
echo Loading Table: mercenaries_orders
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/mercenaries_orders.sql
echo Loading Table: mercenaries_rewards
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < install/mercenaries_rewards.sql

goto upgrade_db

REM ######################################## :upgrade_db
:upgrade_db
echo.
echo Upgrading tables !!!
echo.
echo Loading Table: ai_params
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/ai_params.sql
echo Loading Table: auto_chat
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/auto_chat.sql
echo Loading Table: auto_chat_text
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/auto_chat_text.sql
echo Loading Table: char_templates
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/char_templates.sql
echo Loading Table: class_list
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/class_list.sql
echo Loading Table: community_academy
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/community_academy.sql
echo Loading Table: doors
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/doors.sql
echo Loading Table: droplist
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/droplist.sql
echo Loading Table: four_sepulchers_spawnlist
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/four_sepulchers_spawnlist.sql
echo Loading Table: henna
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/henna.sql
echo Loading Table: henna_trees
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/henna_trees.sql
echo Loading Table: item_mall
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/item_mall.sql
echo Loading Table: itemall
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/itemall.sql
echo Loading Table: lastimperialtomb_spawnlist
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/lastimperialtomb_spawnlist.sql
echo Loading Table: locations
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/locations.sql
echo Loading Table: lvlupgain
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/lvlupgain.sql
echo Loading Table: mapregion
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/mapregion.sql
echo Loading Table: merchant_areas_list
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/merchant_areas_list.sql
echo Loading Table: minions
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/minions.sql
echo Loading Table: npc
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/npc.sql
echo Loading Table: npcskills
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/npcskills.sql
echo Loading Table: pet_data
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/pet_data.sql
echo Loading Table: pets_skills
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/pets_skills.sql
echo Loading Table: random_spawn
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/random_spawn.sql
echo Loading Table: random_spawn_loc
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/random_spawn_loc.sql
echo Loading Table: recipes
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/recipes.sql
echo Loading Table: recitems
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/recitems.sql
echo Loading Table: siege_door
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/siege_door.sql
echo Loading Table: skills
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/skills.sql
echo Loading Table: skill_learn
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/skill_learn.sql
echo Loading Table: skill_spellbooks
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/skill_spellbooks.sql
echo Loading Table: skill_trees
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/skill_trees.sql
echo Loading Table: skill_trees_squad
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/skill_trees_squad.sql
echo Loading Table: spawnlist
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/spawnlist.sql
echo Loading Table: tournament_class_list
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < upgrade/tournament_class_list.sql

echo.
echo.Complete !!!
echo.
if /I %fastend%==yes goto :EOF
pause
goto main_menu

REM ######################################## :full_backup
:full_backup
set ctime=%TIME:~0,2%
if "%ctime:~0,1%" == " " (
set ctime=0%ctime:~1,1%
)
set ctime=%ctime%.%TIME:~3,2%.%TIME:~6,2%
echo.
echo Making a full backup into %DATE%-%ctime%_backup_full.sql
echo.
mysqldump.exe %Ignore% --add-drop-table -h %DBHost% -u %user% --password=%pass% %DBname% > backup/%DATE%-%ctime%_backup_full.sql
goto end

REM ######################################## :general_backup
:general_backup
set ctime=%TIME:~0,2%
if "%ctime:~0,1%" == " " (
set ctime=0%ctime:~1,1%
)
set ctime=%ctime%.%TIME:~3,2%.%TIME:~6,2%
echo.
echo Making a general tables backup into %DATE%-%ctime%_backup_general.sql
echo.
mysqldump.exe %Ignore% --add-drop-table -h %DBHost% -u %user% --password=%pass% %DBname% %Generaltables% > backup/%DATE%-%ctime%_backup_general.sql
goto end

REM ######################################## :restore_DB
:restore_DB
if not exist backup/%filename% (
echo.
echo.File not found !
echo.
pause
goto restore_menu
)
cls
echo.
echo.Restore from file %filename% !
echo.
pause
mysql.exe -h %DBHost% -u %user% --password=%pass% -D %DBname% < backup/%filename%
goto end

REM ######################################## :not_working_now
:not_working_now
echo.
echo Not working NOW !!!
echo.
pause
goto main_menu

REM ######################################## :end
:end
