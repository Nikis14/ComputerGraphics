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
        public List<PointF> get_transformed_PointFs(float[,] afin_matrix, List<PointF> PointFs)
        {
            for (int i = 0; i < PointFs.Count(); ++i)
            {
                float[] transformed = matrix_mult(afin_matrix, new float[3] { PointFs[i].X, PointFs[i].Y, 1 });
                PointFs[i] = new PointF(transformed[0] * transformed[2], transformed[1] * transformed[2]);
            }
            return PointFs;
        }

        public float[] matrix_mult(float[,] afin_matrix, float[] PointF)
        {
            float[] res = new float[3];
            for (int i = 0; i < 3; ++i)
            {
                res[i] = 0;
                for (int k = 0; k < 3; ++k)
                    res[i] += afin_matrix[k, i] * PointF[k];
            }
            float[] result = new float[3];
            for (int i = 0; i < 3; ++i)
                result[i] = (int)Math.Round(res[i]);
            return result;
        }

        public float[,] matrix_offset(float dx, float dy)
        {
            float[,] afin_matrix = new float[3, 3];
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

        public float[,] matrix_rotation(int angle, float a, float b)
        {
            float rad_angle = (float)(angle / 180.0 * Math.PI);
            float[,] afin_matrix = new float[3, 3];
            afin_matrix[0, 0] = (float)Math.Cos(rad_angle);
            afin_matrix[0, 1] = (float)Math.Sin(rad_angle);
            afin_matrix[0, 2] = 0;
            afin_matrix[1, 0] = (float)-Math.Sin(rad_angle);
            afin_matrix[1, 1] = (float)Math.Cos(rad_angle);
            afin_matrix[1, 2] = 0;
            afin_matrix[2, 0] = (float)(-a * Math.Cos(rad_angle) + b * Math.Sin(rad_angle) + a);
            afin_matrix[2, 1] = (float)(-a * Math.Sin(rad_angle) - b * Math.Cos(rad_angle) + b);
            afin_matrix[2, 2] = 1;
            return afin_matrix;
        }

        public float[,] matrix_scale(float koef, float a, float b)
        {
            float[,] afin_matrix = new float[3, 3];
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

        public Matrixes()
        {
        }
    }
}


/*bool is_right = check_is_right(list[0], list[1], list[2]);
                    int i_cur = 0;
                    int dir = 1;
                    int i_cur1 = 1;
                    if (!check_is_right(list[0], list[1], list[2]))
                    {
                        dir = -2;
                        i_cur1 = list.Count() - 1;
                    }
                    PointF check = new PointF((int)dot.Item1, (int)dot.Item2);
                    bool f = true;
                    while (i_cur1 != 0)
                    {
                        if (!check_is_right(list[i_cur], list[i_cur1], check))
                        {
                            label5.Text = "Точка не принадлежит многоугольнику";
                            f = false;
                            break;
                        }
                        i_cur = i_cur1;
                        if (i_cur == 2)
                            dir += 1;
                        i_cur1 = (i_cur1 + dir) % list.Count();
                    }
                    if (f)
                        label5.Text = "Точка принадлежит многоугольнику";
                    break;*/
