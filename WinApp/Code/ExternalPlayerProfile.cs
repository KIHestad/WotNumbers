using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WinApp.Code
{
    public class ExternalPlayerProfile
    {
        public static System.Drawing.Image image_vBAddict { get; set; }
        public static System.Drawing.Image image_Wargaming { get; set; }
        
        public static string GetServer
        {
            get {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru";
                if (server == "asia")
                    server = "sea";
                return server;
            }
            set {
            }
        }

        public static string GetServer_vBAddict
        {
            get
            {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru";
                if (server == "asia")
                    server = "sea";
                if (server == "com")
                    server = "na";
                return server;
            }
            set
            {
            }
        }
        
        public static void Wargaming(string playerName, string playerAccountId)
        {
            try
            {
                string server = Config.Settings.playerServer.ToLower();
                if (server == "net")
                    server = "ru";
                string serverURL = string.Format("http://worldoftanks.{0}/community/accounts/{1}-{2}/", server, playerAccountId, playerName);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on Wargaming website.");
            }
        }

        public static void WotLabs(string playerName)
        {
            try
            {
                string serverURL = string.Format("http://wotlabs.net/{0}/player/{1}", GetServer, playerName);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on WotLabs website.");
            }
        }

        public static void vBAddict(string playerName)
        {
            try
            {
                string serverURL = string.Format("http://www.vbaddict.net/player/{0}-{1}", playerName.ToLower(), GetServer_vBAddict);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on vBAddict website.");
            }
        }

        public static void Noobmeter(string playerName, string playerAccountId)
        {
            try
            {
                string serverURL = string.Format("http://www.noobmeter.com/player/{0}/{1}/{2}/", GetServer, playerName.ToLower(), playerAccountId);
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on vBAddict website.");
            }
        }

        public static void Wot_Life(string playerName)
        {
            try
            {
                string serverURL = string.Format("http://wot-life.com/{0}/player/{1}/", GetServer, playerName.ToLower());
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on WoT-Life.com website.");
            }
        }

        public static void WoTstats(string playerName)
        {
            try
            {
                string serverURL = string.Format("http://www.wotstats.org/stats/{0}/{1}/", GetServer, playerName.ToLower());
                System.Diagnostics.Process.Start(serverURL);
            }
            catch (Exception ex)
            {
                Log.LogToFile(ex, "Error on showing player profile on WoTstats.org website.");
            }
        }

        public static ContextMenuStrip MenuItems()
        {
            // Datagrid context menu (Right click on Grid)
            ContextMenuStrip dataGridPopup = new ContextMenuStrip();
            dataGridPopup.Renderer = new StripRenderer();
            dataGridPopup.BackColor = ColorTheme.ToolGrayMainBack;

            // Separator item
            ToolStripSeparator toolStripItem_Separator0 = new ToolStripSeparator();
            ToolStripSeparator toolStripItem_Separator1 = new ToolStripSeparator();
            ToolStripSeparator toolStripItem_Separator2 = new ToolStripSeparator();
            ToolStripLabel toolStripItem_Label = new ToolStripLabel("Show Player Profile at:");
            toolStripItem_Label.ForeColor = ColorTheme.ToolLabelHeading;

            // Wargaming player profile item
            ToolStripMenuItem toolStripItem_WargamingPlayerLookup = new ToolStripMenuItem("Wargaming");
            toolStripItem_WargamingPlayerLookup.Click += new EventHandler(ToolStripItem_WargamingPP_Click);
            toolStripItem_WargamingPlayerLookup.Image = image_Wargaming;

            // WotLabs player profile item
            ToolStripMenuItem toolStripItem_WotLabsPlayerLookup = new ToolStripMenuItem("WoT Labs");
            toolStripItem_WotLabsPlayerLookup.Click += new EventHandler(ToolStripItem_WotLabsPP_Click);

            // WoTstats.org player profile item
            ToolStripMenuItem toolStripItem_WoTstatsPlayerLookup = new ToolStripMenuItem("WoT stats");
            toolStripItem_WoTstatsPlayerLookup.Click += new EventHandler(ToolStripItem_WoTstatsPP_Click);

            // WoT-Life.com player profile item
            ToolStripMenuItem toolStripItem_Wot_LifePlayerLookup = new ToolStripMenuItem("WoT-Life");
            toolStripItem_Wot_LifePlayerLookup.Click += new EventHandler(ToolStripItem_Wot_LifePP_Click);

            // Noobemeter player profile item
            ToolStripMenuItem toolStripItem_NoobmeterPlayerLookup = new ToolStripMenuItem("NoobMeter");
            toolStripItem_NoobmeterPlayerLookup.Click += new EventHandler(ToolStripItem_NoobmeterPP_Click);

            // vBAddict player profile item
            ToolStripMenuItem toolStripItem_vBAddictPlayerLookup = new ToolStripMenuItem("vBAddict");
            toolStripItem_vBAddictPlayerLookup.Click += new EventHandler(ToolStripItem_vBAddictPP_Click);
            toolStripItem_vBAddictPlayerLookup.ToolTipText = "Profile depends on players uploads to vBAddict";
            toolStripItem_vBAddictPlayerLookup.Image = image_vBAddict;

            // Add cancel events
            dataGridPopup.Opening += new System.ComponentModel.CancelEventHandler(DataGridMainPopup_Opening);

            //Add to main context menu
            dataGridPopup.Items.AddRange(new ToolStripItem[] 
			{ 
			    toolStripItem_Label,
                toolStripItem_Separator0,
                toolStripItem_WargamingPlayerLookup,
                toolStripItem_Separator2,
                toolStripItem_vBAddictPlayerLookup,
                toolStripItem_Separator1,
                toolStripItem_WotLabsPlayerLookup,
			});
            string currentServer = Config.Settings.playerServer;

            List<string> validServers = new List<string>() { "NA", "EU" };
            if (validServers.Contains(currentServer))
                dataGridPopup.Items.Add(toolStripItem_Wot_LifePlayerLookup);

            dataGridPopup.Items.AddRange(new ToolStripItem[] 
			{ 
                toolStripItem_WoTstatsPlayerLookup,
                toolStripItem_NoobmeterPlayerLookup,
			});

            return dataGridPopup;
        }

        private static void DataGridMainPopup_Opening(object sender, CancelEventArgs e)
        {
            // Check if vBAddict PP exists
            if (!vBAddictPlayersManualLookup)
            {
                // Check used prefilled list of users
                ContextMenuStrip cms = (ContextMenuStrip)sender;
                bool vBAddictEnabled = true;
                vBAddictEnabled = vBAddictPlayers.Contains(dataGridRightClick.Rows[dataGridRightClickRow].Cells["AccountId"].Value.ToString());
                foreach (ToolStripItem item in cms.Items)
                {
                    if (item.Text == "vBAddict")
                        item.Enabled = vBAddictEnabled;
                }
            }

            // Close if no valid cell is clicked
            if (dataGridRightClickRow == -1)
            {
                e.Cancel = true; 
            }
        }

        public static DataGridView dataGridRightClick { get; set; }
        public static int dataGridRightClickRow { get; set; }
        public static List<string> vBAddictPlayers { get; set; }
        public static bool vBAddictPlayersManualLookup { get; set; }

        private static void ToolStripItem_WargamingPP_Click(object sender, EventArgs e)
        {
            string playerName = dataGridRightClick.Rows[dataGridRightClickRow].Cells["Player"].Value.ToString();
            string playerAccountId = dataGridRightClick.Rows[dataGridRightClickRow].Cells["AccountId"].Value.ToString();
            ExternalPlayerProfile.Wargaming(playerName, playerAccountId);
        }

        private static void ToolStripItem_WotLabsPP_Click(object sender, EventArgs e)
        {
            string playerName = dataGridRightClick.Rows[dataGridRightClickRow].Cells["Player"].Value.ToString();
            ExternalPlayerProfile.WotLabs(playerName);
        }

        private static void ToolStripItem_vBAddictPP_Click(object sender, EventArgs e)
        {
            string playerName = dataGridRightClick.Rows[dataGridRightClickRow].Cells["Player"].Value.ToString();
            string playerAccountId = dataGridRightClick.Rows[dataGridRightClickRow].Cells["AccountId"].Value.ToString();
            if (vBAddictPlayersManualLookup)
            {
                string error = "";
                List<string> foundPlayer = vBAddictHelper.SearchForUser(playerAccountId, out error);
                if (foundPlayer.Contains(playerAccountId))
                    ExternalPlayerProfile.vBAddict(playerName);
                else
                    Code.MsgBox.Show("Player has no uploads to vBAddict, profile lookup is cancelled", "Player has no vBAddice profile");
            }
            else
            {
                ExternalPlayerProfile.vBAddict(playerName);
            }
                
        }

        private static void ToolStripItem_NoobmeterPP_Click(object sender, EventArgs e)
        {
            string playerName = dataGridRightClick.Rows[dataGridRightClickRow].Cells["Player"].Value.ToString();
            string playerAccountId = dataGridRightClick.Rows[dataGridRightClickRow].Cells["AccountId"].Value.ToString();
            ExternalPlayerProfile.Noobmeter(playerName, playerAccountId);
        }

        private static void ToolStripItem_Wot_LifePP_Click(object sender, EventArgs e)
        {
            string playerName = dataGridRightClick.Rows[dataGridRightClickRow].Cells["Player"].Value.ToString();
            ExternalPlayerProfile.Wot_Life(playerName);
        }

        private static void ToolStripItem_WoTstatsPP_Click(object sender, EventArgs e)
        {
            string playerName = dataGridRightClick.Rows[dataGridRightClickRow].Cells["Player"].Value.ToString();
            ExternalPlayerProfile.WoTstats(playerName);
        }

        public static void GetvBAddictPlayers(int battleId)
        {
            List<string> allPlayers = new List<string>();
            vBAddictPlayers = new List<string>();
            string sql =
                "select battlePlayer.accountId " +
                "from battlePlayer " +
                "where battleId=@battleId;";
            DB.AddWithValue(ref sql, "@battleId", battleId, DB.SqlDataType.Int);
            DataTable dt = DB.FetchData(sql);
            // Terminate if none found
            if (dt.Rows.Count == 0)
                return;
            // Get all players
            foreach (DataRow dr in dt.Rows)
            {
                allPlayers.Add(dr["accountId"].ToString());
            }
            // Get vBAddict players
            string error = "";
            vBAddictPlayers = vBAddictHelper.SearchForUser(allPlayers, out error);
            vBAddictPlayersManualLookup = false;
        }

    }
}
