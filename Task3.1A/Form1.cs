using System;
using System.Drawing;
using System.Windows.Forms;


//lab3
namespace Lab3
{
    public partial class Form1 : Form
    {
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            colorDialog1.Color = Color.Black;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height); 
            g = Graphics.FromImage(pictureBox1.Image);
        }
        private bool isPressed = false;
        private Point Curr, Prev;


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Cursor == Cursors.Default)
                this.Cursor = Cursors.Hand;
            else this.Cursor = Cursors.Default;
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Invalidate();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            button1.BackColor = colorDialog2.Color;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            Curr = e.Location;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                Prev = Curr;
                Curr = e.Location;
                Pen p = new Pen(colorDialog1.Color, 3);
                g.DrawLine(p, Prev, Curr);
                pictureBox1.Invalidate();
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Hand)
            {
                borderColor = GetColor(e.X, e.Y);
                fill(e.X, e.Y);
            }
        }

        private Color GetColor(int X, int Y)
        {
            Color clr = (pictureBox1.Image as Bitmap).GetPixel(X, Y);
            return clr;
        }
        
        private Color borderColor;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fill (int x, int y)
        {
            if (GetColor(x, y) == borderColor)
            {
                int xl = x, xr = x;
                while (--xl >= 0 && GetColor(xl, y) == borderColor)
                    /* empty*/
                    ;
                while (++xr < pictureBox1.Width && GetColor(xr, y) == borderColor)
                    /* empty*/
                    ;
                Pen p = new Pen(colorDialog2.Color);
                if (xl + 1 == xr - 1)
                {
                    (pictureBox1.Image as Bitmap).SetPixel(xl + 1, y, colorDialog2.Color);
                }
                else
                {
                    g.DrawLine(p, new Point(xl + 1, y), new Point(xr - 1, y));
                }

                pictureBox1.Invalidate();
                
                if (y - 1 >= 0)
                {
                    for (int i = xl + 1; i < xr; ++i)
                        fill(i, y - 1);
                }
                if (y + 1 < pictureBox1.Height)
                {
                    for (int i = xl + 1; i < xr; ++i)
                        fill(i, y + 1);
                }
                    
            }
        }

    }
}
