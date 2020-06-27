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
        //TODO - B - should it have different probabilites per parameter
        //TODO - B - distribution class? And used to represent paramater ranges?
        double minW;
        double maxW;
        double minStd;
        double maxStd;
        int minInternalLayer;
        int maxInternalLayer;
        //TODO - B - different minmax values per layer?
        int minNeuronCount;
        int maxNeuronCount;
        List<ActivationFunction> PossibleActivationFunctions;
        List<LossFunction> PossibleLossFunctions;
        List<GradientStepStrategy> PossibleGradientStepStrategies;

        public Hyperparameters GetRandomHyperparameters()
        {
            Hyperparameters result = new Hyperparameters();



            return result;
        }
    }
}
