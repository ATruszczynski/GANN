using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using GANN.MathAT;

namespace GANN.NN
{
    public abstract class NeuralNetwork
    {
        //TODO - B - should this hold hyperparameters?
        public Hyperparameters Hyperparameters;
        public Random Random;
        //TODO - A - more general input/output
        public abstract void Train(double[][] inputs, double[][] s, int epochs, int batchSize);
        public abstract double[] Run(double[] input, out MatrixAT1[] ases, out MatrixAT1[] zs);
        public abstract double[] Test(double[][] inputs, double[][] outputs, string path = null, string logPath = null);
        public abstract void ModelToFile(string path);
        public abstract override string ToString();
    }
}
