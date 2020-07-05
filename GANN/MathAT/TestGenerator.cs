using GANN.MathAT;
using GANN.NN;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GANN.MathAT.ActFuns;
using static GANN.MathAT.ActFuns;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.SamplingStrategies;
using GANN.GA.ReplacementStrategies;
using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.NN.ParameterRanges;
using GANN.MathAT.Ranges;
using GANN.MathAT.Distributions;
using GANN.NN.LossFunctions;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.Parameters;
using GANN.MathAT;
using GANN.GA;

namespace GANN.MathAT
{
    public class TestGenerator
    {
        public static (double[][], double[][]) TTT1(int number, Random r)
        {
            double[][] inputs = new double[number][];
            double[][] outputs = new double[number][];

            for (int i = 0; i < number; i++)
            {
                inputs[i] = new double[9];
                outputs[i] = new double[4]; //0 - remis, 1 - krzyżyk, 2 - kółko
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    inputs[i][j] = r.Next(3);
                }

                bool cross = CheckForWinning(inputs[i], 1);
                bool circle = CheckForWinning(inputs[i], 2);

                if (cross && circle)
                    outputs[i][3] = 1;
                else if (circle)
                    outputs[i][2] = 1;
                else if (cross)
                    outputs[i][1] = 1;
                else
                    outputs[i][0] = 1;

                for (int j = 0; j < inputs[i].Length; j++)
                {
                    inputs[i][j] /= 2;
                }
            }

            return (inputs, outputs);
        }

        public static (double[][] input, double[][] output) GenerateReverseIO(int number, Random random)
        {
            double[][] input = new double[number][];
            double[][] output = new double[number][];

            for (int i = 0; i < number; i++)
            {
                int ind = random.Next(2);
                input[i] = new double[2];
                output[i] = new double[2];
                input[i][ind] = 1;
                output[i][1 - ind] = 1;
            }

            return (input, output);
        }

        static bool CheckForWinning(double[] b, double fig)
        {
            int[][] winningCom = new int[8][]
            {
                new int[] { 0, 1, 2},
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            for (int i = 0; i < winningCom.Length; i++)
            {
                var d = winningCom[i];
                if (b[d[0]] == fig && b[d[1]] == fig && b[d[2]] == fig)
                    return true;
            }

            return false;
        }

        public static void TestScenario1()
        {

            //int num = 2000;
            //(var i, var o) = TestGenerator.TTT(num);

            ANN nn = new ANN(new Hyperparameters(new int[] { 9, 4, 4, 4 }), new Random(1001));
                 //(
                 // new int[] { 9, 4, 4, 4 },
                 // //new Func<double, double>[] { Relu, Sigma },
                 // //new Func<double, double>[] { DerRelu, DerSigma },
                 // //new int[] { 9, 20, 20, 4 },
                 // new Func<double, double>[] { Relu, Relu, Sigma },
                 // new Func<double, double>[] { DerRelu, Relu, DerSigma },
                 // //new Func<double, double>[] { Sigma, Sigma, Sigma },
                 // //new Func<double, double>[] { DerSigma, DerSigma, DerSigma },
                 // null,
                 // DerLoss
                 //);

            //nn.Train(i, o, 20, 100);

            //var res = nn.Run(new double[] { 0, 0, 0, 1, 2, 1, 2, 2, 2 });
            //Console.WriteLine($"{res[0]},{res[1]},{res[2]},{res[3]}");
        }

        public static void TestScenario2()
        {
            ANN nn = new ANN(new Hyperparameters(new int[] { 9, 4, 4, 4 }), new Random(1001));

            nn.Layers[1].weights = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.Layers[2].weights = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.Layers[1].biases = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.Layers[2].biases = new MatrixAT1(new double[,] { { 1 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 });

            nn.Train
                (
                new double[][] { new double[] { 2, 1 } },
                new double[][] { new double[] { 1, 0 } },
                2,
                1
                );
        }

        public static void TTTTest()
        {
            Random random = new Random(1001);

            (var trainInput, var trainOutput) = TestGenerator.TTT1(1000, random);
            (var testInput, var testOutput) = TestGenerator.TTT1(200, random);

            Utility.WriteArary(Utility.ClassCounts(trainOutput));
            Utility.WriteArary(Utility.ClassCounts(testOutput));


            GeneticAlgorithm ga = new GeneticAlgorithm();
            ga.CrossoverOperator = new NNBasicCrossoverOperator();

            var hp = new HyperparameterRanges();
            hp.WeightDistribution = new ContinuousRange(new UniformContinuousRangeDistribution(random, -1, 1));
            hp.StdDistribution = new ContinuousRange(new UniformContinuousRangeDistribution(random, -1, 1));
            hp.InternalLayerCountDistribution = new DiscreteRange(new UniformDiscreteRangeDistribution(random, 1, 5));
            hp.NeuronCountDistribution = new DiscreteRange(new UniformDiscreteRangeDistribution(random, 1, 20));
            hp.ActFuncDist = new SetRange<ActivationFunction>(new ActivationFunction[] { new Relu() }, new UniformDiscreteRangeDistribution(random, 0, 1));
            hp.LossFuncDist = new SetRange<LossFunction>(new LossFunction[] { new QuadDiff() }, new UniformDiscreteRangeDistribution(random, 0, 1));
            hp.GradStratDist = new SetRange<GradientStepStrategy>(new GradientStepStrategy[] { new ConstantGradientStep(0.5), new ConstantGradientStep(1) }, new UniformDiscreteRangeDistribution(random, 0, 2));
            hp.inputSize = trainInput[0].Length;
            hp.outputSize = trainOutput[0].Length;
            hp.outputAct = new Sigma();

            ga.MutationOperator = new NNBasicMutationOperator(hp);
            ga.SamplingStrategy = new RouletteSamplingStrategy();
            ga.ReplacementStrategy = new GenerationalReplacementStrategy();

            NNFitnessFunc nnff = new NNFitnessFunc();
            nnff.trainInputs = trainInput;
            nnff.trainOutputs = trainOutput;
            nnff.testInputs = testInput;
            nnff.testOutputs = testOutput;
            ga.FitnessFunction = new NNFitnessFunc();

            ga.FitnessFunction = nnff;

            int pop = 10;
            int len = 20;
            ga.population = new NNChromosome[pop];
            for (int i = 0; i < pop; i++)
            {
                Hyperparameters param = (Hyperparameters)hp.GetNext();

                ga.population[i] = new NNChromosome(new ANN(param, random));
            }

            ga.crossoverProbability = 0.25;
            ga.mutationProbability = 1;

            ga.Iterations = 25;

            NNChromosome c = (NNChromosome)ga.Run(random);

            Console.WriteLine(ga.FitnessFunction.ComputeFitness(c));

            var res = c.NeuralNetwork.Run(new double[] { 0, 2, 2, 1, 1, 1, 0, 0, 2 });
            Console.WriteLine($"{res[0]},{res[1]},{res[2]},{res[3]}");
        }

        public static void ReverseTest()
        {
            Random random = new Random(1001);
            (var trainInput, var trainOutput) = GenerateReverseIO(10000, random);
            (var testInput, var testOutput) = GenerateReverseIO(1000, random);

            ANN nn = new ANN(new Hyperparameters(new int[] { 2, 2 }));

            nn.Train(trainInput, trainOutput, 5, 100);
            Console.WriteLine("Accuracy: " + nn.Test(testInput, testOutput, "desu.txt").Average());

        }
    }
}
