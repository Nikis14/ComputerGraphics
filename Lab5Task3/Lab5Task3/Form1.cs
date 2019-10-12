using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5Task3
{
    public partial class Form1 : Form
    {
        Bitmap image;
        bool[] changed = new bool[4];
        PointF[] points = new PointF[4];
        Pen[] pens = new Pen[4];
        int current;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "4 points remaining";
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pens[0] = new Pen(Color.Red, 3);
            pens[1] = new Pen(Color.Aqua, 3);
            pens[2] = new Pen(Color.Purple, 3);
            pens[3] = new Pen(Color.Lime, 3);
            button6.Hide();
        }

        private void DrawPoint(int num, float x, float y)
        {
            changed[num] = true;
            var g = Graphics.FromImage(pictureBox1.Image);
            g.DrawEllipse(pens[num], x - 1, y - 1, 2, 2);
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            List<PointF> points = new List<PointF>();
            for (int i = 0; i < 4; i++)
            {
                changed[i] = false;
            }
            button6.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            current = 0;
            groupBox1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            current = 1;
            groupBox1.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            current = 2;
            groupBox1.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            current = 3;
            groupBox1.Hide();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DrawPoint(current, e.X, e.Y);
            points[current] = new PointF(e.X, e.Y);
            groupBox1.Show();
            if (changed[0] && changed[1] && changed[2] && changed[3])
            {
                button6.Show();
            }
        }

        private PointF calculate(double t)
        {
            double f = (1 - t);
            double x = Math.Pow(f, 3) * points[0].X +
                 3 * Math.Pow(f, 2) * t * points[1].X +
                 3 * f * Math.Pow(t, 2) * points[2].X +
                 Math.Pow(t, 3) * points[3].X;
            double y = Math.Pow(f, 3) * points[0].Y +
                 3 * Math.Pow(f, 2) * t * points[1].Y +
                 3 * f * Math.Pow(t, 2) * points[2].Y +
                 Math.Pow(t, 3) * points[3].Y;
            return new PointF((float)x, (float)y);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            double t = 0.0;
            PointF prev = calculate(0);
            Pen p = new Pen(Color.Black, 1);
            Pen checks = new Pen(Color.Blue);
            while (t <= 1.0)
            {
                PointF next = calculate(t);
                g.DrawLine(p, prev, next);
                t += 0.001;
                prev = next;
            }
            p.Dispose();
            g.DrawBezier(checks,points[0], points[1], points[2], points[3]);
            pictureBox1.Invalidate();

        }
       
    }
}
