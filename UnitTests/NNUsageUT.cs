using GANN.MathAT;
using GANN.NN;
using GANN.NN.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class NNUsageUT
    {
        [TestMethod]
        public void ANNInReverseTest()
        {
            Random pr = new Random(1001);
            (var trainInput, var trainOutput) = TestGenerator.GenerateReverseIO(1000, pr);
            (var testInput, var testOutput) = TestGenerator.GenerateReverseIO(100, pr);

            ANN nn = new ANN(new Hyperparameters(2, 2), pr);

            nn.Train(trainInput, trainOutput, 5, 100);

            var res = nn.Test(testInput, testOutput);

            Assert.IsTrue(Utility.ArrayAverage(res) > 0.95);
        }

        [TestMethod]
        public void ANNInReverseTest2()
        {
            Random pr = new Random(1001);
            (var trainInput, var trainOutput) = TestGenerator.GenerateReverseIO(1000, pr);
            (var testInput, var testOutput) = TestGenerator.GenerateReverseIO(100, pr);

            ANN nn = new ANN(new Hyperparameters(2, 2, new int[] { 3 }), pr);

            nn.Train(trainInput, trainOutput, 5, 100);

            var res = nn.Test(testInput, testOutput);

            Assert.IsTrue(Utility.ArrayAverage(res) > 0.95);
        }
    }
}
