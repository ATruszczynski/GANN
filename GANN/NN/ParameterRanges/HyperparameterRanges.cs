using GANN.MathAT.Distributions;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ParameterRanges
{
    public class HyperparameterRanges
    {
        //TODO - B - implement changing probability of cs and mutation in GA

        public ContinuousDistributon WeightDistribution;
        public ContinuousDistributon StdDistribution;
        public DiscreteDistribution LayerCountDistribution;
        public DiscreteDistribution NeuronCountDistribution;
        public ObjectChoosingDistribution<ActivationFunction> ActFuncDist;
        public ObjectChoosingDistribution<LossFunction> LossFuncDist;
        public ObjectChoosingDistribution<GradientStepStrategy> GradStratDist;
        public int inputSize;
        public int outputSize;

        public Hyperparameters GetRandomHyperparameters(Random random)
        {
            //TODO - B - test
            //TODO - C - can there be layer count == 0?

            var meanW = WeightDistribution.GetNext(random);
            var stdW = StdDistribution.GetNext(random);
            var neuronCounts = new int[(int)LayerCountDistribution.GetNext(random) + 2];
            neuronCounts[0] = inputSize;
            neuronCounts[neuronCounts.Length - 1] = outputSize;
            for (int i = 1; i < neuronCounts.Length - 1; i++)
            {
                neuronCounts[i] = (int)NeuronCountDistribution.GetNext(random);
            }
            var ActivationFunctions = new ActivationFunction[neuronCounts.Length - 1];
            for (int i = 0; i < ActivationFunctions.Length; i++)
            {
                ActivationFunctions[i] = ActFuncDist.GetNext(random);
            }
            var LossFunction = LossFuncDist.GetNext(random);
            var GradientStepStrategy = GradStratDist.GetNext(random);


            Hyperparameters result = new Hyperparameters
                (
                    neuronCounts,
                    meanW,
                    stdW,
                    ActivationFunctions,
                    LossFunction,
                    GradientStepStrategy
                );

            return result;
        }
    }
}
