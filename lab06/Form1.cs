using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Drawing2D;
//using System.Runtime.Serialization;

namespace AffinTransform3D
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        rotationFigure current_saved_figure;
        Matrixes matr = new Matrixes();
        Pen pen_shape = new Pen(Color.Red); // для фигуры
        int centerX, centerY; // центр pictureBox
        bool is_axis = false; // выбрана ли ось для поворота
        my_point axis_P1, axis_P2; // точки оси для поворота
        List<face> shape = new List<face>(); // фигура - список граней
        List<my_point> points = new List<my_point>(); // список точек
        List<Tuple<int, int, int, int>> check = new List<Tuple<int, int, int, int>>();
        bool not_redraw = false; // перерисовывать или нет текущее положение
        List<my_point> initial_points = new List<my_point>();
        Dictionary<int, List<int>> relationships = new Dictionary<int, List<int>>();
        //ObjectIDGenerator linker;



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
            groupBox1.Hide();
            panel1.Controls.Add(XOY_o);
            panel1.Controls.Add(XOZ_o);
            panel1.Controls.Add(YOZ_o);
            panel1.Visible = true;
        }
        //Deprecated
        private void build_points()
        {
            points.Clear();
            foreach (face sh in shape)
                for (int i = 0; i < sh.points.Count; i++)
                    if (!points.Contains(sh.points[i]))
                        points.Add(sh.points[i]);
        }

        private bool to_draw_or_not(face f)
        {
            my_point vector_vis = new my_point((double)x_vis.Value,
                (double)y_vis.Value,
                (double)z_vis.Value);
            my_point vector_face = f.calculate_normal();
            double angle = Math.Acos((vector_face.X * vector_vis.X +
                vector_face.Y * vector_vis.Y +
                vector_face.Z * vector_vis.Z) /
                (
                (Math.Sqrt(vector_face.X * vector_face.X
                + vector_face.Y * vector_face.Y
                + vector_face.Z * vector_face.Z)) *
                (Math.Sqrt(vector_vis.X * vector_vis.X
                + vector_vis.Y * vector_vis.Y
                + vector_vis.Z * vector_vis.Z))
                )
            );
            return (angle < 1.5708);
        }

        private void draw_point(my_point p) // рисуем точку
        {
            g.FillEllipse(new SolidBrush(Color.Green), (int)Math.Round(p.X + centerX - 3), (int)Math.Round(-p.Y + centerY - 3), 6, 6);
        }

        private List<my_point> Copy(List<my_point> l)
        {
            List<my_point> res = new List<my_point>(l.Count);
            for (int i = 0; i < l.Count; ++i)
                res.Add(new my_point(l[i].X, l[i].Y, l[i].Z));
            return res;
        }

        private void draw_face(face f) // рисуем грань
        {
            List<my_point> points_to_draw = new List<my_point>(f.points.Count());
            if (isometry.Checked)
            {
                points_to_draw = matr.get_transformed_my_points(matr.matrix_isometry(), Copy(f.points));
                /*for (int i = 0; i < points_to_draw1.Count(); i++)
                {
                    points_to_draw.Add(new my_point(points_to_draw1[i].X, points_to_draw1[i].Y, points_to_draw1[i].Z));
                }*/
            }
            else if (perspective.Checked)
            {
                points_to_draw = matr.get_transformed_my_points(matr.matrix_perspective(1000), Copy(f.points));

            }
            else if (XOY_o.Checked)
            {
                List<my_point> points_to_draw1 = matr.get_transformed_my_points(matr.matrix_projection_xy(), Copy(f.points));
                for (int i = 0; i < points_to_draw1.Count(); i++)
                {
                    points_to_draw.Add(new my_point(points_to_draw1[i].X, points_to_draw1[i].Y, points_to_draw1[i].Z));
                }
            }
            else if (XOZ_o.Checked)
            {
                List<my_point> points_to_draw1 = matr.get_transformed_my_points(matr.matrix_projection_xz(), Copy(f.points));
                for (int i = 0; i < points_to_draw1.Count(); i++)
                {
                    points_to_draw.Add(new my_point(points_to_draw1[i].X, points_to_draw1[i].Z, points_to_draw1[i].Z));
                }
            }
            else
            {
                List<my_point> points_to_draw1 = matr.get_transformed_my_points(matr.matrix_projection_yz(), Copy(f.points));
                for (int i = 0; i < points_to_draw1.Count(); i++)
                {
                    points_to_draw.Add(new my_point(points_to_draw1[i].Y, points_to_draw1[i].Z, points_to_draw1[i].Z));
                }
            }

            for (int i = 0; i < points_to_draw.Count(); i++)
            {
                int x1 = (int)Math.Round(points_to_draw[i].X + centerX);
                int x2 = (int)Math.Round(points_to_draw[(i + 1) % points_to_draw.Count()].X + centerX);
                int y1 = (int)Math.Round(-points_to_draw[i].Y + centerY);
                int y2 = (int)Math.Round(-points_to_draw[(i + 1) % points_to_draw.Count()].Y + centerY);
                g.DrawLine(pen_shape, x1, y1, x2, y2);
            }
            /*
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
            */
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
            /*if (axis_P2 != null)
            {
                draw_point(axis_P1);
                draw_point(axis_P2);
            }*/
            foreach (face f in shape)
            {
                if (to_draw_or_not(f)||!inv_gr.Checked)
                {
                    draw_face(f);
                }
            }
            pictureBox.Image = bmp;
        }

        private void build_tetrahedron()
        {
            double h = Math.Sqrt(3) * 50;
            double h_big = 25 * Math.Sqrt(13);

            my_point p1 = new my_point(-50, -h/3, 0);
            my_point p2 = new my_point(50, -h/3, 0);
            my_point p3 = new my_point(0, 2*h/3, 0);
            my_point p4 = new my_point(0, 0, h_big);
            shape.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2 });
            relationships.Add(1, new List<int>() { 0, 3, 1 });
            relationships.Add(2, new List<int>() { 3, 1, 2 });
            relationships.Add(3, new List<int>() { 0, 3, 2 });
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
            shape.Clear();
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
            shape.Clear();
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
            shape.Clear();
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
        private void build_rotation_figure()
        {
            int cntr = 0;
            int cntrpt = 0;
            my_point pt1 = new my_point((double)axis_1st_X.Value, (double)axis_1st_Y.Value, (double)axis_1st_Z.Value);
            my_point pt2 = new my_point((double)axis_2st_X.Value, (double)axis_2st_Y.Value, (double)axis_2st_Z.Value);
            my_point c = normalize_vector(pt1, pt2);
            int rot = (int)(360 / dividence_count.Value);
            List<List<my_point>> transformed = new List<List<my_point>>();
            transformed.Add(this.Copy(initial_points));
            points.Clear();
            shape.Clear();
            for (int i = 1; i < dividence_count.Value; i++)
            {
                transformed.Add(matr.get_transformed_my_points_nobr(
                    matr.matrix_rotate_general(c.X, c.Y, c.Z, rot * i),
                    initial_points));
            }
            int ctr_depth = 0;
            if ((dividence_count.Value >= 3) && (initial_points.Count >= 2))
            {
                face tmp = new face();
                relationships.Add(cntr, new List<int>());
                foreach (var item in transformed)
                {
                    relationships[cntr].Add(cntrpt);
                    cntrpt += 1;
                    points.Add(item[ctr_depth]);
                    tmp.add(item[ctr_depth]);
                }
                shape.Add(tmp);
                cntr += 1;
                ctr_depth += 1;
                while (ctr_depth < initial_points.Count)
                {


                    for (int i = 0; i < dividence_count.Value - 1; i++)
                    {
                        relationships.Add(cntr, new List<int>());
                        points.Add(transformed[i][ctr_depth]);
                        face t2 = new face();
                        relationships[cntr].Add(cntrpt-(int)dividence_count.Value);
                        relationships[cntr].Add(cntrpt);
                        relationships[cntr].Add(cntrpt+1);
                        relationships[cntr].Add(cntrpt - (int)dividence_count.Value+1);
                        t2.add(transformed[i][ctr_depth - 1]);
                        t2.add(transformed[i][ctr_depth]);
                        t2.add(transformed[i + 1][ctr_depth]);
                        t2.add(transformed[i + 1][ctr_depth - 1]);
                        shape.Add(t2);
                        cntr += 1;
                        cntrpt += 1;
                    }
                    points.Add(transformed[(int)dividence_count.Value - 1][ctr_depth]);
                    face t = new face();
                    relationships.Add(cntr, new List<int>());
                    relationships[cntr].Add(cntrpt - (int)dividence_count.Value);
                    relationships[cntr].Add(cntrpt);
                    relationships[cntr].Add(cntrpt - (int)dividence_count.Value+1);
                    relationships[cntr].Add(cntrpt - 2*(int)dividence_count.Value+1);
                    t.add(transformed[(int)(dividence_count.Value - 1)][ctr_depth - 1]);
                    t.add(transformed[(int)(dividence_count.Value - 1)][ctr_depth]);
                    t.add(transformed[0][ctr_depth]);
                    t.add(transformed[0][ctr_depth - 1]);
                    shape.Add(t);
                    ctr_depth += 1;
                    cntr += 1;
                    cntrpt += 1;
                }
                relationships.Add(cntr, new List<int>());
                face tmp2 = new face();
                foreach (var item in transformed)
                {
                    relationships[cntr].Add(cntrpt-(int)dividence_count.Value);
                    cntrpt += 1;
                    tmp2.add(item[initial_points.Count - 1]);
                }
                shape.Add(tmp2);
            }
            redraw_image();
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
            shape.Clear();
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

        private my_point normalize_vector(my_point pt1,my_point pt2)
        {
            if(pt2.Z < pt1.Z || (pt2.Z == pt1.Z && pt2.Y < pt1.Y) ||
                (pt2.Z == pt1.Z && pt2.Y == pt1.Y) && pt2.X < pt1.X)
            {
                my_point tmp = pt1;
                pt1 = pt2;
                pt2 = tmp;
            }
            double x = pt2.X - pt1.X;
            double y = pt2.Y - pt1.Y;
            double z = pt2.Z - pt1.Z;
            double d = Math.Sqrt(x * x + y * y + z * z);
            if (d != 0)
                return new my_point(x / d, y / d, z / d); 
            return new my_point(0, 0, 0);
        }

        private void axis_rotate(my_point pt1, my_point pt2, double angle) // поворот вокруг оси
        {
            my_point c = normalize_vector(pt1, pt2);
            matr.matrix_projection_xy();
            points = matr.get_transformed_my_points(matr.matrix_rotate_general(c.X, c.Y, c.Z, angle), points);         
        }

        private void displacement_button_Click(object sender, EventArgs e) // перенос
        {
            int kx = (int)x_shift.Value, ky = (int)y_shift.Value, kz = (int)z_shift.Value;
            points = matr.get_transformed_my_points(matr.matrix_offset(kx, ky, kz), points);
            //displacement(kx, ky, kz);
            redraw_image();
        }

        private void rotate_button_Click(object sender, EventArgs e) // поворот
        {
            double x_angle = ((double)x_rotate.Value * Math.PI) / 180;
            double y_angle = ((double)y_rotate.Value * Math.PI) / 180;
            double z_angle = ((double)z_rotate.Value * Math.PI) / 180;
            //rotate(x_angle, y_angle, z_angle);
            matr.get_transformed_my_points(matr.matrix_rotation_x_angular(x_angle), points);
            matr.get_transformed_my_points(matr.matrix_rotation_y_angular(y_angle), points);
            matr.get_transformed_my_points(matr.matrix_rotation_z_angular(z_angle), points);
            redraw_image();
        }

        private void scale_button_Click(object sender, EventArgs e) // масштабирование
        {
            my_point center_P = center_point();
            matr.get_transformed_my_points(matr.matrix_offset(-center_P.X, -center_P.Y, -center_P.Z), points);
            matr.get_transformed_my_points(matr.matrix_scale((double)x_scale.Value, (double)y_scale.Value, (double)z_scale.Value), points);
            matr.get_transformed_my_points(matr.matrix_offset(center_P.X, center_P.Y, center_P.Z), points);
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

        private void orthography_CheckedChanged(object sender, EventArgs e)
        {
            if (!orthography.Checked) return;
            isometry.Checked = false;
            perspective.Checked = false;
            YOZ_o.Checked = true;
            panel1.Visible = true;
            redraw_image();
        }

        private void isometry_CheckedChanged(object sender, EventArgs e)
        { 
            if (!isometry.Checked) return;
            //isometry.Checked = true;
            orthography.Checked = false;
            perspective.Checked = false;
            panel1.Visible = false;
            redraw_image();
        }

        private void perspective_CheckedChanged(object sender, EventArgs e)
        {
            if (!perspective.Checked) return;
            isometry.Checked = false;
            orthography.Checked = false;
            panel1.Visible = false;
            redraw_image();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void XOY_o_CheckedChanged(object sender, EventArgs e)
        {
            if(XOY_o.Checked)
                redraw_image();
        }

        private void XOZ_o_CheckedChanged(object sender, EventArgs e)
        {
            if (XOZ_o.Checked)
                redraw_image();
        }

        private void YOZ_o_CheckedChanged(object sender, EventArgs e)
        {
            if (YOZ_o.Checked)
                redraw_image();
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void rot_list_add_Click(object sender, EventArgs e)
        {
            initial_points.Add(new my_point((int)n_pt_X.Value, (int)n_pt_Y.Value, (int)n_pt_Z.Value));
            init_pts_list.Items.Add("Point #" + (initial_points.Count) +
                " X= " + (int)n_pt_X.Value +
                " Y= " + (int)n_pt_Y.Value +
                " Z= " + (int)n_pt_Z.Value);
        }

        private void rot_list_reset_Click(object sender, EventArgs e)
        {
            initial_points.Clear();
            init_pts_list.Items.Clear();
        }

        private void initiate_build_Click(object sender, EventArgs e)
        {
            my_point pt1 = new my_point((double)axis_1st_X.Value, (double)axis_1st_Y.Value, (double)axis_1st_Z.Value);
            my_point pt2 = new my_point((double)axis_2st_X.Value, (double)axis_2st_Y.Value, (double)axis_2st_Z.Value);
            this.current_saved_figure = new rotationFigure(this.initial_points,
                pt1,
                pt2,
                (int)dividence_count.Value);
            build_rotation_figure();  
        }

        private void save_to_file_Click(object sender, EventArgs e)
        {
            figure f = new figure(this.points, this.relationships);
            string file_str  = JsonConvert.SerializeObject(f);
            //System.IO.File.WriteAllText(@"C:\Users\Sokolov\Downloads\WriteLines.txt", file_str)
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.CheckFileExists)
                {
                    File.Delete(saveFileDialog1.FileName);
                }
                File.WriteAllText(saveFileDialog1.FileName, file_str);
            }
        }

        private void load_from_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists)
                {
                    string res = File.ReadAllText(openFileDialog1.FileName);
                    figure f = JsonConvert.DeserializeObject<figure>(res);
                    this.points = f.points;
                    this.shape.Clear();
                    foreach (var item in f.relationships)
                    {
                        face q = new face();
                        foreach (var pt in item.Value)
                        {
                            q.add(points[pt]);
                        }
                        this.shape.Add(q);
                    }
                    build_points();
                    redraw_image();
                }
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private double sum_sin(double x, double y)
        {
            return Math.Sin(x) + Math.Sin(y);
        }

        private double sum(double x, double y)
        {
            return x+y;
        }

        private double mult(double x, double y)
        {
            return x*y;
        }

        private void process_points(List<my_point> cur_points, ref List<my_point> built_points, ref Dictionary<Tuple<double, double>, int> point_num, ref int cur_est, int i)
        {
            face f_func = new face();
            relationships[i] = new List<int>();
            foreach (my_point p in cur_points)
            {
                Tuple<double, double> t = new Tuple<double, double>(p.X, p.Y);
                
                if (!point_num.ContainsKey(t))
                {
                    f_func.add(p);
                    built_points.Add(p);
                    point_num.Add(t, cur_est);
                    ++cur_est;
                }
                else
                    f_func.add(built_points[point_num[t]]);
                relationships[i].Add(point_num[t]);
            }
            shape.Add(f_func);
        }

        private void make_func_shape(Func<double, double, double> f, double x1, double x2, double y1, double y2, double step)
        {
            relationships.Clear();
            shape.Clear();
            Dictionary<Tuple<double, double>, int> point_num = new Dictionary<Tuple<double, double>, int>();
            List<my_point> built_points = new List<my_point>();
            int i = 0;
            int cur_est = 0;
            for (double x = x1; x <= x2-step; x += step)
            {
                for(double y = y1; y <= y2-step; y += step)
                {
                    List<my_point> cur_points = new List<my_point>(){
                        new my_point(x, y, f(x, y)),
                        new my_point(x+step, y, f(x+step, y)),
                        new my_point(x, y+step, f(x, y+step)),
                        new my_point(x + step, y + step, f(x + step, y + step))
                    };
                    process_points(cur_points, ref built_points, ref point_num, ref cur_est, i);
                    i++;
                }
            }
            if(x1 == x2)
                for (double y = y1; y <= y2 - step; y += step)
                {
                    List<my_point> cur_points = new List<my_point>(){
                        new my_point(x1, y, f(x1, y)),
                        new my_point(x1, y+step, f(x1, y+step))
                    };
                    process_points(cur_points, ref built_points, ref point_num, ref cur_est, i);
                    i++;
                }
            else if(y1 == y2)
                for (double x = x1; x <= x2 - step; x += step)
                {
                    List<my_point> cur_points = new List<my_point>(){
                        new my_point(x, y1, f(x1, y1)),
                        new my_point(x+step, y1, f(x1 + step, y1))
                    };
                    process_points(cur_points, ref built_points, ref point_num, ref cur_est, i);
                    i++;
                }
        }

        private void plot_graphic(Func<double, double, double> f)
        {
            double x1, x2, y1, y2, step;
            bool if_read = Double.TryParse(textBox_x1.Text, out x1);
            if_read = Double.TryParse(textBox_x2.Text, out x2);
            if_read = Double.TryParse(textBox_y1.Text, out y1);
            if_read = Double.TryParse(textBox_y2.Text, out y2);
            if_read = Double.TryParse(textBox_step.Text, out step);

            if(x1 > x2)
            {
                MessageBox.Show("x1 > x2");
                return;
            }
            if (y1 > y2)
            {
                MessageBox.Show("y1 > y2");
                return;
            }
            if(step <= 0)
            {
                MessageBox.Show("Шаг должен быть > 0");
                return;
            }

            make_func_shape(f, x1, x2, y1, y2, step);
            build_points();
            redraw_image();
        }

        private void button_build_Click(object sender, EventArgs e)
        {
            if(listBox_funs.SelectedItem == null)
            {
                MessageBox.Show("Выберите функцию!");
                return;
            }
            string chosen_fun = listBox_funs.SelectedItem.ToString();
            switch(chosen_fun)
            {
                case "sin(x)+sin(y)":
                    plot_graphic(sum_sin);
                    break;
                case "x+y":
                    plot_graphic(sum);
                    break;
                case "x*y":
                    plot_graphic(mult);
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            redraw_image();
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
            groupBox1.Hide();
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
            else if (rotation_figure.Checked)
                groupBox1.Show();
            build_points();
            redraw_image();
        }

    }
}
