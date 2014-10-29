using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WinApp.Gadget;

namespace WinApp.Code
{
	class DBVersion
	{
		public static bool RunDossierFileCheckWithForceUpdate = false;
		public static bool RunWotApi = false;
		public static bool RunRecalcBattleWN8 = false;
		
	
		// The current databaseversion
		public static int ExpectedNumber = 190; // <--------------------------------------- REMEMBER TO ADD DB VERSION NUMBER HERE - AND SUPPLY SQL SCRIPT BELOW

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
				case 74:
					mssql = "ALTER TABLE playerTankBattle ADD wn7 int NOT NULL default 0; " +
							"ALTER TABLE battle ADD wn7 int NOT NULL default 0; ";
					sqlite = mssql.Replace("int", "integer");
					break;
				case 75:
					mssql = "UPDATE battleResult SET color = '#4CFF00' where id = 1; " + // GREEN  victory color
							"UPDATE battleResult SET color = '#FFFF00' where id = 2; " + // YELLOW draw color
							"UPDATE battleResult SET color = '#FF0000' where id = 3; " + // RED    defeat color
							"UPDATE battleResult SET color = '#30A8FF' where id = 4; " + // BLUE   several color
							"UPDATE battleSurvive SET color = '#4CFF00' where id = 1; " + // GREEN Yes - survived color
							"UPDATE battleSurvive SET color = '#30A8FF' where id = 2; " + // BLUE  Some - survived color
							"UPDATE battleSurvive SET color = '#FF0000' where id = 3; " ; // RED   No - survived color
					sqlite = mssql;
					break;
				case 76:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (186, 2, 146, 'battle.wn7', 'WN7', 'Calculated battle WN7 rating (according to formula from vBAddict)', 'Rating', 47, 'Int'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (187, 1, 117, 'playerTankBattle.wn7', 'WN7', 'Calculated battle WN7 rating (according to formula from vBAddict)', 'Rating', 50, 'Int'); " +
							"UPDATE columnSelection SET description = 'Calculated battle WN8 rating (according to formula from vbAddict)' where id=49;";
					sqlite = mssql;
					break;
				case 77:
					mssql = "ALTER VIEW playerTankBattleTotalsView AS " +
							"SELECT        playerTankId, SUM(battles) AS battles, SUM(wins) AS wins, SUM(battles8p) AS battles8p, SUM(losses) AS losses, SUM(survived) AS survived, SUM(frags) AS frags,  " +
							"                         SUM(frags8p) AS frags8p, SUM(dmg) AS dmg, SUM(dmgReceived) AS dmgReceived, SUM(assistSpot) AS assistSpot, SUM(assistTrack) AS assistTrack, SUM(cap)  " +
							"                         AS cap, SUM(def) AS def, SUM(spot) AS spot, SUM(xp) AS xp, SUM(xp8p) AS xp8p, SUM(xpOriginal) AS xpOriginal, SUM(shots) AS shots, SUM(hits) AS hits,  " +
							"                         SUM(heHits) AS heHits, SUM(pierced) AS pierced, SUM(shotsReceived) AS shotsReceived, SUM(piercedReceived) AS piercedReceived, SUM(heHitsReceived)  " +
							"                         AS heHitsReceived, SUM(noDmgShotsReceived) AS noDmgShotsReceived, MAX(maxDmg) AS maxDmg, MAX(maxFrags) AS maxFrags, MAX(maxXp) AS maxXp,  " +
							"                         MAX(battlesCompany) AS battlesCompany, MAX(battlesClan) AS battlesClan, MAX(wn8) AS wn8, MAX(eff) AS eff, MAX(wn7) AS wn7 " +
							"FROM            playerTankBattle " +
							"GROUP BY playerTankId; ";
					sqlite ="DROP VIEW playerTankBattleTotalsView;"+
							"CREATE VIEW playerTankBattleTotalsView AS " +
							"SELECT        playerTankId, SUM(battles) AS battles, SUM(wins) AS wins, SUM(battles8p) AS battles8p, SUM(losses) AS losses, SUM(survived) AS survived, SUM(frags) AS frags,  " +
							"                         SUM(frags8p) AS frags8p, SUM(dmg) AS dmg, SUM(dmgReceived) AS dmgReceived, SUM(assistSpot) AS assistSpot, SUM(assistTrack) AS assistTrack, SUM(cap)  " +
							"                         AS cap, SUM(def) AS def, SUM(spot) AS spot, SUM(xp) AS xp, SUM(xp8p) AS xp8p, SUM(xpOriginal) AS xpOriginal, SUM(shots) AS shots, SUM(hits) AS hits,  " +
							"                         SUM(heHits) AS heHits, SUM(pierced) AS pierced, SUM(shotsReceived) AS shotsReceived, SUM(piercedReceived) AS piercedReceived, SUM(heHitsReceived)  " +
							"                         AS heHitsReceived, SUM(noDmgShotsReceived) AS noDmgShotsReceived, MAX(maxDmg) AS maxDmg, MAX(maxFrags) AS maxFrags, MAX(maxXp) AS maxXp,  " +
							"                         MAX(battlesCompany) AS battlesCompany, MAX(battlesClan) AS battlesClan, MAX(wn8) AS wn8, MAX(eff) AS eff, MAX(wn7) AS wn7 " +
							"FROM            playerTankBattle " +
							"GROUP BY playerTankId; ";
					break;
				case 78:
					Rating.RecalcBattlesWN7();
					break;
				case 79:
					RunDossierFileCheckWithForceUpdate = true;
					break;
				case 80:
					mssql = "UPDATE columnSelection SET position = position + 3 where position >= 132; " +
							"UPDATE columnSelection SET description = 'Damage to enemy tanks done by others after you spotted them' where id = 129; " +
							"UPDATE columnSelection SET description = 'Damage to enemy tanks done by others after you tracked them' where id = 130; " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (188, 1, 132, 'CAST(playerTankBattle.dmg*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Dmg', 'Average damge per battle made on enemy tanks', 'Damage', 50, 'Float'); ";
					sqlite = mssql;
					break;
				case 81:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (189, 1, 133, 'CAST(playerTankBattle.assistSpot*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Dmg Spot', 'Average damage per battle to enemy tanks done by others after you spotted them per battle', 'Damage', 50, 'Float'); " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (190, 1, 134, 'CAST(playerTankBattle.assistTrack*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Dmg Track', 'Average damge per battle to enemy tanks done by others after you tracked them', 'Damage', 50, 'Float'); ";
					sqlite = mssql;
					break;
				case 82:
					mssql = "UPDATE columnSelection SET name='Survival Count', description='Number of battles survived for this battle (could be for several battles recorded in one row)' WHERE id=16; " +
							"UPDATE columnSelection SET name='Survival Rate', description='Survival rate in percent of tank total battles' WHERE id=98; ";
					sqlite = mssql;
					break;
				case 83:
					mssql = "UPDATE columnSelection SET position = position + 1 where position >= 140; " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (191, 1, 140, 'CAST(playerTankBattle.frags*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Frags Avg', 'Average frags (enemy tanks killed) per battle ', 'Result', 50, 'Float'); ";
					sqlite = mssql;
					break;
				case 84:
					mssql = "UPDATE columnSelection SET colName='CAST(battle.frags AS FLOAT)' WHERE id=18; " +
							"UPDATE columnSelection SET colName='CAST(battle.cap AS FLOAT)' WHERE id=24; " +
							"UPDATE columnSelection SET colName='CAST(battle.def AS FLOAT)' WHERE id=25; " +
							"UPDATE columnSelection SET colName='CAST(battle.shots AS FLOAT)' WHERE id=26; " +
							"UPDATE columnSelection SET colName='CAST(battle.hits AS FLOAT)' WHERE id=27; " +
							"UPDATE columnSelection SET colName='CAST(battle.spotted AS FLOAT)' WHERE id=35; " +
							"UPDATE columnSelection SET colName='CAST(tank.tier AS FLOAT)' WHERE id=59; ";
					sqlite = mssql;
					break;
				case 86:
					mssql = "ALTER TABLE columnList ADD lastSortColumn varchar(50) NULL; " +
							"ALTER TABLE columnList ADD lastSortDirectionAsc bit NOT NULL DEFAULT 0; ";
					sqlite = mssql;
					break;
				case 87:
					Config.Settings.gridFontSize = 8;
					string msg = "";
					Config.SaveConfig(out msg);
					break;
				case 88:
					mssql = "UPDATE columnSelection SET name = 'Ventilation' WHERE ID = 68; ";
					sqlite = mssql;
					break;
				case 89:
					mssql = "update playerTank set lastBattleTime = null where lastBattleTime = '1970/01/01'; ";
					sqlite = "update playerTank set lastBattleTime = null where lastbattletime = '1970-01-01 00:00:00'; ";
					break;
				case 90:
					mssql = "UPDATE json2dbMapping SET jsonProperty='piercingsReceived' where jsonProperty='piercedReceived'; " +
							"UPDATE json2dbMapping SET jsonMainSubProperty=jsonMain + '.' + jsonSub + '.' + jsonProperty; " +
							"UPDATE battle SET piercedReceived = 0; ";
					sqlite = mssql.Replace("+","||");
					RunDossierFileCheckWithForceUpdate = true;
					break;
				case 91:
					mssql = "ALTER TABLE columnList ALTER COLUMN lastSortColumn varchar(255) NULL; ";
					// Cannot modify column, need to drop table and related tables, and recreate
					sqlite =
						"alter table columnList rename to columnListToChange; " +
						"alter table columnListSelection rename to columnListSelectionToChange; " +
						"CREATE TABLE columnList (  " +
						"    id                   INTEGER        PRIMARY KEY, " +
						"    colType              INTEGER        NOT NULL, " +
						"    name                 VARCHAR( 50 )  NOT NULL, " +
						"    colDefault           BIT            NOT NULL " +
						"                                        DEFAULT 0, " +
						"    position             INTEGER        NULL, " +
						"    sysCol               BIT            NOT NULL " +
						"                                        DEFAULT 0, " +
						"    defaultFavListId     INTEGER        NOT NULL " +
						"                                        DEFAULT -1, " +
						"    lastSortColumn       VARCHAR(255)   NULL, " +
						"    lastSortDirectionAsc BIT            NOT NULL " +
						"                                        DEFAULT 0  " +
						"); " +
						"CREATE TABLE columnListSelection (  " +
						"    columnSelectionId INTEGER NOT NULL, " +
						"    columnListId      INTEGER NOT NULL, " +
						"    sortorder         INTEGER NOT NULL " +
						"                              DEFAULT 0, " +
						"    colWidth          INTEGER NOT NULL " +
						"                              DEFAULT 50, " +
						"    PRIMARY KEY ( columnSelectionId, columnListId ), " +
						"    FOREIGN KEY ( columnSelectionId ) REFERENCES columnSelection ( id ), " +
						"    FOREIGN KEY ( columnListId ) REFERENCES columnList ( id )  " +
						"); " +
						"INSERT INTO columnList SELECT * FROM  columnListToChange; " +
						"INSERT INTO columnListSelection SELECT * FROM columnListSelectionToChange; " +
						"DROP TABLE columnListSelectionToChange; " +
						"DROP TABLE  columnListToChange; ";
					break;
				case 92:
					mssql = "INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'battlesCount', 'Int', 'battles', 'battlesCount', 'tanks_v2.fortSorties.battlesCount', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'capturePoints', 'Int', 'cap', 'cap', 'tanks_v2.fortSorties.capturePoints', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'damageAssistedRadio', 'Int', 'assistSpot', 'assistSpot', 'tanks_v2.fortSorties.damageAssistedRadio', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'damageAssistedTrack', 'Int', 'assistTrack', 'assistTrack', 'tanks_v2.fortSorties.damageAssistedTrack', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'damageBlockedByArmor', 'Int', 'dmgBlocked', 'dmgBlocked', 'tanks_v2.fortSorties.damageBlockedByArmor', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'damageDealt', 'Int', 'dmg', 'dmg', 'tanks_v2.fortSorties.damageDealt', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'damageReceived', 'Int', 'dmgReceived', 'dmgReceived', 'tanks_v2.fortSorties.damageReceived', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'droppedCapturePoints', 'Int', 'def', 'def', 'tanks_v2.fortSorties.droppedCapturePoints', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'frags', 'Int', 'frags', 'frags', 'tanks_v2.fortSorties.frags', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'frags8p', 'Int', 'frags8p', NULL, 'tanks_v2.fortSorties.frags8p', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'heHitsReceived', 'Int', 'heHitsReceived', NULL, 'tanks_v2.fortSorties.heHitsReceived', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'he_hits', 'Int', 'heHits', NULL, 'tanks_v2.fortSorties.he_hits', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'hits', 'Int', 'hits', 'hits', 'tanks_v2.fortSorties.hits', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'losses', 'Int', 'losses', 'defeat', 'tanks_v2.fortSorties.losses', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'noDamageShotsReceived', 'Int', 'noDmgShotsReceived', NULL, 'tanks_v2.fortSorties.noDamageShotsReceived', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'originalXP', 'Int', 'xpOriginal', NULL, 'tanks_v2.fortSorties.originalXP', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'pierced', 'Int', 'pierced', 'pierced', 'tanks_v2.fortSorties.pierced', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'piercingsReceived', 'Int', 'piercedReceived', 'piercedReceived', 'tanks_v2.fortSorties.piercingsReceived', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'potentialDamageReceived', 'Int', 'potentialDmgReceived', 'potentialDmgReceived', 'tanks_v2.fortSorties.potentialDamageReceived', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'shots', 'Int', 'shots', 'shots', 'tanks_v2.fortSorties.shots', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'shotsReceived', 'Int', 'shotsReceived', 'shotsReceived', 'tanks_v2.fortSorties.shotsReceived', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'spotted', 'Int', 'spot', 'spotted', 'tanks_v2.fortSorties.spotted', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'survivedBattles', 'Int', 'survived', 'survived', 'tanks_v2.fortSorties.survivedBattles', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'wins', 'Int', 'wins', 'victory', 'tanks_v2.fortSorties.wins', 'Strongholds'); " +
							"INSERT INTO json2dbMapping (jsonMain, jsonSub, jsonProperty, dbDataType, dbPlayerTank, dbBattle, jsonMainSubProperty, dbPlayerTankMode) " +
							"VALUES ('tanks_v2', 'fortSorties', 'xp', 'Int', 'xp', 'xp', 'tanks_v2.fortSorties.xp', 'Strongholds'); " +
							"delete from json2dbMapping where jsonMain='tanks_v2' and jsonSub='achievements'; " +
							"delete from json2dbMapping where jsonMain='tanks_v2' and jsonSub='achievements7x7'; ";
					sqlite = mssql;
					break;
				case 93:
					mssql = "ALTER TABLE playerTankBattle ADD dmgBlocked INT NOT NULL DEFAULT 0;" +
							"ALTER TABLE playerTankBattle ADD potentialDmgReceived INT NOT NULL DEFAULT 0;" +
							"ALTER TABLE battle ADD dmgBlocked INT NOT NULL DEFAULT 0;" +
							"ALTER TABLE battle ADD potentialDmgReceived INT NOT NULL DEFAULT 0;";
					sqlite = mssql.Replace("INT", "integer");
					break;
				case 94:
					TankHelper.GetJson2dbMappingFromDB();
					break;
				case 95:
					mssql=  "ALTER TABLE battle ADD credits INT NULL;" +
							"UPDATE columnSelection SET position=158 WHERE ID=162; " +
							"UPDATE columnSelection SET position=position+174 WHERE colType=2 and colGroup='Shooting'; " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (500, 2, 500, 'credits', 'Income', 'Credits net income (without any cost)', 'Credits', 50, 'Int'); ";
					sqlite= "ALTER TABLE battle ADD credits INTEGER NULL;" +
							"UPDATE columnSelection SET position=158 WHERE ID=162; " +
							"UPDATE columnSelection SET position=position+174 WHERE colType=2 and colGroup='Shooting'; " +
							"INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (500, 2, 500, 'credits', 'Income', 'Credits net income (without any cost)', 'Credits', 50, 'Int'); ";
					break;
				case 96:
					mssql = "ALTER TABLE battle ADD arenaUniqueID BIGINT NULL;";
					sqlite = mssql.Replace("BIGINT", "INTEGER");
					break;
				case 97:
					mssql = "ALTER TABLE battle ADD autoRepairCost INT NULL;" +
							"ALTER TABLE battle ADD autoLoadCost INT NULL;" +
							"ALTER TABLE battle ADD creditsPenalty INT NULL;" +
							"ALTER TABLE battle ADD creditsContributionIn INT NULL;" +
							"ALTER TABLE battle ADD creditsContributionOut INT NULL;" +
							"ALTER TABLE battle ADD creditsToDraw INT NULL;" +
							"ALTER TABLE battle ADD eventCredits INT NULL;" +
							"ALTER TABLE battle ADD originalCredits INT NULL;" +
							"ALTER TABLE battle ADD premiumCreditsFactor10 INT NULL;" +
							"ALTER TABLE battle ADD arenaTypeID INT NULL;" +
							"ALTER TABLE battle ADD bonusType INT NULL;" +
							"ALTER TABLE battle ADD bonusTypeName VARCHAR(255) NULL;" +
							"ALTER TABLE battle ADD finishReasonName VARCHAR(255) NULL;" +
							"ALTER TABLE battle ADD deathReason VARCHAR(255) NULL;" +
							"ALTER TABLE battle ADD markOfMastery INT NULL;" +
							"ALTER TABLE battle ADD vehTypeLockTime INT NULL;" +
							"ALTER TABLE battle ADD real_xp INT NULL;" + // xp in battle_result
							"ALTER TABLE battle ADD xpPenalty INT NULL;" +
							"ALTER TABLE battle ADD freeXP INT NULL;" +
							"ALTER TABLE battle ADD dailyXPFactor10 INT NULL;" +
							"ALTER TABLE battle ADD premiumXPFactor10 INT NULL;" +
							"ALTER TABLE battle ADD eventFreeXP INT NULL;" +
							"ALTER TABLE battle ADD fortResource INT NULL;" +
							"ALTER TABLE battle ADD marksOnGun INT NULL;";
					sqlite = mssql.Replace("INT", "INTEGER");
					break;	
				case 98:
					mssql = "ALTER TABLE battle ADD achievementCredits INT NULL;" + // missions?
							"ALTER TABLE battle ADD achievementFreeXP INT NULL;" + // missions?
							"ALTER TABLE battle ADD achievementXP INT NULL;"; // missions?
					sqlite = mssql.Replace("INT", "INTEGER");
					break;
				case 99:
					mssql = "ALTER TABLE battle ADD gameplayName VARCHAR(255) NULL;";
					sqlite = mssql;
					break;	
				case 100:
					mssql = "ALTER TABLE battle ADD eventXP INT NULL;";
					sqlite = mssql.Replace("INT", "INTEGER");
					break;	
				case 101:
					mssql = "ALTER TABLE battle ADD eventTMenXP INT NULL;";
					sqlite = mssql.Replace("INT", "INTEGER");
					break;
				case 102:
					mssql = "ALTER TABLE battle ADD creditsNet INT NULL;"; // calculated
					sqlite = mssql.Replace("INT", "INTEGER");
					break;	
				case 103:
					mssql = "ALTER TABLE battle ADD autoEquipCost INT NULL;"; // calculated
					sqlite = mssql.Replace("INT", "INTEGER");
					break;
				case 104:
					mssql = "ALTER TABLE battle ADD mapId INT NULL;"; // calculated
					sqlite = mssql.Replace("INT", "INTEGER");
					break;	
				case 105:
					mssql = "CREATE TABLE map (id int primary key, name varchar(255) not null);";
					sqlite = "CREATE TABLE map (id integer primary key, name varchar(255) not null);";
					break;	
				case 106:
					mssql = "INSERT INTO map (id, name) VALUES (1,'Karelia'); " +
							"INSERT INTO map (id, name) VALUES (2,'Malinovka'); " +
							"INSERT INTO map (id, name) VALUES (3,'Himmelsdorf'); " +
							"INSERT INTO map (id, name) VALUES (4,'Prokhorovka'); " +
							"INSERT INTO map (id, name) VALUES (5,'Lakeville'); " +
							"INSERT INTO map (id, name) VALUES (6,'Ensk'); " +
							"INSERT INTO map (id, name) VALUES (7,'Murovanka'); " +
							"INSERT INTO map (id, name) VALUES (8,'Erlenberg'); " +
							"INSERT INTO map (id, name) VALUES (9,'Mines'); " +
							"INSERT INTO map (id, name) VALUES (10,'Komarin'); " +
							"INSERT INTO map (id, name) VALUES (11,'Cliff'); " +
							"INSERT INTO map (id, name) VALUES (12,'Abbey'); " +
							"INSERT INTO map (id, name) VALUES (13,'Sand River'); " +
							"INSERT INTO map (id, name) VALUES (14,'Steppes'); " +
							"INSERT INTO map (id, name) VALUES (15,'Mountain Pass'); " +
							"INSERT INTO map (id, name) VALUES (16,'Fjords'); " +
							"INSERT INTO map (id, name) VALUES (17,'Redshire'); " +
							"INSERT INTO map (id, name) VALUES (18,'Fishermans Bay'); " +
							"INSERT INTO map (id, name) VALUES (19,'Arctic Region'); " +
							"INSERT INTO map (id, name) VALUES (20,'Ruinberg'); " +
							"INSERT INTO map (id, name) VALUES (21,'Siegfried Line'); " +
							"INSERT INTO map (id, name) VALUES (22,'Swamp'); " +
							"INSERT INTO map (id, name) VALUES (23,'Westfield'); " +
							"INSERT INTO map (id, name) VALUES (24,'El Halluf'); " +
							"INSERT INTO map (id, name) VALUES (26,'Airfield'); " +
							"INSERT INTO map (id, name) VALUES (27,'Province'); " +
							"INSERT INTO map (id, name) VALUES (28,'Widepark'); " +
							"INSERT INTO map (id, name) VALUES (31,'Live Oaks'); " +
							"INSERT INTO map (id, name) VALUES (32,'South Coast'); " +
							"INSERT INTO map (id, name) VALUES (33,'Northwest'); " +
							"INSERT INTO map (id, name) VALUES (34,'Highway'); " +
							"INSERT INTO map (id, name) VALUES (36,'Port'); " +
							"INSERT INTO map (id, name) VALUES (43,'Hidden Village'); " +
							"INSERT INTO map (id, name) VALUES (44,'Dragon Ridge'); " +
							"INSERT INTO map (id, name) VALUES (49,'Serene Coast'); " +
							"INSERT INTO map (id, name) VALUES (50,'Severogorsk'); " +
							"INSERT INTO map (id, name) VALUES (51,'Sacred Valley'); " +
							"INSERT INTO map (id, name) VALUES (52,'Pearl River'); " +
							"INSERT INTO map (id, name) VALUES (53,'Training area'); " +
							"INSERT INTO map (id, name) VALUES (55,'Tundra'); " +
							"INSERT INTO map (id, name) VALUES (56,'Windstorm'); " +
							"INSERT INTO map (id, name) VALUES (57,'Winter Himmelsdor'); " +
							"INSERT INTO map (id, name) VALUES (58,'Ruinberg on Fire'); " +
							"INSERT INTO map (id, name) VALUES (60,'Kharkov'); " +
							"INSERT INTO map (id, name) VALUES (61,'Himmelsdorf Champion'); " +
							"INSERT INTO map (id, name) VALUES (62,'Fire Arc'); " +
							"INSERT INTO map (id, name) VALUES (1957,'Pearl River'); " +
							"INSERT INTO map (id, name) VALUES (1983,'Sacred Valley'); " +
							"INSERT INTO map (id, name) VALUES (2021,'Training area'); ";
					sqlite = mssql;
					break;
				case 107:
					string s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql = s + "VALUES (501, 2, 501, 'originalCredits', 'Original Income', 'Credits income without premium account', 'Credits', 50, 'Int'); " +
							s + "VALUES (502, 2, 502, 'eventCredits', 'Event Income', 'Credits income as part of mission', 'Credits', 50, 'Int'); " +
							s + "VALUES (503, 2, 503, 'autoRepairCost', 'Repair Cost', 'Credits spent for auto repair vehicle', 'Credits', 50, 'Int'); " +
							s + "VALUES (504, 2, 504, 'autoLoadCost', 'Ammo Cost', 'Credits spent for auto resupply ammunitions', 'Credits', 50, 'Int'); " +
							s + "VALUES (505, 2, 505, 'autoEquipCost', 'Eqip Cost', 'Credits spent for auto resupply consumables', 'Credits', 50, 'Int'); " +
							s + "VALUES (506, 2, 506, 'creditsPenalty', 'Penalty Cost', 'Credits spent for causing damage to allies', 'Credits', 50, 'Int'); " +
							s + "VALUES (507, 2, 507, 'creditsToDraw', 'Comp Income', 'Credits income for compensation for damage caused by allies', 'Credits', 50, 'Int'); " +
							s + "VALUES (508, 2, 508, 'creditsNet', 'Income Battle', 'Total battle credits income, if spent more than earned the total is negative', 'Credits', 50, 'Int'); " +
							s + "VALUES (509, 2, 511, 'deathReason', 'Death Reason', 'If and how you was killed (Alive, Shot, Burned, Rammed, Crashed, Death zone, Drowned)', 'Result', 100, 'VarChar'); " +
							s + "VALUES (510, 2, 513, 'markOfMastery', 'Mastery Badge', 'The mastery badge achieved in this battle (4=Ace Tanker, 3=I Class, 2=II Class, 1=III Class, 0=None)', 'Rating', 40, 'Int'); " +
							s + "VALUES (511, 2, 514, 'gameplayName', 'Game Type', 'The gameplay mode (CTF = Capture The Flag, Assault, Domination = Encounter', 'Battle', 100, 'VarChar'); " +
							s + "VALUES (512, 2, 515, 'map.name', 'Map', 'The map played the battle on', 'Battle', 100, 'Varchar'); " +
							s + "VALUES (513, 2, 510, 'finishReasonName', 'Finish Reason', 'How the game ended (Extermination, Base capture, Timeout)', 'Result', 80, 'VarChar'); " +
							s + "VALUES (514, 2, 517, 'xpPenalty', 'Fine XP', 'Fine in XP for causing damage to alliesXP', 'XP', 50, 'Int'); " +
							s + "VALUES (515, 2, 518, 'freeXP', 'Free XP', 'Free Xp earned for the battle', 'XP', 50, 'Int'); " +
							s + "VALUES (516, 2, 519, 'dailyXPFactor10', 'XP Factor', 'XP', 'XP', 50, 'Int'); " +
							s + "VALUES (517, 2, 520, 'eventFreeXP', 'Free Event XP', 'XP', 'XP', 50, 'Int'); " +
							s + "VALUES (518, 2, 521, 'eventXP', 'Event XP', 'XP earned due to mission', 'XP', 50, 'Int'); " +
							s + "VALUES (519, 2, 516, 'real_xp', 'Total XP', 'Total XP earned for the battle, ', 'XP', 50, 'Int'); " +
							"UPDATE columnSelection SET description='The mastery badge achieved with the tank (4=Ace Tanker, 3=I Class, 2=II Class, 1=III Class, 0=None)' WHERE id=53; " +
							"UPDATE columnSelection SET colGroup='XP', name='Base XP' WHERE id=38; " +
							"UPDATE columnSelection SET colGroup='Battle' WHERE colGroup='Mode' and colType=2; ";
					sqlite = mssql;
					break;
				case 108:
					mssql = "UPDATE columnSelection SET position=1, name='Tank', description='Tank name', colGroup='Tank' WHERE id=58; " +
							 "UPDATE columnSelection SET position=2, name='Tank Icon', description='Tank icon (contour image)', colGroup='Tank' WHERE id=183; " +
							 "UPDATE columnSelection SET position=3, name='Tank Image', description='Tank image (small), suitable for grid', colGroup='Tank' WHERE id=184; " +
							 "UPDATE columnSelection SET position=4, name='Tank Image Large', description='Tank large image, will only partly show if not expanding row height in grid', colGroup='Tank' WHERE id=185; " +
							 "UPDATE columnSelection SET position=12, name='Tier', description='Tank tier (1-10)', colGroup='Tank' WHERE id=59; " +
							 "UPDATE columnSelection SET position=13, name='Premium', description='Tank premium (yes/no)', colGroup='Tank' WHERE id=2; " +
							 "UPDATE columnSelection SET position=14, name='Tank Type', description='Tank type full name', colGroup='Tank' WHERE id=3; " +
							 "UPDATE columnSelection SET position=15, name='Type', description='Tank type short name (LT, MT, HT, TD, SPG)', colGroup='Tank' WHERE id=4; " +
							 "UPDATE columnSelection SET position=16, name='Tank Nation', description='Tank nation full name', colGroup='Tank' WHERE id=5; " +
							 "UPDATE columnSelection SET position=17, name='Nation', description='Tank nation short name (CHI, FRA, GET, JAP, UK, USA, USR)', colGroup='Tank' WHERE id=6; " +
							 "UPDATE columnSelection SET position=18, name='ID', description='Wargaming ID for tank', colGroup='Tank' WHERE id=39; " +
							 "UPDATE columnSelection SET position=110, name='DateTime', description='Battle date and time, the date/time the battle was finished', colGroup='Battle' WHERE id=8; " +
							 "UPDATE columnSelection SET position=111, name='Date', description='Battle date, the date (DD/MM/YYYY) the battle was finished', colGroup='Battle' WHERE id=163; " +
							 "UPDATE columnSelection SET position=112, name='Time', description='Battle time, the time (HH:MM:SS) when the battle was finished', colGroup='Battle' WHERE id=164; " +
							 "UPDATE columnSelection SET position=113, name='Life Time', description='Time staying alive in battle in seconds', colGroup='Battle' WHERE id=9; " +
							 "UPDATE columnSelection SET position=115, name='Battle Mode', description='Battle mode, 15 = Random Battles, Tank Company and Clan Wars, 7 = Team Battle (Historical Battles not included yet)', colGroup='Battle' WHERE id=162; " +
							 "UPDATE columnSelection SET position=116, name='Game Type', description='The gameplay mode (CTF = Capture The Flag, Assault, Domination = Encounter', colGroup='Battle' WHERE id=511; " +
							 "UPDATE columnSelection SET position=120, name='Map', description='The map played the battle on', colGroup='Battle' WHERE id=512; " +
							 "UPDATE columnSelection SET position=130, name='Result', description='The result for battle (Victory, Draw, Defeat or Several if a combination occur when recorded several battles for one row)', colGroup='Battle' WHERE id=10; " +
							 "UPDATE columnSelection SET position=135, name='Survived', description='If survived in battle (Yes / No or Several if a combination occur when recorded several battles for one row)', colGroup='Battle' WHERE id=11; " +
							 "UPDATE columnSelection SET position=160, name='Finish Reason', description='How the game ended (Extermination, Base capture, Timeout)', colGroup='Battle' WHERE id=513; " +
							 "UPDATE columnSelection SET position=161, name='Death Reason', description='If and how you was killed (Alive, Shot, Burned, Rammed, Crashed, Death zone, Drowned)', colGroup='Battle' WHERE id=509; " +
							 "UPDATE columnSelection SET position=200, name='Frags', description='Number of enemy tanks you killed (frags)', colGroup='Result' WHERE id=18; " +
							 "UPDATE columnSelection SET position=210, name='Dmg', description='Damage to enemy tanks by you (shooting, ramming, put on fire)', colGroup='Result' WHERE id=19; " +
							 "UPDATE columnSelection SET position=211, name='Dmg Spot', description='Assisted damage casued by others to enemy tanks due to you spotting the enemy tank', colGroup='Result' WHERE id=21; " +
							 "UPDATE columnSelection SET position=212, name='Dmg Track', description='Assisted damage casued by others to enemy tanks due to you tracking of the enemy tank', colGroup='Result' WHERE id=22; " +
							 "UPDATE columnSelection SET position=213, name='Dmg Received', description='The damage received on your tank', colGroup='Result' WHERE id=20; " +
							 "UPDATE columnSelection SET position=220, name='Cap', description='Cap ponts you achived by staying in cap circle (0 - 100)', colGroup='Result' WHERE id=24; " +
							 "UPDATE columnSelection SET position=230, name='Def', description='Cap points reduced by damaging enemy tanks capping', colGroup='Result' WHERE id=25; " +
							 "UPDATE columnSelection SET position=240, name='Spot', description='Enemy tanks spotted (only first spot on enemy tank counts)', colGroup='Result' WHERE id=35; " +
							 "UPDATE columnSelection SET position=250, name='Mileage', description='Distance driving the tank', colGroup='Result' WHERE id=36; " +
							 "UPDATE columnSelection SET position=260, name='Trees Cut', description='Number of trees overturned by driving into it', colGroup='Result' WHERE id=37; " +
							 "UPDATE columnSelection SET position=280, name='Mastery Badge', description='The mastery badge achieved in this battle (4=Ace Tanker, 3=I Class, 2=II Class, 1=III Class, 0=None)', colGroup='Rating' WHERE id=510; " +
							 "UPDATE columnSelection SET position=281, name='EFF', description='Calculated battle efficiency rating', colGroup='Rating' WHERE id=40; " +
							 "UPDATE columnSelection SET position=282, name='WN7', description='Calculated battle WN7 rating (according to formula from vBAddict)', colGroup='Rating' WHERE id=186; " +
							 "UPDATE columnSelection SET position=283, name='WN8', description='Calculated battle WN8 (WRx) rating (according to formula from vbAddict)', colGroup='Rating' WHERE id=47; " +
							 "UPDATE columnSelection SET position=400, name='XP Factor', description='XP factor for the battle (10 = normal, 20 = 2X, 30 = 3X, 50 = 5X)', colGroup='XP' WHERE id=516; " +
							 "UPDATE columnSelection SET position=405, name='Base XP', description='Base XP earned, not included: 2X (or more) for first victory or bonuses', colGroup='XP' WHERE id=38; " +
							 "UPDATE columnSelection SET position=410, name='Total XP', description='Total XP earned for the battle', colGroup='XP' WHERE id=519; " +
							 "UPDATE columnSelection SET position=430, name='Event XP', description='Part of the total XP earned due to mission', colGroup='XP' WHERE id=518; " +
							 "UPDATE columnSelection SET position=450, name='Free XP', description='Free Xp earned for the battle', colGroup='XP' WHERE id=515; " +
							 "UPDATE columnSelection SET position=480, name='Free Event XP', description='Part of the free XP earned du to mission', colGroup='XP' WHERE id=517; " +
							 "UPDATE columnSelection SET position=490, name='Fine XP', description='Fine for causing damage to alliesXP, this has reduced the total XP', colGroup='XP' WHERE id=514; " +
							 "UPDATE columnSelection SET position=500, name='Credits Result', description='Total battle credits result, if spent more than earned the total is negative', colGroup='Credits' WHERE id=508; " +
							 "UPDATE columnSelection SET position=510, name='Income Total', description='Credits net income total, if premium account the extra is added', colGroup='Credits' WHERE id=500; " +
							 "UPDATE columnSelection SET position=515, name='Income No Prem', description='Credits income without premium account', colGroup='Credits' WHERE id=501; " +
							 "UPDATE columnSelection SET position=520, name='Income Event', description='Part of the total income earned due to mission', colGroup='Credits' WHERE id=502; " +
							 "UPDATE columnSelection SET position=525, name='Income Compen', description='Credits income for compensation for damage caused by allies', colGroup='Credits' WHERE id=507; " +
							 "UPDATE columnSelection SET position=550, name='Cost Repair', description='Credits spent for auto repair vehicle', colGroup='Credits' WHERE id=503; " +
							 "UPDATE columnSelection SET position=552, name='Cost Ammo', description='Credits spent for auto resupply ammunitions', colGroup='Credits' WHERE id=504; " +
							 "UPDATE columnSelection SET position=554, name='Cost Equip', description='Credits spent for auto resupply consumables', colGroup='Credits' WHERE id=505; " +
							 "UPDATE columnSelection SET position=556, name='Cost Penalty', description='Credits spent for causing damage to allies', colGroup='Credits' WHERE id=506; " +
							 "UPDATE columnSelection SET position=600, name='Shots', description='Number of shots you fired', colGroup='Shooting' WHERE id=26; " +
							 "UPDATE columnSelection SET position=601, name='Hits', description='Number of hits from you shots', colGroup='Shooting' WHERE id=27; " +
							 "UPDATE columnSelection SET position=602, name='Hit Rate', description='Persentage hits (hits*100/shots)', colGroup='Shooting' WHERE id=28; " +
							 "UPDATE columnSelection SET position=603, name='Pierced', description='Number of pierced shots', colGroup='Shooting' WHERE id=30; " +
							 "UPDATE columnSelection SET position=604, name='Pierced Shots%', description='Persentage pierced hits based on total shots (pierced*100/shots)', colGroup='Shooting' WHERE id=32; " +
							 "UPDATE columnSelection SET position=605, name='Pierced Hits%', description='Persentage pierced hits based on total hits (pierced*100/hits)', colGroup='Shooting' WHERE id=33; " +
							 "UPDATE columnSelection SET position=606, name='HE Hits', description='Number of HE hits', colGroup='Shooting' WHERE id=159; " +
							 "UPDATE columnSelection SET position=607, name='HE Shots %', description='Percentage HE hits based on total shots (hi hits*100/shots)', colGroup='Shooting' WHERE id=160; " +
							 "UPDATE columnSelection SET position=608, name='HE Hts %', description='Percentage HE hits based on total hits (he hits*100/hits)', colGroup='Shooting' WHERE id=161; " +
							 "UPDATE columnSelection SET position=609, name='Shots Reveived', description='Number of shots received', colGroup='Shooting' WHERE id=29; " +
							 "UPDATE columnSelection SET position=610, name='Pierced Received', description='Number of pierced shots received', colGroup='Shooting' WHERE id=31; " +
							 "UPDATE columnSelection SET position=611, name='HE Received', description='Number of HE shots received', colGroup='Shooting' WHERE id=157; " +
							 "UPDATE columnSelection SET position=612, name='No Dmg Shots Received', description='Number of no damaging shots received', colGroup='Shooting' WHERE id=158; " +
							 "UPDATE columnSelection SET position=710, name='Killed Count', description='Number of battles where killed (not survived) for this row (normally 0/1, or more if battle result is Several)', colGroup='Count' WHERE id=17; " +
							 "UPDATE columnSelection SET position=711, name='Victory', description='Number of victory battles for this row (normally 0/1, or more if battle result is Several)', colGroup='Count' WHERE id=13; " +
							 "UPDATE columnSelection SET position=712, name='Draw', description='Number of drawed battles for this row (normally 0/1, or more if battle result is Several)', colGroup='Count' WHERE id=14; " +
							 "UPDATE columnSelection SET position=713, name='Defeat', description='Number of defeated battles for this row (normally 0/1, or more if battle result is Several)', colGroup='Count' WHERE id=15; " +
							 "UPDATE columnSelection SET position=714, name='Survival Count', description='Number of battles survived for this battle (could be for several battles recorded in one row)', colGroup='Count' WHERE id=16; " +
							 "UPDATE columnSelection SET position=715, name='Clan', description='Number of Clan battles for this row (normally 0/1, or more if battle result is Several)', colGroup='Count' WHERE id=43; " +
							 "UPDATE columnSelection SET position=716, name='Company', description='Number of Tank Company battles for this row (normally 0/1, or more if battle result is Several)', colGroup='Count' WHERE id=45; " +
							 "UPDATE columnSelection SET position=717, name='Battle Count', description='Battle count, number of battles for the row', colGroup='Count' WHERE id=7; ";
					sqlite = mssql;
					break;
				case 109:
					mssql = "UPDATE columnSelection SET colName='battle.markOfMastery' WHERE id=510; ";
					sqlite = mssql;
					break;
				case 110:
					mssql = "ALTER TABLE battle ADD dailyXPFactorTxt VARCHAR(10) NULL;" +
							"UPDATE columnSelection SET description='XP factor for the battle (1X, 2X, 3X, 5X)', colName='dailyXPFactorTxt', colDataType='VarChar' WHERE id=516; ";
					sqlite = mssql;
					break;
				case 111:
					mssql = "UPDATE battle SET dailyXPFactorTxt = '1 X' where dailyXPFactor10 = 10;" +
							"UPDATE battle SET dailyXPFactorTxt = '2 X' where dailyXPFactor10 = 20;" +
							"UPDATE battle SET dailyXPFactorTxt = '3 X' where dailyXPFactor10 = 30;" +
							"UPDATE battle SET dailyXPFactorTxt = '5 X' where dailyXPFactor10 = 50;";
					sqlite = mssql;
					break;
				case 113:
					mssql = "UPDATE map SET name='Winter Himmelsdorf' WHERE id=57";
					sqlite = mssql;
					break;
				case 115:
					mssql = "UPDATE columnSelection SET position=position + 82 where colType=1 and position >117; " +
							"UPDATE columnSelection SET position=118 where id=49; ";
					sqlite = mssql;
					break;
				case 116:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql = s + "VALUES (192, 1, 119, 'tank.expDmg', 'Exp Dmg', 'WN8 Expected value: Damage', 'Rating', 50, 'Float'); " +
							s + "VALUES (193, 1, 120, 'tank.expWR', 'Exp Win Rate', 'WN8 Expected value: Win Rate', 'Rating', 50, 'Float'); " +
							s + "VALUES (194, 1, 121, 'tank.expSpot', 'Exp Spot', 'WN8 Expected value: Spotting', 'Rating', 50, 'Float'); " +
							s + "VALUES (195, 1, 122, 'tank.expFrags', 'Exp Frags', 'WN8 Expected value: Frags', 'Rating', 50, 'Float'); " +
							s + "VALUES (196, 1, 123, 'tank.expDef', 'Exp Def', 'WN8 Expected value: Defence points', 'Rating', 50, 'Float'); " +
							s + "VALUES (197, 2, 290, 'tank.expDmg', 'Exp Dmg', 'WN8 Expected value: Damage', 'Rating', 50, 'Float'); " +
							s + "VALUES (198, 2, 291, 'tank.expWR', 'Exp Win Rate', 'WN8 Expected value: Win Rate', 'Rating', 50, 'Float'); " +
							s + "VALUES (199, 2, 292, 'tank.expSpot', 'Exp Spot', 'WN8 Expected value: Spotting', 'Rating', 50, 'Float'); " +
							s + "VALUES (200, 2, 293, 'tank.expFrags', 'Exp Frags', 'WN8 Expected value: Frags', 'Rating', 50, 'Float'); " +
							s + "VALUES (201, 2, 294, 'tank.expDef', 'Exp Def', 'WN8 Expected value: Defence points', 'Rating', 50, 'Float'); " +
							s + "VALUES (202, 2, 231, 'case when def > 100 then 100 else def end', 'Def Cropped', 'Cropped to max 100 cap points reduced by damaging enemy tanks capping', 'Result', 50, 'Int'); ";
					sqlite = mssql;
					break;
				case 117:
					mssql = "UPDATE columnSelection SET position=position + 1 where colType=1 and position >226; " +
							"UPDATE columnSelection SET position=position + 1 where colType=1 and position >225; " +
							"UPDATE columnSelection SET position=position + 1 where colType=1 and position >224; " +
							"UPDATE columnSelection SET name='Multiplier' where id=516; ";

					sqlite = mssql;
					break;
				case 118:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql = s + "VALUES (203, 1, 225, 'CAST(playerTankBattle.cap*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Cap', 'Average capping points per battle', 'Result', 50, 'Float'); " +
							s + "VALUES (204, 1, 227, 'CAST(playerTankBattle.def*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Def', 'Average defence points per battle', 'Result', 50, 'Float'); " +
							s + "VALUES (205, 1, 229, 'CAST(playerTankBattle.spot*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Spot', 'Average enemy tanks spotted per battles', 'Result', 50, 'Float'); "+
							"UPDATE columnSelection SET name='Avg Frags' where id=191; ";
					sqlite = mssql;
					break;
				case 121:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql =
						s + "VALUES (900, 3, 1, @emptyCol, ' - Separator 0 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (901, 3, 1, @emptyCol, ' - Separator 1 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (902, 3, 1, @emptyCol, ' - Separator 2 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (903, 3, 1, @emptyCol, ' - Separator 3 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (904, 3, 1, @emptyCol, ' - Separator 4 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (905, 3, 1, @emptyCol, ' - Separator 5 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (906, 3, 1, @emptyCol, ' - Separator 6 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (907, 3, 1, @emptyCol, ' - Separator 7 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (908, 3, 1, @emptyCol, ' - Separator 8 -', 'Separator line', 'Separator', 5, 'VarChar'); " +
						s + "VALUES (909, 3, 1, @emptyCol, ' - Separator 9 -', 'Separator line', 'Separator', 5, 'VarChar'); ";
					DB.AddWithValue(ref mssql, "@emptyCol", "''", DB.SqlDataType.VarChar);
					sqlite = mssql;
					break;
				case 123:
					mssql = "UPDATE columnSelection SET colWidth=3 WHERE colType=3; ";
					sqlite = mssql;
					break;
				case 125:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql =
						s + "VALUES (910, 3, 1, @emptyCol, ' - Separator 10 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (911, 3, 1, @emptyCol, ' - Separator 11 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (912, 3, 1, @emptyCol, ' - Separator 12 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (913, 3, 1, @emptyCol, ' - Separator 13 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (914, 3, 1, @emptyCol, ' - Separator 14 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (915, 3, 1, @emptyCol, ' - Separator 15 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (916, 3, 1, @emptyCol, ' - Separator 16 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (917, 3, 1, @emptyCol, ' - Separator 17 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (918, 3, 1, @emptyCol, ' - Separator 18 -', 'Separator line', 'Separator', 3, 'VarChar'); " +
						s + "VALUES (919, 3, 1, @emptyCol, ' - Separator 19 -', 'Separator line', 'Separator', 3, 'VarChar'); ";
					DB.AddWithValue(ref mssql, "@emptyCol", "''", DB.SqlDataType.VarChar);
					sqlite = mssql;
					break;
				case 126:
					s = "INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) ";
					mssql =
						s + "VALUES ('tanks_v2','a15x15_2', 'potentialDamageReceived','Int','potentialDmgReceived','potentialDmgReceived', NULL,'tanks_v2.a15x15_2.potentialDamageReceived','15'); " +
						s + "VALUES ('tanks_v2','a7x7', 'potentialDamageReceived','Int','potentialDmgReceived','potentialDmgReceived', NULL,'tanks_v2.a7x7.potentialDamageReceived','7'); " +
						s + "VALUES ('tanks_v2','a15x15_2', 'damageBlockedByArmor','Int','dmgBlocked','dmgBlocked', NULL,'tanks_v2.a15x15_2.damageBlockedByArmor','15'); " +
						s + "VALUES ('tanks_v2','a7x7', 'damageBlockedByArmor','Int','dmgBlocked','dmgBlocked', NULL,'tanks_v2.a7x7.damageBlockedByArmor','7'); ";
					sqlite = mssql;
					break;
				case 127:
					s = "INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) ";
					mssql =
						s + "VALUES ('tanks_v2','historical', 'damageBlockedByArmor','Int','dmgBlocked','dmgBlocked', NULL,'tanks_v2.historical.damageBlockedByArmor','7'); " +
						s + "VALUES ('tanks_v2','historical', 'potentialDamageReceived','Int','potentialDmgReceived','potentialDmgReceived', NULL,'tanks_v2.historical.potentialDamageReceived','7'); ";
						sqlite = mssql;
					break;
				case 128:
					RunDossierFileCheckWithForceUpdate = true;
					break;
				case 129:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql =
						"UPDATE columnSelection SET position=position+20 where colType=1 and position>220;" +
						s + "VALUES (206, 1, 220, 'dmgBlocked', 'Dmg Blocked', 'Damage blocked by armor', 'Damange', 50, 'Int'); " +
						s + "VALUES (207, 1, 221, 'CAST(playerTankBattle.dmgBlocked*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Dmg Blocked', 'Average damage blocked by armor per battle', 'Damage', 50, 'Float'); " +
						s + "VALUES (208, 1, 222, 'potentialDmgReceived', 'Received Pot Dmg', 'Potential damage received', 'Damage', 50, 'Int'); " +
						s + "VALUES (209, 1, 223, 'CAST(playerTankBattle.potentialDmgReceived*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Received Avg Pot Dmg', 'Average potential damage received per battles', 'Damage', 50, 'Float'); " +
						s + "VALUES (210, 1, 224, 'CAST(playerTankBattle.dmgReceived*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Received Avg Dmg', 'Average damage received per battles', 'Damage', 50, 'Float'); " +
						s + "VALUES (211, 1, 225, 'playerTankBattle.assistSpot+playerTankBattle.assistTrack', 'Dmg Assist', 'Average sum damage done by spotting and tracking', 'Damage', 50, 'Int'); " +
						s + "VALUES (212, 1, 226, 'CAST((playerTankBattle.assistSpot+playerTankBattle.assistTrack)*10/nullif(playerTankBattle.battles,0) as FLOAT) / 10', 'Avg Dmg Assist', 'Average damage done by spotting and tracking per battles', 'Damage', 50, 'Float'); ";
					sqlite = mssql;
					break;
				case 130:
					TankHelper.GetJson2dbMappingFromDB();
					break;
				case 132:
					mssql =
						"ALTER VIEW playerTankBattleTotalsView AS " +
						"SELECT        playerTankId, SUM(battles) AS battles, SUM(wins) AS wins, SUM(battles8p) AS battles8p, SUM(losses) AS losses, SUM(survived) AS survived, SUM(frags) AS frags,  " +
						"                         SUM(frags8p) AS frags8p, SUM(dmg) AS dmg, SUM(dmgReceived) AS dmgReceived, SUM(assistSpot) AS assistSpot, SUM(assistTrack) AS assistTrack, SUM(cap)  " +
						"                         AS cap, SUM(def) AS def, SUM(spot) AS spot, SUM(xp) AS xp, SUM(xp8p) AS xp8p, SUM(xpOriginal) AS xpOriginal, SUM(shots) AS shots, SUM(hits) AS hits,  " +
						"                         SUM(heHits) AS heHits, SUM(pierced) AS pierced, SUM(shotsReceived) AS shotsReceived, SUM(piercedReceived) AS piercedReceived, SUM(heHitsReceived)  " +
						"                         AS heHitsReceived, SUM(noDmgShotsReceived) AS noDmgShotsReceived, MAX(maxDmg) AS maxDmg, MAX(maxFrags) AS maxFrags, MAX(maxXp) AS maxXp,  " +
						"                         MAX(battlesCompany) AS battlesCompany, MAX(battlesClan) AS battlesClan, MAX(wn8) AS wn8, MAX(eff) AS eff, MAX(wn7) AS wn7, SUM(dmgBlocked) AS dmgBlocked,  " +
						"                         SUM(potentialDmgReceived) AS potentialDmgReceived " +
						"FROM            playerTankBattle " +
						"GROUP BY playerTankId; ";
					sqlite = 
						"DROP VIEW playerTankBattleTotalsView;" +
						"CREATE VIEW playerTankBattleTotalsView AS " +
						"SELECT        playerTankId, SUM(battles) AS battles, SUM(wins) AS wins, SUM(battles8p) AS battles8p, SUM(losses) AS losses, SUM(survived) AS survived, SUM(frags) AS frags,  " +
						"                         SUM(frags8p) AS frags8p, SUM(dmg) AS dmg, SUM(dmgReceived) AS dmgReceived, SUM(assistSpot) AS assistSpot, SUM(assistTrack) AS assistTrack, SUM(cap)  " +
						"                         AS cap, SUM(def) AS def, SUM(spot) AS spot, SUM(xp) AS xp, SUM(xp8p) AS xp8p, SUM(xpOriginal) AS xpOriginal, SUM(shots) AS shots, SUM(hits) AS hits,  " +
						"                         SUM(heHits) AS heHits, SUM(pierced) AS pierced, SUM(shotsReceived) AS shotsReceived, SUM(piercedReceived) AS piercedReceived, SUM(heHitsReceived)  " +
						"                         AS heHitsReceived, SUM(noDmgShotsReceived) AS noDmgShotsReceived, MAX(maxDmg) AS maxDmg, MAX(maxFrags) AS maxFrags, MAX(maxXp) AS maxXp,  " +
						"                         MAX(battlesCompany) AS battlesCompany, MAX(battlesClan) AS battlesClan, MAX(wn8) AS wn8, MAX(eff) AS eff, MAX(wn7) AS wn7, SUM(dmgBlocked) AS dmgBlocked,  " +
						"                         SUM(potentialDmgReceived) AS potentialDmgReceived " +
						"FROM            playerTankBattle " +
						"GROUP BY playerTankId; ";
					break;
				case 133:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql =
						"UPDATE columnSelection SET position=position+20 where colType=1 and position>220;" +
						s + "VALUES (214, 2, 215, 'battle.dmgBlocked', 'Dmg Blocked', 'Damage blocked by armor', 'Result', 50, 'Int'); " +
						s + "VALUES (215, 2, 216, 'battle.potentialDmgReceived', 'Dmg Pot Received', 'Potential damage received', 'Result', 50, 'Int'); " +
						s + "VALUES (213, 2, 214, 'battle.assistSpot+battle.assistTrack', 'Dmg Assist', 'Average sum damage done by spotting and tracking', 'Result', 50, 'Int'); ";
					sqlite = mssql;
					break;
				case 134:
					mssql = "UPDATE columnSelection SET colName='playerTankBattle.potentialDmgReceived' WHERE id=208;";
					sqlite = mssql;
					break;
				case 137:
					mssql = "UPDATE columnSelection SET colDataType='VarChar' WHERE id = 512;";
					sqlite = mssql;
					break;
				case 138:
					mssql = "UPDATE json2dbMapping SET dbPlayerTankMode='Skirmishes' WHERE dbPlayerTankMode='Strongholds';" +
							"UPDATE playerTankBattle SET battleMode='Skirmishes' WHERE battleMode='Strongholds';" +
							"UPDATE battle SET battleMode='Skirmishes' WHERE battleMode='Strongholds';";
					sqlite = mssql;
					break;
				case 139:
					mssql = "UPDATE columnSelection SET colGroup='Damage' WHERE colGroup='Damange'";
					sqlite = mssql;
					break;
				case 141:
					Config.Settings.mainGridBattleRowWidht = 24;
					Config.Settings.mainGridTankRowWidht = 24;
					Config.SaveConfig(out msg);
					break;
				case 142:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql =
						"UPDATE columnSelection SET name='Mastery Badge ID' WHERE id IN (53, 510); " +
						"UPDATE columnSelection SET position=position+20 where colType=1 and position>220;" +
						s + "VALUES (216, 1, 114, '', 'Mastery Badge', 'The mastery badge achieved for this tank shown as image', 'Rating', 50, 'Image'); " +
						s + "VALUES (520, 2, 279, '', 'Mastery Badge', 'The mastery badge achieved in this battle shown as image', 'Rating', 50, 'Image'); ";
					sqlite = mssql;
					break;
				case 143:
					mssql =
						"UPDATE columnSelection SET position=214 WHERE id=206; " +
						"UPDATE columnSelection SET position=218 WHERE id=207; ";
					sqlite = mssql;
					break;
				case 144:
					mssql =
						"UPDATE columnSelection SET position=position+10 where colType=1 and position>214;" +
						"UPDATE columnSelection SET position=215 WHERE id=211; ";
					sqlite = mssql;
					break;
				case 145:
					mssql =
						"UPDATE columnSelection SET position=position+10 where colType=1 and position>228;" +
						"UPDATE columnSelection SET position=230 WHERE id=212; ";
					sqlite = mssql;
					break;
				case 146:
					mssql =
						"UPDATE columnSelection SET colName='' where colDataType='Image';";
					sqlite = mssql;
					break;
				case 147:
					mssql =
						"UPDATE columnList SET lastSortColumn='';";
					sqlite = mssql;
					break;
				case 150:
					Config.Settings.useSmallMasteryBadgeIcons = true;
					Config.SaveConfig(out msg);
					break;
				case 152:
					NewSystemTankColList();
					NewSystemBattleColList();
					break;
				case 153:
					s = "INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) ";
					mssql =
						s + "VALUES ('tanks_v2','achievements', 'markOfMastery','Int','markOfMastery', NULL , NULL,'tanks_v2.achievements.markOfMastery', NULL); ";
					sqlite = mssql;
					break;
				case 154:
					TankHelper.GetJson2dbMappingFromDB();
					break;
				case 155:
					RunDossierFileCheckWithForceUpdate = true;
					break;
				case 156:
					mssql =
						"CREATE TABLE gadget ( " +
						"id int IDENTITY(1,1) primary key, " +
						"controlName varchar(255) NOT NULL, " +
						"visible bit NOT NULL, " +
						"sortorder int NOT NULL, " +
						"posX int NOT NULL, " +
						"posY int NOT NULL, " +
						"width int NOT NULL, " +
						"height int NOT NULL) ";
					sqlite =
						"CREATE TABLE gadget ( " +
						"id integer primary key, " +
						"controlName varchar(255) NOT NULL, " +
						"visible bit NOT NULL, " +
						"sortorder integer NOT NULL, " +
						"posX integer NOT NULL, " +
						"posY integer NOT NULL, " +
						"width integer NOT NULL, " +
						"height integer NOT NULL) ";
					break;
				case 157:
					mssql =
						"CREATE TABLE gadgetParameter ( " +
						"id int IDENTITY(1,1) primary key, " +
						"gadgetId int NOT NULL, " +
						"paramNum int NOT NULL, " +
						"dataType varchar(255) NOT NULL, " +
						"value varchar(2000) NOT NULL, " +
						"foreign key (gadgetId) references gadget (id) )";
					sqlite =
						"CREATE TABLE gadgetParameter ( " +
						"id integer primary key, " +
						"gadgetId integer NOT NULL, " +
						"paramNum integer NOT NULL, " +
						"dataType varchar(255) NOT NULL, " +
						"value varchar(2000) NOT NULL, " +
						"foreign key (gadgetId) references gadget (id) )";
					break;
				case 158:
					GadgetHelper.gadgets = new List<GadgetHelper.GadgetItem>();
					GadgetHelper.DefaultSetup();
					break;
				case 159:
					mssql = "UPDATE columnSelection SET colName='battle.battleLifeTime' WHERE id = 9; ";
					sqlite = mssql;
					break;
				case 160:
					s = "INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) ";
					mssql =
						s + "VALUES ('tanks_v2','common', 'compactDescr','Int','compactDescr', NULL , NULL,'tanks_v2.common.compactDescr', NULL); ";
					sqlite = mssql;
					break;
				case 161:
					TankHelper.GetJson2dbMappingFromDB();
					break;
				case 162:
					mssql = "ALTER TABLE playerTank ADD compactDescr int NOT NULL default 0; ";
					sqlite = "ALTER TABLE playerTank ADD compactDescr integer NOT NULL default 0; "; 
					break;
				case 163:
					s = "INSERT INTO json2dbMapping (jsonMain ,jsonSub ,jsonProperty ,dbDataType ,dbPlayerTank ,dbBattle ,dbAch ,jsonMainSubProperty ,dbPlayerTankMode) ";
					mssql =
						s + "VALUES ('tanks','common', 'compactDescr','Int','compactDescr', NULL , NULL,'tanks.common.compactDescr', NULL); ";
					sqlite = mssql;
					break;
				case 164:
					TankHelper.GetJson2dbMappingFromDB();
					break;
				case 167:
					mssql =
						"CREATE TABLE battlePlayer ( " +
						" id int IDENTITY(1,1) primary key, " +
						" battleId int NOT NULL, accountId int NOT NULL, " +
						" name varchar (30) NOT NULL, team int NOT NULL, tankId int NOT NULL, clanDBID int NULL, " +
						" clanAbbrev varchar (10) NULL, platoonID int NULL, xp int NOT NULL, damageDealt int NOT NULL, " +
						" credits int NOT NULL, capturePoints int NOT NULL, damageReceived int NOT NULL, deathReason int NOT NULL, " +
						" directHits int NOT NULL, directHitsReceived int NOT NULL, droppedCapturePoints int NOT NULL, hits int NOT NULL, " +
						" kills int NOT NULL, shots int NOT NULL, shotsReceived int NOT NULL, spotted int NOT NULL, " +
						" tkills int NOT NULL, fortResource int NULL, " +
						" foreign key (battleId) references battle (id), " +
						" foreign key (tankId) references tank (id) ); ";
					// for sqllite it have to be created here, gets dropped and recreated in upgrade 182
					sqlite =
						"CREATE TABLE battlePlayer ( " +
						" id integer primary key, " +
						" battleId integer NOT NULL, accountId integer NOT NULL, " +
						" name varchar (30) NOT NULL, team integer NOT NULL, tankId integer NOT NULL, clanDBID integer NULL, " +
						" clanAbbrev varchar (10) NULL, platoonID integer NULL, xp integer NOT NULL, damageDealt integer NOT NULL, " +
						" credits integer NOT NULL, damageReceived integer NOT NULL, deathReason integer NOT NULL, " +
						" directHits integer NOT NULL, directHitsReceived integer NOT NULL, hits integer NOT NULL, " +
						" kills integer NOT NULL, shots integer NOT NULL, shotsReceived integer NOT NULL, spotted integer NOT NULL, " +
						" tkills integer NOT NULL, fortResource integer NULL, " +
						" foreign key (battleId) references battle (id) " +
						" foreign key (tankId) references tank (id) ); ";
					break; 
				case 168:
					mssql = "ALTER TABLE battle ADD enemyClanAbbrev varchar(10) NULL;" +
							"ALTER TABLE battle ADD enemyClanDBID INT NULL;" +
							"ALTER TABLE battle ADD playerFortResources INT NULL;" +
							"ALTER TABLE battle ADD clanForResources INT NULL;" +
							"ALTER TABLE battle ADD enemyClanFortResources INT NULL;" +
							"ALTER TABLE battle ADD killedByPlayerName varchar(30) NULL;" +
							"ALTER TABLE battle ADD killedByAccountId INT NULL;" +
							"ALTER TABLE battle ADD platoonParticipants INT NULL;";
					sqlite = mssql.Replace("INT", "INTEGER");
					break;
				case 169:
					s = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) ";
					mssql = s + "VALUES (521, 2, 170, 'enemyClanAbbrev', 'Enemy Clan', 'Enemy Clan (name)', 'Battle', 50, 'VarChar'); " +
							s + "VALUES (522, 2, 171, 'enemyClanDBID', 'Enemy Clan ID', 'Enemy Clan (account ID)', 'Result', 35, 'Int'); " +
							s + "VALUES (523, 2, 270, 'playerFortResources', 'IR', 'Earned Industrial Resources by you (Skirmish)', 'Result', 35, 'Int'); " +
							s + "VALUES (524, 2, 271, 'clanForResources', 'Clan IR', 'Your Clan earned Industrial Resources (Skirmish)', 'Result', 35, 'Int'); " +
							s + "VALUES (525, 2, 272, 'enemyClanFortResources', 'Enemy Clan IR', 'Enemy Clan earned Industrial Resources (Skirmish)', 'Result', 35, 'Int'); " +
							s + "VALUES (526, 2, 172, 'killedByPlayerName', 'Killed By', 'Destoyed by player (name)', 'Battle', 50, 'VarChar'); " +
							s + "VALUES (527, 2, 173, 'killedByAccountId', 'Killed By Player ID', 'Destoed by player (account ID)', 'Battle', 35, 'Int'); " +
							s + "VALUES (528, 2, 165, 'platoonParticipants', 'Platoon', 'Number of platoon participants (2 or 3), 0 if not played in platoon', 'Battle', 35, 'Int'); ";
					sqlite = mssql;
					break;
				case 170:
					mssql = "ALTER TABLE battle ADD battleResultMode varchar(20) NULL; " +
						    "UPDATE columnSelection SET name='Main Mode' where id=162; " +
					        "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
					        "VALUES (529, 2, 114, 'battle.battleResultMode', 'Battle Mode', 'Battle mode retrieved from enhanced battle fetch', 'Battle', 50, 'VarChar'); ";
					sqlite = mssql;
					break;
				case 171:
					mssql = "ALTER TABLE battlePlayer ADD potentialDamageReceived INT NULL;" +
							"ALTER TABLE battlePlayer ADD noDamageShotsReceived INT NULL;" +
							"ALTER TABLE battlePlayer ADD sniperDamageDealt INT NULL;" +
							"ALTER TABLE battlePlayer ADD piercingsReceived INT NULL;" +
							"ALTER TABLE battlePlayer ADD pierced INT NULL;" +
							"ALTER TABLE battlePlayer ADD mileage INT NULL;" +
							"ALTER TABLE battlePlayer ADD lifeTime INT NULL;" +
							"ALTER TABLE battlePlayer ADD killerID INT NULL;" +
							"ALTER TABLE battlePlayer ADD isPrematureLeave INT NULL;" +
							"ALTER TABLE battlePlayer ADD explosionHits INT NULL;" +
							"ALTER TABLE battlePlayer ADD explosionHitsReceived INT NULL;" +
							"ALTER TABLE battlePlayer ADD damageBlockedByArmor INT NULL;" +
							"ALTER TABLE battlePlayer ADD damageAssistedTrack INT NULL;" +
							"ALTER TABLE battlePlayer ADD damageAssistedRadio INT NULL;";
					sqlite = ""; // gets recreated in upgrade 182
					break;
				case 172:
					mssql = "ALTER TABLE battlePlayer ADD isTeamKiller INT NULL;";
					sqlite = ""; // gets recreated in upgrade 182
					break;
				case 173:
					mssql = "ALTER TABLE battlePlayer ADD killerName VARCHAR(30) NULL;";
					sqlite = ""; // gets recreated in upgrade 182
					break;
				case 174:
					Config.Settings.wotGameAffinity = 0;
					Config.Settings.wotGameFolder = "";
					Config.Settings.wotGameStartType = ConfigData.WoTGameStartType.NotConfigured;
					break;
				case 175:
					Config.Settings.wotGameAutoStart = false;
					break;
				case 176:
					break;
				case 177:
					Config.Settings.wotGameRunBatchFile = "";
					Config.SaveConfig(out msg);
					break;
				case 178:
					RunRecalcBattleWN8 = true;
					break;
				case 181:
					NewSystemBattleColList_Skirmish(99);
					break;
				case 182:
					mssql = "";
					sqlite =
						"DROP TABLE battlePlayer; " +
						"CREATE TABLE battlePlayer ( " +
						" id integer IDENTITY(1,1) primary key, " +
						" battleId integer NOT NULL, accountId integer NOT NULL, " +
						" name varchar (30) NOT NULL, team integer NOT NULL, tankId integer NOT NULL, clanDBID integer NULL, " +
						" clanAbbrev varchar (10) NULL, platoonID integer NULL, xp integer NOT NULL, damageDealt integer NOT NULL, " +
						" credits integer NOT NULL, capturePoints integer NOT NULL, damageReceived integer NOT NULL, deathReason integer NOT NULL, " +
						" directHits integer NOT NULL, directHitsReceived integer NOT NULL, droppedCapturePoints integer NOT NULL, hits integer NOT NULL, " +
						" kills integer NOT NULL, shots integer NOT NULL, shotsReceived integer NOT NULL, spotted integer NOT NULL, " +
						" tkills integer NOT NULL, fortResource integer NULL, " +
						" foreign key (battleId) references battle (id), " +
						" foreign key (tankId) references tank (id) ); " +
						"ALTER TABLE battlePlayer ADD potentialDamageReceived integer NULL;" +
						"ALTER TABLE battlePlayer ADD noDamageShotsReceived integer NULL;" +
						"ALTER TABLE battlePlayer ADD sniperDamageDealt integer NULL;" +
						"ALTER TABLE battlePlayer ADD piercingsReceived integer NULL;" +
						"ALTER TABLE battlePlayer ADD pierced integer NULL;" +
						"ALTER TABLE battlePlayer ADD mileage integer NULL;" +
						"ALTER TABLE battlePlayer ADD lifeTime integer NULL;" +
						"ALTER TABLE battlePlayer ADD killerID integer NULL;" +
						"ALTER TABLE battlePlayer ADD isPrematureLeave integer NULL;" +
						"ALTER TABLE battlePlayer ADD explosionHits integer NULL;" +
						"ALTER TABLE battlePlayer ADD explosionHitsReceived integer NULL;" +
						"ALTER TABLE battlePlayer ADD damageBlockedByArmor integer NULL;" +
						"ALTER TABLE battlePlayer ADD damageAssistedTrack integer NULL;" +
						"ALTER TABLE battlePlayer ADD damageAssistedRadio integer NULL;" +
						"ALTER TABLE battlePlayer ADD isTeamKiller integer NULL;" +
						"ALTER TABLE battlePlayer ADD killerName VARCHAR(30) NULL;";

					break;
				case 183:
					mssql = "ALTER TABLE battle ADD comment VARCHAR(MAX) NULL;";
					sqlite = "ALTER TABLE battle ADD comment VARCHAR(10) NULL;"; 
					break;
				case 184:
					mssql = "INSERT INTO columnSelection (id, colType, position, colName, name, description, colGroup, colWidth, colDataType) " +
							"VALUES (217, 2, 163, 'battle.comment', 'Comment', 'Battle comment, available to add/edit from battle details', 'Battle', 120, 'VarChar'); ";
					sqlite = mssql;
					break;
				case 185:
					Config.Settings.notifyIconUse = false;
					Config.Settings.notifyIconFormExitToMinimize = false;
					Config.SaveConfig(out msg);
					break;
				case 186:
					mssql = "UPDATE map SET name='Fiery Salient' WHERE id=62;";
					sqlite = mssql;
					break;
				case 187:
					mssql = "ALTER TABLE map ADD description VARCHAR(2000) NULL; " +
							"ALTER TABLE map ADD arena_id VARCHAR(50) NULL; ";
					sqlite = mssql;
					break;
				case 188:
					mssql = GetUpgradeSQL("188");
					sqlite = mssql;
					RunWotApi = true;
					break;
				case 189:
					mssql =
						"CREATE TABLE battleMapPaint ( " +
						"id int IDENTITY(1,1) primary key, " +
						"battleId int NOT NULL, " +
						"painting varbinary(MAX), " +
						"foreign key (battleId) references battle (id) )";
					sqlite =
						"CREATE TABLE battleMapPaint ( " +
						"id integer primary key, " +
						"battleId integer NOT NULL, " +
						"painting blob, " +
						"foreign key (battleId) references battle (id) )";
					break;
				case 190:
					Config.Settings.customBattleTimeFilter = new ConfigData.CustomBattleTimeFilter();
					Config.SaveConfig(out msg);
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

		private static string GetUpgradeSQL(string version)
		{
			string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\Docs\\Database\\upgrade.json";
			// Read content
			StreamReader sr = new StreamReader(file, Encoding.UTF8);
			string json = sr.ReadToEnd();
			sr.Close();
			// Root token
			JToken token_root = JObject.Parse(json);
			// Array containing upgrade scripts
			JArray array_script = (JArray)token_root.SelectToken(version);
			// Convert to list
			List<string> scripts = array_script.ToObject<List<string>>();
			string sql = "";
			foreach (string script in scripts)
			{
				sql += script + Environment.NewLine;
			}
			return sql;
		}

		// Procedure upgrading DB to latest version
		public static bool CheckForDbUpgrade(Form parentForm)
		{
			bool upgradeOK = true;
			int DBVersionCurrentNumber = GetDBVersion(); // Get current DB version
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
					if (sql != "")
						continueNext = DB.ExecuteNonQuery(sql); // Run upgrade script
					Application.DoEvents(); // To avoid freeze
					// Update db _version_ if success
					if (continueNext)
					{
						sql = "update _version_ set version=" + DBVersionCurrentNumber.ToString() + " where id=1";
						continueNext = DB.ExecuteNonQuery(sql);
					}
				}
				// If anything went wrong (continueNext == false), supply error notification here
				if (!continueNext)
					Code.MsgBox.Show("Error occured during database upgrade, failed running SQL script for version: " + DBVersionCurrentNumber.ToString("0000"), "Error Upgrading Database", parentForm);
				upgradeOK = continueNext;
				
			}
			return upgradeOK;
		}

		// Returns database current version, on first run version table is created and version = 1
		public static int GetDBVersion()
		{
			int version = 0;
			string sql = "";
			//bool versionTableFound = false;
			// List tables
			DataTable dt = DB.ListTables();
			if (dt.Rows.Count > 0)
			{
				// Get version now
				sql = "select version from _version_ where id=1; ";
				dt.Dispose();
				dt.Clear();
				dt = DB.FetchData(sql);
				if (dt.Rows.Count > 0)
				{
					version = Convert.ToInt32(dt.Rows[0][0]);
				}
				dt.Dispose();
				dt.Clear();
			}
			return version;
		}

		public static int GetWN8Version()
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
		//SELECT '"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values ('+cast(columnSelectionId as varchar)+',"+id+",'+cast(sortorder as varchar)+','+cast(columnListSelection.colWidth as varchar)+');" + // ' + columnSelection.name
		//FROM columnListSelection inner join columnSelection on columnSelection.Id = columnListSelection.columnSelectionId
		//WHERE columnListSelection.columnListId=28
		//ORDER BY sortorder

		// *** TANK SYSTEM COL LIST *** //

		private static void NewSystemTankColList()
		{
			// First remove all system colList for Tank
			string sql = 
				"delete from columnListSelection where columnListId IN (select id from columnList where sysCol=1 and colType=1); " +
				"delete from columnList where sysCol=1 and colType=1; ";
			DB.ExecuteNonQuery(sql);
			// Create lists all over
			string newDefaultColListId = "";
			newDefaultColListId = NewSystemTankColList_Default(-10);
			NewSystemTankColList_Grinding(-9);
			NewSystemTankColList_WN8(-8);
			// Sort lists
			ColListHelper.ColListSort(1);
			// Set default if missing
			SetFavListAsDefaultIfMissing(newDefaultColListId, 1);
			// get default gridfilter, might be new
			MainSettings.GridFilterTank = GridFilter.GetDefault(GridView.Views.Tank);
		}

		private static string NewSystemTankColList_Default(int position)
		{
			string sql = "insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (1,'Default', 1, " + position.ToString() + ", 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=1 and name='Default';";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (12," + id + ",1,35);" + // Tier
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (181," + id + ",2,90);" + // Tank Image
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (1," + id + ",3,120);" + // Tank
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (44," + id + ",4,50);" + // Type
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (57," + id + ",5,50);" + // Nation
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (900," + id + ",6,3);" + //  - Separator 0 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (54," + id + ",7,100);" + // Last Battle
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (50," + id + ",8,50);" + // Battles
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (95," + id + ",9,50);" + // Win Rate
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (901," + id + ",10,3);" + //  - Separator 1 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (155," + id + ",11,50);" + // Frags Max
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (154," + id + ",12,50);" + // Dmg Max
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (156," + id + ",13,50);" + // XP Max
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (902," + id + ",14,3);" + //  - Separator 2 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (216," + id + ",15,50);" + // Mastery Badge
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (49," + id + ",16,50);" + // WN8
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (187," + id + ",17,50);" + // WN7
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (48," + id + ",18,50);"; // EFF
			DB.ExecuteNonQuery(sql);
			return id;
		}

		private static string NewSystemTankColList_Grinding(int position)
		{
			string sql = "insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (1,'Grinding', 0, " + position.ToString() + ", 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=1 and name='Grinding';";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (12," + id + ",1,35);" +    // tier
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (181," + id + ",2,90);" +   // smallImg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (1," + id + ",3,106);" +    // name
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (175," + id + ",4,140);" +  // gComment
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (171," + id + ",5,70);" +   // gGrindXP
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (173," + id + ",6,67);" +   // gPogressXp
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (177," + id + ",7,40);" +   // gProgressPercent
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (176," + id + ",8,66);" +   // gRestXP
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (178," + id + ",9,40);" +   // gRestBattles
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (174," + id + ",10,40);" +  // gBattlesDay
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (179," + id + ",11,40);";   // gRestDays
			DB.ExecuteNonQuery(sql);
			return id;
		}

		private static string NewSystemTankColList_WN8(int position)
		{
			string sql = "insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (1,'WN8', 0, " + position.ToString() + ", 1, -1); ";
			DB.ExecuteNonQuery(sql);
			Application.DoEvents();
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=1 and name='WN8';";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (12," + id + ",1,35);" + // Tier
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (181," + id + ",2,90);" + // Tank Image
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (1," + id + ",3,120);" + // Tank
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (50," + id + ",4,50);" + // Battles
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (49," + id + ",5,60);" + // WN8
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (900," + id + ",6,3);" + //  - Separator 0 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (188," + id + ",7,60);" + // Avg Dmg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (192," + id + ",8,60);" + // Exp Dmg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (901," + id + ",9,3);" + //  - Separator 1 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (95," + id + ",10,47);" + // Win Rate
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (193," + id + ",11,47);" + // Exp Win Rate
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (902," + id + ",12,3);" + //  - Separator 2 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (205," + id + ",13,40);" + // Avg Spot
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (194," + id + ",14,40);" + // Exp Spot
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (903," + id + ",15,3);" + //  - Separator 3 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (191," + id + ",16,40);" + // Avg Frags
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (195," + id + ",17,40);" + // Exp Frags
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (904," + id + ",18,3);" + //  - Separator 4 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (204," + id + ",19,40);" + // Avg Def
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (196," + id + ",20,40);"; // Exp Def
			DB.ExecuteNonQuery(sql);
			return id;
		}

		// *** BATTLE SYSTEM COL LIST *** //

		private static void NewSystemBattleColList()
		{
			// First remove all system colList for Battle
			string sql =
				"delete from columnListSelection where columnListId IN (select id from columnList where sysCol=1 and colType=2); " +
				"delete from columnList where sysCol=1 and colType=2; ";
			DB.ExecuteNonQuery(sql);
			// Create lists all over
			string newDefaultColListId = "";
			newDefaultColListId = NewSystemBattleColList_Default(-10);
			NewSystemBattleColList_WN8(-9);
			// Sort lists
			ColListHelper.ColListSort(2);
			// Set default if missing
			SetFavListAsDefaultIfMissing(newDefaultColListId, 2);
			// Change to default in case selected no longer exists
			MainSettings.GridFilterBattle = GridFilter.GetDefault(GridView.Views.Battle);

		}

		private static string NewSystemBattleColList_Default(int position)
		{
			// Create new default colList
			string sql = "insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (2,'Default', 0, " + position.ToString() + ", 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=2 and name='Default';";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (59," + id + ",1,35);" + // Tier
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (184," + id + ",2,90);" + // Tank Image
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (58," + id + ",3,109);" + // Tank
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (8," + id + ",4,104);" + // DateTime
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (10," + id + ",5,59);" + // Result
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (11," + id + ",6,53);" + // Survived
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (900," + id + ",7,3);" + //  - Separator 0 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (19," + id + ",8,54);" + // Dmg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (21," + id + ",9,47);" + // Dmg Spot
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (22," + id + ",10,47);" + // Dmg Track
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (901," + id + ",11,3);" + //  - Separator 1 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (18," + id + ",12,35);" + // Frags
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (35," + id + ",13,35);" + // Spot
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (24," + id + ",14,35);" + // Cap
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (25," + id + ",15,35);" + // Def
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (902," + id + ",16,3);" + //  - Separator 2 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (519," + id + ",18,55);" + // Total XP
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (508," + id + ",19,52);" + // Credits Result
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (903," + id + ",20,3);" + //  - Separator 3 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (520," + id + ",21,50);" + // Mastery Badge
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (47," + id + ",22,47);" + // WN8
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (186," + id + ",23,47);" + // WN7
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (512," + id + ",25,97);"; // Map
			DB.ExecuteNonQuery(sql);
			return id;
		}

		private static string NewSystemBattleColList_WN8(int position)
		{
			string sql = "insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) values (2,'WN8', 0, " + position.ToString() + ", 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=2 and name='WN8';";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (59," + id + ",1,35);" + // Tier
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (184," + id + ",2,90);" + // Tank Image
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (58," + id + ",3,120);" + // Tank
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (8," + id + ",4,100);" + // DateTime
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (47," + id + ",5,60);" + // WN8
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (900," + id + ",6,3);" + //  - Separator 0 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (19," + id + ",7,60);" + // Dmg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (197," + id + ",8,60);" + // Exp Dmg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (901," + id + ",9,3);" + //  - Separator 1 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (35," + id + ",10,40);" + // Spot
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (199," + id + ",11,40);" + // Exp Spot
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (902," + id + ",12,3);" + //  - Separator 2 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (18," + id + ",13,40);" + // Frags
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (200," + id + ",14,40);" + // Exp Frags
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (903," + id + ",15,3);" + //  - Separator 3 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (25," + id + ",16,40);" + // Def
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (201," + id + ",17,40);"; // Exp Def
			DB.ExecuteNonQuery(sql);
			return id;
		}
		
		// *** SET NEW DEFAULT COL LIST IF MISSING ***/

		private static void SetFavListAsDefaultIfMissing(string favListId, int colTypeId)
		{
			string sql = "select count(id) from columnList where colDefault=1 and colType=" + colTypeId.ToString() + ";";
			int count = Convert.ToInt32(DB.FetchData(sql).Rows[0][0]);
			if (count == 0)
			{
				sql = "update columnList set colDefault=1 where id=" + favListId + "; ";
				DB.ExecuteNonQuery(sql);
				MainSettings.GridFilterBattle.ColListId = Convert.ToInt32(favListId);
				MainSettings.GridFilterBattle.ColListName = "Default";
			}
		}

		private static string NewSystemBattleColList_Skirmish(int position)
		{
			// Create new default colList
			string sql = 
				"insert into columnList (colType,name,colDefault,position,sysCol,defaultFavListId) " + 
				"values (2,'Skirmish', 0, " + position.ToString() + ", 1, -1); ";
			DB.ExecuteNonQuery(sql);
			// Find id for new list
			sql = "select max(id) from columnList where sysCol=1 and colType=2 and name='Skirmish';";
			string id = DB.FetchData(sql).Rows[0][0].ToString();
			// Insert columns
			sql =
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (59," + id + ",1,35);" + // Tier
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (184," + id + ",2,90);" + // Tank Image
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (58," + id + ",3,96);" + // Tank
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (8," + id + ",4,104);" + // DateTime
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (10," + id + ",5,59);" + // Result
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (11," + id + ",6,53);" + // Survived
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (526," + id + ",7,73);" + // Killed By
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (513," + id + ",8,78);" + // Finish Reason
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (902," + id + ",9,3);" + //  - Separator 2 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (19," + id + ",10,47);" + // Dmg
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (18," + id + ",11,35);" + // Frags
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (904," + id + ",12,3);" + //  - Separator 4 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (519," + id + ",13,55);" + // Total XP
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (508," + id + ",14,52);" + // Credits Result
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (901," + id + ",15,3);" + //  - Separator 1 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (523," + id + ",16,38);" + // IR
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (524," + id + ",17,49);" + // Clan IR
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (900," + id + ",18,3);" + //  - Separator 0 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (525," + id + ",19,50);" + // Enemy Clan IR
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (521," + id + ",20,55);" + // Enemy Clan
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (903," + id + ",21,3);" + //  - Separator 3 -
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (47," + id + ",23,47);" + // WN8
				"insert into columnListSelection (columnSelectionId,columnListId,sortorder,colWidth) values (512," + id + ",25,97);" ; // Map
			DB.ExecuteNonQuery(sql);
			ColListHelper.ColListSort(2);
			return id;
		}

	}
}
