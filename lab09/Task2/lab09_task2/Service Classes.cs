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
        public static my_point multiply_two_vectors(my_point v1, my_point v2)
        {
            my_point res = new my_point();
            res.X = (v1.Y * v2.Z - v1.Z * v2.Y);
            res.Y = (v1.Z * v2.X - v1.X * v2.Z);
            res.Z = (v1.X * v2.Y - v1.Y * v2.X);
            return res;
        }

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
        public my_point normal;

        public face()
        {
            points = new List<my_point>();
        }

        public void add(my_point p)
        {
            points.Add(p);
        }

        public my_point calculate_normal()
        {
            my_point p0 = points[0];
            my_point p1 = points[1];
            my_point p2 = points[points.Count - 1];
            my_point v1 = new my_point(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            my_point v2 = new my_point(p2.X - p0.X, p2.Y - p0.Y, p2.Z - p0.Z);
            return my_point.multiply_two_vectors(v1, v2);
            
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