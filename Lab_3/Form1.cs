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
        Graphics g;
        Point CurrentPoint;
        Point PrevPoint;

        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.Clear(Color.White);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void open_Click(object sender, EventArgs e)
        {

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
        }

        private void paint_simple()
        {
            Pen p = new Pen(CurrentColor);
            g.DrawLine(p, PrevPoint, CurrentPoint);
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
			//jkdjkdkd
        }
    }
}
