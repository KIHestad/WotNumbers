using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinApp.Code;

namespace WinApp.Forms
{
    public partial class TestShowImage : Form
    {
        public TestShowImage()
        {
            InitializeComponent();
        }


        private static Image getImage(int i)
        {
            DataTable dtImg = DB.FetchData("SELECT img, smallImg, contourImg FROM tank WHERE id=1");
            byte[] rawImg = (byte[])dtImg.Rows[0][i];
            MemoryStream ms = new MemoryStream(rawImg);
            Image image = Image.FromStream(ms);
            ms.Close();
            return image;
        }

        private void TestShowImage_Load(object sender, EventArgs e)
        {
            PictureBox pb;

            pb = new PictureBox();
            pb.Image = getImage(0);
            pb.Location = new Point(20, 40);  // position from left/top
            pb.Size = new System.Drawing.Size(160, 100);  // width/height
            TestShowImageTheme.Controls.Add(pb);

            pb = new PictureBox();
            pb.Image = getImage(1);
            pb.Location = new Point(20, 150);
            pb.Size = new System.Drawing.Size(124, 31);
			TestShowImageTheme.Controls.Add(pb);

            pb = new PictureBox();
            pb.Image = getImage(2);
            pb.Location = new Point(20, 190);
            pb.Size = new System.Drawing.Size(65, 24);
			TestShowImageTheme.Controls.Add(pb);


            

        }

        private void chartBtn_Click(object sender, EventArgs e)
        {
            // Chart

            DataTable d = DB.FetchData("SELECT fiDate, bpBattleCount, bpSpotted from wsDossier where cmTankId = 33 and cmCountryId = 1 order by cmId");  // Panther II

            int row = 0;
            while (row < d.Rows.Count)
            {
                testChart1.Series["Battles"].Points.AddXY(Convert.ToDateTime(d.Rows[row]["fiDate"]), Convert.ToInt32(d.Rows[row]["bpBattleCount"]));
                testChart1.Series["Spot"].Points.AddXY(Convert.ToDateTime(d.Rows[row]["fiDate"]), Convert.ToInt32(d.Rows[row]["bpSpotted"]));
                row++;
            }

            //testChart1.Series["s1"].ChartType = SeriesChartType.FastLine;
            //testChart1.Series["s1"].Color = Color.Red;
            
        }




        

        private void chartBtn1_Click(object sender, EventArgs e)
        {
            DataTable d = DB.FetchData("SELECT fiDate, bpBattleCount, bpXP from wsDossier where cmTankId = 24 and cmCountryId = 2 order by cmId");
            
            var testChart2 = new Chart();
            testChart2.Size = new Size(600, 250);
            testChart2.Location = new System.Drawing.Point(379, 358);

            var chartArea = new ChartArea();
            chartArea.AxisX.LabelStyle.Format = "dd/MMM\nhh:mm";
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            testChart2.ChartAreas.Add(chartArea);

            var Series = new Series();
            Series.Name = "s1";
            Series.ChartType = SeriesChartType.FastLine;
            Series.XValueType = ChartValueType.DateTime;
            testChart2.Series.Add(Series);

            int row = 0;
            //while (row < d.Rows.Count)
            while (row < 50)
            {
                testChart2.Series["s1"].Points.AddXY(Convert.ToDateTime(d.Rows[row]["fiDate"]), Convert.ToInt32(d.Rows[row]["bpBattleCount"]));
                row++;
            }

            testChart2.Series["s1"].ChartType = SeriesChartType.FastLine;
            testChart2.Series["s1"].Color = Color.Red;

            testChart2.Invalidate();


        }

    }
}
