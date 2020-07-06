using Microsoft.VisualStudio.TestTools.UnitTesting;
using GANN.MathAT;
using System;

namespace UnitTests
{
    [TestClass]
    public class MatrixUT
    {

        [TestMethod]
        public void MatrixAddition()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 1 }, { 2, 2 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            MatrixAT1 m3 = new MatrixAT1(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            MatrixAT1 mr = new MatrixAT1(new double[,] { { 2, 2 }, { 3, 3 } });

            Assert.IsTrue(MatrixAT1.Compare(mr, m1 + m2));
            Assert.ThrowsException<ArgumentException>(() => m1 + m3);
            Assert.ThrowsException<ArgumentException>(() => m3 + m1);
        }

        [TestMethod]
        public void MatrixComparison()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 2, 3, 4 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 2, 3, 4 } });
            MatrixAT1 m3 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 2, 3, 6 } });
            MatrixAT1 m4 = new MatrixAT1(new double[,] { { 1, 2, 3, 4 }, { 2, 3, 6, 8 } });
            MatrixAT1 m5 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 2, 3, 6 }, { 1, 1, 1} });

            Assert.IsTrue(MatrixAT1.Compare(m1, m2));
            Assert.IsFalse(MatrixAT1.Compare(m1, m3));
            Assert.IsFalse(MatrixAT1.Compare(m1, m4));
            Assert.IsFalse(MatrixAT1.Compare(m1, m5));
            Assert.IsFalse(MatrixAT1.Compare(m3, m5));
            Assert.IsFalse(MatrixAT1.Compare(m4, m5));
            Assert.IsFalse(MatrixAT1.Compare(m4, m3));
        }

        [TestMethod]
        public void MatrixSubtraction()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 1 }, { 2, 2 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            MatrixAT1 m3 = new MatrixAT1(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            MatrixAT1 mr = new MatrixAT1(new double[,] { { 0, 0 }, { 1, 1 } });

            Assert.IsTrue(MatrixAT1.Compare(mr, m1 - m2));
            Assert.ThrowsException<ArgumentException>(() => m1 - m3);
            Assert.ThrowsException<ArgumentException>(() => m3 - m1);
        }

        [TestMethod]
        public void MatrixMultiplication_Square_Success()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 1 }, { 2, 2 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });

            MatrixAT1 expected = new MatrixAT1(new double[,] { { 2, 2 }, { 4, 4 } });

            Assert.IsTrue(MatrixAT1.Compare(m1 * m2, expected));
        }

        [TestMethod]
        public void MatrixMultiplication_NotSquare_Success()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1}, { 3} });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 2, 3 } });

            MatrixAT1 expected = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 3, 6, 9 } });

            Assert.IsTrue(MatrixAT1.Compare(m1 * m2, expected));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixMultiplication2_Exception()
        {
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1 }, { 3 } });
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 } });

            MatrixAT1 expected = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 3, 6, 9 } });

            Assert.IsTrue(MatrixAT1.Compare(m1 * m2, expected));
        }


        [TestMethod]
        //TODO - D - more consistent varaible names
        public void MatrixMultiplicationByScalar()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 2, 3, 4 } });
            MatrixAT1 mr = new MatrixAT1(new double[,] { { 2, 4, 6 }, { 4, 6, 8 } });

            //TODO - B - somewhere in ANN there is a possiblity of dividing by 0?
            Assert.IsTrue(MatrixAT1.Compare(mr, 2 * m1));
        }


        [TestMethod]
        public void MatrixTransposition1()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 1, 2, 3 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 1 }, { 2, 2 }, { 3, 3 } });

            m2.Transpose();

            Assert.IsTrue(MatrixAT1.Compare(m1, m2));
        }

        [TestMethod]
        public void MatrixSizes()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 1, 2, 3 } });

            Assert.AreEqual(2, m1.Rows);
            Assert.AreEqual(3, m1.Columns);
        }

        [TestMethod]
        public void MatrixDims()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 1, 2, 3 } });
            MatrixAT1 m2 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 1, 2, 3 } });
            MatrixAT1 m3 = new MatrixAT1(new double[,] { { 1, 2 }, { 1, 2 } });
            MatrixAT1 m4 = new MatrixAT1(new double[,] { { 1, 2 }, { 1, 2 }, { 1, 2 } });

            Assert.IsTrue(MatrixAT1.CheckSameDimensions(m1, m2));
            Assert.IsFalse(MatrixAT1.CheckSameDimensions(m1, m3));
            Assert.IsFalse(MatrixAT1.CheckSameDimensions(m1, m4));
            Assert.IsFalse(MatrixAT1.CheckSameDimensions(m3, m4));
        }

        [TestMethod]
        public void MatrixConstructorRC()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 1, 2, 3 } });

            for (int r = 0; r < m1.Rows; r++)
            {
                for (int c = 0; c < m1.Columns; c++)
                {
                    m1[r, c] = 0;
                }
            }
        }
        //TODO - C - messy order of test
        [TestMethod]
        public void MatrixDeepCopy()
        {
            MatrixAT1 m1 = new MatrixAT1(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            var dp = m1.DeepCopy();

            Assert.IsTrue(MatrixAT1.Compare(m1, dp));

            m1[1, 1] = 7;
            Assert.IsFalse(MatrixAT1.Compare(m1, dp));

            Assert.AreEqual(7, m1[1, 1]);
            Assert.AreEqual(5, dp[1, 1]);
        }

        [TestMethod]
        public void MatrixConstructorArray()
        {
            double[,] array = new double[,] { { 1, 2, 3 }, { 1, 2, 3 } };
            MatrixAT1 m1 = new MatrixAT1(array);

            Assert.AreEqual(1, m1[0,0]);
            Assert.AreEqual(2, m1[0,1]);
            Assert.AreEqual(3, m1[0,2]);
            Assert.AreEqual(1, m1[1, 0]);
            Assert.AreEqual(2, m1[1, 1]);
            Assert.AreEqual(3, m1[1, 2]);
        }

        [TestMethod]
        public void MatrixElemSum()
        {
            double[,] array = new double[,] { { 1, 2, 3 }, { 1, 2, 3 } };
            MatrixAT1 m1 = new MatrixAT1(array);

            Assert.AreEqual(12, m1.ElementSum());
        }

        [TestMethod]
        public void MatrixFromRow()
        {
            double[] array = new double[] { 0.00005, 1, 124452 };
            MatrixAT1 a = new MatrixAT1(array, false);

            Assert.AreEqual(0.00005, a[0, 0]);
            Assert.AreEqual(1, a[0, 1]);
            Assert.AreEqual(124452, a[0, 2]);
        }

        [TestMethod]
        public void MatrixFromColumn()
        {
            double[] array = new double[] { 0.00005, 1, 124452 };
            MatrixAT1 a = new MatrixAT1(array);

            Assert.AreEqual(0.00005, a[0, 0]);
            Assert.AreEqual(1, a[1, 0]);
            Assert.AreEqual(124452, a[2, 0]);
        }
    }
}
