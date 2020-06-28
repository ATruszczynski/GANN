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
        //TODO - A - implement

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
            Hyperparameters result = new Hyperparameters();

            result.meanW = WeightDistribution.GetNext(random);
            result.stdW = StdDistribution.GetNext(random);
            result.neuronCounts = new int[(int)LayerCountDistribution.GetNext(random) + 2];
            result.neuronCounts[0] = inputSize;
            result.neuronCounts[result.neuronCounts.Length - 1] = outputSize;
            for (int i = 1; i < result.neuronCounts.Length - 1; i++)
            {
                result.neuronCounts[i] = (int)NeuronCountDistribution.GetNext(random);
            }
            result.ActivationFunctions = new ActivationFunction[result.neuronCounts.Length - 1];
            for (int i = 0; i < result.ActivationFunctions.Length; i++)
            {
                result.ActivationFunctions[i] = ActFuncDist.GetNext(random);
            }
            result.LossFunction = LossFuncDist.GetNext(random);
            result.GradientStepStrategy = GradStratDist.GetNext(random);

            return result;
        }
    }
}
