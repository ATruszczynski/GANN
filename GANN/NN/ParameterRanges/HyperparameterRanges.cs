using GANN.MathAT.Distributions;
using GANN.MathAT.Ranges;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
//TODO - C - async random is not deterministic
namespace GANN.NN.ParameterRanges
{
    public class HyperparameterRanges : Range
    {
        //TODO - B - should that class be range?
        //TODO - B - implement changing probability of cs and mutation in GA
        //public ContinuousRange WeightDistribution;
        //public ContinuousRange StdDistribution;
        public DiscreteRange InternalLayerCountDistribution;
        public DiscreteRange NeuronCountDistribution;
        public SetRange<ActivationFunction> InternalActFuncDist;
        public SetRange<ActivationFunction> AggregateFunction;
        public SetRange<LossFunction> LossFuncDist;
        public SetRange<GradientStepStrategy> GradStratDist;
        public int inputSize;
        public int outputSize;

        //TODO - B - validate if everything is initialised

        public override bool IsInRange(object value)
        {
            //TODO - B - implement
            throw new NotImplementedException();
        }

        public HyperparameterRanges(int intput, int output)
        {
            inputSize = intput;
            outputSize = output;
        }

        public override object GetNext()
        {
            //TODO - B - change names of props
            //TODO - A - test tc
            //TODO - C - can there be layer count == 0?
            //double meanW = (double)WeightDistribution.GetNext();
            //double stdW = (double)StdDistribution.GetNext();
            var internalNeuronCounts = new int[(int)InternalLayerCountDistribution.GetNext()];
            for (int i = 0; i < internalNeuronCounts.Length; i++)
            {
                internalNeuronCounts[i] = (int)NeuronCountDistribution.GetNext();
            }
            var ActivationFunctions = new ActivationFunction[internalNeuronCounts.Length];
            for (int i = 0; i < ActivationFunctions.Length; i++)
            {
                ActivationFunctions[i] = (ActivationFunction)InternalActFuncDist.GetNext();
            }
            //TODO - B - last func is output or agg
            var aggAct = (ActivationFunction)AggregateFunction.GetNext();
            var LossFunction = (LossFunction)LossFuncDist.GetNext();
            var GradientStepStrategy = (GradientStepStrategy)GradStratDist.GetNext();


            Hyperparameters result = new Hyperparameters
                (
                    inputSize,
                    outputSize,
                    internalNeuronCounts,
                    ActivationFunctions,
                    aggAct,
                    LossFunction,
                    GradientStepStrategy
                );

            return result;
        }

        public override object GetNeighbour(object obj)
        {
            //Hyperparameters hp = (Hyperparameters)obj;



            //Hyperparameters result = new Hyperparameters
            //    (
            //        inputSize,
            //        outputSize,
            //        internalNeuronCounts,
            //        ActivationFunctions,
            //        aggAct,
            //        LossFunction,
            //        GradientStepStrategy
            //    );

            //return result;
            //shoudl not be used
            throw new NotImplementedException("This method should not be used");
        }
    }
}
