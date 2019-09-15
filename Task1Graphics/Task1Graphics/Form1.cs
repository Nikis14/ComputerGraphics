using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1Graphics
{
    public partial class Form1 : Form
    {
        Bitmap[] images = new Bitmap[5];

        int curr_radio = 0;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1200, 900);
        }

        private string ExecuteFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG" ;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                 return ofd.FileName;
            }
            else
            {
                return "NONINIT";
            }
   
        }

        private Bitmap GenGreyNoMult(Bitmap source)
        {
            Bitmap res = new Bitmap(source.Width,source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int i1 = 0; i1 < source.Height; i1++)
                {
                    Color c = source.GetPixel(i, i1);
                    int br = (int)Math.Ceiling(0.33 * c.R + 0.33 * c.G + 0.33 * c.B);
                    res.SetPixel(i, i1,Color.FromArgb(br,br,br));
                }
            }
            return res;
        }

        private Bitmap GenGreyMult(Bitmap source)
        {
            Bitmap res = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int i1 = 0; i1 < source.Height; i1++)
                {
                    Color c = source.GetPixel(i, i1);
                    int br = (int)Math.Ceiling(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                    res.SetPixel(i, i1, Color.FromArgb(br, br, br));
                }
            }
            return res;
        }

        private Bitmap GenDiff(Bitmap source1,Bitmap source2)
        {
            Bitmap res = new Bitmap(source1.Width, source1.Height);
            for (int i = 0; i < source1.Width; i++)
            {
                for (int i1 = 0; i1 < source1.Height; i1++)
                {                   
                    int diff = Math.Abs(source1.GetPixel(i, i1).R - source2.GetPixel(i, i1).R);
                    res.SetPixel(i, i1, Color.FromArgb(diff, diff, diff));
                }
            }
            return res;
        }

        private void drawHistRectangle(int x,int value,Bitmap source)
        {
            for (int i = 601; i > 600 - value; i--)
            {
                source.SetPixel(x, i,Color.Red);
                source.SetPixel(x+1, i, Color.Red);
                source.SetPixel(x + 2, i, Color.Red);
                source.SetPixel(x + 3, i, Color.Red);
            }
        }

        private Bitmap GenHistogram(Bitmap source1)
        {
            
            Dictionary<int, int> pixelintensities = new Dictionary<int, int>();
            for (int i = 0; i < 256; i++)
            {
                pixelintensities.Add(i, 0);
            }
            Bitmap res = new Bitmap(1024,605);
            for (int i = 0; i < source1.Width; i++)
            {
                for (int i1 = 0; i1 < source1.Height; i1++)
                {
                    int x = source1.GetPixel(i, i1).B;
                    pixelintensities[x] += 1;
                }
            }
            double modifier = 600.0 / pixelintensities.Values.Max();
            for (int i = 0; i < 256; i++)
            {
                double tmp = pixelintensities[i] * modifier;
                drawHistRectangle(i*4, (int)Math.Floor(pixelintensities[i] * modifier), res);
            }
            return res;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            images[0] = new Bitmap(ExecuteFileDialog());
            pictureBox1.Image = images[0];
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            images[1] = GenGreyNoMult(images[0]);
            images[2] = GenGreyMult(images[0]);
            images[3] = GenDiff(images[1], images[2]);
            images[4] = GenHistogram(images[3]);
        }
            

        private void radioButton_checkedChanged(object sender,EventArgs e)
        {
            pictureBox1.Image = null;
            if (radioButton1.Checked == true)
            {
                pictureBox1.Image = images[0];
            }
            if (radioButton2.Checked == true)
            {
                pictureBox1.Image = images[1];
            }
            if (radioButton3.Checked == true)
            {
                pictureBox1.Image = images[2];
            }
            if (radioButton4.Checked == true)
            {
                pictureBox1.Image = images[3];
            }
            if (radioButton5.Checked == true)
            {
                pictureBox1.Image = images[4];
            }
        }
    }
}
