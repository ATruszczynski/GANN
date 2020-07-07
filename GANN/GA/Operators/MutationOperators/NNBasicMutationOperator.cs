using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;
using GANN.NN.ParameterRanges;
using GANN.NN;
using GANN.NN.Parameters;
using static GANN.MathAT.Utility;
using GANN.MathAT;
using GANN.NN.ActivationFunctions;

namespace GANN.GA.Operators.MutationOperators
{
    public class NNBasicMutationOperator : MutationOperator
    {
        //TODO - A - are layers a good idea? Should layers be in Hyperparameters instead?
        //TODO - 0 - figure out how it will work
        //TODO - ? - repairs in GA
        //TODO - A - layers aren't helping that much :/
        //TODO - B - test tc

        public HyperparameterRanges Ranges;
        public double possOfHPChange = 0.5;
        public int MaxMutNumber = 2;
        public NNBasicMutationOperator(HyperparameterRanges ranges, int maxMutNumber = 2)
        {
            Ranges = ranges;
            MaxMutNumber = maxMutNumber;
        }
        //TODO - B - random as property
        public override Chromosome Mutate(Chromosome m, Random radoms)
        {
            NNChromosome nnc = (NNChromosome)m;

            ANN nn = (ANN)nnc.NeuralNetwork;

            Hyperparameters hp = nn.Hyperparameters;

            hp = (Hyperparameters)Ranges.GetNext();

            nnc = new NNChromosome();

            nnc.NeuralNetwork = new ANN(hp, nn.Random);

            return nnc;
        }

        int[] Define(ANN nn)
        {
            List<int> result = new List<int>();

            if (Ranges.WeightDistribution.Min != Ranges.WeightDistribution.Max)
                result.Add(0); //weights can be mutated

            if (Ranges.StdDistribution.Min != Ranges.StdDistribution.Max)
                result.Add(1); //std can be mutated

            if (nn.LayerCount - 2 < Ranges.InternalLayerCountDistribution.Max)
                result.Add(2); //layer can be added

            if (nn.LayerCount > 2)
            {
                result.Add(3); //layer can be removed
                result.Add(4); //layer neruron count can be changed
                if(Ranges.ActFuncDist.Values.Length > 1)
                    result.Add(5); //layer act func can be changed
            }

            if (Ranges.LossFuncDist.Values.Length > 1)
                result.Add(6); //loss function can be changed

            if (Ranges.GradStratDist.Values.Length > 1)
                result.Add(7);

            return result.ToArray();
        }
    }
}
