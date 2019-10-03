using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Task
{
    public partial class Form1 : Form
    {
        bool drawing; //рисуем ли мы на данный момент?
        int cnt; //счетчик для прорисовки ребра
        Bitmap bmp;
        

        Tuple<double, double> dot;
        bool method = false; //применимость(true) или проверка
        bool t5 = false;
        Matrixes m = new Matrixes();

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
            label5.Visible = false;
        }

        //удаление всех объектов
        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
            list.Clear();
            primitiv.Clear();
            dot = Tuple.Create(-1.0, -1.0);
            cnt = 0;
            label5.Text = "";
            comboBox1.SelectedItem = "...";
            t5 = false;
        }

        List<Tuple<double, double>> primitiv = new List<Tuple<double,double>>(); //список точек для примитива
        List<Point> list = new List<Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((radioButton2.Checked && (cnt < 1 || cnt < 2 && t5)) || radioButton3.Checked) //для ребра больше одной линии нельзя
            {
                drawing = true;
                primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                list.Add(new Point(e.X, e.Y));
                cnt++;
            }

            if (radioButton1.Checked && dot.Item1 == -1)
            {
                dot = Tuple.Create(e.X * 1.0, e.Y * 1.0);
                ((Bitmap)pictureBox1.Image).SetPixel(e.X, e.Y, Color.Black);

                pictureBox1.Image = pictureBox1.Image;
            }
        }


        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

            button2.Visible = true;

            if ((radioButton2.Checked && (cnt > 1 && !t5 || cnt > 2 && t5)) || ! drawing ) return;

            list.Add(new Point(e.X, e.Y));
            primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
            Point start = list.First();

            if (!t5)
            {
                foreach (var p in list)
                {
                    var pen = new Pen(Color.Black, 3);
                    var g = Graphics.FromImage(pictureBox1.Image);
                    g.DrawLine(pen, start, p);
                    pen.Dispose();
                    g.Dispose();
                    pictureBox1.Image = pictureBox1.Image;

                    start = p;
                }
            }
            else
            {
                var pen = new Pen(Color.Black, 3);
                var g = Graphics.FromImage(pictureBox1.Image);
                g.DrawLine(pen, start, new Point(Convert.ToInt32(Math.Round(primitiv[1].Item1)), Convert.ToInt32(Math.Round(primitiv[1].Item2))));
                g.DrawLine(pen, new Point(Convert.ToInt32(Math.Round(primitiv[2].Item1)), Convert.ToInt32(Math.Round(primitiv[2].Item2))), 
                    new Point(Convert.ToInt32(Math.Round(primitiv[3].Item1)), Convert.ToInt32(Math.Round(primitiv[3].Item2))));
                pen.Dispose();
                g.Dispose();
                pictureBox1.Image = pictureBox1.Image;
            }

            drawing = false;
        }

        private void button1_Click(object sender, EventArgs e){
            Clear();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e){
            list.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e){
            list.Clear();
        }


        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
        }

        private float Border_X(float x)
        {
            return Math.Min(pictureBox1.Size.Width, Math.Max(0, x));
        }

        private float Border_Y(float y)
        {
            return Math.Min(pictureBox1.Size.Height, Math.Max(0, y));
        }

        private void choose_method()
        {
            //Выбор матрицы афинного преобразования
            switch (comboBox1.SelectedItem.ToString())
            { 
                case "Смещение":


                    break;
                
                case "Масштабирование":

                    break;

                case "Поворот":
                    bool dir;
                    if (primitiv[0].Item1 < primitiv[1].Item1)
                    {
                        dir = false;
                    }
                    else
                    {
                        dir = true;
                    }
                    double cen_x = ((primitiv[0].Item1 + primitiv[1].Item1)/2);
                    double cen_y = ((primitiv[0].Item2 + primitiv[1].Item2)/2);
                    Tuple<Tuple<double,double>, Tuple<double, double>> res = m.Rotate_Edge_90_Grad(
                        primitiv[0].Item1-cen_x,
                        primitiv[0].Item2-cen_y,
                        primitiv[1].Item1-cen_x,
                        primitiv[1].Item2-cen_y,
                        true);
                    var pen = new Pen(Color.Red, 3);
                    var g = Graphics.FromImage(pictureBox1.Image);
                    g.DrawLine(pen,
                        Border_X((float)(res.Item1.Item1+cen_x)),
                        Border_Y((float)(res.Item1.Item2+cen_y)),
                        Border_X((float)(res.Item2.Item1+cen_x)),
                        Border_Y((float)(res.Item2.Item2+cen_y)));
                    pictureBox1.Image = pictureBox1.Image;


                    break;

                case "Положение точки относительно ребра":


                    break;

                case "Поиск точки пересечения двух ребер":

                    break;

                default:
                    break;
            }
        }

        //для очищения экрана для перерисовке при применении алгоритма 
        private void ClearWithout()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }



		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Смещение":
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите точку смещения";
                    textBox2.Visible = true;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    //method = true;
                    label5.Visible = false;
                    break;

                case "Поворот":
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите точку поворота";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    textBox3.Visible = true;
                    label5.Visible = false;
                    //method = true;
                    break;

                case "Масштабирование":
                    label2.Visible = true;
                    label5.Visible = false;
                    textBox1.Visible = true;
                    label2.Text = "Выберите коэффициент масштабирования";
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    //method = true;
                    break;

                case "Положение точки относительно ребра":
                    label2.Visible = false;
                    label5.Visible = true;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    //method = false;
                    break;

                case "Принадлежит ли точка многоугольнику":
                    label2.Visible = false;
                    label5.Visible = true;
                    label5.Text = "Принадлежит точка многоугольнику: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    //method = false;
                    break;

                case "Поиск точки пересечения двух ребер":
                    label5.Visible = true;
                    label5.Text = "Нарисуйте новую прямую: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    //method = false;
                    t5 = true;
                    break;

                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choose_method();
        }
        // public Tuple<int,int> Intersection(int x11,int y11,int x12,int y12,int x21,int y21,int x22,int y22)
        // {

        // }
    }
}
