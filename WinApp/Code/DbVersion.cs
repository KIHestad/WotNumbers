using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
	class DBVersion
	{
		// The current databaseversion
		public static int ExpectedNumber = 73; // <--------------------------------------- REMEMBER TO ADD DB VERSION NUMBER HERE - AND SUPPLY SQL SCRIPT BELOW

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
					mssql = "update json2dbMapping set dbPlayerTank='battlesClan', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.clan.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battlesClan', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.clan.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battlesCompany', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.company.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battlesCompany', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.company.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battles', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battles', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battles', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.battlesCount'; " +
							"update json2dbMapping set dbPlayerTank='battles8p', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.battlesCountBefore8_8'; " +
							"update json2dbMapping set dbPlayerTank='cap', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.capturePoints'; " +
							"update json2dbMapping set dbPlayerTank='cap', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.capturePoints'; " +
							"update json2dbMapping set dbPlayerTank='cap', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.capturePoints'; " +
							"update json2dbMapping set dbPlayerTank='assistSpot', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.damageAssistedRadio'; " +
							"update json2dbMapping set dbPlayerTank='assistSpot', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.damageAssistedRadio'; " +
							"update json2dbMapping set dbPlayerTank='assistTrack', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.damageAssistedTrack'; " +
							"update json2dbMapping set dbPlayerTank='assistTrack', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.damageAssistedTrack'; " +
							"update json2dbMapping set dbPlayerTank='dmg', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.damageDealt'; " +
							"update json2dbMapping set dbPlayerTank='dmg', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.damageDealt'; " +
							"update json2dbMapping set dbPlayerTank='dmg', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.damageDealt'; " +
							"update json2dbMapping set dbPlayerTank='dmgReceived', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.damageReceived'; " +
							"update json2dbMapping set dbPlayerTank='dmgReceived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.damageReceived'; " +
							"update json2dbMapping set dbPlayerTank='dmgReceived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.damageReceived'; " +
							"update json2dbMapping set dbPlayerTank='def', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.droppedCapturePoints'; " +
							"update json2dbMapping set dbPlayerTank='def', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.droppedCapturePoints'; " +
							"update json2dbMapping set dbPlayerTank='def', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.droppedCapturePoints'; " +
							"update json2dbMapping set dbPlayerTank='frags', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.frags'; " +
							"update json2dbMapping set dbPlayerTank='frags', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.frags'; " +
							"update json2dbMapping set dbPlayerTank='frags', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.frags'; " +
							"update json2dbMapping set dbPlayerTank='frags8p', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.frags8p'; " +
							"update json2dbMapping set dbPlayerTank='frags8p', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.frags8p'; " +
							"update json2dbMapping set dbPlayerTank='frags8p', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.frags8p'; " +
							"update json2dbMapping set dbPlayerTank='heHits', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.he_hits'; " +
							"update json2dbMapping set dbPlayerTank='heHits', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.he_hits'; " +
							"update json2dbMapping set dbPlayerTank='heHitsReceived', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.heHitsReceived'; " +
							"update json2dbMapping set dbPlayerTank='heHitsReceived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.heHitsReceived'; " +
							"update json2dbMapping set dbPlayerTank='hits', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.hits'; " +
							"update json2dbMapping set dbPlayerTank='hits', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.hits'; " +
							"update json2dbMapping set dbPlayerTank='hits', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.hits'; " +
							"update json2dbMapping set dbPlayerTank='losses', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.losses'; " +
							"update json2dbMapping set dbPlayerTank='losses', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.losses'; " +
							"update json2dbMapping set dbPlayerTank='losses', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.losses'; " +
							"update json2dbMapping set dbPlayerTank='maxDmg', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.max15x15.maxDamage'; " +
							"update json2dbMapping set dbPlayerTank='maxDmg', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.max7x7.maxDamage'; " +
							"update json2dbMapping set dbPlayerTank='maxFrags', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.max7x7.maxFrags'; " +
							"update json2dbMapping set dbPlayerTank='maxFrags', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.maxFrags'; " +
							"update json2dbMapping set dbPlayerTank='maxFrags', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.max15x15.maxFrags'; " +
							"update json2dbMapping set dbPlayerTank='maxXp', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.max15x15.maxXP'; " +
							"update json2dbMapping set dbPlayerTank='maxXp', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.maxXP'; " +
							"update json2dbMapping set dbPlayerTank='maxXp', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.max7x7.maxXP'; " +
							"update json2dbMapping set dbPlayerTank='noDmgShotsReceived', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.noDamageShotsReceived'; " +
							"update json2dbMapping set dbPlayerTank='noDmgShotsReceived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.noDamageShotsReceived'; " +
							"update json2dbMapping set dbPlayerTank='xpOriginal', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.originalXP'; " +
							"update json2dbMapping set dbPlayerTank='xpOriginal', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.originalXP'; " +
							"update json2dbMapping set dbPlayerTank='pierced', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.pierced'; " +
							"update json2dbMapping set dbPlayerTank='pierced', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.pierced'; " +
							"update json2dbMapping set dbPlayerTank='piercedReceived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.piercedReceived'; " +
							"update json2dbMapping set dbPlayerTank='piercedReceived', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.piercedReceived'; " +
							"update json2dbMapping set dbPlayerTank='shots', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.shots'; " +
							"update json2dbMapping set dbPlayerTank='shots', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.shots'; " +
							"update json2dbMapping set dbPlayerTank='shots', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.shots'; " +
							"update json2dbMapping set dbPlayerTank='shotsReceived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15_2.shotsReceived'; " +
							"update json2dbMapping set dbPlayerTank='shotsReceived', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.shotsReceived'; " +
							"update json2dbMapping set dbPlayerTank='spot', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.spotted'; " +
							"update json2dbMapping set dbPlayerTank='spot', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.spotted'; " +
							"update json2dbMapping set dbPlayerTank='spot', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.spotted'; " +
							"update json2dbMapping set dbPlayerTank='survived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.survivedBattles'; " +
							"update json2dbMapping set dbPlayerTank='survived', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.survivedBattles'; " +
							"update json2dbMapping set dbPlayerTank='survived', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.survivedBattles'; " +
							"update json2dbMapping set dbPlayerTank='wins', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.wins'; " +
							"update json2dbMapping set dbPlayerTank='wins', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.wins'; " +
							"update json2dbMapping set dbPlayerTank='wins', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.wins'; " +
							"update json2dbMapping set dbPlayerTank='xp', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.xp'; " +
							"update json2dbMapping set dbPlayerTank='xp', dbPlayerTankMode='15' where jsonMainSubProperty='tanks.tankdata.xp'; " +
							"update json2dbMapping set dbPlayerTank='xp', dbPlayerTankMode='7' where jsonMainSubProperty='tanks_v2.a7x7.xp'; " +
							"update json2dbMapping set dbPlayerTank='xp8p', dbPlayerTankMode='15' where jsonMainSubProperty='tanks_v2.a15x15.xpBefore8_8'; ";
					sqlite = mssql;
					break;

				case 13:
					mssql = "ALTER TABLE playerTankBattle ADD wn8 int NOT NULL default 0, eff int NOT NULL default 0; ";
					sqlite= "ALTER TABLE playerTankBattle ADD wn8 integer NOT NULL default 0; ALTER TABLE playerTankBattle ADD eff integer NOT NULL default 0; "; ;
					break;
				case 14:
					// Removed from Create New DB - not used any more
					// mssql = "DROP VIEW tankData2BattleMappingView; DROP VIEW tankInfoShort; DROP VIEW PlayerTankStatsView; DROP VIEW playerTankAchAllView; ";
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
					// Insert playerTankBattles for all tanks - new method from version 53 - not needed anymore
					//DataTable dt = DB.FetchData("select * from playerTank");
					//foreach (DataRow dr in dt.Rows)
					//{
					//	Dossier2db.SaveNewPlayerTankBattle(Convert.ToInt32(dr["id"])); 
					//}
					break;
				case 18:
					mssql = "ALTER TABLE playerTankBattle ADD battleOfTotal float NOT NULL default 0; ";
					sqlite = "ALTER TABLE playerTankBattle ADD battleOfTotal real NOT NULL default 0; ";
					break;
				case 19:
					mssql = "DELETE FROM columnSelection; ALTER TABLE columnSelection ADD colDataType varchar(50) NOT NULL default 'Int';";
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
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (43, 2, 42, 'battle.modeClan', 'Clan', 'Number of Clan battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 50, 'Int'); " + 
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
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (56, 1, 14, 'playerTank.hasClan', 'Clan Wars', 'Used in clan wars (0 = No, 1 = yes)', 'Tank', 35, 'Int'); " + 
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
				case 22:
					mssql = "UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.wins*1000/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) / 10 AS NUMERIC (10,1))' where id = 95 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.survived*1000/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) /10 AS NUMERIC (10,1))' where id = 98 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.xp/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal) AS INT)' where id = 140 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.hits*1000/nullif(playerTankBattle.shots,0)*playerTankBattle.battleOfTotal)  / 10 AS NUMERIC (10,1))' where id = 145 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.shots*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal)  / 10 AS NUMERIC (10,1))' where id = 146 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.hits*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal)  / 10 AS NUMERIC (10,1))' where id = 147 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.heHits*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal)  / 10 AS NUMERIC (10,1))' where id = 148 ; " +
							"UPDATE columnSelection SET colName='CAST(SUM(playerTankBattle.pierced*10/nullif(playerTankBattle.battles,0)*playerTankBattle.battleOfTotal)  / 10 AS NUMERIC (10,1))' where id = 149" ; 
					sqlite = mssql;
					break;
				case 23:
					mssql =
						"CREATE PROCEDURE SP_DROP_COL_CONSTRAINT @table VARCHAR(50), @col VARCHAR(50) " +
						"AS " +
						"BEGIN " +
						"	SET NOCOUNT ON " +
						"	DECLARE @table_id AS INT " +
						"	DECLARE @name_column_id AS INT " +
						"	DECLARE @sql nvarchar(255)  " +
						"	SET @table_id = OBJECT_ID(@table) " +
						"	SELECT @name_column_id = column_id " +
						"	FROM sys.columns " +
						"	WHERE object_id = @table_id " +
						"	AND name = @col " +
						"	SELECT @sql = 'ALTER TABLE ' + @table + ' DROP CONSTRAINT ' + D.name " +
						"	FROM sys.default_constraints AS D " +
						"	WHERE D.parent_object_id = @table_id " +
						"	AND D.parent_column_id = @name_column_id " +
						"	EXECUTE sp_executesql @sql " +
						"	SELECT @sql = 'ALTER TABLE ' + @table + ' DROP COLUMN ' + @col " +
						"	EXECUTE sp_executesql @sql " +
						"END ;";
					// No support for SQLite
					break;
				case 24:
					mssql = "SP_DROP_COL_CONSTRAINT 'playerTank' , 'battles15' ;" +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'battles8p15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'wins15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'losses15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'survived15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'frags15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'frags8p15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'dmg15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'dmgReceived15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'assistSpot15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'assistTrack15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'cap15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'def15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'spot15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'xp15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'xp8p15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'xpOriginal15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'shots15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'hits15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'heHits15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'pierced15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'shotsReceived15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'piercedReceived15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'heHitsReceived15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'noDmgShotsReceived15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'maxDmg15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'maxFrags15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'maxXp15' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'battles7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'wins7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'losses7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'survived7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'frags7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'frags8p7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'dmg7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'dmgReceived7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'assistSpot7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'assistTrack7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'cap7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'def7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'spot7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'xp7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'xpOriginal7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'shots7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'hits7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'heHits7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'pierced7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'shotsReceived7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'piercedReceived7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'heHitsReceived7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'noDmgShotsReceived7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'maxDmg7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'maxFrags7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'maxXp7' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'wn8' ; " +
							"SP_DROP_COL_CONSTRAINT 'playerTank' , 'eff' ; ";
					// No support for SQLite
					break;
				case 25:
					mssql =
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','battlesCountBefore8_8','Int','battles8p',NULL,NULL,'tanks.tankdata.battlesCountBefore8_8',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','damageAssistedRadio','Int','assistSpot','assistSpot',NULL,'tanks.tankdata.damageAssistedRadio',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','damageAssistedTrack','Int','assistTrack','assistTrack',NULL,'tanks.tankdata.damageAssistedTrack',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','heHitsReceived','Int','heHitsReceived',NULL,NULL,'tanks.tankdata.heHitsReceived',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','he_hits','Int','heHits',NULL,NULL,'tanks.tankdata.he_hits',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','mileage','Int','mileage',NULL,NULL,'tanks.tankdata.mileage',NULL); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','noDamageShotsReceived','Int','noDmgShotsReceived',NULL,NULL,'tanks.tankdata.noDamageShotsReceived',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','originalXP','Int','xpOriginal',NULL,NULL,'tanks.tankdata.originalXP',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','pierced','Int','pierced','pierced',NULL,'tanks.tankdata.pierced',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','piercedReceived','Int','piercedReceived','piercedReceived',NULL,'tanks.tankdata.piercedReceived',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','shotsReceived','Int','shotsReceived','shotsReceived',NULL,'tanks.tankdata.shotsReceived',15); " +
						"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) VALUES ('tanks','tankdata','xpBefore8_8','Int','xp8p',NULL,NULL,'tanks.tankdata.xpBefore8_8',15); " +
						"UPDATE columnSelection SET colName='battle.modeClan', name='Clan' where id = 43; " +
						"UPDATE columnSelection SET colName='battle.hasClan' where id = 56; " +
						"ALTER TABLE battle ADD heHitsReceived int NOT NULL default 0; " +
						"ALTER TABLE battle ADD noDmgShotsReceived int NOT NULL default 0; " +
						"UPDATE json2dbMapping SET dbBattle='heHitsReceived' where id IN(293,55,28); " +
						"UPDATE json2dbMapping SET dbBattle='noDmgShotsReceived' where id IN(29,56,297); " +
						"UPDATE columnSelection SET position=position+2 WHERE colType=2 AND position >= 32; " +
						"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (157, 2, 32, 'battle.heHitsReceived', 'HE Received', 'Number of HE shots received ', 'Shooting', 50, 'Int'); " +
						"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (158, 2, 33, 'battle.noDmgShotsReceived', 'No Dmg Shots Received', 'Number of no damaging shots received ', 'Shooting', 50, 'Int'); " +
						"ALTER TABLE battle ADD heHits int NOT NULL default 0; " +
						"UPDATE columnSelection SET position=position+3 WHERE colType=2 AND position >= 30; " +
						"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (159, 2, 30, 'battle.heHits', 'HE Hits', 'Number of HE hits', 'Shooting', 50, 'Int');  " +
						"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (160, 2, 31, 'battle.heHits * 100 / nullif(battle.shots,0)', 'HE Shots %', 'Percentage HE hits based on total shots (hi hits*100/shots)', 'Shooting', 50, 'Int');  " +
						"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (161, 2, 32, 'battle.heHits * 100 / nullif(battle.hits,0)', 'HE Hts %', 'Percentage HE hits based on total hits (he hits*100/hits)', 'Shooting', 50, 'Int');  ";
					sqlite = mssql;
					break;
				case 26:
					mssql = "SP_DROP_COL_CONSTRAINT 'battle' , 'mode15' ; " +
							"SP_DROP_COL_CONSTRAINT 'battle' , 'mode7' ; ";
					break;
				case 27:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (162, 2, 40, 'battle.battleMode', 'Battle Mode', 'Battle mode, 15 = Random Battles, Tank Company and Clan Wars, 7 = Team Battle (Historical Battles not included yet)', 'Mode', 50, 'VarChar'); ";
					sqlite = mssql;
					break;	
				case 28:
					mssql = "UPDATE columnSelection SET position=position+2 WHERE colType=2 AND position >= 11; " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (163, 2, 11, 'CAST(battle.battleTime AS DATE)', 'Battle Date', 'Battle date, the date (DD/MM/YYYY) the battle was finished', 'Battle', 70, 'DateTime'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) VALUES (164, 2, 12, 'CAST(battle.battleTime AS TIME)', 'Battle Time', 'Battle time, the clock (HH:MM:SS) when the battle was finished', 'Battle', 60, 'DateTime'); " +
							"UPDATE columnSelection SET name='Battle DateTime' WHERE id=8; " +
							"update columnSelection set colWidth = 35 where id IN (59,18,26,27,30,159,29,31,157,158,24,25,35,37,43,45);" +
							"update columnSelection set colWidth = 40 where id IN (28,32,33,160,161,162,36,38,40,47);";
					sqlite = mssql;
					break;	
				case 30:
					// Korrigert av KI - image datatype tar ikke default 0
					//mssql = "alter table tank add imgPath varchar(255) NULL ; " +
					//		"alter table tank add smallImgPath varchar(255)  NULL ; " +
					//		"alter table tank add contourImgPath varchar(255)  NULL; " +
					//		"alter table tank add img image  NULL ; " +
					//		"alter table tank add smallImg image NULL ; " +
					//		"alter table tank add contourImg image NULL; ";
					//sqlite = "alter table tank add imgPath varchar(255) NOT NULL default 0; " +
					//		"alter table tank add smallImgPath varchar(255) NOT NULL default 0; " +
					//		"alter table tank add contourImgPath varchar(255) NOT NULL default 0; " +
					//		"alter table tank add img blob NOT NULL default 0; " +
					//		"alter table tank add smallImg blob NOT NULL default 0; " +
					//		"alter table tank add contourImg blob NOT NULL default 0; ";
					//break;
				case 31:
					//mssql = "ALTER TABLE ach ADD imgPath VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img1Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img2Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img3Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img4Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img IMAGE NULL; " +
					//		"ALTER TABLE ach ADD img1 IMAGE NULL; " +
					//		"ALTER TABLE ach ADD img2 IMAGE NULL; " +
					//		"ALTER TABLE ach ADD img3 IMAGE NULL; " +
					//		"ALTER TABLE ach ADD img4 IMAGE NULL; " ;
					//sqlite = "ALTER TABLE ach ADD imgPath VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img1Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img2Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img3Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img4Path VARCHAR(255) NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img BLOB NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img1 BLOB NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img2 BLOB NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img3 BLOB NOT NULL DEFAULT 0; " +
					//		"ALTER TABLE ach ADD img4 BLOB NOT NULL DEFAULT 0;";
					break;
				case 32:
					mssql = "UPDATE columnSelection SET colDataType = 'Float' WHERE id IN (18,24,25,26,27,35,59)";
					sqlite = mssql;
					break;
				case 33:
					mssql = "UPDATE columnSelection SET name = 'Dmg' WHERE id = 19; " +
							"UPDATE columnSelection SET name = 'Dmg Received' WHERE id = 20; " +
							"UPDATE columnSelection SET name = 'Dmg Spot' WHERE id = 21; " +
							"UPDATE columnSelection SET name = 'Dmg Track' WHERE id = 22; " +
							"UPDATE columnSelection SET name = 'Dmg' WHERE id = 128; " +
							"UPDATE columnSelection SET name = 'Dmg Spot' WHERE id = 129; " +
							"UPDATE columnSelection SET name = 'Dmg Track' WHERE id = 130; " +
							"UPDATE columnSelection SET colWidth = 54 WHERE id = 20; " ;
					sqlite = mssql;
					break;
				case 35:
					mssql = "UPDATE columnSelection SET colName='playerTank.hasClan' where id = 56; ";
					sqlite = mssql;
					break;
				case 36:
					mssql = "UPDATE columnSelection SET colWidth=70, name='XP Tot' WHERE id=137;" +
							"UPDATE columnSelection SET name='XP Max' WHERE id=156;" +
							"UPDATE columnSelection SET name='Dmg Max' WHERE id=154;" +
							"UPDATE columnSelection SET name='Frags Max' WHERE id=155;" ;
					sqlite = mssql;
					break;
				case 37:
					mssql = "ALTER TABLE playerTank ADD gCurrentXP int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gGrindXP int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gGoalXP int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gProgressXP int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gBattlesDay int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gComment varchar(100) NOT NULL default ''; ";
					sqlite = mssql.Replace("int", "integer");
					break;
				case 38:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (174, 1, 76, 'playerTank.gBattlesDay', 'Battles Day', 'Estimated battles per day when grinding', 'Grinding', 40, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (175, 1, 77, 'playerTank.gComment', 'Comment', 'Grinding comment, normally what to achive with the grinding, eg: next tank, top gun...', 'Grinding', 100, 'VarChar'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (170, 1, 78, 'playerTank.gCurrentXP', 'Current XP', 'Current XP when started to grind', 'Grinding', 55, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (171, 1, 79, 'playerTank.gGrindXP', 'Grind XP', 'The total amount of XP wanted to grind', 'Grinding', 55, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (172, 1, 80, 'playerTank.gGoalXP', 'Goal XP', 'The goal in XP wanted to achieve with the grinding', 'Grinding', 55, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (173, 1, 81, 'playerTank.gProgressXP', 'Progress XP', 'Current progress in XP for this grinding', 'Grinding', 55, 'Int'); ";
					sqlite = mssql;
					break;
				case 39:
					mssql = "ALTER TABLE playerTank ADD gRestXP int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gProgressPercent int NOT NULL default 0; ";
					sqlite = mssql.Replace("int", "integer");
					break;
				case 40:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (177, 1, 82, 'playerTank.gProgressPercent', 'Prog %', 'Current progress in persent for this grinding', 'Grinding', 40, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (176, 1, 83, 'playerTank.gRestXP', 'Rest XP', 'Rest XP needed to reach goal', 'Grinding', 55, 'Int'); ";
							
					sqlite = mssql;
					break;
				case 41:
					mssql = "UPDATE columnSelection SET colWidth=60, name='XP Earned' WHERE id=137;";
					sqlite = mssql;
					break;
				case 42:
					mssql = "ALTER TABLE playerTank ADD gRestBattles int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD gRestDays int NOT NULL default 0; ";
					sqlite = mssql.Replace("int", "integer");
					break;
				case 43:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (178, 1, 84, 'playerTank.gRestBattles', 'Rest Battles', 'Rest battles needed to reach goal', 'Grinding', 40, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (179, 1, 85, 'playerTank.gRestDays', 'Rest Days', 'Rest days needed to reach goal', 'Grinding', 40, 'Int'); ";

					sqlite = mssql;
					break;
				case 45:
					mssql = "UPDATE columnSelection SET colWidth = 47 WHERE colWidth = 50 and colType=2; " +
							"UPDATE columnSelection SET colWidth = 47 WHERE id IN (38,40,47);";
					sqlite = mssql;
					break;
				case 46:
					mssql = "ALTER TABLE playerTank ADD lastVictoryTime datetime NULL; ";
					sqlite = mssql;
					break;
				case 47:
					mssql = "UPDATE columnSelection SET name='Premium' WHERE id=2 ; " +
							"UPDATE columnSelection SET name='Tank Type' WHERE id=3 ; " +
							"UPDATE columnSelection SET name='Type' WHERE id=4 ; " +
							"UPDATE columnSelection SET name='Tank Nation' WHERE id=5 ; " +
							"UPDATE columnSelection SET name='Nation' WHERE id=6 ; " +
							"UPDATE columnSelection SET name='Battles' WHERE id=7 ; " +
							"UPDATE columnSelection SET name='Battle DateTime' WHERE id=8 ; " +
							"UPDATE columnSelection SET name='Life Time' WHERE id=9 ; " +
							"UPDATE columnSelection SET name='Result' WHERE id=10 ; " +
							"UPDATE columnSelection SET name='Survived' WHERE id=11 ; " +
							"UPDATE columnSelection SET name='Victory' WHERE id=13 ; " +
							"UPDATE columnSelection SET name='Draw' WHERE id=14 ; " +
							"UPDATE columnSelection SET name='Defeat' WHERE id=15 ; " +
							"UPDATE columnSelection SET name='Survived Count' WHERE id=16 ; " +
							"UPDATE columnSelection SET name='Killed Count' WHERE id=17 ; " +
							"UPDATE columnSelection SET name='Frags' WHERE id=18 ; " +
							"UPDATE columnSelection SET name='Dmg' WHERE id=19 ; " +
							"UPDATE columnSelection SET name='Dmg Received' WHERE id=20 ; " +
							"UPDATE columnSelection SET name='Dmg Spot' WHERE id=21 ; " +
							"UPDATE columnSelection SET name='Dmg Track' WHERE id=22 ; " +
							"UPDATE columnSelection SET name='Cap' WHERE id=24 ; " +
							"UPDATE columnSelection SET name='Def' WHERE id=25 ; " +
							"UPDATE columnSelection SET name='Shots' WHERE id=26 ; " +
							"UPDATE columnSelection SET name='Hits' WHERE id=27 ; " +
							"UPDATE columnSelection SET name='Hit Rate' WHERE id=28 ; " +
							"UPDATE columnSelection SET name='Shots Reveived' WHERE id=29 ; " +
							"UPDATE columnSelection SET name='Pierced' WHERE id=30 ; " +
							"UPDATE columnSelection SET name='Pierced Received' WHERE id=31 ; " +
							"UPDATE columnSelection SET name='Pierced Shots%' WHERE id=32 ; " +
							"UPDATE columnSelection SET name='Pierced Hts%' WHERE id=33 ; " +
							"UPDATE columnSelection SET name='Spot' WHERE id=35 ; " +
							"UPDATE columnSelection SET name='Mileage' WHERE id=36 ; " +
							"UPDATE columnSelection SET name='Trees Cut' WHERE id=37 ; " +
							"UPDATE columnSelection SET name='XP' WHERE id=38 ; " +
							"UPDATE columnSelection SET name='ID' WHERE id=39 ; " +
							"UPDATE columnSelection SET name='EFF' WHERE id=40 ; " +
							"UPDATE columnSelection SET name='Clan' WHERE id=43 ; " +
							"UPDATE columnSelection SET name='Company' WHERE id=45 ; " +
							"UPDATE columnSelection SET name='WN8' WHERE id=47 ; " +
							"UPDATE columnSelection SET name='Tank' WHERE id=58 ; " +
							"UPDATE columnSelection SET name='Tier' WHERE id=59 ; " +
							"UPDATE columnSelection SET name='HE Received' WHERE id=157 ; " +
							"UPDATE columnSelection SET name='No Dmg Shots Received' WHERE id=158 ; " +
							"UPDATE columnSelection SET name='HE Hits' WHERE id=159 ; " +
							"UPDATE columnSelection SET name='HE Shots %' WHERE id=160 ; " +
							"UPDATE columnSelection SET name='HE Hts %' WHERE id=161 ; " +
							"UPDATE columnSelection SET name='Battle Mode' WHERE id=162 ; " +
							"UPDATE columnSelection SET name='Battle Date' WHERE id=163 ; " +
							"UPDATE columnSelection SET name='Battle Time' WHERE id=164 ; ";
					sqlite = mssql;
					break;
				case 48:
					mssql = "UPDATE columnSelection SET name = 'Start XP' WHERE id = 170; " +
							"UPDATE columnSelection SET name = 'End XP' WHERE id = 172; ";
					sqlite = mssql;
					break;
				case 49:
					mssql = "ALTER TABLE columnList ADD defaultFavListId int NOT NULL default -1; " ;
					sqlite = mssql.Replace("int", "integer");
					break;
				case 50:
					mssql = "update columnSelection set colName='modTurret.armorFront' where id=79; " +
							"update columnSelection set colName='modTurret.armorSides' where id=80; " +
							"update columnSelection set colName='modTurret.armorRear' where id=81; " +
							"update columnSelection set colName='modGun.name' where id=83; " +
							"update columnSelection set name='Turret Front Arm', colWidth=60 where id=79; " +
							"update columnSelection set name='Turret Side Arm', colWidth=60 where id=80; " +
							"update columnSelection set name='Turret Rear Arm', colWidth=60 where id=81; " +
							"update columnSelection set position=70 where id=78; " +
							"update columnSelection set colDataType='Float' where id IN(95,98,145,146,147,148,149); " +
							"update columnSelection set colName='playerTank.skillRecon',name='Recon' where id=69; " +
							"update columnSelection set Name='Camo Skill' where id=71;" +
							"update columnSelection set Name='Camo eqp' where id=67;" +
							"update columnSelection set colDataType='Int' where id=2;" +
							"update columnSelection set name='Pierced Hits%' where name='Pierced Hts%';";
					sqlite = mssql;
					break;
				case 51:
					mssql = "UPDATE columnSelection SET position=position+100 where colGroup <> 'Tank' and colType=1; " +
							"UPDATE columnSelection SET position=position+10 where colGroup = 'Tank' and colType=1 and position > 1;" +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (180, 1, 2, 'tank.contourImg', 'Tank Icon', 'Tank icon (contour image)', 'Tank', 65, 'Image'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (181, 1, 3, 'tank.smallImg', 'Tank Image', 'Tank image (small), suitable for grid', 'Tank', 90, 'Image'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (182, 1, 4, 'tank.img', 'Tank Image Large', 'Tank large image, will only partly show if not expanding row height in grid', 'Tank', 145, 'Image'); "; 
					sqlite = mssql; 
					break;
				case 52:
					mssql = "UPDATE columnSelection SET position=position+100 where colGroup <> 'Tank' and colType=2; " +
							"UPDATE columnSelection SET position=position+10 where colGroup = 'Tank' and colType=2 and position > 1;" +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (183, 2, 2, 'tank.contourImg', 'Tank Icon', 'Tank icon (contour image)', 'Tank', 65, 'Image'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (184, 2, 3, 'tank.smallImg', 'Tank Image', 'Tank image (small), suitable for grid', 'Tank', 90, 'Image'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (185, 2, 4, 'tank.img', 'Tank Image Large', 'Tank large image, will only partly show if not expanding row height in grid', 'Tank', 145, 'Image'); ";
					sqlite = mssql;
					break;
				case 53:
					mssql = "UPDATE columnSelection SET colName='tank.name' WHERE id=1; " +
							"UPDATE columnSelection SET colName='tank.tier' WHERE id=12; " +
							"UPDATE columnSelection SET colName='tank.premium' WHERE id=23; " +
							"UPDATE columnSelection SET colName='tankType.name' WHERE id=34; " +
							"UPDATE columnSelection SET colName='tankType.shortName' WHERE id=44; " +
							"UPDATE columnSelection SET colName='tank.id' WHERE id=46; " +
							"UPDATE columnSelection SET colName='playerTankBattle.eff' WHERE id=48; " +
							"UPDATE columnSelection SET colName='playerTankBattle.wn8' WHERE id=49; " +
							"UPDATE columnSelection SET colName='playerTankBattle.battles' WHERE id=50; " +
							"UPDATE columnSelection SET colName='country.name' WHERE id=51; " +
							"UPDATE columnSelection SET colName='playerTank.battleLifeTime' WHERE id=52; " +
							"UPDATE columnSelection SET colName='playerTank.markOfMastery' WHERE id=53; " +
							"UPDATE columnSelection SET colName='playerTank.lastBattleTime' WHERE id=54; " +
							"UPDATE columnSelection SET colName='playerTank.has15' WHERE id=55; " +
							"UPDATE columnSelection SET colName='playerTank.hasClan' WHERE id=56; " +
							"UPDATE columnSelection SET colName='country.shortName' WHERE id=57; " +
							"UPDATE columnSelection SET colName='playerTank.hasCompany' WHERE id=60; " +
							"UPDATE columnSelection SET colName='playerTank.has7' WHERE id=61; " +
							"UPDATE columnSelection SET colName='playerTank.mileage' WHERE id=63; " +
							"UPDATE columnSelection SET colName='playerTank.treesCut' WHERE id=64; " +
							"UPDATE columnSelection SET colName='playerTank.eqBino' WHERE id=65; " +
							"UPDATE columnSelection SET colName='playerTank.eqCoated' WHERE id=66; " +
							"UPDATE columnSelection SET colName='playerTank.eqCamo' WHERE id=67; " +
							"UPDATE columnSelection SET colName='playerTank.equVent' WHERE id=68; " +
							"UPDATE columnSelection SET colName='playerTank.skillRecon' WHERE id=69; " +
							"UPDATE columnSelection SET colName='playerTank.skillAwareness' WHERE id=70; " +
							"UPDATE columnSelection SET colName='playerTank.skillCamo' WHERE id=71; " +
							"UPDATE columnSelection SET colName='playerTank.skillBia' WHERE id=72; " +
							"UPDATE columnSelection SET colName='playerTank.premiumCons' WHERE id=73; " +
							"UPDATE columnSelection SET colName='modRadio.name' WHERE id=74; " +
							"UPDATE columnSelection SET colName='modRadio.signalRange' WHERE id=75; " +
							"UPDATE columnSelection SET colName='modTurret.name' WHERE id=77; " +
							"UPDATE columnSelection SET colName='modTurret.viewRange' WHERE id=78; " +
							"UPDATE columnSelection SET colName='modTurret.armorFront' WHERE id=79; " +
							"UPDATE columnSelection SET colName='modTurret.armorSides' WHERE id=80; " +
							"UPDATE columnSelection SET colName='modTurret.armorRear' WHERE id=81; " +
							"UPDATE columnSelection SET colName='modGun.name' WHERE id=83; " +
							"UPDATE columnSelection SET colName='modGun.tier' WHERE id=84; " +
							"UPDATE columnSelection SET colName='playerTankBattle.battles8p' WHERE id=85; " +
							"UPDATE columnSelection SET colName='playerTankBattle.wins' WHERE id=86; " +
							"UPDATE columnSelection SET colName='playerTankBattle.battles-playerTankBattle.wins-playerTankBattle.losses' WHERE id=91; " +
							"UPDATE columnSelection SET colName='playerTankBattle.losses' WHERE id=92; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.wins*1000/nullif(playerTankBattle.battles,0) as FLOAT) / 10' WHERE id=95; " +
							"UPDATE columnSelection SET colName='playerTankBattle.survived' WHERE id=96; " +
							"UPDATE columnSelection SET colName='playerTankBattle.battles-playerTankBattle.survived' WHERE id=97; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.survived*1000/nullif(playerTankBattle.battles,0) as FLOAT) / 10' WHERE id=98; " +
							"UPDATE columnSelection SET colName='playerTankBattle.dmg' WHERE id=128; " +
							"UPDATE columnSelection SET colName='playerTankBattle.assistSpot' WHERE id=129; " +
							"UPDATE columnSelection SET colName='playerTankBattle.assistTrack' WHERE id=130; " +
							"UPDATE columnSelection SET colName='playerTankBattle.frags' WHERE id=131; " +
							"UPDATE columnSelection SET colName='playerTankBattle.dmgReceived' WHERE id=132; " +
							"UPDATE columnSelection SET colName='playerTankBattle.frags8p' WHERE id=133; " +
							"UPDATE columnSelection SET colName='playerTankBattle.cap' WHERE id=134; " +
							"UPDATE columnSelection SET colName='playerTankBattle.def' WHERE id=135; " +
							"UPDATE columnSelection SET colName='playerTankBattle.spot' WHERE id=136; " +
							"UPDATE columnSelection SET colName='playerTankBattle.xp' WHERE id=137; " +
							"UPDATE columnSelection SET colName='playerTankBattle.xp8p' WHERE id=138; " +
							"UPDATE columnSelection SET colName='playerTankBattle.xpOriginal' WHERE id=139; " +
							"UPDATE columnSelection SET colName='playerTankBattle.xp/nullif(playerTankBattle.battles,0)' WHERE id=140; " +
							"UPDATE columnSelection SET colName='playerTankBattle.shots' WHERE id=141; " +
							"UPDATE columnSelection SET colName='playerTankBattle.hits' WHERE id=142; " +
							"UPDATE columnSelection SET colName='playerTankBattle.heHits' WHERE id=143; " +
							"UPDATE columnSelection SET colName='playerTankBattle.pierced' WHERE id=144; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.hits*1000/nullif(playerTankBattle.shots,0) as FLOAT) / 10' WHERE id=145; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.shots*10/nullif(playerTankBattle.battles,0)  as FLOAT) / 10' WHERE id=146; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.hits*10/nullif(playerTankBattle.battles,0)  as FLOAT) / 10' WHERE id=147; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.heHits*10/nullif(playerTankBattle.battles,0)   as FLOAT) / 10' WHERE id=148; " +
							"UPDATE columnSelection SET colName='CAST(playerTankBattle.pierced*10/nullif(playerTankBattle.battles,0)  as FLOAT) / 10' WHERE id=149; " +
							"UPDATE columnSelection SET colName='playerTankBattle.shotsReceived' WHERE id=150; " +
							"UPDATE columnSelection SET colName='playerTankBattle.piercedReceived' WHERE id=151; " +
							"UPDATE columnSelection SET colName='playerTankBattle.heHitsReceived' WHERE id=152; " +
							"UPDATE columnSelection SET colName='playerTankBattle.noDmgShotsReceived' WHERE id=153; " +
							"UPDATE columnSelection SET colName='playerTankBattle.maxDmg' WHERE id=154; " +
							"UPDATE columnSelection SET colName='playerTankBattle.maxFrags' WHERE id=155; " +
							"UPDATE columnSelection SET colName='playerTankBattle.maxXp' WHERE id=156; " +
							"UPDATE columnSelection SET colName='playerTank.gCurrentXP' WHERE id=170; " +
							"UPDATE columnSelection SET colName='playerTank.gGrindXP' WHERE id=171; " +
							"UPDATE columnSelection SET colName='playerTank.gGoalXP' WHERE id=172; " +
							"UPDATE columnSelection SET colName='playerTank.gProgressXP' WHERE id=173; " +
							"UPDATE columnSelection SET colName='playerTank.gBattlesDay' WHERE id=174; " +
							"UPDATE columnSelection SET colName='playerTank.gComment' WHERE id=175; " +
							"UPDATE columnSelection SET colName='playerTank.gRestXP' WHERE id=176; " +
							"UPDATE columnSelection SET colName='playerTank.gProgressPercent' WHERE id=177; " +
							"UPDATE columnSelection SET colName='playerTank.gRestBattles' WHERE id=178; " +
							"UPDATE columnSelection SET colName='playerTank.gRestDays' WHERE id=179; " +
							"UPDATE columnSelection SET colName='tank.contourImg' WHERE id=180; " +
							"UPDATE columnSelection SET colName='tank.smallImg' WHERE id=181; " +
							"UPDATE columnSelection SET colName='tank.img' WHERE id=182; ";
					sqlite = mssql;
					break;
				case 54:
					mssql = "DELETE FROM playerTankBattle WHERE battles=0;";
					sqlite = mssql;
					break;
				case 55:
					mssql = "CREATE VIEW playerTankBattleTotalsView AS " +
							"SELECT        playerTankId, SUM(battles) AS battles, SUM(wins) AS wins, SUM(battles8p) AS battles8p, SUM(losses) AS losses, SUM(survived) AS survived, SUM(frags) AS frags,  " +
							"                         SUM(frags8p) AS frags8p, SUM(dmg) AS dmg, SUM(dmgReceived) AS dmgReceived, SUM(assistSpot) AS assistSpot, SUM(assistTrack) AS assistTrack, SUM(cap)  " +
							"                         AS cap, SUM(def) AS def, SUM(spot) AS spot, SUM(xp) AS xp, SUM(xp8p) AS xp8p, SUM(xpOriginal) AS xpOriginal, SUM(shots) AS shots, SUM(hits) AS hits,  " +
							"                         SUM(heHits) AS heHits, SUM(pierced) AS pierced, SUM(shotsReceived) AS shotsReceived, SUM(piercedReceived) AS piercedReceived, SUM(heHitsReceived)  " +
							"                         AS heHitsReceived, SUM(noDmgShotsReceived) AS noDmgShotsReceived, MAX(maxDmg) AS maxDmg, MAX(maxFrags) AS maxFrags, MAX(maxXp) AS maxXp,  " +
							"                         MAX(battlesCompany) AS battlesCompany, MAX(battlesClan) AS battlesClan, MAX(wn8) AS wn8, MAX(eff) AS eff " +
							"FROM            playerTankBattle " +
							"GROUP BY playerTankId ";
					sqlite = mssql;
					break;
				case 56:
					mssql = "ALTER TABLE playerTank ADD hasFort int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD hasHistorical int NOT NULL default 0; " +
							"ALTER TABLE playerTank ADD hasSortie int NOT NULL default 0; ";
					sqlite = mssql.Replace("int", "integer");
					break;
				case 57:
					mssql = "INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) " +
							"VALUES ('tanks_v2','common','has_fort','Int','hasFort',NULL,NULL,'tanks_v2.common.has_fort',NULL); " +
							"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) " +
							"VALUES ('tanks_v2','common','has_historical','Int','hasHistorical',NULL,NULL,'tanks_v2.common.has_historical',NULL); " +
							"INSERT INTO json2dbMapping (jsonMain,jsonSub,jsonProperty,dbDataType,dbPlayerTank,dbBattle,dbAch,jsonMainSubProperty,dbPlayerTankMode) " +
							"VALUES ('tanks_v2','common','has_sortie','Int','hasSortie',NULL,NULL,'tanks_v2.common.has_sortie',NULL); ";
					sqlite = mssql;
					break;
				case 58:
					mssql = "INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'battlesCount','Int','battles','battlesCount',NULL,'tanks_v2.historical.battlesCount','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'capturePoints','Int','cap','cap',NULL,'tanks_v2.historical.capturePoints','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'damageAssistedRadio','Int','assistSpot','assistSpot',NULL,'tanks_v2.historical.damageAssistedRadio','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'damageAssistedTrack','Int','assistTrack','assistTrack',NULL,'tanks_v2.historical.damageAssistedTrack','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'damageDealt','Int','dmg','dmg',NULL,'tanks_v2.historical.damageDealt','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'damageReceived','Int','dmgReceived','dmgReceived',NULL,'tanks_v2.historical.damageReceived','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'droppedCapturePoints','Int','def','def',NULL,'tanks_v2.historical.droppedCapturePoints','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'frags','Int','frags','frags',NULL,'tanks_v2.historical.frags','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'frags8p','Int','frags8p',NULL,NULL,'tanks_v2.historical.frags8p','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'he_hits','Int','heHits',NULL,NULL,'tanks_v2.historical.he_hits','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'heHitsReceived','Int','heHitsReceived','heHitsReceived',NULL,'tanks_v2.historical.heHitsReceived','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'hits','Int','hits','hits',NULL,'tanks_v2.historical.hits','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'losses','Int','losses','defeat',NULL,'tanks_v2.historical.losses','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','maxHistorical', 'maxDamage','Int','maxDmg',NULL,NULL,'tanks_v2.maxHistorical.maxDamage','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','maxHistorical', 'maxFrags','Int','maxFrags',NULL,NULL,'tanks_v2.maxHistorical.maxFrags','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','maxHistorical', 'maxXP','Int','maxXp',NULL,NULL,'tanks_v2.maxHistorical.maxXP','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'noDamageShotsReceived','Int','noDmgShotsReceived','noDmgShotsReceived',NULL,'tanks_v2.historical.noDamageShotsReceived','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'originalXP','Int','xpOriginal',NULL,NULL,'tanks_v2.historical.originalXP','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'pierced','Int','pierced','pierced',NULL,'tanks_v2.historical.pierced','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'piercedReceived','Int','piercedReceived','piercedReceived',NULL,'tanks_v2.historical.piercedReceived','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'shots','Int','shots','shots',NULL,'tanks_v2.historical.shots','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'shotsReceived','Int','shotsReceived','shotsReceived',NULL,'tanks_v2.historical.shotsReceived','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'spotted','Int','spot','spotted',NULL,'tanks_v2.historical.spotted','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'survivedBattles','Int','survived','survived',NULL,'tanks_v2.historical.survivedBattles','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'wins','Int','wins','victory',NULL,'tanks_v2.historical.wins','Historical'); " +
							"INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) VALUES ('tanks_v2','historical', 'xp','Int','xp','xp',NULL,'tanks_v2.historical.xp','Historical'); ";
					sqlite = mssql;
					break;
				case 59:
					mssql = "CREATE UNIQUE INDEX IX_ach_name ON ach (name ASC); ";
					sqlite = mssql;
					break;
				case 60:
					// damageBlockedByArmor
					break;
				case 61:
					mssql = "ALTER TABLE _version_ ADD description varchar(255) NULL; " +
							"UPDATE _version_ SET description = 'DB version' WHERE id = 1; " +
							"INSERT INTO _version_ (id, version, description) VALUES (2, 0, 'WN8 version'); ";
					sqlite = mssql;
					break;
				case 62:
					mssql = "ALTER TABLE columnListSelection ADD colWidth int NOT NULL default 50; ";
					sqlite = mssql.Replace("int", "integer");
					break;
				case 63:
					mssql = "UPDATE columnListSelection SET columnListSelection.colWidth = CS.colWidth " + 
							"FROM columnListSelection CLS INNER JOIN columnSelection CS ON CLS.columnSelectionId = CS.id ;";
					sqlite = "UPDATE columnListSelection SET " +
							 "colWidth = (SELECT colWidth FROM columnSelection WHERE id = columnListSelection.columnSelectionId)";
					break;
				case 64: 
					mssql = "ALTER TABLE columnSelection ADD colNameSQLite VARCHAR(255) NULL; ";
					sqlite = mssql;
					break;
				case 66:
					mssql = "UPDATE columnSelection SET colNameSQLite = 'strftime(''%H:%M'', battleTime)' where id=164;" +
							"UPDATE columnSelection SET colNameSQLite = 'strftime(''%d.%m.%Y'', battleTime)' where id=163;";
					sqlite = mssql;
					break;
				case 67:
					NewSystemTankColList();
					NewSystemBattleColList();
					break;
				case 68:
					mssql = "UPDATE columnSelection SET name='Remaining XP' WHERE id=176; " +
							"UPDATE columnSelection SET description='Remaining XP needed to reach target XP' WHERE id=176; " +
							"UPDATE columnSelection SET name='Days To Go' WHERE id=179; " +
							"UPDATE columnSelection SET description='Remaining days needed to reach target XP' WHERE id=179; " +
							"UPDATE columnSelection SET name='Battles To Go' WHERE id=178; " +
							"UPDATE columnSelection SET description='Remaining battles needed to reach target XP' WHERE id=178; " +
							"UPDATE columnSelection SET name='Target XP' WHERE id=171; " +
							"UPDATE columnSelection SET description='The total amount of XP that is the target for the grinding' WHERE id=171; " +
							"UPDATE columnSelection SET description='Current progress in percent for this grinding' WHERE id=177; ";
					sqlite = mssql;
					break;	
				case 69:
					mssql = "UPDATE columnSelection SET colName='coalesce(battle.hits * 100 / nullif(battle.shots,0),0)' WHERE id=28; ";
					sqlite = mssql;
					break;	
				case 70:
					mssql = "UPDATE columnSelection SET colName='coalesce(battle.pierced * 100 / nullif(battle.shots,0),0)' WHERE id=32; " +
							"UPDATE columnSelection SET colName='coalesce(battle.pierced * 100 / nullif(battle.hits,0),0)' WHERE id=33; " +
							"UPDATE columnSelection SET colName='coalesce(battle.heHits * 100 / nullif(battle.shots, 0),0)' WHERE id=160; " +
							"UPDATE columnSelection SET colName='coalesce(battle.heHits * 100 / nullif(battle.hits, 0),0)' WHERE id=161; " ;
					sqlite = mssql;
					break;	
				case 71:
					NewSystemBattleColList();
					break;
				case 72:
					mssql = "insert into wsTankId (tankId, tankName, wsCountryId, wsTankId) values (54289, 'Lowe', 1, 212); " +
							"insert into wsTankId (tankId, tankName, wsCountryId, wsTankId) values (57857, 'T-62A SPORT', 0, 226); " +
							"insert into wsTankId (tankId, tankName, wsCountryId, wsTankId) values (59921, 'Karl', 1, 234);" ;
					sqlite = mssql;
					break;
				case 73:
					mssql = "UPDATE json2dbMapping SET dbBattle=NULL where jsonMainSubProperty='tanks_v2.maxHistorical.maxDamage'; " +
							"UPDATE json2dbMapping SET dbBattle=NULL where jsonMainSubProperty='tanks_v2.maxHistorical.maxFrags'; ";
					sqlite = mssql;
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
						sql = "update _version_ set version=" + DBVersionCurrentNumber.ToString() + " where id=1";
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
			//bool versionTableFound = false;
			// List tables
			DataTable dt = DB.ListTables();
			if (dt.Rows.Count > 0)
			{
				//// Check if _version_ table containing db version number exists
				//foreach (DataRow dr in dt.Rows)
				//{
				//    if (dr["TABLE_NAME"].ToString() == "_version_")
				//    {
				//        versionTableFound = true;
				//        break;
				//    }
				//}
				//// if _version_ table not exist create it
				//if (!versionTableFound)
				//{
				//    if (Config.Settings.databaseType == ConfigData.dbType.SQLite)
				//        sql = "create table _version_ (id integer primary key, version integer not null, description varchar(255)); ";
				//    else if (Config.Settings.databaseType == ConfigData.dbType.MSSQLserver)
				//        sql = "create table _version_ (id int primary key, version int not null, description varchar(255)); ";
				//    bool createTableOK = DB.ExecuteNonQuery(sql); // Create _version_ table now
				//    if (!createTableOK)
				//        return 0; // Error occured creating _version_ table
				//    else
				//    {
				//        // Add initial db version
				//        sql = "insert into _version_ (id, version, description) values (1, 1, 'DB version'); ";
				//        bool insertVersionOK = DB.ExecuteNonQuery(sql);
				//        if (!insertVersionOK)
				//            return 0; // Error occured inserting version number in _version_ table
				//        // Add initial WN8 version
				//        sql = "insert into _version_ (id, version, description) values (2, 0, 'WN8 version'); ";
				//        insertVersionOK = DB.ExecuteNonQuery(sql);
				//        if (!insertVersionOK)
				//            return 0; // Error occured inserting version number in _version_ table
				//    }
				//}
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

		public static int WN8Version()
		{
			int version = 0;
			string sql = "select version from _version_ where id=2; ";
			DataTable dt = DB.FetchData(sql, Config.Settings.showDBErrors);
			if (dt.Rows.Count > 0)
			{
				version = Convert.ToInt32(dt.Rows[0][0]);
			}
			else version = 0;
			return version;
		}

		//
		// *** SQL TO GENERATE INSERT FOR columnListSelection BASED ON EXISTING COLLIST **
		//
		// SELECT '"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values ('+cast(columnSelectionId as varchar)+',"+id+",'+cast(sortorder as varchar)+','+cast(colWidth as varchar)+');" +'
		// FROM [WotNumbers].[dbo].[columnListSelection]
		// WHERE columnListId=28
		// ORDER BY sortorder

		private static void NewSystemTankColList()
		{
			// First remove all other system colList for Tank
			string sql = 
				"delete from columnListSelection where columnListId IN (select id from columnList where sysCol=1 and colType=1); " +
				"delete from columnList where sysCol=1 and colType=1; ";
			DB.ExecuteNonQuery(sql);
			// Then remove current startup, and create new default colList
			sql =	
				"update columnList set colDefault=0 where colType=1; " +
				"insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (1,'Default', 1, 1, 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=1;";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (12," + id + ",1,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (181," + id + ",2,90);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (1," + id + ",3,120);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (44," + id + ",4,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (57," + id + ",5,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (54," + id + ",6,100);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (50," + id + ",7,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (95," + id + ",8,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (154," + id + ",9,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (155," + id + ",10,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (156," + id + ",11,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (53," + id + ",12,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (48," + id + ",13,50);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (49," + id + ",14,50);";
			DB.ExecuteNonQuery(sql);
			// Then create grinding colList
			sql = "insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (1,'Grinding', 0, 2, 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=1;";
			id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (12," + id + ",1,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (181," + id + ",2,90);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (1," + id + ",3,106);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (175," + id + ",4,140);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (171," + id + ",5,70);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (173," + id + ",6,67);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (177," + id + ",7,40);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (176," + id + ",8,66);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (178," + id + ",9,40);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (174," + id + ",10,40);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (179," + id + ",11,40);";
			DB.ExecuteNonQuery(sql);
		}

		private static void NewSystemBattleColList()
		{
			// First remove all other system colList for Battle
			string sql =
				"delete from columnListSelection where columnListId IN (select id from columnList where sysCol=1 and colType=2); " +
				"delete from columnList where sysCol=1 and colType=2; ";
			DB.ExecuteNonQuery(sql);
			// Then remove current startup, and create new default colList
			sql =
				"update columnList set colDefault=0 where colType=2; " +
				"insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (2,'Default', 1, 1, 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=2;";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (59," + id + ",1,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (184," + id + ",2,90);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (58," + id + ",3,109);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (8," + id + ",6,104);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (10," + id + ",7,54);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (11," + id + ",8,54);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (19," + id + ",9,47);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (21," + id + ",10,47);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (22," + id + ",11,47);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (20," + id + ",12,47);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (18," + id + ",13,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (35," + id + ",14,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (24," + id + ",15,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (25," + id + ",16,35);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (28," + id + ",17,40);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (38," + id + ",18,47);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (40," + id + ",19,47);" +
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (47," + id + ",20,47);";
			DB.ExecuteNonQuery(sql);
		}

	}
}
