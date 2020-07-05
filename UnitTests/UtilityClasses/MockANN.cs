using GANN.NN;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class MockANN : ANN
    {
        public double[] testResult = new double[] { 0.75, 0.25, 0.5, 1, 0 };
        public double[][] runResults;
        int resultInd = 0;
        public MockANN() : base(new Hyperparameters(2,3), new Random(1001))
        {

        }
        public override double[] Run(double[] input)
        {
            return runResults[resultInd++];
        }

        public override double[] Test(double[][] inputs, double[][] outputs, string path = null)
        {
            return testResult;
        }

        public double[] Test2(double[][] inputs, double[][] ouutputs, string path = null)
        {
            return base.Test(inputs, ouutputs, "test.txt");
        }


        public override void Train(double[][] inputs, double[][] s, int epochs, int batchSize)
        {

        }
    }
}
