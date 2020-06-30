using GANN.MathAT.Distributions;
using GANN.MathAT.Ranges;
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

        public ContinuousRange WeightDistribution;
        public ContinuousRange StdDistribution;
        public DiscreteRange InternalLayerCountDistribution;
        public DiscreteRange NeuronCountDistribution;
        public SetRange<ActivationFunction> ActFuncDist;
        public SetRange<LossFunction> LossFuncDist;
        public SetRange<GradientStepStrategy> GradStratDist;
        public int inputSize;
        public int outputSize;
        //TODO - B - change names of props
        public Hyperparameters GetRandomHyperparameters()
        {
            //TODO - B - test
            //TODO - C - can there be layer count == 0?

            double meanW = (double)WeightDistribution.GetNext();
            double stdW = (double)StdDistribution.GetNext();
            var neuronCounts = new int[(int)InternalLayerCountDistribution.GetNext() + 2];
            neuronCounts[0] = inputSize;
            neuronCounts[neuronCounts.Length - 1] = outputSize;
            for (int i = 1; i < neuronCounts.Length - 1; i++)
            {
                neuronCounts[i] = (int)NeuronCountDistribution.GetNext();
            }
            var ActivationFunctions = new ActivationFunction[neuronCounts.Length - 1];
            for (int i = 0; i < ActivationFunctions.Length; i++)
            {
                ActivationFunctions[i] = (ActivationFunction)ActFuncDist.GetNext();
            }
            var LossFunction = (LossFunction)LossFuncDist.GetNext();
            var GradientStepStrategy = (GradientStepStrategy)GradStratDist.GetNext();


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
