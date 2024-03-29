﻿using GANN.GA.GA_Elements;
using GANN.MathAT;
using GANN.NN;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.Operators.CrossoverOperators
{
    public class NNBasicCrossoverOperator : CrossoverOperator
    {
        public override (Chromosome, Chromosome) Crossover(Chromosome ch1, Chromosome ch2, Random random)
        {
            //TODO - B - validation
            NNChromosome cnn1 = (NNChromosome)ch1;
            NNChromosome cnn2 = (NNChromosome)ch2;
            Hyperparameters hp1 = cnn1.Hyperparameters;
            Hyperparameters hp2 = cnn2.Hyperparameters;


            //if (p <= 0.5)
            //{
            //    double w = hp1.meanW;
            //    hp1.meanW = hp2.meanW;
            //    hp2.meanW = w;
            //}

            //p = random.NextDouble();

            //if (p <= 0.5)
            //{
            //    double std = hp1.stdW;
            //    hp1.stdW = hp2.stdW;
            //    hp2.stdW = std;
            //}

            double p = random.NextDouble();

            if (p <= 0.5)
            {
                var nc = hp1.InternalNeuronCounts;
                hp1.InternalNeuronCounts = hp2.InternalNeuronCounts;
                hp2.InternalNeuronCounts = nc;

                var acts = hp1.InternalActivationFunctions;
                hp1.InternalActivationFunctions = hp2.InternalActivationFunctions;
                hp2.InternalActivationFunctions = acts;
            }

            p = random.NextDouble();

            if (p <= 0.5)
            {
                int howMany = random.Next(1, Math.Min(hp1.InternalNeuronCounts.Length, hp2.InternalNeuronCounts.Length) + 1);
                List<int> counts1 = new List<int>();
                List<int> counts2 = new List<int>();

                for (int i = 0; i < hp1.InternalNeuronCounts.Length; i++)
                {
                    counts1.Add(i);
                }

                for (int i = 0; i < hp2.InternalNeuronCounts.Length; i++)
                {
                    counts2.Add(i);
                }

                SampleWithoutReplacement<int> swr1 = new SampleWithoutReplacement<int>(counts1, random);
                SampleWithoutReplacement<int> swr2 = new SampleWithoutReplacement<int>(counts2, random);

                for (int i = 0; i < howMany; i++)
                {
                    swr1.GetNext(out int ind1);
                    swr2.GetNext(out int ind2);

                    int tmp = hp1.InternalNeuronCounts[ind1];
                    hp1.InternalNeuronCounts[ind1] = hp2.InternalNeuronCounts[ind2];
                    hp2.InternalNeuronCounts[ind2] = tmp;
                }
            }

            p = random.NextDouble();

            if (p <= 0.5)
            {
                var agg = hp1.AggFunc;
                hp1.AggFunc = hp2.AggFunc;
                hp2.AggFunc = agg;
            }

            p = random.NextDouble();

            if (p <= 0.5)
            {
                LossFunction lf = hp1.LossFunction;
                hp1.LossFunction = hp2.LossFunction;
                hp2.LossFunction = lf;
            }

            p = random.NextDouble();

            if (p <= 0.5)
            {
                GradientStepStrategy gss = hp1.GradientStepStrategy;
                hp1.GradientStepStrategy = hp2.GradientStepStrategy;
                hp2.GradientStepStrategy = gss;
            }

            cnn1.Hyperparameters = hp1;
            cnn2.Hyperparameters = hp2;

            return (cnn1, cnn2);
        }
    }
}
