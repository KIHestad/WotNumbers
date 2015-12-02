using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class Replay : Form
    {
        private int _battleId { get; set; }
        public Replay(int battleId)
        {
            InitializeComponent();
            _battleId = battleId;
        }

        private void Replay_Shown(object sender, EventArgs e)
        {
            FileInfo fi = ReplayHelper.GetReplayFile(_battleId);
            if (fi != null)
                txtReplayFile.Text = fi.FullName;
            else
                txtReplayFile.Text = "Did not find any replay file for this battle";
        }

    }
}
