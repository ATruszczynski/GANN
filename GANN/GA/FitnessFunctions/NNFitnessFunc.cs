using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.FitnessFunctions
{
    public class NNFitnessFunc : FitnessFunction
    {
        public double[][] trainInputs;
        public double[][] trainOutputs;
        public double[][] testInputs;
        public double[][] testOutputs;
        public int epochs = 5;
        public int batches = 100;


        //TODO - B - removve params, necessary parametrs in objects?
        public override double ComputeFitness(Chromosome c, params object[] args)
        {
            NNChromosome nnc = (NNChromosome)c;

            var nn = nnc.NeuralNetwork;

            nn.Train(trainInputs, trainOutputs, epochs, batches);

            double[] testRes = nn.Test(testInputs, testOutputs);

            double res = 0;

            for (int i = 0; i < testRes.Length; i++)
            {
                res += testRes[i];
            }

            res /= testRes.Length;

            return res;
        }
    }
}
