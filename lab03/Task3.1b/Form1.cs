using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class Form1 : Form
    {
        Color CurrentColor = Color.Black;
        bool isPressed = false;
        bool choose_point = false;
        bool file_opened = false;
        Graphics g;
        Point CurrentPoint;
        Point PrevPoint;
        bool[,] filled;
        Bitmap img2;

        public Form1()
        {
            InitializeComponent();
            var bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bit;
            g = Graphics.FromImage(bit);
            g.Clear(Color.White);
            filled = new bool[pictureBox1.Width, pictureBox1.Height];
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            //g.Dispose();
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            pictureBox1.Invalidate();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!choose_point)
            {
                isPressed = true;
                CurrentPoint = e.Location;
            }
            else if(file_opened)
                Fill_by_pic(e.Location);
        }

        private void paint_simple()
        {
            Pen p = new Pen(CurrentColor);
            g.DrawLine(p, PrevPoint, CurrentPoint);
            pictureBox1.Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed && !choose_point)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                paint_simple();
            }
        }

        private void choose_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
                choose_point = true;
            else
                choose_point = false;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void open_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            file_opened = true;
        }

        private int Fill_untill_border(int cur_X, int cur_Y, int dir, int border)
        {
            while (cur_X*(-dir) >= border)
            {
                Color cur_color = ((Bitmap)pictureBox1.Image).GetPixel(cur_X, cur_Y);
                if (cur_color.ToArgb() == CurrentColor.ToArgb())
                    break;
                ((Bitmap)pictureBox1.Image).SetPixel(cur_X, cur_Y, img2.GetPixel(cur_X, cur_Y));
                filled[cur_X, cur_Y] = true;
                cur_X += dir;
            }
            return cur_X - dir;
        }

        private void Fill_by_pixels(int cur_X, int cur_Y)
        {
            Color pixelColor = ((Bitmap)pictureBox1.Image).GetPixel(cur_X, cur_Y);
            if (pixelColor.ToArgb() == CurrentColor.ToArgb() || filled[cur_X, cur_Y])
                return;
            int left_border = Fill_untill_border(cur_X, cur_Y, -1, 0);
            int right_border = Fill_untill_border(cur_X, cur_Y, 1, 1-pictureBox1.Width);
            if(cur_Y < pictureBox1.Height - 1)
                for (int x = left_border; x < right_border + 1; ++x)
                    Fill_by_pixels(x, cur_Y+1);
            if (cur_Y > 0)
                for (int x = left_border; x < right_border + 1; ++x)
                    Fill_by_pixels(x, cur_Y - 1);
        }

        private void Fill_by_pic(Point start)
        {
            Image img = Image.FromFile(openFileDialog1.FileName);
            img2 = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
            Fill_by_pixels(start.X, start.Y);
            for (int x = 0; x < img2.Width; ++x)
                for (int y = 0; y < img2.Height; ++y)
                    filled[x,y] = false;
            pictureBox1.Invalidate();
        }
    }
}
