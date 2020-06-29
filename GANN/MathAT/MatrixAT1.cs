using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class MatrixAT1
    {
        //TODO - B - Vector subclass?
        //TODO - B - Diagonal subclass?
        double[,] cells;

        //TODO - B - Test
        public int Rows { get => cells.GetLength(0); }
        //TODO - B - Test
        public int Columns { get => cells.GetLength(1); }

        public MatrixAT1(int r, int c)
        {
            //TODO - B - Test
            cells = new double[r, c];
        }

        public MatrixAT1(double[,] array)
        {
            //TODO - B - Test
            cells = array;
        }

        public MatrixAT1(double[] array, bool column = true)
        {
            //TODO - C - Simplify
            if (!column)
            {
                cells = new double[1, array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    cells[0, i] = array[i];
                }
            }
            else
            {
                cells = new double[array.Length, 1];

                for (int i = 0; i < array.Length; i++)
                {
                    cells[i, 0] = array[i];
                }
            }
        }

        static void CheckDimensions(MatrixAT1 m1, MatrixAT1 m2, bool trans = false)
        {
            //TODO - B - Test
            if (!trans)
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
                    throw new ArgumentException($"First matrix row number ({m1.Columns}) is different than second matrix's column number ({m2.Rows})");
                }
            }

        }

        public double this[int a, int b]
        {
            //TODO - B - Test
            get { return cells[a, b]; }
            set { cells[a, b] = value; }
        }

        public static MatrixAT1 operator+(MatrixAT1 m1, MatrixAT1 m2)
        {
            //TODO - B - Test
            CheckDimensions(m1, m2);
            MatrixAT1 result = new MatrixAT1(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return result;
        }
        //TODO - B - ToString
        public static MatrixAT1 operator-(MatrixAT1 m1, MatrixAT1 m2)
        {
            //TODO - B - Test
            CheckDimensions(m1, m2);
            MatrixAT1 result = new MatrixAT1(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }

            return result;
        }

        public static MatrixAT1 operator*(MatrixAT1 m1, MatrixAT1 m2)
        {
            //TODO - B - Make faster
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
            //TODO - B - Test
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

        public static MatrixAT1 operator*(double d, MatrixAT1 m)
        {
            //TODO - B - Test
            MatrixAT1 result = new MatrixAT1(m.Rows, m.Columns);

            for (int r = 0; r < result.Rows; r++)
            {
                for (int c = 0; c < result.Columns; c++)
                {
                    result[r, c] = m[r,c] * d;
                }
            }

            return result;
        }

        public double ElementSum()
        {
            //TODO - B - Test
            double sum = 0;

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    sum += cells[r, c];
                }
            }

            return sum;
        }

        public override string ToString()
        {
            string result = "";

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    result += $"{cells[r, c]}";
                    if(c == Columns - 1)
                    {
                        result += "|" + Environment.NewLine;
                    }
                    else
                    {
                        result += ",";
                    }
                }
            }

            return result;
        }

        public static bool CheckSameDimensions(MatrixAT1 m, MatrixAT1 m2)
        {
            //TODO - B - test
            return m.Rows == m2.Rows && m.Columns == m2.Columns;
        }

        public static bool Compare(MatrixAT1 m, MatrixAT1 m2)
        {
            //TODO - B - test
            if (!CheckSameDimensions(m, m2))
                return false;

            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Columns; c++)
                {
                    if (m[r, c] != m2[r, c])
                        return false;
                }
            }

            return false;
        }

        public MatrixAT1 DeepCopy()
        {
            //TODO - B - test
            MatrixAT1 result = new MatrixAT1(Rows, Columns);

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    result[r, c] = cells[r, c];
                }
            }

            return result;
        }

        public void Squish() //TODO - C - should this do something else?
        {
            //TODO - B - test
            //TODO - B - what if max == 0
            double max = double.MinValue;
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    double elem = cells[r, c];
                    if (elem > max)
                        max = elem;

                }
            }

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    cells[r, c] /= max;
                }
            }
        }
    }
}
