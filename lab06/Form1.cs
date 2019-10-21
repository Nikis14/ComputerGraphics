﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AffinTransform3D
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Matrixes matr = new Matrixes();
        Pen pen_shape = new Pen(Color.Red); // для фигуры
        int centerX, centerY; // центр pictureBox
        bool is_axis = false; // выбрана ли ось для поворота
        my_point axis_P1, axis_P2; // точки оси для поворота
        List<face> shape = new List<face>(); // фигура - список граней
        List<my_point> points = new List<my_point>(); // список точек
        bool not_redraw = false; // перерисовывать или нет текущее положение

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            centerX = pictureBox.Width / 2;
            centerY = pictureBox.Height / 2;
            pictureBox.Image = bmp;
            g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.White);
            pictureBox.Invalidate();
        }

        private void build_points()
        {
            points.Clear();
            foreach (face sh in shape)
                for (int i = 0; i < sh.points.Count; i++)
                    if (!points.Contains(sh.points[i]))
                        points.Add(sh.points[i]);
        }

        private void draw_point(my_point p) // рисуем точку
        {
            g.FillEllipse(new SolidBrush(Color.Green), (int)Math.Round(p.X + centerX - 3), (int)Math.Round(-p.Y + centerY - 3), 6, 6);
        }

        private void draw_face(face f) // рисуем грань
        {
            int n = f.points.Count - 1;

            int x1 = (int)Math.Round(f.points[0].X + centerX);
            int x2 = (int)Math.Round(f.points[n].X + centerX);
            int y1 = (int)Math.Round(-f.points[0].Y + centerY);
            int y2 = (int)Math.Round(-f.points[n].Y + centerY);
            g.DrawLine(pen_shape, x1, y1, x2, y2);

            for (int i = 0; i < n; i++)
            {
               x1 = (int)Math.Round(f.points[i].X + centerX);
               x2 = (int)Math.Round(f.points[i + 1].X + centerX);
               y1 = (int)Math.Round(-f.points[i].Y + centerY);
               y2 = (int)Math.Round(-f.points[i + 1].Y + centerY);
               g.DrawLine(pen_shape, x1, y1, x2, y2);
            }
        }

        Bitmap bmp2;
        bool flag = true;
        private void redraw_image() // перерисовываем картинку
        {
            if (not_redraw)
            {
                g.Save();
                if (flag)
                {
                    bmp2 = new Bitmap(bmp);
                    flag = false;
                }
                g.Clear(Color.White);
                bmp = new Bitmap(bmp2);
                g = Graphics.FromImage(bmp);
            }
            else
            {
                flag = true;
                g.Clear(Color.White);
            }
            if (axis_P2 != null)
            {
                draw_point(axis_P1);
                draw_point(axis_P2);
            }
            foreach (face f in shape)
                draw_face(f);
            pictureBox.Image = bmp;
        }

        private void build_tetrahedron()
        {
            double h = Math.Sqrt(3) / 2 * 100;

            my_point p1 = new my_point(-50, -h / 2, 0);
            my_point p2 = new my_point(0, -h / 2, -h);
            my_point p3 = new my_point(50, -h / 2, 0);
            my_point p4 = new my_point(0, h / 2, 0);

            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); shape.Add(f1);
            face f2 = new face(); f2.add(p1); f2.add(p4); f2.add(p2); shape.Add(f2);
            face f3 = new face(); f3.add(p4); f3.add(p2); f3.add(p3); shape.Add(f3);
            face f4 = new face(); f4.add(p1); f4.add(p4); f4.add(p3); shape.Add(f4);
        }

        private void build_hexahedron()
        {
            my_point p1 = new my_point(-50, -50, -50);
            my_point p2 = new my_point(-50, 50, -50);
            my_point p3 = new my_point(50, 50, -50);
            my_point p4 = new my_point(50, -50, -50);
            my_point p5 = new my_point(-50, -50, 50);
            my_point p6 = new my_point(-50, 50, 50);
            my_point p7 = new my_point(50, 50, 50);
            my_point p8 = new my_point(50, -50, 50);

            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            face f2 = new face(); f2.add(p1); f2.add(p2); f2.add(p6); f2.add(p5); shape.Add(f2);
            face f3 = new face(); f3.add(p5); f3.add(p6); f3.add(p7); f3.add(p8); shape.Add(f3);
            face f4 = new face(); f4.add(p4); f4.add(p3); f4.add(p7); f4.add(p8); shape.Add(f4);
            face f5 = new face(); f5.add(p2); f5.add(p6); f5.add(p7); f5.add(p3); shape.Add(f5);
            face f6 = new face(); f6.add(p1); f6.add(p5); f6.add(p8); f6.add(p4); shape.Add(f6);
        }

        private void build_octahedron()
        {
            double a = Math.Sqrt(3) / 2 * 100;
            double p = (a + a + 100) / 2;
            double h = 2 * Math.Sqrt(p * (p - 100) * (p - a) * (p - a)) / 100;

            my_point p1 = new my_point(0, -h, 0);
            my_point p2 = new my_point(-50, 0, -50);
            my_point p3 = new my_point(0, h, 0);
            my_point p4 = new my_point(50, 0, -50);
            my_point p5 = new my_point(-50, 0, 50);
            my_point p6 = new my_point(50, 0, 50);

            face f1 = new face(); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            face f2 = new face(); f2.add(p2); f2.add(p1); f2.add(p4); shape.Add(f2);
            face f3 = new face(); f3.add(p2); f3.add(p3); f3.add(p5); shape.Add(f3);
            face f4 = new face(); f4.add(p2); f4.add(p1); f4.add(p5); shape.Add(f4);
            face f5 = new face(); f5.add(p4); f5.add(p3); f5.add(p6); shape.Add(f5);
            face f6 = new face(); f6.add(p4); f6.add(p1); f6.add(p6); shape.Add(f6);
            face f7 = new face(); f7.add(p5); f7.add(p3); f7.add(p6); shape.Add(f7);
            face f8 = new face(); f8.add(p5); f8.add(p1); f8.add(p6); shape.Add(f8);
        }

        private void build_dodecahedron()
        {
            double r = 100 * (3 + Math.Sqrt(5)) / 4; // радиус полувписанной окружности
            double x = 100 * (1 + Math.Sqrt(5)) / 4; // половина стороны пятиугольника в сечении 

            my_point p1 = new my_point(0, -50, -r);
            my_point p2 = new my_point(0, 50, -r);
            my_point p3 = new my_point(x, x, -x);
            my_point p4 = new my_point(r, 0, -50);
            my_point p5 = new my_point(x, -x, -x);
            my_point p6 = new my_point(50, -r, 0);
            my_point p7 = new my_point(-50, -r, 0);
            my_point p8 = new my_point(-x, -x, -x);
            my_point p9 = new my_point(-r, 0, -50);
            my_point p10 = new my_point(-x, x, -x);
            my_point p11 = new my_point(-50, r, 0);
            my_point p12 = new my_point(50, r, 0);
            my_point p13 = new my_point(-x, -x, x);
            my_point p14 = new my_point(0, -50, r);
            my_point p15 = new my_point(x, -x, x);
            my_point p16 = new my_point(0, 50, r);
            my_point p17 = new my_point(-x, x, x);
            my_point p18 = new my_point(x, x, x);
            my_point p19 = new my_point(-r, 0, 50);
            my_point p20 = new my_point(r, 0, 50);

            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); f1.add(p4); f1.add(p5); shape.Add(f1);
            face f2 = new face(); f2.add(p1); f2.add(p5); f2.add(p6); f2.add(p7); f2.add(p8); shape.Add(f2);
            face f3 = new face(); f3.add(p1); f3.add(p2); f3.add(p10); f3.add(p9); f3.add(p8); shape.Add(f3);
            face f4 = new face(); f4.add(p2); f4.add(p10); f4.add(p11); f4.add(p12); f4.add(p3); shape.Add(f4);
            face f5 = new face(); f5.add(p4); f5.add(p5); f5.add(p6); f5.add(p15); f5.add(p20); shape.Add(f5);
            face f6 = new face(); f6.add(p4); f6.add(p3); f6.add(p12); f6.add(p18); f6.add(p20); shape.Add(f6);
            face f7 = new face(); f7.add(p9); f7.add(p8); f7.add(p7); f7.add(p13); f7.add(p19); shape.Add(f7);
            face f8 = new face(); f8.add(p9); f8.add(p10); f8.add(p11); f8.add(p17); f8.add(p19); shape.Add(f8);
            face f9 = new face(); f9.add(p11); f9.add(p12); f9.add(p18); f9.add(p16); f9.add(p17); shape.Add(f9);
            face f10 = new face(); f10.add(p7); f10.add(p6); f10.add(p15); f10.add(p14); f10.add(p13); shape.Add(f10);
            face f11 = new face(); f11.add(p19); f11.add(p17); f11.add(p16); f11.add(p14); f11.add(p13); shape.Add(f11);
            face f12 = new face(); f12.add(p16); f12.add(p14); f12.add(p15); f12.add(p20); f12.add(p18); shape.Add(f12);
        }

        private void build_icosahedron() // икосаэдр
        {
            double r = 100 * (1 + Math.Sqrt(5)) / 4; // радиус полувписанной окружности
            my_point p1 = new my_point(0, -50, -r);
            my_point p2 = new my_point(0, 50, -r);
            my_point p3 = new my_point(50, r, 0);
            my_point p4 = new my_point(r, 0, -50);
            my_point p5 = new my_point(50, -r, 0);
            my_point p6 = new my_point(-50, -r, 0);
            my_point p7 = new my_point(-r, 0, -50);
            my_point p8 = new my_point(-50, r, 0);
            my_point p9 = new my_point(r, 0, 50);
            my_point p10 = new my_point(-r, 0, 50);
            my_point p11 = new my_point(0, -50, r);
            my_point p12 = new my_point(0, 50, r);

            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p4); shape.Add(f1);
            face f2 = new face(); f2.add(p1); f2.add(p2); f2.add(p7); shape.Add(f2);
            face f3 = new face(); f3.add(p7); f3.add(p2); f3.add(p8); shape.Add(f3);
            face f4 = new face(); f4.add(p8); f4.add(p2); f4.add(p3); shape.Add(f4);
            face f5 = new face(); f5.add(p4); f5.add(p2); f5.add(p3); shape.Add(f5);
            face f6 = new face(); f6.add(p6); f6.add(p1); f6.add(p5); shape.Add(f6);
            face f7 = new face(); f7.add(p6); f7.add(p7); f7.add(p10); shape.Add(f7);
            face f8 = new face(); f8.add(p10); f8.add(p7); f8.add(p8); shape.Add(f8);
            face f9 = new face(); f9.add(p10); f9.add(p8); f9.add(p12); shape.Add(f9);
            face f10 = new face(); f10.add(p12); f10.add(p8); f10.add(p3); shape.Add(f10);
            face f11 = new face(); f11.add(p9); f11.add(p4); f11.add(p3); shape.Add(f11);
            face f12 = new face(); f12.add(p5); f12.add(p4); f12.add(p9); shape.Add(f12);
            face f13 = new face(); f13.add(p12); f13.add(p3); f13.add(p9); shape.Add(f13);
            face f14 = new face(); f14.add(p5); f14.add(p1); f14.add(p4); shape.Add(f14);
            face f15 = new face(); f15.add(p7); f15.add(p1); f15.add(p6); shape.Add(f15);
            face f16 = new face(); f16.add(p11); f16.add(p5); f16.add(p6); shape.Add(f16);
            face f17 = new face(); f17.add(p11); f17.add(p6); f17.add(p10); shape.Add(f17);
            face f18 = new face(); f18.add(p11); f18.add(p10); f18.add(p12); shape.Add(f18);
            face f19 = new face(); f19.add(p11); f19.add(p12); f19.add(p9); shape.Add(f19);
            face f20 = new face(); f20.add(p11); f20.add(p5); f20.add(p9); shape.Add(f20);
        }

        private void displacement(int kx, int ky, int kz) // сдвиг
        {
            foreach (my_point p in points)
            {
                p.X += kx;
                p.X += kx;
                p.Y += ky;
                p.Z += kz;
            }
        }

        private void rotate(double xAngle, double yAngle, double zAngle) // поворот
        {
            foreach (my_point p in points)
            {
                rotate_0X(p, xAngle);
                rotate_0Y(p, yAngle);
                rotate_0Z(p, zAngle);
            }
        }

        private void rotate_0X(my_point p, double angle) // поворот вокруг OX
        {
            double y = p.Y;
            double z = p.Z;
            p.Y = y * Math.Cos(angle) + z * Math.Sin(angle);
            p.Z = y * -Math.Sin(angle) + z * Math.Cos(angle);
        }

        private void rotate_0Y(my_point p, double angle) // поворот вокруг OY
        {
            double x = p.X;
            double z = p.Z;
            p.X = x * Math.Cos(angle) + z * -Math.Sin(angle);
            p.Z = x * Math.Sin(angle) + z * Math.Cos(angle);
        }

        private void rotate_0Z(my_point p, double angle) // поворот вокруг OZ
        {
            double x = p.X;
            double y = p.Y;
            p.X = x * Math.Cos(angle) + y * Math.Sin(angle);
            p.Y = x * -Math.Sin(angle) + y * Math.Cos(angle);
        }

        private my_point center_point() // центр фигуры
        {
            double sumX = 0, sumY = 0, sumZ = 0;
            int count = 0;
            for (int i = 0; i < shape.Count; i++)
                for (int j = 0; j < shape[i].points.Count; j++)
                {
                    sumX += shape[i].points[j].X;
                    sumY += shape[i].points[j].Y;
                    sumZ += shape[i].points[j].Z;
                    ++count;
                }
            return new my_point(sumX / count, sumY / count, sumZ / count);
        }

        private void scaling(double xScale, double yScale, double zScale) // масштабирование
        {
            my_point center_P = center_point();
            foreach (my_point p in points)
            {
                p.X -= center_P.X;
                p.Y -= center_P.Y;
                p.Z -= center_P.Z;

                p.X *= xScale;
                p.Y *= yScale;
                p.Z *= zScale;

                p.X += center_P.X;
                p.Y += center_P.Y;
                p.Z += center_P.Z;
            }
        }

        private void reflection(int ind) // отражение: 1 - XOY, 2 - XOZ, 3 - YOZ
        {
            int koef1 = 1;
            int koef2 = 1;
            int koef3 = 1;
            if (xoy_reflect.Checked)
            {
                koef3 = -1;
            }
            if (xoz_reflect.Checked)
            {
                koef2 = -1;
            }
            if (yoz_reflect.Checked)
            {
                koef1 = -1;
            }
            matr.get_transformed_my_points(matr.matrix_refl(koef1, koef2, koef3), points);
            redraw_image();
        }


        private void axis_rotate(my_point pt1, my_point pt2, double angle) // поворот вокруг оси
        {
            // Пункт 0. Выводим вектор.
            double l = pt2.X - pt1.X;
            double m = pt2.Y - pt1.Y;
            double n = pt2.Z - pt1.Z;
            if(n == 0)
            {
                n = 1;
            }
            //double rad_angle = (angle / 180.0 * Math.PI);
            double psi = (Math.Atan(l / n) / 180.0 * Math.PI);
            double ot = (Math.Atan(m / n) / 180.0 * Math.PI);
            //Как в учебнике: Пункт 1 - перенести точку в начало координат.
            points = matr.get_transformed_my_points(matr.matrix_offset(-pt1.X, -pt1.Y, -pt1.Z),points);
            //Пункт 2 - вращаем вектор
            points = matr.get_transformed_my_points(matr.matrix_rotation_x(psi),points);
            points = matr.get_transformed_my_points(matr.matrix_rotation_y(ot), points);
            //Пункт 3 - вращаем сам полигон
            points = matr.get_transformed_my_points(matr.matrix_rotation(angle), points);
            //Пункт 4 - 2 в обратном порядке
            points = matr.get_transformed_my_points(matr.matrix_rotation_y(-ot), points);
            points = matr.get_transformed_my_points(matr.matrix_rotation_x(-psi), points);
            //Пункт 5 - обратен 1
            points = matr.get_transformed_my_points(matr.matrix_offset(pt1.X, pt1.Y, pt1.Z), points);
           
            
        }

        private void displacement_button_Click(object sender, EventArgs e) // перенос
        {
            int kx = (int)x_shift.Value, ky = (int)y_shift.Value, kz = (int)z_shift.Value;
            var new_points = matr.get_transformed_my_points(matr.matrix_offset(kx, ky, kz), points);
            //displacement(kx, ky, kz);
            redraw_image();
        }

        private void rotate_button_Click(object sender, EventArgs e) // поворот
        {
            double x_angle = ((double)x_rotate.Value * Math.PI) / 180;
            double y_angle = ((double)y_rotate.Value * Math.PI) / 180;
            double z_angle = ((double)z_rotate.Value * Math.PI) / 180;
            //rotate(x_angle, y_angle, z_angle);
            matr.get_transformed_my_points(matr.matrix_rotation_x(x_angle), points);
            matr.get_transformed_my_points(matr.matrix_rotation_y(y_angle), points);
            matr.get_transformed_my_points(matr.matrix_rotation_z(z_angle), points);
            redraw_image();
        }

        private void scale_button_Click(object sender, EventArgs e) // масштабирование
        {
            my_point center_P = center_point();
            matr.get_transformed_my_points(matr.matrix_offset(-center_P.X, -center_P.Y, -center_P.Z),points);
            matr.get_transformed_my_points(matr.matrix_scale((double)x_scale.Value, (double)y_scale.Value, (double)z_scale.Value), points);
            matr.get_transformed_my_points(matr.matrix_offset(center_P.X, center_P.Y, center_P.Z),points);
            //scaling((double)x_scale.Value, (double)y_scale.Value, (double)z_scale.Value);
            redraw_image();
        }

        private void reflect_button_Click(object sender, EventArgs e) // отражение
        {
            reflection(0);
        }

        private void cancel_button_Click(object sender, EventArgs e) // отмена
        {
            axis_P1 = axis_P2 = null;
            shape_CheckedChanged(null, null);
            redraw_image();
        }

        private void axis_choice_button_Click(object sender, EventArgs e) // выбираем точки для поворота вокруг оси
        {

        }

        private void axis_rotate_button_Click(object sender, EventArgs e) // поворот вокруг оси
        {
            axis_rotate(new my_point(Convert.ToDouble(x1_box.Value),
                Convert.ToDouble(y1_box.Value), 
                Convert.ToDouble(z1_box.Value)),
                new my_point(Convert.ToDouble(x2_box.Value),
                Convert.ToDouble(y2_box.Value),
                Convert.ToDouble(z2_box.Value)),
                Convert.ToDouble(axis_angle.Value));
            redraw_image();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!is_axis || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            if (axis_P1 == null)
            {
                axis_P1 = new my_point((e.X - centerX), (-e.Y + centerY), 0);
                draw_point(axis_P1);
            }
            else
            {
                axis_P2 = new my_point((e.X - centerX), (-e.Y + centerY), 0);
                draw_point(axis_P2);
                is_axis = false;
            }
            pictureBox.Image = bmp;
        }

        private void shape_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            if ((sender as RadioButton).Checked == false)
                return;
            shape.Clear();
            if (tetrahedron.Checked)
                build_tetrahedron();
            else if (hexahedron.Checked)
                build_hexahedron();
            else if (octahedron.Checked)
                build_octahedron();
            else if (dodecahedron.Checked)
                build_dodecahedron();
            else if (icosahedron.Checked)
                build_icosahedron();
            build_points();
            redraw_image();
        }

    }
}
