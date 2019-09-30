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
        bool isPressed = false;
        Matrixes matr = new Matrixes();
        Tuple<double, double> dot;
        bool method = false; //применимость(true) или проверка
        bool t5 = false;

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
            label5.Visible = false;
            choose_point.Visible = false;
        }

        //удаление всех объектов
        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            isPressed = false;
            choose_point.Checked = false;
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
            if (!isPressed)
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

                    pictureBox1.Invalidate();
                }
            }
            else
            {
                textBox1.Text = e.X.ToString();
                textBox2.Text = e.Y.ToString();
            }
        }


        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isPressed)
            {
                button2.Visible = true;

                if ((radioButton2.Checked && (cnt > 1 && !t5 || cnt > 2 && t5)) || !drawing) return;

                list.Add(new Point(e.X, e.Y));
                primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                Point start = list.First();

                if (!t5)
                {
                    foreach (var p in list)
                    {
                        var pen = new Pen(Color.Black, 1);
                        var g = Graphics.FromImage(pictureBox1.Image);
                        g.DrawLine(pen, start, p);
                        pen.Dispose();
                        g.Dispose();
                        pictureBox1.Invalidate();

                        start = p;
                    }
                }
                else
                {
                    var pen = new Pen(Color.Black, 1);
                    var g = Graphics.FromImage(pictureBox1.Image);
                    g.DrawLine(pen, start, new Point(Convert.ToInt32(Math.Round(primitiv[1].Item1)), Convert.ToInt32(Math.Round(primitiv[1].Item2))));
                    g.DrawLine(pen, new Point(Convert.ToInt32(Math.Round(primitiv[2].Item1)), Convert.ToInt32(Math.Round(primitiv[2].Item2))),
                        new Point(Convert.ToInt32(Math.Round(primitiv[3].Item1)), Convert.ToInt32(Math.Round(primitiv[3].Item2))));
                    pen.Dispose();
                    g.Dispose();
                    pictureBox1.Invalidate();
                }

                drawing = false;
            }
        }

        private void button1_Click(object sender, EventArgs e){
            Clear();
        }

      


        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
        }


        private void draw_new_polygon(List<Point> points)
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            var pen = new Pen(Color.Black, 3);
            for (int i = 0; i < points.Count() - 1; i++)
            {
                g.DrawLine(pen, points[i], points[i + 1]);
            }
            //g.DrawLine(pen, points[0], points[points.Count() - 1]);
            pictureBox1.Invalidate();
        }

        private void get_center_fig(ref int a, ref int b)
        {
            a = 0;
            b = 0;
            foreach(Point p in list)
            {
                a += p.X;
                b += p.Y;
            }

            a /= list.Count();
            b /= list.Count();
        }

        private bool check_is_right(Point p1, Point p2, Point p3)
        {
            int xa = p2.X - p1.X;
            int ya = p2.Y - p1.Y;
            int xb = p3.X - p1.X;
            int yb = p3.Y - p1.Y;
            return yb*xa - xb*ya <= 0;
        }

        private bool get_intersect(Point a, Point b, Point c, Point d, ref Point res)
        {
            int div = (d.X-c.X)*(b.Y-a.Y) - (d.Y - c.Y) * (b.X - a.X);
            if (div == 0 && (b.X == c.X || b.Y == c.Y))
            {
                res = new Point(-1, -1);
                return true;
            }
            if(div == 0)
            {
                res = new Point(-1, -1);
                return false;
            }
            double t2 = (a.X * (b.Y - a.Y) - a.Y * (b.X - a.X) + c.Y * (b.X - a.X) - c.X * (b.Y - a.Y)) / (double)div;
            double t1;
            if (b.X == a.X)
                t1 = (c.Y + (d.Y - c.Y) * t2 - a.Y) / (double)(b.Y - a.Y);
            else
                t1 = (c.X + (d.X - c.X) * t2 - a.X) / (double)(b.X - a.X);
            int x = (int)(a.X + (b.X - a.X) * t1);
            int y = (int)(a.Y + (b.Y - a.Y) * t1);
            res = new Point(x, y);
            return true;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        private float Border_X(float x)
        {
            return Math.Min(pictureBox1.Size.Width, Math.Max(0, x));
        }

        private float Border_Y(float y)
        {
            return Math.Min(pictureBox1.Size.Height, Math.Max(0, y));
        }

        private Point Tuple_Double_ToPoint(Tuple<double,double> t)
        {
            return new Point((int)Math.Round(t.Item1), (int)Math.Round(t.Item2));
        }

        private bool point_between(Point target, Point source1,Point source_2)
        {
            bool x_beetween = target.X <= Math.Max(source1.X, source_2.X) && target.X >= Math.Min(source1.X, source_2.X);
            bool y_beetween = target.Y <= Math.Max(source1.Y, source_2.Y) && target.Y >= Math.Min(source1.Y, source_2.Y);
            return x_beetween && y_beetween;
        }

        private void choose_method()
        {
            //Выбор матрицы афинного преобразования
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Смещение":
                    int dx, dy;
                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        dx = Int32.Parse(textBox1.Text);
                        dy = Int32.Parse(textBox2.Text);
                    }
                    else
                        break;
                    double[,] afin_matrix = matr.matrix_offset(dx, dy);
                    list = matr.get_transformed_points(afin_matrix, list);
                    draw_new_polygon(list);
                    break;

                case "Масштабирование":
                    int x = 0, y = 0;
                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        x = Int32.Parse(textBox1.Text);
                        y = Int32.Parse(textBox2.Text);
                    }
                    else
                        get_center_fig(ref x, ref y);
                    double koef;
                    if (textBox3.Text != "")
                        koef = Double.Parse(textBox3.Text);
                    else
                        break;
                    afin_matrix = matr.matrix_scale(koef, x, y);
                    list = matr.get_transformed_points(afin_matrix, list);
                    var pen = new Pen(Color.Red, 3);
                    var g = Graphics.FromImage(pictureBox1.Image);
                    g.DrawLine(pen,
                        Border_X((list[0].X)),
                        Border_Y((list[0].Y)),
                        Border_X((list[1].X)),
                        Border_Y((list[1].Y)));
                    pictureBox1.Image = pictureBox1.Image;
                    break;

                case "Поворот":
                    int a = 0, b = 0;
                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        a = Int32.Parse(textBox1.Text);
                        b = Int32.Parse(textBox2.Text);
                    }
                    else
                        get_center_fig(ref a, ref b);
                    int angle;
                    if (textBox3.Text != "")
                        angle = Int32.Parse(textBox3.Text);
                    else
                        break;
                    afin_matrix = matr.matrix_rotation(angle, a, b);
                    list = matr.get_transformed_points(afin_matrix, list);
                    draw_new_polygon(list);
                    break;

                case "Поворот отрезка":
                    double cen_x = ((primitiv[0].Item1 + primitiv[1].Item1) / 2);
                    double cen_y = ((primitiv[0].Item2 + primitiv[1].Item2) / 2);
                    afin_matrix = matr.matrix_rotation(90, (int)Math.Round(cen_x), (int)Math.Round(cen_y));
                    list = matr.get_transformed_points(afin_matrix, list);
                    draw_new_polygon(list);
                    break;

                case "Положение точки относительно ребра":
                    if (dot.Item1 == -1 || primitiv.Count != 2)
                        return;
                    Tuple<double, double> cm1 = primitiv.First();

                    label5.Text = "Точка лежит относительно ребра: ";

                    double yb = primitiv.Last().Item2 - cm1.Item2;
                    double xb = primitiv.Last().Item1 - cm1.Item1;
                    double ya = dot.Item2 - cm1.Item2;
                    double xa = dot.Item1 - cm1.Item1;

                    if (yb * xa - xb * ya > 0)
                        label5.Text += " левее";
                    if (yb * xa - xb * ya < 0)
                        label5.Text += " правее";
                    break;

                case "Принадлежит ли точка многоугольнику":
                    Point check = new Point((int)dot.Item1, (int)dot.Item2);
                    Point res = new Point(0,0);
                    Point start = new Point((list[0].X + list[1].X) / 2, (list[0].Y + list[1].Y) / 2);
                    bool isSingle = get_intersect(check, start, list[0], list[1], ref res);
                    if(isSingle && res.X == -1 && res.Y == -1)
                    {
                        label5.Text = "Точка принадлежит многоугольнику";
                        break;
                    }
                    int cnt = 1;
                    for(int i = 1; i < list.Count(); ++i)
                    {
                        int ind = (i + 1) % list.Count();
                        bool is_inter = get_intersect(check, start, list[i], list[ind], ref res);
                        if (is_inter && point_between(res,list[i],list[ind]) && distance(res, start) < distance(res, check))
                            cnt++;
                    }
                    if(cnt%2 == 0)
                        label5.Text = "Точка принадлежит не многоугольнику";
                    else
                        label5.Text = "Точка принадлежит многоугольнику";

                    break;
                    

                case "Поиск точки пересечения двух ребер":
                    Point intersection = new Point(0, 0);
                    bool intersected = get_intersect(
                        Tuple_Double_ToPoint(primitiv[0]),
                        Tuple_Double_ToPoint(primitiv[1]),
                        Tuple_Double_ToPoint(primitiv[2]),
                        Tuple_Double_ToPoint(primitiv[3]),
                        ref intersection);
                    pen = new Pen(Color.Red, 3);
                    g = Graphics.FromImage(pictureBox1.Image);
                    if (intersected &&
                        point_between(
                            intersection,
                            Tuple_Double_ToPoint(primitiv[0]),
                            Tuple_Double_ToPoint(primitiv[1])) &&
                         point_between(
                            intersection,
                            Tuple_Double_ToPoint(primitiv[2]),
                            Tuple_Double_ToPoint(primitiv[3])))
                    { 
                        g.DrawLine(pen, new Point(intersection.X - 1, intersection.Y -1), 
                            new Point(intersection.X+1,intersection.Y+1));
                        pictureBox1.Invalidate();
                        label5.Text = "";
                    }
                    else
                    {
                        label5.Text = "Нет пересечения"; 
                    }
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


        bool less1(double p1, double p2)
        {
            return (p1 < p2 && Math.Abs(p1 - p2) >= 0.00001);
        }

        private void button2_Click1(object sender, EventArgs e)
        {
            choose_method();

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Смещение":
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите смещения по х и y";
                    textBox2.Visible = true;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = true;
                    label5.Visible = false;
                    choose_point.Visible = false;
                    break;

                case "Поворот":
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите точку поворота";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Введите угол поворота";
                    textBox3.Visible = true;
                    label5.Visible = false;
                    choose_point.Visible = true;
                    method = true;
                    break;

                case "Масштабирование":
                    label2.Visible = true;
                    label5.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите точку";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    label3.Text = "Выберите коэф масштабир-я";
                    textBox3.Visible = true;
                    choose_point.Visible = true;
                    method = true;
                    break;

                case "Положение точки относительно ребра":
                    label2.Visible = false;
                    label5.Visible = true;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = false;
                    choose_point.Visible = false;
                    break;

                case "Принадлежит ли точка многоугольнику":
                    label2.Visible = false;
                    label5.Visible = true;
                    label5.Text = "Принадлежит точка многоугольнику: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = false;
                    choose_point.Visible = false;
                    break;

                case "Поиск точки пересечения двух ребер":
                    label5.Visible = true;
                    label5.Text = "Нарисуйте новую прямую: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    label2.Visible = false;
                    textBox3.Visible = false;
                    method = false;
                    t5 = true;
                    choose_point.Visible = false;
                    break;

                default:
                    break;
            }
        }

        private void choose_point_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
                isPressed = true;
            else
                isPressed = false;
        }
    }
}
