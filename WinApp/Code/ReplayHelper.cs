using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Code
{
    public class ReplayHelper
    {
        public static string GetWoTDefaultReplayFolder()
        {
            string defaultFolder = "";
            if (Directory.Exists(Config.Settings.wotGameFolder))
            {
                defaultFolder = Config.Settings.wotGameFolder;
                if (Directory.Exists(defaultFolder + "\\replays"))
                    defaultFolder += "\\replays";
            }
            return defaultFolder;
        }

        public async static Task AddReplayFolder(string path, bool subfolder)
        {
            // First remove if existing
            string sql = "DELETE FROM replayFolder where path=@path;";
            DB.AddWithValue(ref sql, "@path", path, DB.SqlDataType.VarChar);
            // Add new path
            sql += "INSERT INTO replayFolder (path, subfolder) VALUES (@path, @subfolder);";
            DB.AddWithValue(ref sql, "@path", path, DB.SqlDataType.VarChar);
            DB.AddWithValue(ref sql, "@subfolder", subfolder, DB.SqlDataType.Boolean);
            await DB.ExecuteNonQuery(sql);
        }

        public async static Task RemoveReplayFolder(int id)
        {
            string sql = "DELETE FROM replayFolder WHERE id=@id";
            DB.AddWithValue(ref sql, "@id", id, DB.SqlDataType.Int);
            await DB.ExecuteNonQuery(sql);
        }

        public async static Task<FileInfo> GetReplayFile(int battleId)
        {
            // Get Battle info
            string sql =
                "select tank.id, battle.battleTime, battle.battleLifeTime, tank.name as tankName, tank.imgpath , map.arena_id as mapArenaId " + 
                "from battle inner join playerTank pt on battle.playerTankId = pt.id inner join tank on pt.tankId = tank.Id left join map on battle.mapId = map.id  " +
                "where battle.id=" + battleId.ToString();
            DataTable dtBattle = await DB.FetchData(sql);
            FileInfo fi = null;
            if (dtBattle.Rows.Count > 0)
            {
                DataRow drBattle = dtBattle.Rows[0];
                DateTime battleTime = Convert.ToDateTime(drBattle["battleTime"]);
                int battleLifeTime = Convert.ToInt32(drBattle["battleLifeTime"]);
                DateTime battleApproxStartTime = battleTime.AddSeconds(-battleLifeTime); // timestamp on file is approx this, normally later
                string mapArenaId = drBattle["mapArenaId"].ToString();
                string tankImgPath = drBattle["imgPath"].ToString();
                string tankFileName = "";
                if (tankImgPath.Length > 10)
                {
                    int pos = tankImgPath.LastIndexOf("/") + 1;
                    tankFileName = tankImgPath.Substring(pos, tankImgPath.Length - pos - 4);
                }
                
                // problem - tank name in images file name do not comply with tank name on reply files
                //string replayFileName = "_" + tankFileName + "_" + mapArenaId + ".wotreplay"; 
                
                // Only use nation name from icon file name
                string nationName = tankFileName.Substring(0, tankFileName.IndexOf("-"));
                string replayFileName = "_" + nationName + "-*_" + mapArenaId + ".wotreplay";
                fi = await GetReplayFile(battleApproxStartTime, replayFileName);
            }
            return fi;
        }

        public async static Task<FileInfo> GetReplayFile(DateTime battleApproxStartTime, string fileName)
        {
            // Create file prefix according to approx start time
            DateTime battleEarliestStartTime = battleApproxStartTime.AddMinutes(-10);
            DateTime battleLatestEndTime = battleApproxStartTime.AddMinutes(+10);
            string fileprefix1 = battleEarliestStartTime.Year + battleEarliestStartTime.Month.ToString("00") + battleEarliestStartTime.Day.ToString("00");
            string fileprefix2 = battleLatestEndTime.Year + battleLatestEndTime.Month.ToString("00") + battleLatestEndTime.Day.ToString("00");
            string filename1 = fileprefix1 + "_*" + fileName;
            string filename2 = fileprefix2 + "_*" + fileName;
            // Search for files now
            List<FileInfo> fileList = new List<FileInfo>();
            DataTable dtReplayFolder = await DB.FetchData("select * from replayFolder order by path");
            foreach (DataRow dr in dtReplayFolder.Rows)
            {
                // Check that folder still exists, if not remove it for folder to search
                if (!Directory.Exists(dr["path"].ToString()))
                {
                    await RemoveReplayFolder(Convert.ToInt32(dr["id"]));
                }
                else
                {
                    // Start search now
                    DirectoryInfo rootDir = new DirectoryInfo(dr["path"].ToString());
                    SearchOption so = SearchOption.TopDirectoryOnly;
                    if (Convert.ToBoolean(dr["subfolder"]))
                        so = SearchOption.AllDirectories;
                    fileList.AddRange(rootDir.GetFiles(filename1, so));
                    if (filename1 != filename2)
                        fileList.AddRange(rootDir.GetFiles(filename2, so));
                }
            }
            // Check date if found several files, to find the correct one
            FileInfo fi = null;
            if (fileList.Count > 1)
            {
                List<FileInfo> sortedFiles = (
                    from f 
                    in fileList 
                    where f.CreationTime > battleEarliestStartTime && f.CreationTime < battleLatestEndTime 
                    orderby f.CreationTime 
                    select f
                ).ToList();
                //foreach (FileInfo file in sortedFiles)
                //{
                //    string filename = Path.GetFileNameWithoutExtension(file.FullName);
                //    DateTime replayDateTime = GetDateTimeFromReplayFile(filename);
                //}
            }
            if (fileList.Count >= 1)
            {
                fi = fileList[0]; 
            }
            return fi;
        }

        private static DateTime GetDateTimeFromReplayFile(string filename)
        {
            int y = Convert.ToInt32(filename.Substring(0,4));
            int m = Convert.ToInt32(filename.Substring(4, 2));
            int d = Convert.ToInt32(filename.Substring(6, 2));
            int H = Convert.ToInt32(filename.Substring(9, 2));
            int M = Convert.ToInt32(filename.Substring(11, 2));
            DateTime dateTime = new DateTime(y, m, d, H, M, 0);
            return dateTime;
        }

    }
}
