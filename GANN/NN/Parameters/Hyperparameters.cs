using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.Parameters
{
    public class Hyperparameters
    {
        public double meanW;
        public double stdW;
        public int[] neuronCounts;
        public ActivationFunction[] ActivationFunctions;
        public LossFunction LossFunction;
        public GradientStepStrategy GradientStepStrategy;

        public int LayerCount { get => neuronCounts.Length; }
    }
}
