﻿using System;
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
            chart2.Hide();
            chart2.Series.RemoveAt(0);
            this.Size = new Size(1200, 700);
            System.Windows.Forms.DataVisualization.Charting.Series ser1 =
                new System.Windows.Forms.DataVisualization.Charting.Series("Difference",256);
            chart2.Series.Add(ser1);
            
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
            chart2.Hide();
            pictureBox1.BringToFront();
            Bitmap res = new Bitmap(source.Width,source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int i1 = 0; i1 < source.Height; i1++)
                {
                    progressBar1.Value +=1;
                    Color c = source.GetPixel(i, i1);
                    int br = (int)Math.Ceiling(0.3 * c.R + 0.5 * c.G + 0.11 * c.B);
                    res.SetPixel(i, i1,Color.FromArgb(br,br,br));
                }
            }
            return res;
        }

        private Bitmap GenGreyMult(Bitmap source)
        {
            chart2.Hide(); 
            pictureBox1.BringToFront();
            Bitmap res = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int i1 = 0; i1 < source.Height; i1++)
                {
                    progressBar1.Value += 1;
                    Color c = source.GetPixel(i, i1);
                    int br = (int)Math.Ceiling(0.21 * c.R + 0.72 * c.G + 0.07 * c.B);
                    res.SetPixel(i, i1, Color.FromArgb(br, br, br));
                }
            }
            return res;
        }

        private Bitmap GenDiff(Bitmap source1,Bitmap source2)
        {
            chart2.Hide();
            pictureBox1.BringToFront();
            Bitmap res = new Bitmap(source1.Width, source1.Height);
            for (int i = 0; i < source1.Width; i++)
            {
                for (int i1 = 0; i1 < source1.Height; i1++)
                {
                    progressBar1.Value += 1;
                    int diff = Math.Abs(source1.GetPixel(i, i1).R - source2.GetPixel(i, i1).R);
                    res.SetPixel(i, i1, Color.FromArgb(diff, diff, diff));
                }
            }
            return res;
        }

        private void drawChart(Dictionary<int,int> dict)
        {

            List<int> c = new List<int>();//A workaround for a chart
            for (int i = 1; i <257; i++)
            {
                c.Add(i);
            }
            chart2.Series["Difference"].Points.DataBindXY(c, dict.Values);
           
        }

        private void GenHistogram(Bitmap source1)
        {
            chart2.Hide();
            Dictionary<int, int> pixelintensities = new Dictionary<int, int>();
            for (int i = 0; i < 256; i++)
            {
                pixelintensities.Add(i, 0);
            }
            for (int i = 0; i < source1.Width; i++)
            {
                for (int i1 = 0; i1 < source1.Height; i1++)
                {
                    progressBar1.Value += 1;
                    int x = source1.GetPixel(i, i1).B;
                    pixelintensities[x] += 1;
                }
            }
            drawChart(pixelintensities);
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            radioButton1.Checked = true;
            images[0] = new Bitmap(ExecuteFileDialog());
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = images[0].Width * images[0].Height * 4;
            pictureBox1.Image = images[0];
            images[1] = GenGreyNoMult(images[0]);
            images[2] = GenGreyMult(images[0]);
            images[3] = GenDiff(images[1], images[2]);
            GenHistogram(images[3]);
        }
            

        private void radioButton_checkedChanged(object sender,EventArgs e)
        {
            chart2.Hide();
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
                chart2.Show();
                chart2.BringToFront();
            }
        }
    }
}
