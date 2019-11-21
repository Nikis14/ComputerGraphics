using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic3D
{
    public partial class Form1 : Form
    {
        double fi1 = 80 * 0.0174532925;  //углы фи и пси для отображения 
        double fi2 = 340 * 0.0174532925;
        int last = 0;

        int upDown = 80;
        int leftRight = 340;

        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(700, 500); //где рисуем

        }

        //нижняя и верхняя граница горизонта
        List<double> maxF;
        List<double> minF;

        //отрисовываем график
        void draw_graphic(int num)
        {
            bmp = new Bitmap(700, 500);
            maxF = new List<double>(720);
            minF = new List<double>(720);


            //40 точек
            List<Point> predLine = new List<Point>(40);
            List<Point> currentLine = new List<Point>(40);
            //заполняем нижнюю и верхнюю границу горизонта
            for (int i = 0; i < 720; i++)
            {
                maxF.Add(0);
                minF.Add(500);
            }

            Pen p = new Pen(Color.Indigo);
            for (double y = -6; y < 6; y += 0.2)
            {
                currentLine = new List<Point>(40);
                for (double x = -6; x < 6; x += 0.2)
                {
                    double z;
                    //графики
                    switch (num)
                    {
                        case 3:
                            z = (0.5) * Math.Cos(Math.Sqrt(x * x + y * y));
                            break;
                        case 4:
                            z = Math.Cos(Math.Sqrt(x * x + y * y));
                            break;
                        default:
                            z = 0;
                            break;
                    }

                    //отображение координат на данной проекции
                    double fx = x * Math.Cos(fi2) - (-Math.Sin(fi1) * y + Math.Cos(fi1) * z) * Math.Sin(fi2);
                    double fy = y * Math.Cos(fi1) + z * Math.Sin(fi1);
                    double fz = (-Math.Sin(fi1) * y + Math.Cos(fi1) * z);

                    //отображение графика на Bitmap
                    int xx2 = (int)Math.Round(fx * 50 + (bmp.Width / 2));
                    int yy2 = (int)Math.Round(fy * 50 + (bmp.Height / 2) - 50);

                    currentLine.Add(new Point(xx2, yy2));

                    //рисует линии по горизонтали
                    if (currentLine.Count > 1)
                        DrawLine(currentLine[currentLine.Count - 1], currentLine[currentLine.Count - 2]);

                    //рисует соединяющие линии для создания сетки
                    if (predLine.Count > 0)
                    {
                        DrawLine(currentLine[currentLine.Count - 1], predLine[currentLine.Count - 1]);
                        if (predLine.Count > 1 && currentLine.Count > 1)
                            DrawLine(currentLine[currentLine.Count - 2], predLine[currentLine.Count - 2]);
                    }

                }
                predLine = currentLine;
            }
            pictureBox1.Image = bmp;
        }

        //алгоритм Брезенхейма
        void DrawLine(Point LineP1, Point LineP2)
        {
            //Если проекция отрезка на ось x меньше проекции на ось y или начало и конец отрезка переставлены местами, то алгоритм не будет работать.
            //Чтобы этого не случилось, нужно проверять направление вектора и его наклон, а потом по необходимости менять местами координаты отрезка и поворачивать оси
            // Проверяем рост отрезка по оси икс и
            var steep = Math.Abs(LineP2.Y - LineP1.Y) > Math.Abs(LineP2.X - LineP1.X);
            if (steep)//если разность между игриками больше, чем между иксами, то меняем местами игрики с иксами
            {
                int k = LineP1.X;
                LineP1.X = LineP1.Y;
                LineP1.Y = k;

                k = LineP2.X;
                LineP2.X = LineP2.Y;
                LineP2.Y = k;
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (LineP1.X > LineP2.X)
            {
                int k = LineP1.X;
                LineP1.X = LineP2.X;
                LineP2.X = k;

                k = LineP1.Y;
                LineP1.Y = LineP2.Y;
                LineP2.Y = k;
            }
            //пусть первая точка находится ближе к началу координат,
            //тогда вычтем из соответствующих координат x1 и y1 тем самым перенеся первую точку в начало координат,
            //тогда конечная окажется в (Δx, Δy), где Δx = x2 - x1, Δy = y2 - y1.
            float dx = LineP2.X - LineP1.X;
            float dy = LineP2.Y - LineP1.Y;
            //угловой коэффициент
            float gradient = dy / dx;
            //расстояние между реальной координатой y в этом месте и ближайшей ячейкой сетки.
            float y = LineP1.Y + gradient;

            // Берётся отрезок и его начальная координата x. К иксу в цикле прибавляем по единичке в сторону конца отрезка.
            // На каждом шаге вычисляется ошибка — расстояние между реальной координатой y в этом месте и ближайшей ячейкой сетки.
            // Если ошибка не превышает половину высоты ячейки, то она заполняется.
            for (int x = LineP1.X + 1; x < LineP2.X; x++)
            {
                // Отражаем линию по диагонали, если угол наклона слишком большой и перетасовываем координаты
                int xx1 = steep ? (int)Math.Round(y) : x;
                int xx2 = steep ? (int)Math.Round(y) : x;
                int yy1 = steep ? x : (int)Math.Round(y);
                int yy2 = steep ? x : (int)Math.Round(y + 1);

                //рассчет ошибки
                xx1 = Math.Max(Math.Min(xx1, bmp.Width - 1), 0);
                xx2 = Math.Max(Math.Min(xx2, bmp.Width - 1), 0);
                yy1 = Math.Max(Math.Min(yy1, bmp.Height - 1), 0);
                yy2 = Math.Max(Math.Min(yy2, bmp.Height - 1), 0);
                //сравниваем что ошибка не превышает половину высоты ячейки, то она заполняется.
                //maxF minF - ячейки сетки
                if ((yy1 >= maxF[xx1] && yy2 >= maxF[xx2]))
                {
                    bmp.SetPixel(xx2, yy2, Color.BurlyWood);
                    maxF[xx1] = yy1;
                    maxF[xx2] = yy2;
                }
                if (yy1 <= minF[xx1] && yy2 <= minF[xx2])
                {
                    bmp.SetPixel(xx1, yy1, Color.LightSeaGreen);
                    bmp.SetPixel(xx2, yy2, Color.LightSeaGreen);
                    minF[xx1] = yy1;
                    minF[xx2] = yy2;

                }
                //возвращение осей на место
                maxF[xx1] = Math.Max(yy1, maxF[xx1]);
                minF[xx1] = Math.Min(yy1, minF[xx1]);
                maxF[xx2] = Math.Max(yy2, maxF[xx2]);
                minF[xx2] = Math.Min(yy2, minF[xx2]);

                //к ошибке прибавляетя угловой коэфициет на каждом шаге
                y = y + gradient;

            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            last = 4;
            draw_graphic(4);
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            last = 3;
            draw_graphic(3);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            upDown += 10;
            fi1 = upDown * 0.0174532925;
            draw_graphic(last);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            upDown -= 10;
            fi1 = upDown * 0.0174532925;
            draw_graphic(last);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            leftRight -= 10;
            fi2 = leftRight * 0.0174532925;

            draw_graphic(last);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            leftRight += 10;
            fi2 = leftRight * 0.0174532925;

            draw_graphic(last);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                button2_Click_1(null, null);
            else if (e.KeyCode == Keys.D)
                button3_Click(null, null);
            else if (e.KeyCode == Keys.W)
                button1_Click_1(null, null);
            else if (e.KeyCode == Keys.S)
                button4_Click(null, null);

        }
    }
}
