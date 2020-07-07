using GANN.GA;
using GANN.NN;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN
{
    class GANN
    {
        public GeneticAlgorithm GeneticAlgorithm;
        public double[][] trainInput;
        public double[][] trainOutput;
        public double[][] testInput;
        public double[][] testOutput;

        public GANN(int inputSize, int outputSize)
        {
            GeneticAlgorithm = ga;
        }

        public ANN GetGoodNN(int maxIt, double desiredAcc)
        {
            throw new NotImplementedException();
        }
    }
}
