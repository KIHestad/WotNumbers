using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApp.Code;
using WinApp.Code.FormLayout;

namespace WinApp.Forms.Settings
{
    public partial class AppSettingsLayout : UserControl
    {
        private bool currentMasteryBadgeIcons;
        private static string currentValue = "";
        private static string currentRatingColorValue = "";
        private static string currentBattleViewMode = "";

        public AppSettingsLayout()
        {
            InitializeComponent();
        }

        private void AppSettingsLayout_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void DataBind()
        {
            chkBattleTotalsPosition.Checked = Config.Settings.gridBattlesTotalsTop;
            int fs = Config.Settings.gridFontSize;
            if (fs == 0) fs = 8;
            if (fs < 6) fs = 6;
            if (fs > 14) fs = 14;
            ddFontSize.Text = fs.ToString();
            ddRatingColor.Text = Config.Settings.RatingColors.ToString().Replace("_", " ");
            currentMasteryBadgeIcons = Config.Settings.useSmallMasteryBadgeIcons;
            chkSmallMasteryBadgeIcons.Checked = currentMasteryBadgeIcons;
            ddBattleViewMode.Text = Config.Settings.battleViewMode.ToString();

            EditChangesApply(false);
        }

        private void EditChangesApply(bool changesApplied)
        {
            AppSettingsHelper.ChangesApplied = changesApplied;
            btnCancel.Enabled = changesApplied;
            btnSave.Enabled = changesApplied;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            Config.Settings.gridBattlesTotalsTop = chkBattleTotalsPosition.Checked;
            Config.Settings.gridFontSize = Convert.ToInt32(ddFontSize.Text);
            string ratingColorsEnumText = ddRatingColor.Text.Replace(" ", "_");
            Config.Settings.RatingColors = (ColorRangeScheme.RatingColorScheme)Enum.Parse(typeof(ColorRangeScheme.RatingColorScheme), ratingColorsEnumText);
            Config.Settings.useSmallMasteryBadgeIcons = chkSmallMasteryBadgeIcons.Checked;
            if (ddBattleViewMode.Text == "Old")
            {
                Config.Settings.battleViewMode = ConfigData.BattleViewMode.Old;
            }
            else
            {
                Config.Settings.battleViewMode = ConfigData.BattleViewMode.New;
            }

            await Config.SaveConfig();
            // Load new mastery badge icons if changed
            if (currentMasteryBadgeIcons != chkSmallMasteryBadgeIcons.Checked)
                ImageHelper.CreateMasteryBageImageTable();

            EditChangesApply(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataBind();
            EditChangesApply(false);
        }

        private void chkBattleTotalsPosition_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private void chkSmallMasteryBadgeIcons_Click(object sender, EventArgs e)
        {
            EditChangesApply(true);
        }

        private async void ddFontSize_Click(object sender, EventArgs e)
        {
            currentValue = ddFontSize.Text;
            await Code.DropDownGrid.Show(ddFontSize, Code.DropDownGrid.DropDownGridType.List, "6,7,8,9,10,11,12,14");
        }

        private void ddFontSize_TextChanged(object sender, EventArgs e)
        {
            if (currentValue != ddFontSize.Text)
                EditChangesApply(true);
        }

        private async void ddRatingColor_Click(object sender, EventArgs e)
        {
            currentRatingColorValue = ddRatingColor.Text;
            await Code.DropDownGrid.Show(ddRatingColor, Code.DropDownGrid.DropDownGridType.List, "WN Official Colors,WoT Labs Colors,XVM Colors");
        }

        private void ddRatingColor_TextChanged(object sender, EventArgs e)
        {
            if (currentRatingColorValue != ddRatingColor.Text)
                EditChangesApply(true);
        }

        private async void ddBattleViewMode_Click(object sender, EventArgs e)
        {
            currentBattleViewMode = ddBattleViewMode.Text;
            await Code.DropDownGrid.Show(ddBattleViewMode, Code.DropDownGrid.DropDownGridType.List, "Old, New");
        }

        private void ddBattleViewMode_TextChanged(object sender, EventArgs e)
        {
            if (currentBattleViewMode != ddBattleViewMode.Text)
            {
                if (ddBattleViewMode.Text == "Old")
                {
                    lblBattleViewMode.Text = "Show battles deduced from dossier files. Don't show battle file only deduced battles.";
                }
                else
                {
                    lblBattleViewMode.Text = "Show battles deduced from battle files. Don't show dossier file only deduced battles.";
                }

                EditChangesApply(true);
            }
        }

    }
}
