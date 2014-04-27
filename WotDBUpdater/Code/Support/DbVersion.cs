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
		public static int ExpectedNumber = 9; // <--------------------------------------- REMEMBER TO ADD DB VERSION NUMBER HERE - AND SUPPLY SQL SCRIPT BELOW

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
							" id int identity(1,1) primary key, " +
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
					mssql = "INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 1, 'tank.name', 'Tank', 'Tank name', 'Tank', 120); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 3, 'tank.premium', 'Premium', 'Tank premium (yes/no)', 'Tank', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 4, 'tankType.name', 'Tank Type', 'Tank type full name', 'Tank', 100); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 5, 'tankType.shortName', 'Type', 'Tank type short name (LT, MT, HT, TD, SPG)', 'Tank', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 6, 'country.name', 'Tank Natio', 'Tank nation full name', 'Tank', 100); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 7, 'country.shortName', 'Natio', 'Tank nation short name', NULL, 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 9, 'battle.battlesCount', 'Battles', 'Battle count, number of battles for the row', 'Battle', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 10, 'battleTime', 'Time', 'Battle time, the date/time the battle was finished', 'Battle', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 11, 'battleLifeTime', 'Life Time', 'Time staying alive in battle', 'Battle', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 12, 'battleResult.name', 'Result', 'The result for battle (Victory, Draw, Defeat or Several if a combination occur when recorded several battles for one row) ', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 16, 'battleSurvive.name', 'Survived', 'If survived in battle (Yes / No or Several if a combination occur when recorded several battles for one row)', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 2, 'tank.tier', 'Tier', 'Tank tier (1-10)', 'Tank', 35); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 13, 'battle.victory', 'Victory', 'Number of victory battles for this row (normally 0/1, or more if battle result is Several)', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 14, 'battle.draw', 'Draw', 'Number of drawed battles for this row (normally 0/1, or more if battle result is Several)', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 15, 'battle.defeat', 'Defeat', 'Number of defeated battles for this row (normally 0/1, or more if battle result is Several)', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 17, 'battle.survived', 'Survived Count', 'Number of battles where survived for this row (normally 0/1, or more if battle result is Several)', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 18, 'battle.killed', 'Killed Count', 'Number of battles where killed (not survived) for this row (normally 0/1, or more if battle result is Several)', 'Result', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 19, 'battle.frags', 'Frags', 'Number of enemy tanks you killed (frags)', 'Damage', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 20, 'battle.dmg', 'Damage', 'Damage to enemy tanks by you (shooting, ramming, put on fire)', 'Damage', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 23, 'battle.dmgReceived', 'Damage Received', 'The damage received on your tank', 'Damage', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 21, 'battle.assistSpot', 'Damage Spotting', 'Assisted damage casued by others to enemy tanks due to you spotting the enemy tank', 'Damage', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 22, 'battle.assistTrack', 'Damgae Tracking', 'Assisted damage casued by others to enemy tanks due to you tracking of the enemy tank', 'Damage', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 3, 'tank.premium', 'Premium', 'Tank premium (yes/no)', 'Tank', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 32, 'battle.cap', 'Cap', 'Cap ponts you achived by staying in cap circle (0 - 100)', 'Other', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 33, 'battle.def', 'Defense', 'Cap points reduced by damaging enemy tanks capping', 'Other', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 24, 'battle.shots', 'Shots', 'Number of shots you fired', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 25, 'battle.hits', 'Hits', 'Number of hits from you shots', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 26, 'battle.hits * 100 / battle.shots', 'Hits %', 'Persentage hits (hits*100/shots)', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 30, 'battle.shotsReceived', 'Shots Reveived', 'Number of shots received ', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 27, 'battle.pierced', 'Pierced', 'Number of pierced shots', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 31, 'battle.piercedReceived', 'Pierced Received', 'Number of pierced shots received ', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 28, 'battle.pierced * 100 / battle.shots', 'Pierced Shots %', 'Persentage pierced hits based on total shots (pierced*100/shots)', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 29, 'battle.pierced * 100 / battle.hits', 'Pierced Hts %', 'Persentage pierced hits based on total hits (pierced*100/hits)', 'Shooting', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 4, 'tankType.name', 'Tank Type', 'Tank type full name', 'Tank', 100); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 34, 'battle.spotted', 'Spotted', 'Enemy tanks spotted (only first spot on enemy tank counts)', 'Other', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 35, 'battle.mileage', 'Mileage', 'Distance driving the tank', 'Other', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 36, 'battle.treesCut', 'Trees Cut', 'Number of trees overturned by driving into it', 'Other', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 37, 'battle.xp', 'XP', 'Default XP earned, 50% extra for victory or 2X (or more) for first victory or campaign not included', 'Rating', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 8, 'tank.id', 'ID', 'Wargaming ID for tank', 'Tank', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 38, 'battle.eff', 'EFF', 'Calculated battle efficiency rating', 'Rating', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 40, 'battle.mode15', '15x15', 'Number of 15x15 battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 41, 'battle.mode7', '7x7', 'Number of 7x7 battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 42, 'battle.modeClan', 'Cla', 'Number of Clan battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 5, 'tankType.shortName', 'Type', 'Tank type short name (LT, MT, HT, TD, SPG)', 'Tank', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 43, 'battle.modeCompany', 'Company', 'Number of Tank Company battles for this row (normally 0/1, or more if battle result is Several)', 'Mode', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 8, 'tank.id', 'ID', 'Wargaming ID for tank', 'Tank', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 39, 'battle.wn8', 'WN8', 'Calculated battle WN8 (WRx) rating (according to formula from vbAddict)', 'Rating', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 31, 'playerTank.eff', 'EFF', 'Calculated battle efficiency rating', 'Rating', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 32, 'playerTank.wn8', 'WN8', 'Calculated battle WN8 (WRx) rating (according to formula from vbAddict)', 'Rating', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 33, 'playerTank.xp15+playerTank.xp7', 'XP Total', 'Average XP earned, 50% extra for victory or 2X (or more) for first victory or campaign not included', 'Rating', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 6, 'country.name', 'Tank Natio', 'Tank nation full name', 'Tank', 100); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 10, 'playerTank.battleLifeTime', 'Life Time', 'Total battle life time', 'Battle', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 30, 'playerTank.markOfMastery', 'Master Badge', 'Master Badge achived (0 = None, 1-3 = Mastery Badge Level, 4 = Ace Tanker)', 'Rating', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 9, 'playerTank.lastBattleTime', 'Last Battle', 'Last battle time', 'Battle', 70); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 12, 'playerTank.hasCompany', 'Company', 'Used in company battle (0 = No, 1 = yes)', 'Mode', 35); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 13, 'playerTank.hasClan', 'Cla', 'Used in clan wars (0 = No, 1 = yes)', 'Mode', 35); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (1, 7, 'country.shortName', 'Natio', 'Tank nation short name (USR,', 'Tank', 50); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 1, 'tank.name', 'Tank', 'Tank name', 'Tank', 120); " +
							"INSERT INTO columnSelection (colType, position, colName, name, description, colGroup, colWidth) VALUES (2, 2, 'tank.tier', 'Tier', 'Tank tier (1-10)', 'Tank', 35); ";
					sqlite=mssql;
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
