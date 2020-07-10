using GANN.GA.GA_Elements;
using GANN.MathAT;
using GANN.NN;
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
        public Random Random;


        //TODO - B - removve params, necessary parametrs in objects?
        public override double ComputeFitness(Chromosome c, params object[] args)
        {
            double res = 0;

            NNChromosome nnc = (NNChromosome)c;

            var nn = new ANN(nnc.Hyperparameters, Random);

            try
            {
                nn.Train(trainInputs, trainOutputs, epochs, batches);

                double[] testRes = nn.Test(testInputs, testOutputs);

                res = Utility.ArrayAverage(testRes);
            }
            catch
            {
                //TODO - C - make better. Perhaps remove completely
                Console.WriteLine("Warning - network threw excpetion while calculationg");
                return 0;
            }

            return res;
        }
    }
}
