﻿using GANN.MathAT;
using GANN.MathAT.Distributions;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Other;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace GANN.NN
{
    public class ANN : NeuralNetwork
    {
        //TODO - 0 - make faster
        //TODO - D - remove needless pubilc
        //TODO - B - names from capital letters

        public int[] neuronCounts;
        public ActivationFunction[] activationFuncs;
        public MatrixAT1[] weights;
        public MatrixAT1[] biases;
        public MatrixAT1[] zs;
        public MatrixAT1[] ases;
        public LossFunction LossFunction;
        public GradientStepStrategy GradientStepStrategy;

        public int LayerCount { get => neuronCounts.Length; }
        object[] syncLocks = new object[] { new object(), new object(), new object() };
        public int masDeg = -1;

        //TODO - B - random as property
        //TODO - B - alternative to Relu?
        //TODO - B - Add argument validation
        //TODO - B - custom edges
        //TODO - A - hyperparameters as only property
        public ANN(Hyperparameters hyperparameters, Random random = null)
        {
            Hyperparameters = hyperparameters;

            if (random == null)
                Random = new Random();
            else
                Random = random;

            neuronCounts = new int[hyperparameters.internalNeuronCounts.Length + 2];
            neuronCounts[0] = hyperparameters.inputSize;
            neuronCounts[neuronCounts.Length - 1] = hyperparameters.outputSize;

            for (int i = 1; i < neuronCounts.Length - 1; i++)
            {
                neuronCounts[i] = hyperparameters.internalNeuronCounts[i - 1];
            }

            activationFuncs = new ActivationFunction[neuronCounts.Length];
            for (int i = 1; i < neuronCounts.Length - 1; i++)
            {
                activationFuncs[i] = hyperparameters.InternalActivationFunctions[i - 1].DeepCopy();
            }
            activationFuncs[neuronCounts.Length - 1] = hyperparameters.AggFunc;
            weights = new MatrixAT1[neuronCounts.Length];
            biases = new MatrixAT1[neuronCounts.Length];
            zs = new MatrixAT1[neuronCounts.Length];
            ases = new MatrixAT1[neuronCounts.Length];

            for (int i = 1; i < neuronCounts.Length; i++)
            {
                weights[i] = new MatrixAT1(neuronCounts[i], neuronCounts[i - 1]);
                biases[i] = new MatrixAT1(neuronCounts[i], 1);
                zs[i] = new MatrixAT1(neuronCounts[i], 1);
                ases[i] = new MatrixAT1(neuronCounts[i], 1);
            }

            GaussianDistribution gd = new GaussianDistribution(random, hyperparameters.meanW, hyperparameters.stdW);
            
            for (int i = 1; i < weights.Length; i++)
            {
                MatrixAT1 wei = weights[i];
                for (int r = 0; r < wei.Rows; r++)
                {
                    for (int c = 0; c < wei.Columns; c++)
                    {
                        wei[r, c] = gd.GetNext();
                    }
                }
            }

            LossFunction = hyperparameters.LossFunction;
            GradientStepStrategy = hyperparameters.GradientStepStrategy;
        }

        public override double[] Run(double[] input)
        {
            if (input.Length != neuronCounts[0])
                throw new ArgumentException($"Wrong numbers of arguments in input - {input.Length} (expected {neuronCounts[0]})");

            ases[0] = new MatrixAT1(input);

            for (int layer = 1; layer < LayerCount; layer++)
            {
                ases[layer] = new MatrixAT1(neuronCounts[layer], 1);

                MatrixAT1 a_Lminus1 = ases[layer-1];

                zs[layer] = weights[layer] * a_Lminus1 + biases[layer];

                for (int j = 0; j < neuronCounts[layer]; j++)
                {
                    ases[layer][j, 0] = activationFuncs[layer].Compute(zs[layer][j, 0]); 
                }
            }

            double[] result = new double[neuronCounts[neuronCounts.Length - 1]];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = ases[neuronCounts.Length - 1][i, 0];
            }

            return result;
        }

        public override void Train(double[][] inputs, double[][] outputs, int epochs, int batchSize)
        {
            double batches = Ceiling((double)inputs.Length / (double)batchSize);
            for (int e = 0; e < epochs; e++)
            {
                for (int b = 0; b < batches; b++)
                {
                    int startIndInc = b * batchSize;
                    int endIndExc = Min(inputs.Length, (b + 1) * batchSize);
                    int n = endIndExc - startIndInc;

                    if (n == 0)
                        break;

                    (MatrixAT1[] weightGradChange, MatrixAT1[] biasesGradChange, _) = InitialiseComponents();
                    double averageDiff = 0;

                    //for (int inputInd = startIndInc; inputInd < endIndExc; inputInd++)
                    //{
                    var listOfTasks = Utility.SeparateForTasks(startIndInc, endIndExc, 10);
                    Parallel.For(startIndInc, endIndExc, new ParallelOptions { MaxDegreeOfParallelism = masDeg }, inputInd => 
                    { 
                        (MatrixAT1[] weightGrad, MatrixAT1[] biasesGrad, MatrixAT1[] activationsGrad) = InitialiseComponents();
                        for (int layer = LayerCount - 1; layer >= 1; layer--)
                        {
                            if(layer == LayerCount - 1)
                            {
                                MatrixAT1 currOutput = new MatrixAT1(Run(inputs[inputInd]));

                                MatrixAT1 goodOutput = new MatrixAT1(outputs[inputInd]);

                                lock(syncLocks[0])
                                    averageDiff += LossFunction.Compute(currOutput, goodOutput); //TODO - C - sumDiff into averageDiff

                                activationsGrad[layer] = CalculateLastLayerLossGradient(currOutput, goodOutput);
                            }
                            else
                            {
                                activationsGrad[layer] = CalculateActivationGradient(layer, biasesGrad[layer + 1]);
                            }

                            biasesGrad[layer] = CalculateBiasGradientForLayer(layer, activationsGrad[layer]);
                            weightGrad[layer] = CalculateWeightGradientForLayer(layer, biasesGrad[layer]);
                        }

                        //for (int i = 0; i < weightGrad.Length; i++)
                        //{
                        //    weightGradChange[i] += weightGrad[i];
                        //    biasesGradChange[i] += biasesGrad[i];
                        //}

                        //lock (syncLocks[1])
                        for (int i = 1; i < weightGrad.Length; i++)
                        {
                            lock (weightGradChange[i])
                                weightGradChange[i] += weightGrad[i];
                        }

                        //lock (syncLocks[2])
                        for (int i = 1; i < weightGrad.Length; i++)
                        {
                            lock (biasesGradChange[i])
                                biasesGradChange[i] += biasesGrad[i];
                        }

                    });

                    averageDiff /= n;

                    for (int i = 1; i < weightGradChange.Length; i++)
                    {
                        weightGradChange[i] = 1d / n * weightGradChange[i];
                        biasesGradChange[i] = 1d / n * biasesGradChange[i];
                    }

                    for (int w = 1; w < neuronCounts.Length; w++)
                    {
                        double gradientVelocity = GradientStepStrategy.GetStepSize(averageDiff);
                        weights[w] = weights[w] - gradientVelocity * weightGradChange[w];
                        biases[w] = biases[w] - gradientVelocity * biasesGradChange[w];
                    }
                    Console.WriteLine($"Batch {b + 1} completed");
                }
            }
        }
        (MatrixAT1[], MatrixAT1[], MatrixAT1[]) InitialiseComponents()
        {
            MatrixAT1[] weightGrad = new MatrixAT1[neuronCounts.Length];
            MatrixAT1[] biasGrad = new MatrixAT1[neuronCounts.Length];
            for (int i = 1; i < weightGrad.Length; i++)
            {
                weightGrad[i] = new MatrixAT1(weights[i].Rows, weights[i].Columns);
                biasGrad[i] = new MatrixAT1(biases[i].Rows, biases[i].Columns);
            }

            MatrixAT1[] actGrad = new MatrixAT1[neuronCounts.Length];
            for (int i = 1; i < actGrad.Length; i++)
            {
                actGrad[i] = new MatrixAT1(ases[i].Rows, ases[i].Columns);
            }

            return (weightGrad, biasGrad, actGrad);
        }

        MatrixAT1 CalculateLastLayerLossGradient(MatrixAT1 current, MatrixAT1 expected)
        {
            if (!MatrixAT1.CheckSameDimensions(current, expected))
                throw new ArgumentException($"Network output has wrong dimensions {current.Rows}x{current.Columns} (expected {expected.Rows}x{expected.Columns})");
            
            MatrixAT1 ag_L = new MatrixAT1(current.Rows, 1);

            for (int i = 0; i < ag_L.Rows; i++)
            {
                ag_L[i, 0] = LossFunction.ComputeDerivative(current[i, 0], expected[i, 0]);
            }

            return ag_L;
        }

        //TODO - B - Nomenclature of functions
        MatrixAT1 CalculateActivationGradient(int layer, MatrixAT1 biasGradLplus1)
        {
            MatrixAT1 weightsLplus1 = weights[layer+1];
            MatrixAT1 ag_L = new MatrixAT1(neuronCounts[layer], 1);

            for (int k = 0; k < ag_L.Rows; k++)
            {
                double sum = 0;
                for (int j = 0; j < biasGradLplus1.Rows; j++)
                {
                    sum += weightsLplus1[j, k] * biasGradLplus1[j, 0];
                }
                ag_L[k, 0] = sum;
            }

            return ag_L;
        }

        MatrixAT1 CalculateBiasGradientForLayer(int layer, MatrixAT1 ag_L)
        {
            MatrixAT1 bg_L = new MatrixAT1(neuronCounts[layer], 1);

            var actF = activationFuncs[layer];

            for (int j = 0; j < bg_L.Rows; j++)
            {
                bg_L[j, 0] = actF.ComputeDerivative(zs[layer][j, 0]) * ag_L[j, 0];
            }

            return bg_L;
        }

        MatrixAT1 CalculateWeightGradientForLayer(int layer, MatrixAT1 bg_L)
        {
            MatrixAT1 a_Lminus1 = ases[layer-1];
            MatrixAT1 wg_L = new MatrixAT1(bg_L.Rows, a_Lminus1.Rows);

            for (int j = 0; j < wg_L.Rows; j++)
            {
                for (int k = 0; k < wg_L.Columns; k++)
                {
                    wg_L[j, k] = a_Lminus1[k, 0] * bg_L[j, 0];
                }
            }

            return wg_L;
        }
        //TODO - B - works only for output in 0/1 range
        public override double[] Test(double[][] inputs, double[][] outputs, string path = null)
        {
            //TODO - B - validation
            //TODO - B - test tc
            //TODO - B - add precision and recall
            int len = outputs[0].Length;
            MatrixAT1 confusionMatrix = new MatrixAT1(len, len);

            for (int i = 0; i < inputs.Length; i++)
            {
                double[] result = Run(inputs[i]);
                int predClass = 0;
                int actualClass = -1;
                for (int j = 0; j < result.Length; j++)
                {
                    if(result[j] > result[predClass])
                    {
                        predClass = j;
                    }

                    if(outputs[i][j] == 1)
                    {
                        actualClass = j;
                    }
                }
                confusionMatrix[actualClass, predClass]++;
            }
            Console.WriteLine("Test finished");

            if(path != null)
            {
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(confusionMatrix.ToString());
                sw.Flush();
                sw.Close();
            }

            double accuracy = 0;
            double sumMat = 0;
            for (int r = 0; r < len; r++)
            {
                for (int c = 0; c < len; c++)
                {
                    if (r == c)
                        accuracy += confusionMatrix[r, c];
                    sumMat += confusionMatrix[r, c];
                }
            }

            accuracy /= sumMat;

            return new double[] { accuracy };
        }

        public override void ModelToFile(string path)
        {
            //TODO - B - test
            StreamWriter sw = new StreamWriter(path);

            for (int i = 1; i < neuronCounts.Length; i++)
            {
                sw.WriteLine("weights " + i);
                sw.WriteLine(weights[i].ToString());
                sw.WriteLine("biases " + i);
                sw.WriteLine(biases[i].ToString());
                sw.Flush();
            }
            sw.Flush();
            sw.Close();
        }
    }
}
