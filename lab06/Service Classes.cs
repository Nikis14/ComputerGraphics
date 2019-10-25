using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AffinTransform3D
{
    public class my_point
    {
        public double X, Y, Z;

        public my_point()
        {
            this.X = this.Y = this.Z = 0;
        }

        public my_point(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

    }

    public class face
    {
        public List<my_point> points;

        public face()
        {
            points = new List<my_point>();
        }

        public void add(my_point p)
        {
            points.Add(p);
        }
    }
}