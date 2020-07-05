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
        ActivationFunction defaultActInner = new Relu();
        ActivationFunction defaultAggregate = new Sigma();
        LossFunction defaultLoss = new QuadDiff();
        GradientStepStrategy defaultGradientStep = new ConstantGradientStep(1);
        //TODO - C - internal neurons to hidden
        public int inputSize = -1;
        public int outputSize = -1;
        public int[] internalNeuronCounts;
        public double meanW;
        public double stdW;
        public int LayerCount { get => internalNeuronCounts.Length + 2; }
        public ActivationFunction[] InternalActivationFunctions;
        public ActivationFunction AggFunc;
        public LossFunction LossFunction;
        public GradientStepStrategy GradientStepStrategy;

        public Hyperparameters(int inpSize, int outSize, int[] inNeuronCounts = null, double mw = 0, double sw = 1, ActivationFunction[] intActFuns = null, ActivationFunction aggregateActFunc = null, LossFunction lossFunc = null, GradientStepStrategy gradStep = null)
        {
            //TODO - A - validation
            //TODO - B - variable names in many classes
            //TODO - C - should I deep copy arrays?
            //TODO - B - test

            inputSize = inpSize;
            outputSize = outSize;

            if (inNeuronCounts == null)
                internalNeuronCounts = new int[0];
            else
                internalNeuronCounts = inNeuronCounts;

            meanW = mw;
            stdW = sw;

            if(intActFuns != null)
            {
                InternalActivationFunctions = intActFuns;
            }
            else
            {
                InternalActivationFunctions = new ActivationFunction[internalNeuronCounts.Length];
                for (int i = 0; i < internalNeuronCounts.Length; i++)
                {
                    InternalActivationFunctions[i] = defaultActInner.DeepCopy();
                }
            }

            if(aggregateActFunc != null)
            {
                AggFunc = aggregateActFunc;
            }
            else
            {
                AggFunc = defaultAggregate.DeepCopy();
            }

            if (lossFunc != null)
                LossFunction = lossFunc;
            else
                LossFunction = defaultLoss.DeepCopy();

            if (gradStep != null)
                GradientStepStrategy = gradStep;
            else
                GradientStepStrategy = defaultGradientStep.DeepCopy();
        }


        //TODO - B - implement
        public Hyperparameters DeepCopy()
        {
            //TODO - B - test
            int[] nc = new int[internalNeuronCounts.Length];
            for (int i = 0; i < nc.Length; i++)
            {
                nc[i] = internalNeuronCounts[i];
            }

            ActivationFunction[] acts = new ActivationFunction[internalNeuronCounts.Length];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = InternalActivationFunctions[i].DeepCopy();
            }

            Hyperparameters hp = new Hyperparameters(inputSize, outputSize, nc, meanW, stdW, acts, AggFunc.DeepCopy(), LossFunction.DeepCopy(), GradientStepStrategy.DeepCopy());



            return hp;
        }
    }
}
