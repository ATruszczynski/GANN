using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN
{
    public abstract class NeuralNetwork
    {
        //TODO - A - more general input/output
        public abstract void Train(double[][] inputs, double[][] s, int epochs, int batchSize);
        public abstract double[] Run(double[] input);
        public abstract double[] Test(double[][] inputs, double[][] outputs);
    }
}
