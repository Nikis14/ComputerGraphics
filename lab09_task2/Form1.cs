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
        Matrixes matr = new Matrixes();
        Pen pen_shape = new Pen(Color.Red); // для фигуры
        int centerX, centerY; // центр pictureBox
        List<face> shape = new List<face>(); // фигура - список граней
        List<my_point> points = new List<my_point>(); // список точек
        List<Tuple<int, int, int, int>> check = new List<Tuple<int, int, int, int>>();
        bool not_redraw = false; // перерисовывать или нет текущее положение
        List<my_point> initial_points = new List<my_point>();
        Dictionary<int, List<int>> relationships = new Dictionary<int, List<int>>();
        Dictionary<my_point, double> saturations = new Dictionary<my_point, double>();
        //ObjectIDGenerator linker;

        Color[,] color_buffer; //соответсвие между пикселем и цветом
        double size_xx = 0, size_yy = 0;
        double size_diff_x = 0, size_diff_y = 0;

        bool is_texturing = false;
        Timer timer;
        

        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
         //   rotate_button_Click(null, null);
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
            panel1.Controls.Add(XOY_o);
            panel1.Controls.Add(XOZ_o);
            panel1.Controls.Add(YOZ_o);
            panel1.Visible = true;
            color_buffer = new Color[pictureBox.Width, pictureBox.Height];
            openFileDialog_texture.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";

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

        private double lambert_model_pt(my_point pt, my_point normal, my_point light)
        {
            my_point vectr = new my_point(light.X - pt.X, light.Y - pt.Y, light.Z - pt.Z);
            double cos_angle = (vectr.X * normal.X + vectr.Y * normal.Y + vectr.Z * normal.Z) /
                (normal.calculate_len() * vectr.calculate_len());
            return cos_angle;
        }

        private Dictionary<my_point,double> calculate_shading()
        {

            Dictionary<my_point, my_point> point_normal = new Dictionary<my_point, my_point>();
            foreach (var p in points)
            {
                int rel_id = points.IndexOf(p);
                List<face> needed_faces = new List<face>();
                foreach (var item in relationships)
                {
                    if (item.Value.Contains(rel_id))
                    {
                        needed_faces.Add(shape[item.Key]);
                    }
                }
                point_normal.Add(p, p.calculate_normal(needed_faces));
            }
            Dictionary<my_point, double> point_intensity = new Dictionary<my_point, double>();
            foreach (var pt in points)
            {
                point_intensity.Add(pt, lambert_model_pt(pt, point_normal[pt], new my_point((double)light_x.Value,(double)light_y.Value,(double)light_z.Value)));
            }
            return point_intensity;

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
        }

        Bitmap bmp2;
        bool flag = true;

        private void draw_pic_by_pixels()
        {
            build_pixels_to_draw();
            for (int i = 0; i < pictureBox.Width; ++i)
                for (int j = 0; j < pictureBox.Height; ++j)
                    ((Bitmap)pictureBox.Image).SetPixel(i, j, color_buffer[i, j]);
            pictureBox.Invalidate();
        }

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

            if (checkbox_delete_invisible.Checked || is_texturing || checkBox1.Checked)
                draw_pic_by_pixels();
            else
            {
                foreach (face f in shape)
                {
                    draw_face(f);
                }
                pictureBox.Image = bmp;
            }
        }

        private void build_tetrahedron()
        {
            double h = Math.Sqrt(3) * 50;
            double h_big = 25 * Math.Sqrt(13);
            points.Clear();
            my_point p1 = new my_point(-50, -h/3, 0);
            my_point p2 = new my_point(50, -h/3, 0);
            my_point p3 = new my_point(0, 2*h/3, 0);
            my_point p4 = new my_point(0, 0, h_big);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            shape.Clear();
            relationships.Clear();
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
            points.Clear();
            my_point p1 = new my_point(-50, -50, -50);
            my_point p2 = new my_point(-50, 50, -50);
            my_point p3 = new my_point(50, 50, -50);
            my_point p4 = new my_point(50, -50, -50);
            my_point p5 = new my_point(-50, -50, 50);
            my_point p6 = new my_point(-50, 50, 50);
            my_point p7 = new my_point(50, 50, 50);
            my_point p8 = new my_point(50, -50, 50);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2, 3 });
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            relationships.Add(1, new List<int>() { 0, 4, 5,1 });
            face f2 = new face(); f2.add(p1); f2.add(p2); f2.add(p6); f2.add(p5); shape.Add(f2);
            relationships.Add(2, new List<int>() { 4,6,7,5 });
            face f3 = new face(); f3.add(p5); f3.add(p6); f3.add(p7); f3.add(p8); shape.Add(f3);
            relationships.Add(3, new List<int>() { 2, 6, 7, 3 });
            face f4 = new face(); f4.add(p4); f4.add(p3); f4.add(p7); f4.add(p8); shape.Add(f4);
            relationships.Add(4, new List<int>() { 1, 5, 6,2 });
            face f5 = new face(); f5.add(p2); f5.add(p6); f5.add(p7); f5.add(p3); shape.Add(f5);
            relationships.Add(5, new List<int>() { 3, 7, 4,0 });
            face f6 = new face(); f6.add(p1); f6.add(p5); f6.add(p8); f6.add(p4); shape.Add(f6);
        }

        private void build_octahedron()
        {
            double a = Math.Sqrt(3) / 2 * 100;
            double p = (a + a + 100) / 2;
            double h = 2 * Math.Sqrt(p * (p - 100) * (p - a) * (p - a)) / 100;
            points.Clear();
            my_point p1 = new my_point(0, -h, 0);
            my_point p2 = new my_point(-50, 0, -50);
            my_point p3 = new my_point(0, h, 0);
            my_point p4 = new my_point(50, 0, -50);
            my_point p5 = new my_point(-50, 0, 50);
            my_point p6 = new my_point(50, 0, 50);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 1, 2, 3 });
            face f1 = new face(); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            relationships.Add(1, new List<int>() { 1, 0, 3 });
            face f2 = new face(); f2.add(p2); f2.add(p1); f2.add(p4); shape.Add(f2);
            relationships.Add(2, new List<int>() { 1, 2, 4 });
            face f3 = new face(); f3.add(p2); f3.add(p3); f3.add(p5); shape.Add(f3);
            relationships.Add(3, new List<int>() { 1, 0, 4 });
            face f4 = new face(); f4.add(p2); f4.add(p1); f4.add(p5); shape.Add(f4);
            relationships.Add(4, new List<int>() { 3, 2, 5 });
            face f5 = new face(); f5.add(p4); f5.add(p3); f5.add(p6); shape.Add(f5);
            relationships.Add(5, new List<int>() { 3, 0, 5 });
            face f6 = new face(); f6.add(p4); f6.add(p1); f6.add(p6); shape.Add(f6);
            relationships.Add(6, new List<int>() { 4, 2, 5 });
            face f7 = new face(); f7.add(p5); f7.add(p3); f7.add(p6); shape.Add(f7);
            relationships.Add(7, new List<int>() { 4, 0, 5 });
            face f8 = new face(); f8.add(p5); f8.add(p1); f8.add(p6); shape.Add(f8);
        }

        private void build_dodecahedron()
        {
            double r = 100 * (3 + Math.Sqrt(5)) / 4; // радиус полувписанной окружности
            double x = 100 * (1 + Math.Sqrt(5)) / 4; // половина стороны пятиугольника в сечении 
            points.Clear();
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
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            points.Add(p9);
            points.Add(p10);
            points.Add(p11);
            points.Add(p12);
            points.Add(p13);
            points.Add(p14);
            points.Add(p15);
            points.Add(p16);
            points.Add(p17);
            points.Add(p18);
            points.Add(p19);
            points.Add(p20);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2, 3, 4 });
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); f1.add(p4); f1.add(p5); shape.Add(f1);
            relationships.Add(1, new List<int>() { 0, 4, 5, 6, 7 });
            face f2 = new face(); f2.add(p1); f2.add(p5); f2.add(p6); f2.add(p7); f2.add(p8); shape.Add(f2);
            relationships.Add(2, new List<int>() { 0, 1, 9, 8, 7 });
            face f3 = new face(); f3.add(p1); f3.add(p2); f3.add(p10); f3.add(p9); f3.add(p8); shape.Add(f3);
            relationships.Add(3, new List<int>() { 1, 9, 10, 11, 2 });
            face f4 = new face(); f4.add(p2); f4.add(p10); f4.add(p11); f4.add(p12); f4.add(p3); shape.Add(f4);
            relationships.Add(4, new List<int>() { 3, 4, 5, 14, 19 });
            face f5 = new face(); f5.add(p4); f5.add(p5); f5.add(p6); f5.add(p15); f5.add(p20); shape.Add(f5);
            relationships.Add(5, new List<int>() { 3, 2, 11, 17, 19 });
            face f6 = new face(); f6.add(p4); f6.add(p3); f6.add(p12); f6.add(p18); f6.add(p20); shape.Add(f6);
            relationships.Add(6, new List<int>() { 8, 7, 6, 12, 18 });
            face f7 = new face(); f7.add(p9); f7.add(p8); f7.add(p7); f7.add(p13); f7.add(p19); shape.Add(f7);
            relationships.Add(7, new List<int>() { 8, 7, 6, 12, 18 });
            face f8 = new face(); f8.add(p9); f8.add(p10); f8.add(p11); f8.add(p17); f8.add(p19); shape.Add(f8);
            relationships.Add(8, new List<int>() { 8, 9, 10, 16, 18 });
            face f9 = new face(); f9.add(p11); f9.add(p12); f9.add(p18); f9.add(p16); f9.add(p17); shape.Add(f9);
            relationships.Add(9, new List<int>() { 6, 5, 14, 13, 12 });
            face f10 = new face(); f10.add(p7); f10.add(p6); f10.add(p15); f10.add(p14); f10.add(p13); shape.Add(f10);
            relationships.Add(10, new List<int>() { 18, 16, 15, 13, 12 });
            face f11 = new face(); f11.add(p19); f11.add(p17); f11.add(p16); f11.add(p14); f11.add(p13); shape.Add(f11);
            relationships.Add(11, new List<int>() { 15, 13, 14, 19, 17 });
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
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>()); relationships[0].Add(0);
            relationships[0].Add(1); relationships[0].Add(3);
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p4); shape.Add(f1);
            relationships.Add(1, new List<int>()); relationships[1].Add(0);
            relationships[1].Add(1); relationships[1].Add(6);
            face f2 = new face(); f2.add(p1); f2.add(p2); f2.add(p7); shape.Add(f2);
            relationships.Add(2, new List<int>()); relationships[2].Add(6);
            relationships[2].Add(1); relationships[2].Add(7);
            face f3 = new face(); f3.add(p7); f3.add(p2); f3.add(p8); shape.Add(f3);
            relationships.Add(3, new List<int>()); relationships[3].Add(7);
            relationships[3].Add(1); relationships[3].Add(2);
            face f4 = new face(); f4.add(p8); f4.add(p2); f4.add(p3); shape.Add(f4);
            relationships.Add(4, new List<int>()); relationships[4].Add(3);
            relationships[4].Add(1); relationships[4].Add(2);
            face f5 = new face(); f5.add(p4); f5.add(p2); f5.add(p3); shape.Add(f5);
            relationships.Add(5, new List<int>()); relationships[5].Add(5);
            relationships[5].Add(0); relationships[5].Add(4);
            face f6 = new face(); f6.add(p6); f6.add(p1); f6.add(p5); shape.Add(f6);
            relationships.Add(6, new List<int>()); relationships[6].Add(5);
            relationships[6].Add(9); relationships[6].Add(6);
            face f7 = new face(); f7.add(p6); f7.add(p7); f7.add(p10); shape.Add(f7);
            relationships.Add(7, new List<int>()); relationships[7].Add(9);
            relationships[7].Add(6); relationships[7].Add(7);
            face f8 = new face(); f8.add(p10); f8.add(p7); f8.add(p8); shape.Add(f8);
            relationships.Add(8, new List<int>()); relationships[8].Add(9);
            relationships[8].Add(7); relationships[8].Add(11);
            face f9 = new face(); f9.add(p10); f9.add(p8); f9.add(p12); shape.Add(f9);
            relationships.Add(9, new List<int>()); relationships[9].Add(11);
            relationships[9].Add(7); relationships[9].Add(2);
            face f10 = new face(); f10.add(p12); f10.add(p8); f10.add(p3); shape.Add(f10);
            relationships.Add(10, new List<int>()); relationships[10].Add(8);
            relationships[10].Add(3); relationships[10].Add(2);
            face f11 = new face(); f11.add(p9); f11.add(p4); f11.add(p3); shape.Add(f11);
            relationships.Add(11, new List<int>()); relationships[11].Add(4);
            relationships[11].Add(3); relationships[11].Add(8);
            face f12 = new face(); f12.add(p5); f12.add(p4); f12.add(p9); shape.Add(f12);
            relationships.Add(12, new List<int>()); relationships[12].Add(11);
            relationships[12].Add(2); relationships[12].Add(8);
            face f13 = new face(); f13.add(p12); f13.add(p3); f13.add(p9); shape.Add(f13);
            relationships.Add(13, new List<int>()); relationships[13].Add(4);
            relationships[13].Add(0); relationships[13].Add(3);
            face f14 = new face(); f14.add(p5); f14.add(p1); f14.add(p4); shape.Add(f14);
            relationships.Add(14, new List<int>()); relationships[14].Add(6);
            relationships[14].Add(0); relationships[14].Add(5);
            face f15 = new face(); f15.add(p7); f15.add(p1); f15.add(p6); shape.Add(f15);
            relationships.Add(15, new List<int>()); relationships[15].Add(10);
            relationships[15].Add(4); relationships[15].Add(5);
            face f16 = new face(); f16.add(p11); f16.add(p5); f16.add(p6); shape.Add(f16);
            relationships.Add(16, new List<int>()); relationships[16].Add(10);
            relationships[16].Add(5); relationships[16].Add(9);
            face f17 = new face(); f17.add(p11); f17.add(p6); f17.add(p10); shape.Add(f17);
            relationships.Add(17, new List<int>()); relationships[17].Add(10);
            relationships[17].Add(9); relationships[17].Add(11);
            face f18 = new face(); f18.add(p11); f18.add(p10); f18.add(p12); shape.Add(f18);
            relationships.Add(18, new List<int>()); relationships[18].Add(10);
            relationships[18].Add(11); relationships[18].Add(8);
            face f19 = new face(); f19.add(p11); f19.add(p12); f19.add(p9); shape.Add(f19);
            relationships.Add(19, new List<int>()); relationships[19].Add(10);
            relationships[19].Add(4); relationships[19].Add(8);
            face f20 = new face(); f20.add(p11); f20.add(p5); f20.add(p9); shape.Add(f20);
        }


        private void build_trapezoidal_hexahedron()
        {
            points.Clear();
            my_point p1 = new my_point(-30, -30, -50);
            my_point p2 = new my_point(-30, 30, -50);
            my_point p3 = new my_point(30, 30, -50);
            my_point p4 = new my_point(30, -30, -50);
            my_point p5 = new my_point(-50, -50, 50);
            my_point p6 = new my_point(-50, 50, 50);
            my_point p7 = new my_point(50, 50, 50);
            my_point p8 = new my_point(50, -50, 50);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2, 3 });
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); f1.add(p4); shape.Add(f1);
            relationships.Add(1, new List<int>() { 0, 4, 5, 1 });
            face f2 = new face(); f2.add(p1); f2.add(p2); f2.add(p6); f2.add(p5); shape.Add(f2);
            relationships.Add(2, new List<int>() { 4, 6, 7, 5 });
            face f3 = new face(); f3.add(p5); f3.add(p6); f3.add(p7); f3.add(p8); shape.Add(f3);
            relationships.Add(3, new List<int>() { 2, 6, 7, 3 });
            face f4 = new face(); f4.add(p4); f4.add(p3); f4.add(p7); f4.add(p8); shape.Add(f4);
            relationships.Add(4, new List<int>() { 1, 5, 6, 2 });
            face f5 = new face(); f5.add(p2); f5.add(p6); f5.add(p7); f5.add(p3); shape.Add(f5);
            relationships.Add(5, new List<int>() { 3, 7, 4, 0 });
            face f6 = new face(); f6.add(p1); f6.add(p5); f6.add(p8); f6.add(p4); shape.Add(f6);
        }

        private void build_non_convex()
        {
            points.Clear();
            my_point p1 = new my_point(-50, 0, 50);
            my_point p2 = new my_point(0, 50, 50);
            my_point p3 = new my_point(50, 0, 50);
            my_point p4 = new my_point(0, 25, 0);
            my_point p5 = new my_point(-50, 100, 50);
            my_point p6 = new my_point(0, 100, 0);
            my_point p7 = new my_point(50, 100, 50);

            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            shape.Clear();
            relationships.Clear();
            relationships.Add(0, new List<int>() { 0, 1, 2, 3 });
            face f1 = new face(); f1.add(p1); f1.add(p2); f1.add(p3); shape.Add(f1);
            relationships.Add(1, new List<int>() { 0, 3, 2});
            face f2 = new face(); f2.add(p1); f2.add(p4); f2.add(p3); shape.Add(f2);
            relationships.Add(2, new List<int>() { 2, 3, 5, 6 });
            face f3 = new face(); f3.add(p3); f3.add(p4); f3.add(p6); f3.add(p7); shape.Add(f3);
            relationships.Add(3, new List<int>() { 0, 3, 5, 4 });
            face f4 = new face(); f4.add(p1); f4.add(p4); f4.add(p6); f4.add(p5); shape.Add(f4);
            
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
            XOY_o.Checked = true;
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

        private void save_to_file_Click(object sender, EventArgs e)
        {
            figure f = new figure(this.points, this.relationships);
            string file_str  = JsonConvert.SerializeObject(f);
            //System.IO.File.WriteAllText(@"C:\Users\Sokolov\Downloads\WriteLines.txt", file_str)
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "3-d files (*.trd)|*.trd";
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
            openFileDialog1.Filter = "3-d files (*.trd)|*.trd";
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
            return x*x+y*y;
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

        private Tuple<int, int, int, int> find_min_max_XYpoint(face f)
        {
            int ind_max_X = 0;
            int ind_max_Y = 0;
            int ind_min_X = 0;
            int ind_min_Y = 0;

            for (int i = 1; i < f.points.Count(); ++i)
            {
                if(f.points[i].Y < f.points[ind_min_Y].Y)
                    ind_min_Y = i;

                if (f.points[i].X < f.points[ind_min_X].X)
                    ind_min_X = i;

                if (f.points[i].Y > f.points[ind_max_Y].Y)
                    ind_max_Y = i;

                if (f.points[i].X > f.points[ind_max_X].X)
                    ind_max_X = i;
            }
            return new Tuple<int, int, int, int>(ind_min_X, ind_max_X, ind_min_Y, ind_max_Y);
        }

        private double update_point(double t1, double t2, double q, double q1, double q2)
        {
            return t1 + (t2 - t1) * ((q - q1) / (q2 - q1));
        }

        private bool check_if_correctSize(double[,] buffer, int x)
        {
            return x >= 0 && x < pictureBox.Width;
        }

        private void update_pixel_by_texture(int x, int y, Bitmap img)
        {
            if (x - size_xx >= size_diff_x || x <= size_xx)
                return;
            if(!checkBox1.Checked)
                color_buffer[x, y] = img.GetPixel((int)((x - size_xx)/size_diff_x*img.Width), img.Height-1-(int)((-y + size_yy)/ size_diff_y * img.Height));
           // else

        }

        private void make_pixel_line(ref double[,] z_buffer, int x1, int x2, int y, double za, double zb, int sign, Color color, Bitmap img)
        {
            for (int cur_X = x1; cur_X * sign <= x2 * sign; cur_X += sign)
            {
                double z = update_point(za, zb, cur_X, x1, x2);
                int x_cur_int = cur_X + centerX;
                if (check_if_correctSize(z_buffer, x_cur_int) && z_buffer[x_cur_int, y] < z && xOK((int)x_cur_int))
                {
                    z_buffer[x_cur_int, y] = z;
                    if (img == null)
                        color_buffer[x_cur_int, y] = color;
                    else
                        update_pixel_by_texture(x_cur_int, y, img);
                }
            }
        }

        private bool xOK(double x)
        {
            return x - size_xx <= size_diff_x && x - size_xx <= size_xx + size_diff_x;
        }

        private void set_pixel(ref double[,] z_buffer, int x, int y, double z)
        {
            if (check_if_correctSize(z_buffer, x) && z_buffer[x, y] <= z+5) //&& xOK((double)x))
            {
                z_buffer[x, y] = z;
                color_buffer[x, y] = Color.Red;
            }
        }

        private void update_indices(ref int up_ind, ref int down_ind, int count, int sign)
        {
            up_ind = down_ind;
            down_ind = (count + down_ind + sign) % count;
        }

        private void update_color_map(face f, double cur_pointXa, double cur_pointXb, int Y, double za, double zb, Color color, ref double[,] z_buffer, Bitmap img)
        {
            int Xa = (int)Math.Round(cur_pointXa);
            int Xb = (int)Math.Round(cur_pointXb);
            int sign = Math.Sign(Xb - Xa);
            //
            double its = 0;
            double middle_x = (cur_pointXa + cur_pointXb) / 2;
            foreach (var item in f.points)
            {
                double distns = Math.Abs(middle_x / item.X) + Math.Abs(Y / item.Y) + Math.Abs(((za + zb) / 2) / item.X);
                distns /= 3;
                its += saturations[item]*distns;

            }
            its /= f.points.Count ;
            int clr = (int)Math.Round(255 * Math.Abs(its));
            clr = Math.Max(0, Math.Min(clr, 255));
            //
            //set_pixel(ref z_buffer, Xa, Y, za);
            if (sign != 0)
            {
                if (!checkBox1.Checked)
                { make_pixel_line(ref z_buffer, Xa + sign, Xb - sign, Y, za, zb, sign, color, img); }
                else
                {
                    make_pixel_line(ref z_buffer, Xa + sign, Xb - sign, Y, za, zb, sign,Color.FromArgb(0,0,clr), img);
                }
                //set_pixel(ref z_buffer, Xb, Y, zb);
            }
        }

        private bool find_nonEq_Y(face f, ref int up_ind_b, ref int down_ind_b, ref double[,] z_buffer, int down_ind_a, int sign1)
        {
            while (f.points[up_ind_b].Y == f.points[down_ind_b].Y && down_ind_a != up_ind_b)
            {
                int Y = (int)Math.Round(-f.points[up_ind_b].Y) + centerY;
                if (Y < 0 || Y >= pictureBox.Height)
                {
                    up_ind_b = down_ind_b;
                    down_ind_b = (f.points.Count() + down_ind_b - 1) % f.points.Count();
                    break;
                }
                //double za = f.points[up_ind_b].Z;
               // double zb = f.points[down_ind_b].Z;

                //update_color_map(f, f.points[up_ind_b].X, f.points[down_ind_b].X, Y, za, zb, Color.Red, ref z_buffer, null);

                update_indices(ref up_ind_b, ref down_ind_b, f.points.Count(), sign1);
            }
            return down_ind_a != up_ind_b;
        }

        private void swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        private void draw_line(double x0_d, double y0_d, double x1_d, double y1_d, double z0, double z1, ref double[,] z_buffer)
        {
            int x0 = (int)Math.Round(x0_d);
            int x1 = (int)Math.Round(x1_d);
            int y0 = (int)Math.Round(y0_d);
            int y1 = (int)Math.Round(y1_d);
            if (x0 > x1)
            {
                swap(ref x0, ref x1);
                swap(ref y0, ref y1);
                swap(ref z0, ref z1);
            }
            int sign_x = Math.Sign(x1 - x0);
            int sign_y = Math.Sign(y1 - y0);
            if (sign_x == 0)
            {
                for(int y = y0; y * sign_y <= y1 * sign_y; y += sign_y)
                {
                    double z = update_point(z0, z1, y, y0, y1);
                    set_pixel(ref z_buffer, x0 + centerX, -y + centerY, z);
                }
                return;
            }
            
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            double grad = dy / (double)dx;
            if(grad > 1)
            {
                int di = 2 * dx - dy;
                int x = x0;
                double yd = y0_d;
                for (int y = y0; y * sign_y <= y1 * sign_y; y += sign_y)
                {
                    double z = update_point(z0, z1, y, y0, y1);
                    set_pixel(ref z_buffer, x + centerX, -y + centerY, z);
                    if (di < 0)
                        di += 2 * dx;
                    else
                    {
                        x += sign_x;
                        di += 2 * (dx - dy);
                    }
                    y0_d += sign_y;
                }
            }
            else
            {
                int di = 2 * dy - dx;
                int y = y0;
                for (int x = x0; x * sign_x <= x1 * sign_x; x += sign_x)
                {
                    double z = update_point(z0, z1, x, x0, x1);
                    set_pixel(ref z_buffer, x + centerX, -y + centerY, z);
                    if (di < 0)
                        di += 2 * dy;
                    else
                    {
                        y += sign_y;
                        di += 2 * (dy - dx);
                    }
                }
            }
        }

        private void draw_all_edges(face f, ref double[,] z_buffer)
        {
            for (int i = 0; i < f.points.Count(); i++)
            {
                int ind_next = (i + 1) % f.points.Count();
                int x0 = (int)Math.Round(f.points[i].X);
                int x1 = (int)Math.Round(f.points[ind_next].X);
                int y0 = (int)Math.Round(f.points[i].Y);
                int y1 = (int)Math.Round(f.points[ind_next].Y);
                double by0 = f.points[i].Y;
                double by1 = f.points[ind_next].Y;
                double t0 = f.points[i].Y;
                double t1 = f.points[ind_next].Y;
                if (y0 == y1)
                {
                    by0 = f.points[i].X;
                    by1 = f.points[ind_next].X;
                    t0 = f.points[i].X;
                    t1 = f.points[ind_next].X;
                }
                if(by0 == by1)
                    continue;
                double z0 = update_point(f.points[i].Z, f.points[ind_next].Z, t0, by0, by1);
                double z1 = update_point(f.points[i].Z, f.points[ind_next].Z, t1, by0, by1);
                draw_line(f.points[i].X, f.points[i].Y, f.points[ind_next].X, f.points[ind_next].Y, z0, z1, ref z_buffer);
            }
        }

        //Create map of colors to draw pixels in face
        private void set_texture_with_Zbuffer(face f, ref double[,] z_buffer)
        {
            if (checkBox1.Checked)
            {
                this.saturations = calculate_shading();
            }
            Tuple<int, int, int, int> min_maxY = find_min_max_XYpoint(f);
            double cur_y = f.points[min_maxY.Item4].Y;
            double cur_pointXa = (int)Math.Round(f.points[min_maxY.Item4].X);
            double cur_pointXb = (int)Math.Round(f.points[min_maxY.Item4].X);

            size_diff_x = f.points[min_maxY.Item2].X - f.points[min_maxY.Item1].X;
            size_diff_y = f.points[min_maxY.Item4].Y - f.points[min_maxY.Item3].Y;

            int size_x = (int)Math.Round(f.points[min_maxY.Item2].X - f.points[min_maxY.Item1].X) + 1;
            int size_y = (int)Math.Round(f.points[min_maxY.Item4].Y - f.points[min_maxY.Item3].Y) + 1;
            size_xx = f.points[min_maxY.Item1].X + centerX;
            size_yy = -f.points[min_maxY.Item3].Y + centerY;
            Bitmap img = null;

            if (is_texturing)
                img = new Bitmap(Image.FromFile(openFileDialog_texture.FileName), size_x, size_y); //probably worth changing

            int up_ind_a = min_maxY.Item4;
            int up_ind_b = min_maxY.Item4;
            int down_ind_a = (f.points.Count() + min_maxY.Item4 - 1)% f.points.Count();
            int down_ind_b = (min_maxY.Item4 + 1) % f.points.Count();

            //While not bottom y
            while (Math.Round(cur_y) >= Math.Round(f.points[min_maxY.Item3].Y))
            {
                int Y = (int)Math.Round(-cur_y) + centerY;
                if (Y < 0 || Y >= pictureBox.Height)
                    break;

                if (!find_nonEq_Y(f, ref up_ind_a, ref down_ind_a, ref z_buffer, down_ind_b, -1))
                {
                    //update_color_map(f, f.points[up_ind_a].X, f.points[down_ind_a].X, Y, f.points[up_ind_a].Z, f.points[down_ind_a].Z, Color.Red, ref z_buffer, null);
                    break;
                }

                if (!find_nonEq_Y(f, ref up_ind_b, ref down_ind_b, ref z_buffer, down_ind_a, 1))
                {
                    //update_color_map(f, f.points[up_ind_b].X, f.points[down_ind_b].X, Y, f.points[up_ind_b].Z, f.points[down_ind_b].Z, Color.Red, ref z_buffer, null);
                    break;
                }

                double za = update_point(f.points[up_ind_a].Z, f.points[down_ind_a].Z, (int)Math.Round(cur_y), (int)Math.Round(f.points[up_ind_a].Y), (int)Math.Round(f.points[down_ind_a].Y));
                double zb = update_point(f.points[up_ind_b].Z, f.points[down_ind_b].Z, (int)Math.Round(cur_y), (int)Math.Round(f.points[up_ind_b].Y), (int)Math.Round(f.points[down_ind_b].Y));
                update_color_map(f, cur_pointXa, cur_pointXb, Y, za, zb, Color.White, ref z_buffer, img);

                //update all coordinates
                cur_y--;
                if (cur_y <= f.points[down_ind_a].Y)
                    update_indices(ref up_ind_a, ref down_ind_a, f.points.Count(), -1);

                if (cur_y <= f.points[down_ind_b].Y)
                    update_indices(ref up_ind_b, ref down_ind_b, f.points.Count(), 1);

                cur_pointXa = (int)Math.Round(update_point(f.points[up_ind_a].X, f.points[down_ind_a].X, (int)Math.Round(cur_y), f.points[up_ind_a].Y, f.points[down_ind_a].Y));
                cur_pointXb = (int)Math.Round(update_point(f.points[up_ind_b].X, f.points[down_ind_b].X, (int)Math.Round(cur_y), f.points[up_ind_b].Y, f.points[down_ind_b].Y));
            }
            draw_all_edges(f, ref z_buffer);
        }

        //create color map
        private void build_pixels_to_draw()
        {
            double[,] z_buffer = new double[pictureBox.Width, pictureBox.Height];
            for (int i = 0; i < pictureBox.Width; ++i)
                for (int j = 0; j < pictureBox.Height; ++j)
                {
                    color_buffer[i, j] = Color.White;
                    z_buffer[i, j] = Double.MinValue;
                }

            foreach (face f in shape)
                set_texture_with_Zbuffer(f, ref z_buffer);
        }

        private void checkbox_delete_invisible_CheckedChanged(object sender, EventArgs e)
        {
            if(shape.Count > 0)
                redraw_image();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            redraw_image();
            
        }

        //load texture
        private void load_texture_Click(object sender, EventArgs e)
        {
            if(shape.Count() == 0)
            {
                MessageBox.Show("Сначала выберите фигуру для текстурирования");
                return;
            }
            if (openFileDialog_texture.ShowDialog() == DialogResult.OK)
            {
                is_texturing = true;
                redraw_image();
            } 
        }

        //delete texture
        private void delete_texture_Click(object sender, EventArgs e)
        {
            is_texturing = false;
            redraw_image();
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
            else if (trapezoidal_hexahedron.Checked)
                build_trapezoidal_hexahedron();
            else if (non_convex.Checked)
                build_non_convex();
            build_points();
            redraw_image();   
        }
    }
}
