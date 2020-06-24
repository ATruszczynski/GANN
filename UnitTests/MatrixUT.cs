using Microsoft.VisualStudio.TestTools.UnitTesting;
using GANN.MathAT;
using System;

namespace UnitTests
{
    [TestClass]
    public class MatrixUT
    {
        public static bool CompareMatrixes(MatrixAT1 m1, MatrixAT1 m2)
        {
            if (m1.Rows != m2.Rows)
                return false;
            if (m1.Columns != m2.Columns)
                return false;

            for (int r = 0; r < m1.Rows; r++)
            {
                for (int c = 0; c < m1.Columns; c++)
                {
                    if (m1[r, c] != m2[r, c])
                        return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void MatrixMultiplication_Square_Success()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 1 }, { 2, 2 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });

            MatrixAT1 expected = new MatrixAT1(new double[,] { { 2, 2 }, { 4, 4 } });

            Assert.IsTrue(CompareMatrixes(m1 * m2, expected));
        }

        [TestMethod]
        public void MatrixMultiplication_NotSquare_Success()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1}, { 3} });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 2, 3 } });

            MatrixAT1 expected = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 3, 6, 9 } });

            Assert.IsTrue(CompareMatrixes(m1 * m2, expected));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixMultiplication2_Exception()
        {
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1 }, { 3 } });
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 } });

            MatrixAT1 expected = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 3, 6, 9 } });

            Assert.IsTrue(CompareMatrixes(m1 * m2, expected));
        }

        [TestMethod]
        public void MatrixTransposition1()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 1, 2, 3 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 1 }, { 2, 2 }, { 3, 3 } });

            m2.Transpose();

            Assert.IsTrue(CompareMatrixes(m1, m2));
        }
    }
}
