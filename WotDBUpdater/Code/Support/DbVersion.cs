using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotDBUpdater.Code
{
	class DBVersion
	{
		// The current databaseversion
		public static int ExpectedNumber = 21; // <--------------------------------------- REMEMBER TO ADD DB VERSION NUMBER HERE - AND SUPPLY SQL SCRIPT BELOW

		// The upgrade scripts
		private static string UpgradeSQL(int version, ConfigData.dbType dbType)
		{
			// first define sqlscript for both mssql and sqlite for all versions
			string mssql = "";
			string sqlite = "";
			switch (version)
			{
				case 1: 
					break; // First version, no script
				case 2:                                                
					mssql=	"CREATE TABLE favListTank ( "+
							" favListId int NOT NULL, tankId int NOT NULL, sortorder int NOT NULL DEFAULT 0, " +
							" primary key (favListId, tankId), " +
							" foreign key (favListId) references favList (id), " +
							" foreign key (tankId) references tank (id) " +
							") ";
					sqlite= "CREATE TABLE favListTank ( " +
							" favListId integer NOT NULL, tankId integer NOT NULL, sortorder integer NOT NULL DEFAULT 0, " +
							" primary key (favListId, tankId), " +
							" foreign key (favListId) references favList (id), " +
							" foreign key (tankId) references tank (id) " +
							") ";
					break;
				case 3:
					mssql=	"create table columnSelection ( " +
							" id int primary key, " +
							" colType int not null, " +
							" position int not null, " +
							" colName varchar(255) not null, " +
							" name varchar(50) not null, " +
							" description varchar(2000) not null " +
							"); ";
					sqlite=	"create table columnSelection ( " +
							" id integer primary key, " +
							" colType integer not null, " +
							" position integer not null, " +
							" colName varchar(255) not null, " +
							" name varchar(50) not null, " +
							" description varchar(2000) not null " +
							"); ";
					break;
				case 4:
					mssql=	"create table columnList ( " +
							" id int identity(1,1) primary key, " +
							" colType int not null, " +
							" name varchar(50) not null, " +
							" colDefault bit not null default 0, " +
							" position int null, " +
							" sysCol bit not null default 0 " +
							"); ";
					sqlite=	"create table columnList ( " +
							" id integer primary key, " +
							" colType integer not null, " +
							" name varchar(50) not null, " +
							" colDefault bit not null default 0, " +
							" position integer null, " +
							" sysCol bit not null default 0 " +
							"); ";
					break;
				case 5:
					mssql =	"insert into columnList (colType,name,colDefault,position,sysCol) values (1,'Default Column Setup', 1, 1, 1); " +
							"insert into columnList (colType,name,colDefault,position,sysCol) values (1,'Minimalistic Column Setup', 0, 2, 1); " +
							"insert into columnList (colType,name,colDefault,position,sysCol) values (1,'All Columns', 0, 3, 1); " +
							"insert into columnList (colType,name,colDefault,position,sysCol) values (2,'Default Column Setup', 1, 1, 1); " +
							"insert into columnList (colType,name,colDefault,position,sysCol) values (2,'Minimalistic Column Setup', 0, 2, 1); " +
							"insert into columnList (colType,name,colDefault,position,sysCol) values (2,'All Columns', 0, 3, 1); ";
					sqlite = mssql;
					break;
				case 6:
					mssql = "create table columnListSelection ( " +
							" columnSelectionId int not null, " +
							" columnListId int not null, " +
							" sortorder int not null default 0, " +
							" primary key (columnSelectionId, columnListId), " +
							" foreign key (columnSelectionId) references columnSelection (id), " +
							" foreign key (columnListId) references columnList (id) " +
							") ";
					sqlite= "create table columnListSelection ( " +
							" columnSelectionId integer not null, " +
							" columnListId integer not null, " +
							" sortorder integer not null default 0, " +
							" primary key (columnSelectionId, columnListId), " +
							" foreign key (columnSelectionId) references columnSelection (id), " +
							" foreign key (columnListId) references columnList (id) " +
							") ";
					break;
				case 7:
					mssql=	"ALTER TABLE columnSelection ADD colGroup varchar(50) NULL; ";
					sqlite=mssql;
					break;
				case 8:
					mssql=	"ALTER TABLE columnSelection ADD colWidth int NOT NULL default 70; ";
					sqlite=	"ALTER TABLE columnSelection ADD colWidth integer NOT NULL default 70; ";;
					break;
				case 9:
					break; // Replaced by upgrade 19
				case 10:
					mssql = "CREATE TABLE playerTankBattle( " +
							"	id int IDENTITY(1,1) primary key, " +
							"	playerTankId int NOT NULL, " +
							"   battleMode varchar(50) NOT NULL, " +
							"	battles int NOT NULL default 0, " +
							"	battles8p int NOT NULL default 0, " +
							"	wins int NOT NULL default 0, " +
							"	losses int NOT NULL default 0, " +
							"	survived int NOT NULL default 0, " +
							"	frags int NOT NULL default 0, " +
							"	frags8p int NOT NULL default 0, " +
							"	dmg int NOT NULL default 0, " +
							"	dmgReceived int NOT NULL default 0, " +
							"	assistSpot int NOT NULL default 0, " +
							"	assistTrack int NOT NULL default 0, " +
							"	cap int NOT NULL default 0, " +
							"	def int NOT NULL default 0, " +
							"	spot int NOT NULL default 0, " +
							"	xp int NOT NULL default 0, " +
							"	xp8p int NOT NULL default 0, " +
							"	xpOriginal int NOT NULL default 0, " +
							"	shots int NOT NULL default 0, " +
							"	hits int NOT NULL default 0, " +
							"	heHits int NOT NULL default 0, " +
							"	pierced int NOT NULL default 0, " +
							"	shotsReceived int NOT NULL default 0, " +
							"	piercedReceived int NOT NULL default 0, " +
							"	heHitsReceived int NOT NULL default 0, " +
							"	noDmgShotsReceived int NOT NULL default 0, " +
							"	maxDmg int NOT NULL default 0, " +
							"	maxFrags int NOT NULL default 0, " +
							"	maxXp int NOT NULL default 0, " +
							"	battlesCompany int NOT NULL default 0, " +
							"	battlesClan int NOT NULL default 0, " +
							"foreign key (playerTankId) references playerTank (id) " +
							"); ";
					sqlite = "CREATE TABLE playerTankBattle( " +
							"	id integer primary key, " +
							"	playerTankId integer NOT NULL, " +
							"   battleMode varchar(50) NOT NULL, " +
							"	battles integer NOT NULL default 0, " +
							"	battles8p integer NOT NULL default 0, " +
							"	wins integer NOT NULL default 0, " +
							"	losses integer NOT NULL default 0, " +
							"	survived integer NOT NULL default 0, " +
							"	frags integer NOT NULL default 0, " +
							"	frags8p integer NOT NULL default 0, " +
							"	dmg integer NOT NULL default 0, " +
							"	dmgReceived integer NOT NULL default 0, " +
							"	assistSpot integer NOT NULL default 0, " +
							"	assistTrack integer NOT NULL default 0, " +
							"	cap integer NOT NULL default 0, " +
							"	def integer NOT NULL default 0, " +
							"	spot integer NOT NULL default 0, " +
							"	xp integer NOT NULL default 0, " +
							"	xp8p integer NOT NULL default 0, " +
							"	xpOriginal integer NOT NULL default 0, " +
							"	shots integer NOT NULL default 0, " +
							"	hits integer NOT NULL default 0, " +
							"	heHits integer NOT NULL default 0, " +
							"	pierced integer NOT NULL default 0, " +
							"	shotsReceived integer NOT NULL default 0, " +
							"	piercedReceived integer NOT NULL default 0, " +
							"	heHitsReceived integer NOT NULL default 0, " +
							"	noDmgShotsReceived integer NOT NULL default 0, " +
							"	maxDmg integer NOT NULL default 0, " +
							"	maxFrags integer NOT NULL default 0, " +
							"	maxXp integer NOT NULL default 0, " +
							"	battlesCompany integer NOT NULL default 0, " +
							"	battlesClan integer NOT NULL default 0, " +
							"foreign key (playerTankId) references playerTank (id) " +
							"); ";
					break;
				case 11:
					mssql = "ALTER TABLE json2dbMapping ADD dbPlayerTankMode varchar(50) NULL; ";
					sqlite = mssql;
					break;
				case 12:
					mssql = "";
					sqlite = mssql;
					break;
				case 13:
					mssql = "ALTER TABLE playerTankBattle ADD wn8 int NOT NULL default 0, eff int NOT NULL default 0; ";
					sqlite= "ALTER TABLE playerTankBattle ADD wn8 integer NOT NULL default 0, eff integer NOT NULL default 0; "; ;
					break;
				case 14:
					mssql = "DROP VIEW tankData2BattleMappingView; DROP VIEW tankInfoShort; DROP VIEW PlayerTankStatsView; DROP VIEW playerTankAchAllView; ";
					sqlite = mssql;
					break;
				case 15:
					mssql = "update json2dbMapping set dbBattle='modeClan' where jsonMainSubProperty='tanks.clan.battlesCount'; " +
							"update json2dbMapping set dbBattle='modeClan' where jsonMainSubProperty='tanks_v2.clan.battlesCount'; " +
							"update json2dbMapping set dbBattle='modeCompany' where jsonMainSubProperty='tanks.company.battlesCount'; " +
							"update json2dbMapping set dbBattle='modeCompany' where jsonMainSubProperty='tanks_v2.company.battlesCount'; ";
					sqlite = mssql;
					break;
				case 16:
					mssql = "ALTER TABLE battle ADD battleMode varchar(50) NOT NULL default '15'; UPDATE battle SET battleMode='7' WHERE mode7>0 AND mode15=0; ";
					sqlite = mssql;
					break;
				case 17:
					// Insert playerTankBattles for all tanks
					DataTable dt = DB.FetchData("select * from playerTank");
					foreach (DataRow dr in dt.Rows)
					{
						Dossier2db.SaveNewPlayerTankBattle(Convert.ToInt32(dr["id"])); 
					}
					break;
				case 18:
					mssql = "ALTER TABLE playerTankBattle ADD battleOfTotal float NOT NULL default 0; ";
					sqlite = "ALTER TABLE playerTankBattle ADD battleOfTotal real NOT NULL default 0; ";
					break;
				case 19:
					mssql = "DELETE FROM columnListSelection; DELETE FROM columnSelection; ALTER TABLE columnSelection ADD colDataType varchar(50) NOT NULL; ";
					sqlite = mssql;
					break;
				case 20:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (1, 1, 1, 'tank.name', 'Tank', 'Tank name', 'Tank', 120, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (10, 2, 12, 'battleResult.name', 'Result', 'The result for battle (Victory, Draw, Defeat or Several if a combination occur when recorded several battles for one row) ', 'Result', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (11, 2, 16, 'battleSurvive.name', 'Survived', 'If survived in battle (Yes / No or Several if a combination occur when recorded several battles for one row)', 'Result', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (12, 1, 2, 'tank.tier', 'Tier', 'Tank tier (1-10)', 'Tank', 35, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (128, 1, 29, 'SUM(playerTankBattle.dmg)', 'Damage', 'Damge made on enemy tanks', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (129, 1, 30, 'SUM(playerTankBattle.assistSpot)', 'Damage Spot', 'Damage to enem tanks done by others after you spotted them', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (13, 2, 13, 'battle.victory', 'Victory', 'Number of victory battles for this row (normally 0/1, or more if battle result is Several)', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (130, 1, 31, 'SUM(playerTankBattle.assistTrack)', 'Damage Track', 'Damage to enem tanks done by others after you tracked them', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (131, 1, 33, 'SUM(playerTankBattle.frags)', 'Frags', 'Number of enemy tanks killed', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (132, 1, 32, 'SUM(playerTankBattle.dmgReceived)', 'Received Damage', 'Received Damage from enemy tanks', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (133, 1, 34, 'SUM(playerTankBattle.frags8p)', 'Frags pre 8.8', 'Number of enemy tanks killed before version 8.8', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (134, 1, 35, 'SUM(playerTankBattle.cap)', 'Cap', 'Capping points', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (135, 1, 36, 'SUM(playerTankBattle.def)', 'Def', 'Defence ponts caused by you reducing enemy cap', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (136, 1, 37, 'SUM(playerTankBattle.spot)', 'Spot', 'Enemy tanks spotted', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (137, 1, 38, 'SUM(playerTankBattle.xp)', 'XP Total', 'Total base XP earned, not included 50% extra for wins or 2X (or more) for first battle or events ', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (138, 1, 39, 'SUM(playerTankBattle.xp8p)', 'XP Total pre 8.8', 'Total base XP pre version 8.8', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (139, 1, 40, 'SUM(playerTankBattle.xpOriginal)', 'XP Total Original', 'Total Original XP, unknown parameter????', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (14, 2, 14, 'battle.draw', 'Draw', 'Number of drawed battles for this row (normally 0/1, or more if battle result is Several)', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (140, 1, 41, 'CAST(SUM(playerTankBattle.xp/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) AS INT)', 'XP Avg', 'Average base XP earned per battle', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (141, 1, 42, 'SUM(playerTankBattle.shots)', 'Shots Total', 'Total shots fired by you', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (142, 1, 43, 'SUM(playerTankBattle.hits)', 'Hits Total', 'Total hits on enemy tanks', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (143, 1, 44, 'SUM(playerTankBattle.heHits)', 'HE Hits Total', 'Total HE Hits on enemy tanks', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (144, 1, 45, 'SUM(playerTankBattle.pierced)', 'Pierced', 'Total pierced shots on enemy tanks', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (145, 1, 46, 'ROUND(SUM(playerTankBattle.hits*1000/nullif(playerTankBattle.shots,0)*playerTankBattle.battleOfTotal)  / 10,1)', 'Hit Rate', 'Hits in persentage of shots', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (146, 1, 47, 'ROUND(SUM(playerTankBattle.shots*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal)  / 10,1)', 'Shots Avg', 'Average shots fired by you per battle', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (147, 1, 48, 'ROUND(SUM(playerTankBattle.hits*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) /10,1)', 'Hits Avg', 'Average Hits on enemy tanks', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (148, 1, 49, 'ROUND(SUM(playerTankBattle.heHits*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) /10,1)', 'HE Hits Avg', 'Average HE Hits on enemy tanks', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (149, 1, 50, 'ROUND(SUM(playerTankBattle.pierced*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) /10,1)', 'Pierced Avg', 'Average pierced shots on enemy tanks', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (15, 2, 15, 'battle.defeat', 'Defeat', 'Number of defeated battles for this row (normally 0/1, or more if battle result is Several)', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (150, 1, 51, 'SUM(playerTankBattle.shotsReceived)', 'Received Shots', 'Received shots from enemy tanks,  including bounces', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (151, 1, 52, 'SUM(playerTankBattle.piercedReceived)', 'Received Pierced', 'Received pierced shots', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (152, 1, 53, 'SUM(playerTankBattle.heHitsReceived)', 'Received HE Hits', 'Regeived hits, not counting bounces', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (153, 1, 54, 'SUM(playerTankBattle.noDmgShotsReceived)', 'Received No Dmg Shots', 'Received shots not damaging your tank', 'Shots', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (154, 1, 55, 'MAX(playerTankBattle.maxDmg)', 'Max Damage', 'Max damage achived in a single battle', 'Max', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (155, 1, 56, 'MAX(playerTankBattle.maxFrags)', 'Max Frags', 'Max frags in a single battle', 'Max', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (156, 1, 57, 'MAX(playerTankBattle.maxXp)', 'Max XP', 'Max XP earned in a single battle', 'Max', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (16, 2, 17, 'battle.survived', 'Survived Count', 'Number of battles where survived for this row (normally 0/1, or more if battle result is Several)', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (17, 2, 18, 'battle.killed', 'Killed Count', 'Number of battles where killed (not survived) for this row (normally 0/1, or more if battle result is Several)', 'Result', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (18, 2, 19, 'battle.frags', 'Frags', 'Number of enemy tanks you killed (frags)', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (19, 2, 20, 'battle.dmg', 'Damage', 'Damage to enemy tanks by you (shooting, ramming, put on fire)', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (2, 2, 3, 'tank.premium', 'Premium', 'Tank premium (yes/no)', 'Tank', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (20, 2, 23, 'battle.dmgReceived', 'Damage Received', 'The damage received on your tank', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (21, 2, 21, 'battle.assistSpot', 'Damage Spotting', 'Assisted damage casued by others to enemy tanks due to you spotting the enemy tank', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (22, 2, 22, 'battle.assistTrack', 'Damgae Tracking', 'Assisted damage casued by others to enemy tanks due to you tracking of the enemy tank', 'Damage', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (23, 1, 3, 'tank.premium', 'Premium', 'Tank premium (yes/no)', 'Tank', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (24, 2, 32, 'battle.cap', 'Cap', 'Cap ponts you achived by staying in cap circle (0 - 100)', 'Other', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (25, 2, 33, 'battle.def', 'Defense', 'Cap points reduced by damaging enemy tanks capping', 'Other', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (26, 2, 24, 'battle.shots', 'Shots', 'Number of shots you fired', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (27, 2, 25, 'battle.hits', 'Hits', 'Number of hits from you shots', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (28, 2, 26, 'battle.hits * 100 / nullif(battle.shots,0)', 'Hits %', 'Persentage hits (hits*100/shots)', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (29, 2, 30, 'battle.shotsReceived', 'Shots Reveived', 'Number of shots received ', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (3, 2, 4, 'tankType.name', 'Tank Type', 'Tank type full name', 'Tank', 100, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (30, 2, 27, 'battle.pierced', 'Pierced', 'Number of pierced shots', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (31, 2, 31, 'battle.piercedReceived', 'Pierced Received', 'Number of pierced shots received ', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (32, 2, 28, 'battle.pierced * 100 / nullif(battle.shots)', 'Pierced Shots %', 'Persentage pierced hits based on total shots (pierced*100/shots)', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (33, 2, 29, 'battle.pierced * 100 / nullif(battle.hits)', 'Pierced Hts %', 'Persentage pierced hits based on total hits (pierced*100/hits)', 'Shooting', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (34, 1, 4, 'tankType.name', 'Tank Type', 'Tank type full name', 'Tank', 100, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (35, 2, 34, 'battle.spotted', 'Spotted', 'Enemy tanks spotted (only first spot on enemy tank counts)', 'Other', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (36, 2, 35, 'battle.mileage', 'Mileage', 'Distance driving the tank', 'Other', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (37, 2, 36, 'battle.treesCut', 'Trees Cut', 'Number of trees overturned by driving into it', 'Other', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (38, 2, 37, 'battle.xp', 'XP', 'Default XP earned, 50% extra for victory or 2X (or more) for first victory or campaign not included', 'Rating', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (39, 2, 8, 'tank.id', 'ID', 'Wargaming ID for tank', 'Tank', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (4, 2, 5, 'tankType.shortName', 'Type', 'Tank type short name (LT, MT, HT, TD, SPG)', 'Tank', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (40, 2, 38, 'battle.eff', 'EFF', 'Calculated battle efficiency rating', 'Rating', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (41, 2, 40, 'battle.mode15', '15x15', 'Number of 15x15 battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (42, 2, 41, 'battle.mode7', '7x7', 'Number of 7x7 battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (43, 2, 42, 'battle.modeCla', 'Cla', 'Number of Clan battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (44, 1, 5, 'tankType.shortName', 'Type', 'Tank type short name (LT, MT, HT, TD, SPG)', 'Tank', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (45, 2, 43, 'battle.modeCompany', 'Company', 'Number of Tank Company battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (46, 1, 8, 'tank.id', 'ID', 'Wargaming ID for tank', 'Tank', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (47, 2, 39, 'battle.wn8', 'WN8', 'Calculated battle WN8 (WRx) rating (according to formula from vbAddict)', 'Rating', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (48, 1, 16, 'CAST(SUM(playerTankBattle.eff*playerTankBattle.battleOfTotal) AS INT)', 'EFF', 'Calculated battle efficiency rating', 'Rating', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (49, 1, 17, 'CAST(SUM(playerTankBattle.wn8*playerTankBattle.battleOfTotal) AS INT)', 'WN8', 'Calculated battle WN8 (WRx) rating (according to formula from vbAddict)', 'Rating', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (5, 2, 6, 'country.name', 'Tank Nation', 'Tank nation full name', 'Tank', 100, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (50, 1, 20, 'SUM(playerTankBattle.battles)', 'Battles', 'Battle count', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (51, 1, 6, 'country.name', 'Tank Nation', 'Tank nation full name', 'Tank', 100, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (52, 1, 19, 'playerTank.battleLifeTime', 'Life Time', 'Total battle life time in seconds', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (53, 1, 15, 'playerTank.markOfMastery', 'Mastery Badge', 'Mastery Badge achived (0=None, 1=Ace Tanker, 2=I Class, 3=II Class, 4=III Class)', 'Rating', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (54, 1, 18, 'playerTank.lastBattleTime', 'Last Battle', 'Last battle time', 'Battle', 100, 'DateTime'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (55, 1, 11, 'playerTank.has15', '15x15', 'Used in 15x15 battles (0 = No, 1 = yes)', 'Tank', 35, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (56, 1, 14, 'playerTank.hasCla', 'Clan Wars', 'Used in clan wars (0 = No, 1 = yes)', 'Tank', 35, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (57, 1, 7, 'country.shortName', 'Nation', 'Tank nation short name (CHI, FRA, GET, JAP, UK, USA, USR)', 'Tank', 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (58, 2, 1, 'tank.name', 'Tank', 'Tank name', 'Tank', 120, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (59, 2, 2, 'tank.tier', 'Tier', 'Tank tier (1-10)', 'Tank', 35, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (6, 2, 7, 'country.shortName', 'Nation', 'Tank nation short name (CHI, FRA, GET, JAP, UK, USA, USR)', NULL, 50, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (60, 1, 13, 'playerTank.hasCompany', 'Company', 'Used in company battle (0 = No, 1 = yes)', 'Tank', 35, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (61, 1, 12, 'playerTank.has7', '7x7', 'Used in 7x7 battles (0 = No, 1 = yes)', 'Tank', 35, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (63, 1, 9, 'playerTank.mileage', 'Mileage', 'Total drive distance for tank, not dependent on battle mode', 'Tank', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (64, 1, 10, 'playerTank.treesCut', 'Trees Cut', 'Total tree cuts for tank, not dependent on battle mode', 'Tank', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (65, 1, 65, 'playerTank.eqBino', 'Binocular', 'If Binocular Telescope equipment is mounted', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (66, 1, 58, 'playerTank.eqCoated', 'Coated Optics', 'If Coated Optics equipment is mounted', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (67, 1, 59, 'playerTank.eqCamo', 'Comoflage', 'If Camoflage equipment is mounted', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (68, 1, 60, 'playerTank.equVent', 'Ventialtio', 'If Ventilation equipment is mounted', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (69, 1, 61, 'playerTank.skillReco', 'Reco', 'Level of Recon skill value (prosentage) achivived for crew', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (7, 2, 9, 'battle.battlesCount', 'Battles', 'Battle count, number of battles for the row', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (70, 1, 62, 'playerTank.skillAwareness', 'Awareness', 'Level of Awareness skill value (prosentage) achivived for crew', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (71, 1, 63, 'playerTank.skillCamo', 'Camo', 'Camo skill value (prosentage) achivived for crew', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (72, 1, 64, 'playerTank.skillBia', 'B.I.A.', 'If Brothers In Arms skill achivived for crew', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (73, 1, 65, 'playerTank.premiumCons', 'VR Cons', 'Premium Consumable affecting view rate is used', 'Equip/Crew', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (74, 1, 67, 'modRadio.name', 'Radio', 'Radio mounted on tank', 'Module', 120, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (75, 1, 68, 'modRadio.signalRange', 'Radio Range', 'Radio signal range ', 'Module', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (77, 1, 69, 'modTurret.name', 'Turret', 'Turret mounted on tank', 'Module', 120, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (78, 1, 50, 'modTurret.viewRange', 'Turret View Range', 'Turret view range', 'Module', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (79, 1, 71, 'modTurret.armourFront', 'Turret Front Armour', 'Turret front armour', 'Module', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (8, 2, 10, 'battleTime', 'Time', 'Battle time, the date/time the battle was finished', 'Battle', 100, 'DateTime'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (80, 1, 72, 'modTurret.armourSide', 'Turret Side Armour', 'Turret side armour', 'Module', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (81, 1, 73, 'modTurret.armourRear', 'Turret Side Armour', 'Turret rear armour', 'Module', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (83, 1, 74, 'motGun.name', 'Gun', 'Gun mounted on tank', 'Module', 120, 'VarChar'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (84, 1, 75, 'modGun.tier', 'Gun Tier', 'Gun tier', 'Module', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (85, 1, 21, 'SUM(playerTankBattle.battles8p)', 'Battles pre 8.8', 'Battle count performed before WoT version 8.8', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (86, 1, 22, 'SUM(playerTankBattle.wins)', 'Victory', 'Victory count', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (9, 2, 11, 'battleLifeTime', 'Life Time', 'Time staying alive in battle in seconds', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (91, 1, 23, 'SUM(playerTankBattle.battles-playerTankBattle.wins-playerTankBattle.losses)', 'Draw', 'Draw count', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (92, 1, 24, 'SUM(playerTankBattle.losses)', 'Defeat', 'Defeat count', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (95, 1, 25, 'CAST(SUM(playerTankBattle.wins*100/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) AS INT)', 'Win Rate', 'Win rate in percent of tank total battles', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (96, 1, 26, 'SUM(playerTankBattle.survived)', 'Survived', 'Survived count', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (97, 1, 27, 'SUM(playerTankBattle.battles-playerTankBattle.survived)', 'Killed', 'Killed count (not survived)', 'Battle', 50, 'Int'); " + 
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (98, 1, 28, 'CAST(SUM(playerTankBattle.survived*100/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) AS INT)', 'Survived Rate', 'Survived in percent of tank total battles', 'Battle', 50, 'Int'); " ;
					sqlite = mssql;
					break;
				case 21:
					mssql = "INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (12,1,1) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (1,1,2) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (44,1,3) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (57,1,4) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (54,1,5) ; " +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (50,1,6) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (95,1,7) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (154,1,8) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (155,1,9) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (156,1,10) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (53,1,11) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (48,1,12) ;" +
							"INSERT INTO columnListSelection (columnSelectionId,columnListId,sortorder) VALUES (49,1,13) ;";
					sqlite = mssql;
					break;
				default:
					break;
			}
			string sql = "";
			// get sql for correct dbtype
			if (dbType == ConfigData.dbType.MSSQLserver) 
				sql = mssql;
			else if (dbType == ConfigData.dbType.SQLite) 
				sql = sqlite;
			// return sql
			return sql;
		}

		// Procedure upgrading DB to latest version
		public static bool CheckForDbUpgrade()
		{
			bool upgradeOK = true;
			int DBVersionCurrentNumber = CurrentNumber(); // Get current DB version
			if (DBVersionCurrentNumber == 0) return false; // Quit verison check if no db version could be found
			if (DBVersionCurrentNumber == ExpectedNumber) return true; // Quit version check when expected version is found, everything is OK!
			if (DBVersionCurrentNumber < ExpectedNumber)
			{
				// Loop through all versions missing, as long as current db version < expected version
				bool continueNext = true;
				while (DBVersionCurrentNumber < ExpectedNumber && continueNext)
				{
					// Move to next upgrade number
					DBVersionCurrentNumber++;
					// Upgrade to next db version now
					string sql = UpgradeSQL(DBVersionCurrentNumber, Config.Settings.databaseType); // Get upgrade script for this version and dbType 
					continueNext = DB.ExecuteNonQuery(sql); // Run upgrade script
					// Update db _version_ if success
					if (continueNext)
					{
						sql = "update _version_ set version=" + DBVersionCurrentNumber.ToString();
						continueNext = DB.ExecuteNonQuery(sql);
					}
				}
				// If anything went wrong (continueNext == false), supply error notification here
				if (!continueNext)
					Code.MsgBox.Show("Error occured during database upgrade, failed running SQL script for version: " + DBVersionCurrentNumber.ToString("0000"), "Error Upgrading Database");
				upgradeOK = continueNext;
				
			}
			return upgradeOK;
		}

		// Returns database current version, on first run version table is created and version = 1
		public static int CurrentNumber()
		{
			int version = 0;
			string sql = "";
			bool versionTableFound = false;
			// List tables
			DataTable dt = DB.ListTables();
			if (dt.Rows.Count > 0)
			{
				// Check if _version_ table containing db version number exists
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["TABLE_NAME"].ToString() == "_version_")
					{
						versionTableFound = true;
						break;
					}
				}
				// if _version_ table not exist create it
				if (!versionTableFound)
				{
					if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
						sql = "create table _version_ (id integer primary key, version integer not null); ";
					else if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
						sql = "create table _version_ (id int primary key, version int not null); ";
					bool createTableOK = DB.ExecuteNonQuery(sql); // Create _version_ table now
					if (!createTableOK)
						return 0; // Error occured creating _version_ table
					else
					{
						// Add initial version
						sql = "insert into _version_ (id, version) values (1,1); ";
						bool insertVersionOK = DB.ExecuteNonQuery(sql);
						if (!insertVersionOK)
							return 0; // Error occured inserting version number in _version_ table
					}
				}
				// Get version now
				sql = "select version from _version_ where id=1; ";
				dt.Dispose();
				dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					version = Convert.ToInt32(dt.Rows[0][0]);
				}
			}
			return version;
		}
	}
}
