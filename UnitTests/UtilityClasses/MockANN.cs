using GANN.NN;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class MockANN : ANN
    {
        double[] testResult = new double[] { 0.75, 0.25, 0.5, 1, 0 };
        public MockANN() : base(new Hyperparameters(new int[] { 2, 3}), new Random(1001))
        {

        }
        public override double[] Run(double[] input)
        {
            throw new NotImplementedException();
        }

        public override double[] Test(double[][] inputs, double[][] outputs)
        {
            return testResult;
        }

        public override void Train(double[][] inputs, double[][] s, int epochs, int batchSize)
        {

        }
    }
}
