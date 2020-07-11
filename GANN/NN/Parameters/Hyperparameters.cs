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
        public int[] InternalNeuronCounts;
        public double meanW;
        public double stdW;
        public int LayerCount { get => InternalNeuronCounts.Length + 2; }
        public ActivationFunction[] InternalActivationFunctions;
        public ActivationFunction AggFunc;
        public LossFunction LossFunction;
        public GradientStepStrategy GradientStepStrategy;

        public Hyperparameters(int inpSize, int outSize, int[] inNeuronCounts = null, ActivationFunction[] intActFuns = null, ActivationFunction aggregateActFunc = null, LossFunction lossFunc = null, GradientStepStrategy gradStep = null, double mw = 0, double sw = -1)
        {
            //TODO - A - validation
            //TODO - B - variable names in many classes
            //TODO - C - should I deep copy arrays?
            //TODO - A - test

            inputSize = inpSize;
            outputSize = outSize;

            if (inNeuronCounts == null)
                InternalNeuronCounts = new int[0];
            else
                InternalNeuronCounts = inNeuronCounts;

            meanW = mw;

            if (sw == -1)
            {
                //stddev = sqrt(2 / Nr_input_neurons) - Xe initilization
                stdW = Math.Sqrt(2d / inputSize);
            }
            else
            {
                stdW = sw;
            }

            if (intActFuns != null)
            {
                InternalActivationFunctions = intActFuns;
            }
            else
            {
                InternalActivationFunctions = new ActivationFunction[InternalNeuronCounts.Length];
                for (int i = 0; i < InternalNeuronCounts.Length; i++)
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
            //TODO - A - test
            int[] nc = new int[InternalNeuronCounts.Length];
            for (int i = 0; i < nc.Length; i++)
            {
                nc[i] = InternalNeuronCounts[i];
            }

            ActivationFunction[] acts = new ActivationFunction[InternalNeuronCounts.Length];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = InternalActivationFunctions[i].DeepCopy();
            }

            Hyperparameters hp = new Hyperparameters(inputSize, outputSize, nc, acts, AggFunc.DeepCopy(), LossFunction.DeepCopy(), GradientStepStrategy.DeepCopy(), meanW, stdW);



            return hp;
        }

        public override string ToString()
        {
            string result = "";

            double m = Math.Round(meanW, 2);
            result += m + "|";
            double s = Math.Round(stdW, 2);
            result += s + "|";

            result += inputSize + "|-|";

            for (int i = 0; i < InternalNeuronCounts.Length; i++)
            {
                result += InternalNeuronCounts[i] + "|" + InternalActivationFunctions[i].ToString() + "|";
            }

            result += outputSize + "|" + AggFunc.ToString() + "|" + LossFunction.ToString() + "|" + GradientStepStrategy.ToString();


            return result;
        }
    }
}
