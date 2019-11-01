using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

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

    public class rotationFigure
    {
        public List<my_point> initial_points;
        public my_point point_1_axis;
        public my_point point_2_axis;
        public int divs;

        public rotationFigure(List<my_point> initial_points,
            my_point point_1_axis,
            my_point point_2_axis,
            int divs)
        {
            this.initial_points = initial_points;
            this.point_1_axis = point_1_axis;
            this.point_2_axis = point_2_axis;
            this.divs = divs;
        }
    }

    public class figure
    {
        public List<my_point> points;
        public Dictionary<int, List<int>> relationships;
        public figure(List<my_point> points, Dictionary<int, List<int>> relationships)
        {
            this.points = points;
            this.relationships = relationships;
        }
    }
}