using GANN.MathAT;
using GANN.NN.GradientStepStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.NNElems
{
    [TestClass]
    public class GradientStepUT
    {
        [TestMethod]
        public void ConstatnGradientTest()
        {
            ConstantGradientStep gds = new ConstantGradientStep(0.2);

            var wu = new MatrixAT1[2];
            var bu = new MatrixAT1[2];

            wu[1] = new MatrixAT1(new double[] { 10, 20 });
            bu[1] = new MatrixAT1(new double[] { 1, 2 });

            (var wg, var bg) =  gds.GetStepSize(-231232123, wu, bu);

            Assert.IsTrue(MatrixAT1.Compare(wg[1], new MatrixAT1(new double[] { 2, 4 })));
            Assert.IsTrue(MatrixAT1.Compare(bg[1], new MatrixAT1(new double[] { 0.2, 0.4 })));
            Assert.AreEqual(2, wg.Length);
            Assert.AreEqual(2, bg.Length);
            Assert.AreEqual(0, gds.CompareTo(new ConstantGradientStep(0.2)));
            Assert.AreEqual(-1, gds.CompareTo(new ConstantGradientStep(0.1)));
            Assert.AreEqual(0, gds.CompareTo(gds.DeepCopy()));

            Assert.IsTrue(MatrixAT1.Compare(wg[1], gds.stepSize * wu[1]));
        }

        [TestMethod]
        public void MomentumGradientTest()
        {
            MomentumStrategy mst = new MomentumStrategy(10, 0.1);

            Assert.AreEqual(0, mst.CompareTo(new MomentumStrategy(10, 0.1)));
            Assert.AreEqual(0, mst.DeepCopy().CompareTo(new MomentumStrategy(10, 0.1)));
            Assert.AreEqual(0, mst.CompareTo(mst.DeepCopy()));
            Assert.AreEqual(int.MinValue, mst.CompareTo(new ConstantGradientStep(10)));
            Assert.AreEqual(-1, mst.CompareTo(new MomentumStrategy(11, 0.1)));
            Assert.AreEqual(-1, mst.CompareTo(new MomentumStrategy(10, 0.2)));

            MomentumStrategy ms2 = (MomentumStrategy)mst.DeepCopy();
            ms2.StepSize = 100;
            Assert.AreEqual(10, mst.StepSize);
            Assert.AreEqual(100, ms2.StepSize);

            MatrixAT1 mw1 = new MatrixAT1(new double[,] { { 20, 10 } });
            MatrixAT1 mw2 = new MatrixAT1(new double[,] { { 40 },{ 20 } });
            MatrixAT1 mb1 = new MatrixAT1(new double[,] { { 60 } });
            MatrixAT1 mb2 = new MatrixAT1(new double[,] { { 80, 100 } });

            (MatrixAT1[] rw1, MatrixAT1[] rb1) = mst.GetStepSize(-1, new MatrixAT1[] { null, mw1, mw2 }, new MatrixAT1[] { null, mb1, mb2 });
            Assert.AreEqual(3, rw1.Length);
            Assert.AreEqual(3, rb1.Length);
            Assert.IsTrue(MatrixAT1.Compare(rw1[1], new MatrixAT1(new double[,] { { 200, 100 } })));
            Assert.IsTrue(MatrixAT1.Compare(rw1[2], new MatrixAT1(new double[,] { { 400 }, { 200 } })));
            Assert.IsTrue(MatrixAT1.Compare(rb1[1], new MatrixAT1(new double[,] { { 600 } })));
            Assert.IsTrue(MatrixAT1.Compare(rb1[2], new MatrixAT1(new double[,] { { 800, 1000 } })));

            (rw1, rb1) = mst.GetStepSize(-1, rw1, rb1);
            Assert.AreEqual(3, rw1.Length);
            Assert.AreEqual(3, rb1.Length);
            Assert.IsTrue(MatrixAT1.Compare(rw1[1], new MatrixAT1(new double[,] { { 2180, 1090 } })));
            Assert.IsTrue(MatrixAT1.Compare(rw1[2], new MatrixAT1(new double[,] { { 4360 }, { 2180 } })));
            Assert.IsTrue(MatrixAT1.Compare(rb1[1], new MatrixAT1(new double[,] { { 6540 } })));
            Assert.IsTrue(MatrixAT1.Compare(rb1[2], new MatrixAT1(new double[,] { { 8720, 10900 } })));

            Assert.IsNull(ms2.WeightMom);
            Assert.IsNull(ms2.BiasMom);

            MomentumStrategy ms3 = (MomentumStrategy)mst.DeepCopy();
            Assert.IsNull(ms2.WeightMom);
            Assert.IsNull(ms2.BiasMom);

            Assert.AreEqual("MS10-0.1", mst.ToString());

        }
    }
}
