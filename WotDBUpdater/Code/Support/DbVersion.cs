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
		public static int ExpectedNumber = 7; // <--------------------------------------- REMEMBER TO ADD DB VERSION NUMBER HERE - AND SUPPLY SQL SCRIPT BELOW

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
					mssql = "ALTER TABLE columnSelection ADD colGroup varchar(50) NULL; ";
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
