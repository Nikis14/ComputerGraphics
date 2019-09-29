using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    
    
    class Matrixes
    {
        double[][] matrix_rotation = new double[3][];

        private double[] multiply_matrixes(double[] point)
        {
            double[] res = new double[3];
            for (int i = 0; i < 3; i++)
            {
                res[i] = 0;
                for (int i1 = 0; i1 < 3; i1++)
                {
                    res[i] += point[i1] * matrix_rotation[i1][i];
                }
            }
            return res;
        }

        public Tuple<Tuple<double, double>, Tuple<double, double>> Rotate_Edge_90_Grad(double x1, double x2, double y1, double y2, bool dir)
        {
            double[] source1 = { x1, y1, 1 };
            double[] source2 = { x2, y2, 1 };
            double[] res1 = multiply_matrixes(source1);
            double[] res2 = multiply_matrixes(source2);
            Tuple<double, double> point1 = new Tuple<double, double>(res1[0], res1[1]);
            Tuple<double, double> point2 = new Tuple<double, double>(res2[0], res2[1]);
            Tuple<Tuple<double, double>, Tuple<double, double>> res = new Tuple<Tuple<double, double>, Tuple<double, double>>(point1, point2);
            return res;

        }

        public Matrixes()
        {
            //_________________________________
            for (int i = 0; i < 3; i++)
            {
                matrix_rotation[i] = new double[3];
            }
            matrix_rotation[0][0] = 0;
            matrix_rotation[0][1] = 1;
            matrix_rotation[0][2] = 0;
            matrix_rotation[1][0] = -1;
            matrix_rotation[1][1] = 0;
            matrix_rotation[1][2] = 0;
            matrix_rotation[2][0] = 0;
            matrix_rotation[2][1] = 0;
            matrix_rotation[2][2] = 1;
        }
    }
}
