using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Bitmap image2, image3, image4;

		List<int> lr, lg, lb;

        private void ShowPictures()
        {
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

            image2 = new Bitmap(openFileDialog1.FileName, true);
            pictureBox2.Image = image2;
            image3 = new Bitmap(openFileDialog1.FileName, true);
            pictureBox3.Image = image3;
            image4 = new Bitmap(openFileDialog1.FileName, true);
            pictureBox4.Image = image4;

			lr = new List<int>();
			lg = new List<int>();
			lb = new List<int>();

			for (int i = 0; i < 256; ++i) {
				lr.Add(0);
				lg.Add(0);
				lb.Add(0);
			}

            for (int x = 0; x < image2.Width; x++) {
                for (int y = 0; y < image2.Height; y++) {

                    Color pixelColor = image2.GetPixel(x, y);

                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    image2.SetPixel(x, y, newColor);

                    newColor = Color.FromArgb(0, pixelColor.G, 0);
                    image3.SetPixel(x, y, newColor);

                    newColor = Color.FromArgb(0, 0, pixelColor.B);
                    image4.SetPixel(x, y, newColor);

					++lr[pixelColor.R];
					++lg[pixelColor.G];
					++lb[pixelColor.B];
                }
            }

			chart3.Series["Series1"].Points.Clear();
			chart4.Series["Series1"].Points.Clear();
			chart5.Series["Series1"].Points.Clear();

			for (int i = 0; i < 256; ++i) { 
				chart3.Series["Series1"].Points.AddY(lr[i]);
				chart3.Series["Series1"].Points[i].Color = Color.Red;
				chart4.Series["Series1"].Points.AddY(lg[i]);
				chart4.Series["Series1"].Points[i].Color = Color.Green;
				chart5.Series["Series1"].Points.AddY(lb[i]);
				chart5.Series["Series1"].Points[i].Color = Color.Blue;
			}
			chart3.Update();
			chart4.Update();
			chart5.Update();
		}

        private void button1_Click(object sender, EventArgs e) {

            openFileDialog1.ShowDialog();
            ShowPictures();
        }
       
    }
}
