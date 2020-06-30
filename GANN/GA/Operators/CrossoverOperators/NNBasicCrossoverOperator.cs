using GANN.GA.GA_Elements;
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
            NNChromosome cnn1 = (NNChromosome)ch1;
            NNChromosome cnn2 = (NNChromosome)ch2;
            ANN nn1 = (ANN)cnn1.NeuralNetwork;
            ANN nn2 = (ANN)cnn2.NeuralNetwork;
            Hyperparameters hp1 = nn1.Hyperparameters;
            Hyperparameters hp2 = nn2.Hyperparameters;

            double p = random.NextDouble();

            if (p <= 0.5)
            {
                double w = hp1.meanW;
                hp1.meanW = hp2.meanW;
                hp2.meanW = w;
            }

            p = random.NextDouble();

            if (p <= 0.5)
            {
                double std = hp1.stdW;
                hp1.stdW = hp2.stdW;
                hp2.stdW = std;
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
                hp2.GradientStepStrategy = hp1.GradientStepStrategy;
            }

            p = random.NextDouble();

            if (p <= 0.5)
            {
                var nc = hp1.neuronCounts;
                hp1.neuronCounts = hp2.neuronCounts;
                hp2.neuronCounts = nc;

                var acts = hp1.ActivationFunctions;
                hp1.ActivationFunctions = hp2.ActivationFunctions;
                hp2.ActivationFunctions = acts;
            }

            cnn1.NeuralNetwork = new ANN(hp1, nn1.Random);
            cnn2.NeuralNetwork = new ANN(hp2, nn2.Random);

            return (cnn1, cnn2);
        }
    }
}
