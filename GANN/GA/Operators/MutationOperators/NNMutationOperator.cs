using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;
using GANN.NN.ParameterRanges;
using GANN.NN;
using GANN.NN.Parameters;
using static GANN.MathAT.Utility;

namespace GANN.GA.Operators.MutationOperators
{
    public class NNMutationOperator : MutationOperator
    {
        //TODO - 0 - figure out how it will work
        //TODO - 0 - range and repairs in GA
        //TODO - 0 - layer structure?
        public HyperparameterRanges Ranges;
        public double possOfHPChange = 0.5;
        public NNMutationOperator(HyperparameterRanges ranges)
        {
            Ranges = ranges;
        }
        //TODO - B - random as property
        public override Chromosome Mutate(Chromosome m, Random radoms)
        {
            NNChromosome nnc = (NNChromosome)m;

            NeuralNetwork nn = nnc.NeuralNetwork;

            Hyperparameters hp = nn.Hyperparameters;
            //TODO - B - range class could help with that
            double p = radoms.NextDouble();

            if(p <= possOfHPChange)
            {
                hp.meanW = Ranges.WeightDistribution.GetNext();
            }

            p = radoms.NextDouble();

            if (p <= possOfHPChange)
            {
                hp.stdW = Ranges.StdDistribution.GetNext();
            }

            //for (int i = 0; i < hp.ActivationFunctions.Length; i++)
            //{ 
            //    p = radoms.NextDouble();
            //    if(p <= possOfHPChange)

            //}

            p = radoms.NextDouble();

            if(p <= possOfHPChange)
            {
                int newnc = (int)Ranges.InternalLayerCountDistribution.GetNext();
            }

            nnc.NeuralNetwork = new ANN(hp, nn.Random);

            return nnc;
        }
    }
}
