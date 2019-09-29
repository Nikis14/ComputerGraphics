using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Matrixes
    {
        int[][] matrix_rotation = new int[3][];

        private int[] multiply_matrixes(int[] point)
        {
            int[] res = new int[3];
            for (int i = 0; i < 3; i++)
            {
                res[i] = 0;
                for (int i1 = 0; i1 < 3; i1++)
                {
                    res[i] += point[i1] * matrix_rotation[i][i1];
                }
            }
            return res;
        }

        public Tuple<Tuple<int,int>,Tuple<int,int>> Rotate_Edge_90_Grad(int x1, int x2, int y1, int y2, bool dir)
        {
            int[] source1 = { x1, y1, 1 };
            int[] source2 = { x2, y2, 1 };
            int[] res1 = multiply_matrixes(source1);
            int[] res2 = multiply_matrixes(source2);
            Tuple<int, int> point1 = new Tuple<int, int>(res1[0], res1[1]);
            Tuple<int, int> point2 = new Tuple<int, int>(res2[0], res2[1]);
            Tuple<Tuple<int, int>, Tuple<int, int>> res = new Tuple<Tuple<int, int>, Tuple<int, int>>(point1, point2);
            return res;

        }

        public Matrixes()
        {
            //_________________________________
            for (int i = 0; i < 3; i++)
            {
                matrix_rotation[i] = new int[3];
            }
            matrix_rotation[0][0] = 1;
            matrix_rotation[0][1] = 0;
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
