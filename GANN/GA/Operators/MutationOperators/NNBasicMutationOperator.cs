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
using static GANN.MathAT.MutateAction;
using GANN.NN.LossFunctions;
using GANN.NN.GradientStepStrategies;

namespace GANN.GA.Operators.MutationOperators
{
    public class NNBasicMutationOperator : MutationOperator
    {
        //TODO - ? - repairs in GA
        //TODO - A - test tc

        public HyperparameterRanges Ranges;
        public double possOfHPChange = 0.5;
        public int MaxMutNumber = 2;
        public NNBasicMutationOperator(HyperparameterRanges ranges, int maxMutNumber = 2)
        {
            Ranges = ranges;
            MaxMutNumber = maxMutNumber;
        }
        //TODO - B - random as property
        public override Chromosome Mutate(Chromosome m, Random random)
        {
            NNChromosome nnc = (NNChromosome)m;

            Hyperparameters hp = nnc.Hyperparameters;

            var  possibleMutations = Define(hp);
            MutateAction mutation = possibleMutations[random.Next(possibleMutations.Count)];


            if (mutation == AddLayer)
            {
                int ind = random.Next(hp.InternalNeuronCounts.Length + 1);
                int newNeuronCount = (int)Ranges.NeuronCountDistribution.GetNext();
                int[] newInternalNC = new int[hp.InternalNeuronCounts.Length + 1];
                int oldInd = 0;
                for (int i = 0; i < newInternalNC.Length; i++)
                {
                    if (i != ind)
                    {
                        newInternalNC[i] = hp.InternalNeuronCounts[oldInd++];
                    }
                    else
                    {
                        newInternalNC[i] = newNeuronCount;
                    }
                }
                hp.InternalNeuronCounts = newInternalNC;
            }
            else if (mutation == RemoveLayer)
            {
                int ind = random.Next(hp.InternalNeuronCounts.Length);
                int[] newInternalNC = new int[hp.InternalNeuronCounts.Length - 1];
                int newInd = 0;
                for (int i = 0; i < hp.InternalNeuronCounts.Length; i++)
                {
                    if(i != ind)
                    {
                        newInternalNC[newInd++] = hp.InternalNeuronCounts[i];
                    }
                }
                hp.InternalNeuronCounts = newInternalNC;
                
            }
            else if (mutation == ChangeNeuronCount)
            {
                int ind = random.Next(hp.InternalNeuronCounts.Length);
                hp.InternalNeuronCounts[ind] = (int)Ranges.NeuronCountDistribution.GetNeighbour(hp.InternalNeuronCounts[ind]);
            }
            else if (mutation == ChangeActFunc)
            {
                ActivationFunction af = hp.InternalActivationFunctions[0];
                ActivationFunction naf = (ActivationFunction)Ranges.InternalActFuncDist.GetNeighbour(af);
                for (int i = 0; i < hp.InternalActivationFunctions.Length; i++)
                {
                    hp.InternalActivationFunctions[i] = naf.DeepCopy();
                }
            }
            else if (mutation == ChangeLossFunction)
            {
                LossFunction nfl = (LossFunction)Ranges.LossFuncDist.GetNeighbour(hp.LossFunction);
                hp.LossFunction = nfl;
            }
            else if (mutation == ChangeGradientStrat)
            {
                hp.GradientStepStrategy = (GradientStepStrategy)Ranges.GradStratDist.GetNeighbour(hp.GradientStepStrategy);
            }
            else
            {
                throw new ArgumentException("Da fuq?");
            }

            nnc = new NNChromosome();

            nnc.Hyperparameters = hp.DeepCopy();

            return nnc;
        }

        List<MutateAction> Define(Hyperparameters hp)
        {
            List<MutateAction> result = new List<MutateAction>();

            //if (Ranges.WeightDistribution.Min != Ranges.WeightDistribution.Max)
            //    result.Add(0); //weights can be mutated

            //if (Ranges.StdDistribution.Min != Ranges.StdDistribution.Max)
            //    result.Add(1); //std can be mutated

            if (hp.LayerCount - 2 < Ranges.InternalLayerCountDistribution.Max)
                result.Add(AddLayer); //layer can be added

            if (hp.LayerCount > 2)
            {
                result.Add(RemoveLayer); //layer can be removed
                result.Add(ChangeNeuronCount); //layer neruron count can be changed
                if(Ranges.InternalActFuncDist.Values.Length > 1)
                    result.Add(ChangeActFunc); //layer act func can be changed
            }

            if (Ranges.LossFuncDist.Values.Length > 1)
                result.Add(ChangeLossFunction); //loss function can be changed

            if (Ranges.GradStratDist.Values.Length > 1)
                result.Add(ChangeGradientStrat);

            return result;
        }
    }
}
