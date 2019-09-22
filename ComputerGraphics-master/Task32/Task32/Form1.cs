using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task32
{
    public partial class Form1 : Form
    {
        Bitmap image;
        bool allow_click=false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            image = new Bitmap(ExecuteFileDialog());
            this.allow_click = true;
            pictureBox1.Image = image;
        }


        private string ExecuteFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return "NONINIT";
            }

        }

        private Tuple<int,int> get_pixel_near(int xsource,int ysource,int direction)
        {
            switch (direction)
            {
                case 0:
                    return Tuple.Create(xsource - 1, ysource - 1);
                case 1:
                    return Tuple.Create(xsource, ysource - 1);
                case 2:
                    return Tuple.Create(xsource+1, ysource - 1);
                case 3:
                    return Tuple.Create(xsource+1, ysource);
                case 4:
                    return Tuple.Create(xsource+1, ysource + 1);
                case 5:
                    return Tuple.Create(xsource, ysource + 1);
                case 6:
                    return Tuple.Create(xsource-1, ysource + 1);
                case 7:
                    return Tuple.Create(xsource-1, ysource);
                default:
                    return Tuple.Create(xsource, ysource - 1);
            }
        }

        //012
        //7x3
        //654
        private List<string> find_border(int x,int y)
        {
           
            List<String> points=new List<string>();
            HashSet<string> points_visited= new HashSet<string>();
            bool first_time = true;
            Color c = image.GetPixel(x, y);
            int direction = 5;
            int curr_x = x;
            int curr_y = y;
            points_visited.Add(x.ToString() + "," + y.ToString());
            while (true)
            {
                pictureBox1.Image = image;
                if(first_time)
                {
                    first_time = false;
                    for (int i = 0; i < 8; i++)
                    {
                        Tuple<int, int> point = get_pixel_near(curr_x, curr_y, Math.Abs((direction-i) % 8));
                        String str = point.Item1.ToString() + "," + point.Item2.ToString();
                        if (image.GetPixel(point.Item1,point.Item2) == c)
                        {
                            if (!points_visited.Contains(str))
                            {
                                direction = i;
                                curr_x = point.Item1;
                                curr_y = point.Item2;
                                image.SetPixel(curr_x, curr_y, Color.Red);
                                points_visited.Add(str);
                                points.Add(str);
                                break;
                            }
                            else return points;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Tuple<int, int> point = get_pixel_near(curr_x, curr_y, Math.Abs((direction + i) % 8));
                        String str = point.Item1.ToString() + "," + point.Item2.ToString();
                        if (image.GetPixel(point.Item1, point.Item2) == c)
                        {
                            if (!points_visited.Contains(str))
                            {
                                direction = i;
                                curr_x = point.Item1;
                                curr_y = point.Item2;
                                image.SetPixel(curr_x, curr_y, Color.Red);
                                points_visited.Add(str);
                                points.Add(str);
                                break;
                            }
                            else return points;
                        }
                    }
                }

            }
        }

        private void Picturebox_Mouse_Click(object sender,MouseEventArgs e)
        {
            List<string> t = find_border(448, 42);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HashSet<string> border = new HashSet<string>();
            List<string> t = find_border(447, 41);
        }
    }
}
