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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            image = new Bitmap(ExecuteFileDialog());
           
            pictureBox1.Image = image;
            pictureBox1.MouseClick += Picturebox_Mouse_Click;
            pictureBox1.MouseMove += Picturebox_Mouse_Hover;
        }

        private void Picturebox_Mouse_Hover(object sender, MouseEventArgs e)
        {
            this.textBoxX.Text = e.X.ToString();
            this.textBoxY.Text = e.Y.ToString();
            this.textBoxR.Text = image.GetPixel(e.X, e.Y).R.ToString();
            this.textBoxG.Text = image.GetPixel(e.X, e.Y).G.ToString();
            this.textBoxB.Text = image.GetPixel(e.X, e.Y).B.ToString();

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

        private int Border(int border,int x)
        {
            return Math.Max(0, Math.Min(border-1, x));
        }

        private Tuple<int,int> get_pixel_near(int xsource,int ysource,int direction)
        {
            
            switch (direction)
            {
                case 0:
                    return Tuple.Create(Border(1200,xsource-1),Border(600,ysource - 1));
                case 1:
                    return Tuple.Create(Border(1200,xsource), Border(600,ysource - 1));
                case 2:
                    return Tuple.Create(Border(1200,xsource+1), Border(600,ysource - 1));
                case 3:
                    return Tuple.Create(Border(1200, xsource +1), Border(600, ysource));
                case 4:
                    return Tuple.Create(Border(1200, xsource +1), Border(600, ysource + 1));
                case 5:
                    return Tuple.Create(Border(1200, xsource), Border(600, ysource + 1));
                case 6:
                    return Tuple.Create(Border(1200, xsource -1), Border(600, ysource + 1));
                case 7:
                    return Tuple.Create(Border(1200, xsource -1), Border(600, ysource));
                default:
                    return Tuple.Create(Border(1200, xsource), Border(600, ysource - 1));
            }
        }

        //012
        //7x3
        //654
        

        private List<Tuple<int,int>> find_border(int x,int y)
        {
            Queue<Tuple<int,int>> points_to_visit = new Queue<Tuple<int,int>>();
            List<Tuple<int,int>> points=new List<Tuple<int,int>>();
            HashSet<string> points_visited= new HashSet<string>();
            bool first_time = true;
            //bool two_points_on_walk = true;
            Color c = image.GetPixel(x, y);
            int direction = 5;
            int curr_x = 0;
            int curr_y = 0;
            points.Add(Tuple.Create(x, y));
            points_to_visit.Enqueue(Tuple.Create(x,y));
            points_visited.Add(x.ToString() + "," + y.ToString());  
            while (points_to_visit.Count !=0 )
            {
                curr_x = points_to_visit.Peek().Item1;
                curr_y = points_to_visit.Peek().Item2;
                points_to_visit.Dequeue();
                //two_points_on_walk = true;
                pictureBox1.Image = image;
                if(first_time)
                {
                    first_time = false;
                    for (int i = 0; i < 8; i++)
                    {
                        Tuple<int, int> point = get_pixel_near(curr_x, curr_y, (direction-i+8) % 8);
                        String str = point.Item1.ToString() + "," + point.Item2.ToString();
                        if (image.GetPixel(point.Item1,point.Item2) == c)
                        {
                            if (!points_visited.Contains(str))
                            {
                                direction = (direction + i)%8;
                                points_to_visit.Enqueue(Tuple.Create(point.Item1, point.Item2));
                                points_visited.Add(str);
                                points.Add(Tuple.Create(point.Item1,point.Item2));
                                break;
                            }
                          
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Tuple<int, int> point = get_pixel_near(curr_x, curr_y, (direction + i + 2) % 8);
                        String str = point.Item1.ToString() + "," + point.Item2.ToString();
                        if (image.GetPixel(point.Item1, point.Item2) == c)
                        {
                            if (!points_visited.Contains(str))
                            {
                                direction = (direction+i)%8;
                                points_to_visit.Enqueue(Tuple.Create(point.Item1, point.Item2));
                                points_visited.Add(str);
                                points.Add(Tuple.Create(point.Item1, point.Item2));
                                /*if (two_points_on_walk)
                                {
                                    two_points_on_walk = false;
                                }
                                else break;*/
                                break;
                            }
                           
                        }
                    }
                }
            }
            return points;
        }

        private void Picturebox_Mouse_Click(object sender,MouseEventArgs e)
        {
            List<Tuple<int,int>> t = find_border(e.X,e.Y);
            foreach (var point in t)
            {
                image.SetPixel(point.Item1, point.Item2, Color.Red);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HashSet<string> border = new HashSet<string>();
            //List<string> t = find_border(447, 41);
        }
    }
}
