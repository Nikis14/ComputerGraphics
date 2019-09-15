using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3Graphics
{
    public partial class Form1 : Form
    {
        Bitmap source;
        int hue_addition = 0;
        int sat_addition = 0;
        int val_addition = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            // Color c = Color.fromHS
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void valuebar_Scroll(object sender, EventArgs e)
        {
            val_addition = valuebar.Value;
            //redraw();
        }

        private void saturationbar_Scroll(object sender, EventArgs e)
        {
            sat_addition = saturationbar.Value;
            //redraw();
        }

        private void huebar_Scroll(object sender, EventArgs e)
        {
            hue_addition = huebar.Value;
            //redraw();
        }

        

        private Tuple<double,double,double> tohsw(Tuple<int, int, int> rgb)
        {
            double h = 0.0;
            double s = 0.0;
            double v = 0.0;
            double redfloat = rgb.Item1 / 255.0;
            double greenfloat = rgb.Item2 / 255.0;
            double bluefloat = rgb.Item3 / 255.0;
            double max = Math.Max(redfloat, Math.Max(greenfloat, bluefloat));
            double min = Math.Min(redfloat, Math.Min(greenfloat, bluefloat));
            double divisor = max - min;
            //Hue
            if (max == min)
            {
                h = 0;
            }
            else if (max == redfloat && greenfloat >= bluefloat)
            {
                h = ((60 * (greenfloat - bluefloat) / divisor));
            }
            else if (max == redfloat && greenfloat < bluefloat)
            {
                h = ((60 * (greenfloat - bluefloat) / divisor)+360);
            }
            else if (max == greenfloat)
            {
                h = ((60 * (bluefloat-redfloat) / divisor) + 120);
            }
            else if (max == bluefloat)
            {
                h = ((60 * (redfloat - greenfloat) / divisor) + 240);
            }
            //Saturation
            if (max == 0)
            {
                s = 0;
            }
            else
            {
                s = 1 - (min / max);
            }
            //Value
            v = max;
            return new Tuple<double, double, double>(Math.Abs((h + hue_addition)%360), Math.Abs(s + ((sat_addition / 100.0) % 1.0)),Math.Abs( v + (val_addition / 100.0)%1.0));
        }

        private Tuple<int,int,int> torgb(Tuple<double, double, double> hsw)
        {
            double hue = hsw.Item1;
            double saturation = hsw.Item2 * 100;
            double value = hsw.Item3 * 100;
            int h_i = (int)Math.Floor(hsw.Item1 / 60) % 6;
            double vmin = ((100 - saturation) * value) / 100;
            double alpha = (value - vmin) * ((hue % 60) / 60);
            double vinc = vmin + alpha;
            double vdec = value - alpha;
            switch (h_i)
            {
                case 0:
                    return new Tuple<int, int, int>((int)(value*2.55), (int)(vinc * 2.55), (int)(vmin * 2.55));
                case 1:
                    return new Tuple<int, int, int>((int)(vdec*2.55), (int)(value*2.55), (int)(vmin*2.55));
                case 2:
                    return new Tuple<int, int, int>((int)(vmin*2.55), (int)(value * 2.55), (int)(vinc * 2.55));
                case 3:
                    return new Tuple<int, int, int>((int)(vmin * 2.55), (int)(vdec * 2.55), (int)(value * 2.55));
                case 4:
                    return new Tuple<int, int, int>((int)(vinc * 2.55), (int)(vmin * 2.55), (int)(value * 2.55));
                case 5:
                    return new Tuple<int, int, int>((int)(value * 2.55), (int)(vmin * 2.55), (int)(vdec * 2.55));

                default:
                    throw new Exception();
            }
        }

        private void redraw()
        {
            Bitmap res = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int i1 = 0; i1 < source.Height; i1++)
                {
                    Tuple<int, int, int> t = new Tuple<int, int, int>(source.GetPixel(i, i1).R,
                        source.GetPixel(i, i1).G,
                        source.GetPixel(i, i1).B);
                    Tuple<int, int, int> tres = torgb(tohsw(t));
                    res.SetPixel(i,i1,Color.FromArgb(255, Math.Abs(tres.Item1 % 256), Math.Abs(tres.Item2 % 256), Math.Abs(tres.Item3 % 256)));
                }
            }
            pictureBox1.Image = res;
        }

        private void openfilebutton_Click(object sender, EventArgs e)
        {
            source = new Bitmap(ExecuteFileDialog());
            redraw();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.hue_addition = 0;
            this.val_addition = 0;
            this.sat_addition = 0;
            saturationbar.Value = 0;
            huebar.Value = 0;
            valuebar.Value = 0;
        }

        private void savefilebutton_Click(object sender, EventArgs e)
        {
            string fname = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = "c:\\";
            sfd.Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fname = sfd.FileName;
            }
            pictureBox1.Image.Save(fname); 
        }

        private string ExecuteFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG";
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

        private void redrawbutton_Click(object sender, EventArgs e)
        {
            redraw();
        }
    }
}
