using GANN.MathAT;
using GANN.NN.ActivationFunctions;
using GANN.NN.LossFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;
using static GANN.MathAT.Utility;

namespace UnitTests.Math
{
    [TestClass]
    public class OtherUT
    {
        [TestMethod]
        public void SampleWithoutReplacementTest()
        {
            PseudoRandom pr = new PseudoRandom(1,1,0, 0);
            var swr = new SampleWithoutReplacement<double>(new double[] { 1, 2.4, 10, 5}, pr);

            Assert.IsTrue(swr.GetNext(out double d));
            Assert.AreEqual(2.4, d);
            Assert.IsTrue(swr.GetNext(out d));
            Assert.AreEqual(10, d);
            Assert.IsTrue(swr.GetNext(out d));
            Assert.AreEqual(1, d);
            Assert.IsTrue(swr.GetNext(out d));
            Assert.AreEqual(5, d);
            Assert.IsFalse(swr.GetNext(out d));

        }

        [TestMethod]
        public void RouletteTest()
        {
            PseudoRandom pr = new PseudoRandom(0.2, 0.6, 0.99);
            Assert.AreEqual(0, Roulette(new double[] { 0.7, 0.9, 1 }, pr));
            Assert.AreEqual(0, Roulette(new double[] { 0.7, 0.9, 1 }, pr));
            Assert.AreEqual(2, Roulette(new double[] { 0.7, 0.9, 1 }, pr));
        }

        [TestMethod]
        public void NormalisedSumTest()
        {
            double[] array = new double[] { 0.2, 0.6, 0.8, 0.4 };
            var sum = NormalisedCumulativeSum(array);
            Assert.AreEqual(0.1, sum[0]);
            Assert.AreEqual(0.4, sum[1]);
            Assert.AreEqual(0.8, sum[2]);
            Assert.AreEqual(1, sum[3]);

            sum = NormalisedCumulativeSum(array, 0.25);
            Assert.AreEqual((0.2 + 0.25)/3, sum[0]);
            Assert.AreEqual((0.8 + 0.5)/3, sum[1]);
            Assert.AreEqual((1.6 + 0.75)/3, sum[2]);
            Assert.AreEqual((3)/3, sum[3]);
        }

        [TestMethod]
        public void HighestValueIndInArrayTest()
        {
            double[] array = new double[] { 0.2, 0.6, 0.8, 0.4 };
            Assert.AreEqual(2, HighestValueIndInArray(array)); 
            array = new double[] { 0.2, 0.6, 0.8, 0.8 };
            Assert.AreEqual(2, HighestValueIndInArray(array));
        }

        [TestMethod]
        public void ClassCountsTest()
        {
            double[][] d = new double[][]
            {
                new double[] { 1, 0, 0, 0, 0 },
                new double[] { 1, 0, 0, 0, 0 },
                new double[] { 0, 1, 0, 0, 0 },
                new double[] { 0, 0, 0, 0, 1 },
            };

            var counts = ClassCounts(d);
            Assert.AreEqual(2, counts[0]);
            Assert.AreEqual(1, counts[1]);
            Assert.AreEqual(1, counts[4]);
        }

        [TestMethod]
        public void TryCastToDoubleTest()
        {
            int intZero = 0;
            double doubleZero = 0;
            double doubleAlmostZero = 1e-10;
            double doubleAlmostZero2 = -1e-10;
            float floatAlmostZero = 1e-4f;
            decimal decimalAlmostZero = (decimal)1e-15;

            double d = (double)floatAlmostZero;

            Assert.AreEqual(0, TryCastToDouble(intZero));
            Assert.AreEqual(0, TryCastToDouble(doubleZero));
            Assert.AreEqual(1e-10, TryCastToDouble(doubleAlmostZero));
            Assert.AreEqual(-1e-10, TryCastToDouble(doubleAlmostZero2));
            Assert.IsTrue(NNUT.CloseCompare(1e-4, TryCastToDouble(floatAlmostZero), 1e-5));
            Assert.AreEqual(1e-15, TryCastToDouble(decimalAlmostZero));
            Assert.ThrowsException<ArgumentException>(() => TryCastToDouble("d"));
        }

        [TestMethod]
        public void TryCastToIntTest()
        {
            int intZero = 0;
            double doubleZero = 0;
            double doubleAlmostZero = 1e-10;
            double doubleAlmostZero2 = -1e-10;
            float floatAlmostZero = 1e-4f;
            decimal decimalAlmostZero = (decimal)1e-15;

            Assert.AreEqual(0, TryCastToInt(intZero));
            Assert.AreEqual(0, TryCastToInt(doubleZero));
            Assert.AreEqual(0, TryCastToInt(doubleAlmostZero));
            Assert.AreEqual(0, TryCastToInt(doubleAlmostZero2));
            Assert.AreEqual(0, TryCastToInt(floatAlmostZero));
            Assert.AreEqual(0, TryCastToInt(decimalAlmostZero));
            Assert.ThrowsException<ArgumentException>(() => TryCastToDouble("d"));
        }

        [TestMethod]
        public void SeparateForTasksTest()
        {
            var res = SeparateForTasks(0, 5, 2);
            Assert.AreEqual(0, res[0][0]);
            Assert.AreEqual(2, res[0][1]);
            Assert.AreEqual(4, res[0][2]);
            Assert.AreEqual(1, res[1][0]);
            Assert.AreEqual(3, res[1][1]);


            res = SeparateForTasks(1, 4, 2);
            Assert.AreEqual(2, res[0][0]);
            Assert.AreEqual(1, res[1][0]);
            Assert.AreEqual(3, res[1][1]); 

            res = SeparateForTasks(1, 4, 5);
            Assert.AreEqual(1, res[0][0]);
            Assert.AreEqual(2, res[1][0]);
            Assert.AreEqual(3, res[2][0]);
        }

        [TestMethod]
        [Timeout(1000)]
        public void ChooseOnCycleContTest()
        {
            PseudoRandom pr = new PseudoRandom(0.25, 0.75);
            double res1 = NeighbourOnCircleCont(0.1, 0.05, 0.0, 0.2, pr);
            double res2 = NeighbourOnCircleCont(0.1, 0.05, 0.0, 0.2, pr);
            Assert.IsTrue(NNUT.CloseCompare(0.075, res1, 1e-6));
            Assert.IsTrue(NNUT.CloseCompare(0.125, res2, 1e-6));


            res1 = NeighbourOnCircleCont(0.1, 0.4, 0.0, 1, pr);
            res2 = NeighbourOnCircleCont(0.9, 0.4, 0.0, 1, pr);
            Assert.IsTrue(NNUT.CloseCompare(0.9, res1, 1e-6));
            Assert.IsTrue(NNUT.CloseCompare(0.1, res2, 1e-6));
        }

        [TestMethod]
        [Timeout(1000)]
        public void ChooseOnCycleDiscTest()
        {
            PseudoRandom pr = new PseudoRandom(0.1, 1, 0.9, 3,
                                               0.1, -1, 0.9, 12);

            int res1 = NeighbourOnCircleDisc(2, 3, 0, 10, pr);
            int res2 = NeighbourOnCircleDisc(2, 1, 0, 10, pr);
            Assert.AreEqual(1, res1);
            Assert.AreEqual(3, res2);

            res1 = NeighbourOnCircleDisc(1, 3, 0, 10, pr);
            res2 = NeighbourOnCircleDisc(9, 3, 0, 10, pr);
            Assert.AreEqual(9, res1);
            Assert.AreEqual(2, res2);
        }
        //TODO - C - test names jesus
        [TestMethod]
        public void SortedListTest()
        {
            var lc = new LimitedCapacitySortedList<LossFunction>(2);

            lc.Add(1, new QuadDiff(1));
            lc.Add(2, new QuadDiff(2));
            lc.Add(0, new QuadDiff(0));
            lc.Add(-1, new QuadDiff(-1));

            var l = lc.ExtractList();
            Assert.AreEqual(2, l.Count);
            Assert.AreEqual(0, l[0].CompareTo(new QuadDiff(2)));
            Assert.AreEqual(0, l[1].CompareTo(new QuadDiff(1)));
        }
    }

}
