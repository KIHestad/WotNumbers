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
using WotDBUpdater.Code;

namespace WotDBUpdater.Forms.Test
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
            pb.Location = new Point(20, 20);  // position from left/top
            pb.Size = new System.Drawing.Size(160, 100);  // width/height
            this.Controls.Add(pb);

            pb = new PictureBox();
            pb.Image = getImage(1);
            pb.Location = new Point(20, 130);
            pb.Size = new System.Drawing.Size(124, 31);
            this.Controls.Add(pb);

            pb = new PictureBox();
            pb.Image = getImage(2);
            pb.Location = new Point(20, 170);
            pb.Size = new System.Drawing.Size(65, 24);
            this.Controls.Add(pb);
        }


    }
}
