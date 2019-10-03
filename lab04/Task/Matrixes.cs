using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task
{
    
    class Matrixes
    {
       
        private int[] matrix_mult(double[,] afin_matrix, int[] point)
        {
            double[] res = new double[3];
            for (int i = 0; i < 3; ++i)
            {
                res[i] = 0;
                for (int k = 0; k < 3; ++k)
                    res[i] += afin_matrix[k, i] * point[k];
            }
            int[] result = new int[3];
            for (int i = 0; i < 3; ++i)
                result[i] = (int)Math.Round(res[i]);
            return result;
        }

        private double[,] matrix_offset(int dx, int dy)
        {
            double[,] afin_matrix = new double[3, 3];
            for (int i = 0; i < 3; ++i)
                afin_matrix[i, i] = 1;
            afin_matrix[0, 1] = 0;
            afin_matrix[0, 2] = 0;
            afin_matrix[1, 0] = 0;
            afin_matrix[1, 2] = 0;
            afin_matrix[2, 0] = dx;
            afin_matrix[2, 1] = dy;
            return afin_matrix;
        }

        private double[,] matrix_rotation(int angle, int a, int b)
        {
            double rad_angle = angle / 180.0 * Math.PI;
            double[,] afin_matrix = new double[3, 3];
            afin_matrix[0, 0] = Math.Cos(rad_angle);
            afin_matrix[0, 1] = Math.Sin(rad_angle);
            afin_matrix[0, 2] = 0;
            afin_matrix[1, 0] = -Math.Sin(rad_angle);
            afin_matrix[1, 1] = Math.Cos(rad_angle);
            afin_matrix[1, 2] = 0;
            afin_matrix[2, 0] = -a * Math.Cos(rad_angle) + b * Math.Sin(rad_angle) + a;
            afin_matrix[2, 1] = -a * Math.Sin(rad_angle) - b * Math.Cos(rad_angle) + b;
            afin_matrix[2, 2] = 1;
            return afin_matrix;
        }

        private double[,] matrix_scale(double koef, int a, int b)
        {
            double[,] afin_matrix = new double[3, 3];
            afin_matrix[0, 0] = koef;
            afin_matrix[0, 1] = 0;
            afin_matrix[0, 2] = 0;
            afin_matrix[1, 0] = 0;
            afin_matrix[1, 1] = koef;
            afin_matrix[1, 2] = 0;
            afin_matrix[2, 0] = -a * koef + a;
            afin_matrix[2, 1] = -b * koef + b;
            afin_matrix[2, 2] = 1;
            return afin_matrix;
        }

        private List<Point> get_transformed_points(double[,] afin_matrix, List<Point> points)
        {
            for (int i = 0; i < points.Count(); ++i)
            {
                int[] transformed = matrix_mult(afin_matrix, new int[3] { points[i].X, points[i].Y, 1 });
                points[i] = new Point(transformed[0] * transformed[2], transformed[1] * transformed[2]);
            }
            return points;
        }

        public Matrixes()
        {
            //_________________________________
          
        }
    }
}
