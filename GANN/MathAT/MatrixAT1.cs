using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class MatrixAT1
    {
        double[,] cells;

        public int Rows { get => cells.GetLength(0); }
        public int Columns { get => cells.GetLength(1); }

        public MatrixAT1(int r, int c)
        {
            cells = new double[r, c];
        }

        public MatrixAT1(double[,] array)
        {
            cells = array;
        }

        public MatrixAT1(double[] array)
        {
            cells = new double[1, array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                cells[1, i] = array[i];
            }
        }

        static void CheckDimensions(MatrixAT1 m1, MatrixAT1 m2, bool trans)
        {
            if(!trans)
            {
                if(m1.Rows != m2.Rows)
                {
                    throw new ArgumentException($"Row numbers of first matrix ({m1.Rows}) are not equal to second matrix's ({m2.Rows})");
                }
                if (m1.Columns != m2.Columns)
                {
                    throw new ArgumentException($"Columns number of first matrix ({m1.Columns}) are not equal to second matrix's ({m2.Columns})");
                }
            }
            else
            {
                if(m1.Columns != m2.Rows)
                {
                    throw new ArgumentException($"First matrix row number ({m1.Rows}) is different than second matrix's column number ({m2.Columns})");
                }
            }

        }

        public double this[int a, int b]
        {
            get { return cells[a, b]; }
            set { cells[a, b] = value; }
        }

        public static MatrixAT1 operator+(MatrixAT1 m1, MatrixAT1 m2)
        {
            //TODO implement
            throw new NotImplementedException();
        }

        public static MatrixAT1 operator*(MatrixAT1 m1, MatrixAT1 m2)
        {
            //TODO Recognise identity matrix as argument
            //TODO Make faster
            CheckDimensions(m1, m2, true);

            MatrixAT1 result = new MatrixAT1(m1.Rows, m2.Columns);

            for(int r = 0; r < result.Rows; r++)
            {
                for (int c = 0; c < result.Columns; c++)
                {
                    double sum = 0;
                    for (int a = 0; a < m1.Columns; a++)
                    {
                        sum += m1[r, a] * m2[a, c];
                    }
                    result[r, c] = sum;
                }
            }

            return result;
        }

        public void Transpose()
        {
            double[,] results = new double[Columns, Rows];

            for (int or = 0; or < Rows; or++)
            {
                for (int oc = 0; oc < Columns; oc++)
                {
                    results[oc, or] = cells[or, oc];
                }
            }

            cells = results;
        }
    }
}
