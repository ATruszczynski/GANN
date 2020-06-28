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

        public Hyperparameters(int[] neuronCountss, double mw = 0, double sw = 1, ActivationFunction[] actFuns = null, LossFunction lossFunc = null, GradientStepStrategy gradStep = null)
        {
            //TODO - B - variable names in many classes
            //TODO - C - should I deep copy arrays?
            //TODO - B - test
            neuronCounts = neuronCountss;
            meanW = mw;
            stdW = sw;

            if(actFuns != null)
            {
                ActivationFunctions = actFuns;
            }
            else
            {
                for (int i = 0; i < neuronCounts.Length - 2; i++)
                {
                    ActivationFunctions[i] = new Relu();
                }
                ActivationFunctions[neuronCounts.Length - 1] = new Sigma();
            }

            if (lossFunc != null)
                LossFunction = lossFunc;
            else
                LossFunction = new QuadDiff();

            if (gradStep != null)
                GradientStepStrategy = gradStep;
            else
                GradientStepStrategy = new ConstantGradientStep(1);
        }

        public int LayerCount { get => neuronCounts.Length; }
    }
}
