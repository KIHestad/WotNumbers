using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinApp.Code;
using System.IO;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsReplay : UserControl
    {
        public AppSettingsReplay()
        {
            InitializeComponent();
        }

        private void AppSettingsReplay_Load(object sender, EventArgs e)
        {
            // Style datagrid
            GridHelper.StyleDataGrid(dataGridReplayFolder);
            // Show content
            ShowReplayFolders();
        }

        private void ShowReplayFolders()
        {
            DataTable dt = DB.FetchData("select path as 'Path', '' as 'Sub', id, subfolder from replayFolder order by path");
            // Modify datatable by adding values to '*' column, indication subfolders included
            foreach (DataRow row in dt.Rows)
            {
                bool subfolder = Convert.ToBoolean(row["subfolder"]);
                if (subfolder) row["Sub"] = "X";
                row.AcceptChanges();
            }
            // Show in grid
            dataGridReplayFolder.DataSource = dt;
            // Format
            dataGridReplayFolder.Columns["Sub"].Width = 40;
            dataGridReplayFolder.Columns["Sub"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridReplayFolder.Columns["Path"].Width = dataGridReplayFolder.Width - dataGridReplayFolder.Columns["Sub"].Width - 2;
            dataGridReplayFolder.Columns["id"].Visible = false;
            dataGridReplayFolder.Columns["subfolder"].Visible = false;
            // Unfocus
            dataGridReplayFolder.ClearSelection();
            // Connect to scrollbar
            scrollY.ScrollElementsTotals = dt.Rows.Count;
            scrollY.ScrollElementsVisible = dataGridReplayFolder.DisplayedRowCount(false);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDBPath.ShowNewFolderButton = false;
            // Select WoT Game folder as default if it exists
            string defaultFolder = ReplayHelper.GetWoTDefaultReplayFolder();
            // Select current folder if any selected
            if (dataGridReplayFolder.SelectedRows.Count == 1)
            {
                defaultFolder = dataGridReplayFolder.SelectedRows[0].Cells["path"].Value.ToString();
            }
            if (defaultFolder != "")
                folderBrowserDialogDBPath.SelectedPath = defaultFolder;
            DialogResult result = folderBrowserDialogDBPath.ShowDialog();
            // If file selected save config with new values
            if (result == DialogResult.OK)
            {
                string path = folderBrowserDialogDBPath.SelectedPath;
                MsgBox.Button answer = MsgBox.Show("Does subfolders with replay files exists?", "Include subfolders?", MsgBox.Type.YesNo);
                bool subfolder = (answer == MsgBox.Button.Yes);
                await ReplayHelper.AddReplayFolder(path, subfolder);
                ShowReplayFolders();
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridReplayFolder.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridReplayFolder.SelectedRows[0].Cells["id"].Value);
                await ReplayHelper.RemoveReplayFolder(id);
                ShowReplayFolders();
            }
        }

        private void scrollY_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridReplayFolder.RowCount > 0)
                    dataGridReplayFolder.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
            }
            catch (Exception)
            {

                // Scrolling event error handling
            }
        }

        private void scrollY_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridReplayFolder.RowCount > 0)
                    dataGridReplayFolder.FirstDisplayedScrollingRowIndex = scrollY.ScrollPosition;
            }
            catch (Exception)
            {
                // Scrolling event error handling
            }
        }
    }
}
