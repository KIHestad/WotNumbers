using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class TankSearch : Form
    {
        public TankSearch()
        {
            InitializeComponent();
        }

        private void TankSearch_Load(object sender, EventArgs e)
        {
            // Style toolbar
            toolStripMain.Renderer = new StripRenderer();
            toolStripMain.Left = 1;
        }

        private void TankSearch_Resize(object sender, EventArgs e)
        {
            toolStripMain.Width = this.Width - 2;
        }

        private void mNationToggleAll_Click(object sender, EventArgs e)
        {
            if (mNationToggleAll.Text == "All")
            {
                mNationToggleAll.Text = "None";
                SelectAllNations(true);
            }
            else
            {
                mNationToggleAll.Text = "All";
                SelectAllNations(false);
            }
            SearchNow();
        }

        private void mNation_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            if (mNationSelectMode.Text == "Single" && !btn.Checked)
                SelectAllNations(false);
            btn.Checked = !btn.Checked;
            SearchNow();
        }

        private int GetNationCheckedCount()
        {
            int count = 0;
            for (int i = 0; i <= 7; i++)
            {
                ToolStripButton item = (ToolStripButton)toolStripMain.Items.Find("mNation" + i.ToString(), false)[0];
                if (item.Checked)
                    count++;
            }
            return count;
        }

        private void SelectAllNations(bool select)
        {
            for (int i = 0; i <= 7; i++)
            {
                ToolStripButton item = (ToolStripButton)toolStripMain.Items.Find("mNation" + i.ToString(), false)[0];
                item.Checked = select;
            }
        }

        private void mTxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchNow();
        }

        private void mNationSelectMode_Click(object sender, EventArgs e)
        {
            if (mNationSelectMode.Text == "Single")
            {
                mNationSelectMode.Text = "Multi";
                mNationToggleAll.Visible = true;
            }
            else
            {
                mNationSelectMode.Text = "Single";
                mNationToggleAll.Visible = false;
                mNationToggleAll.Text = "All";
                if (GetNationCheckedCount() > 1)
                    SelectAllNations(false);
            }
        }

        private void SearchNow()
        {

        }

        private void toolAllTanks_Toggle_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}
