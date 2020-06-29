﻿using GANN.MathAT;
using GANN.MathAT.Distributions;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN
{
    public class ANN : NeuralNetwork
    {
        //TODO - D - remove needless pubilc
        public MatrixAT1[] weights;
        public MatrixAT1[] biases;
        public ActivationFunction[] activationFuncs;
        public LossFunction lossFunction;
        public MatrixAT1[] zs;
        public MatrixAT1[] ases;
        public int[] neuronCounts;
        //TODO - B - random as property
        public int LayerCount { get => neuronCounts.Length; }
        public GradientStepStrategy GradientStepStrategy;
        //TODO - B - alternative to Relu?
        //TODO - B - Add argument validation
        //TODO - B - custom edges
        //TODO - A - hyperparameters as only property
        public ANN(Hyperparameters hyperparameters, Random random = null)
        {
            //TODO - B - paramterize seeds
            Hyperparameters = hyperparameters;
            if (random == null)
                Random = new Random();
            else
                Random = random;
            var neurons = hyperparameters.neuronCounts;
            neuronCounts = new int[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
            {
                neuronCounts[i] = neurons[i];
            }

            var actFunc = hyperparameters.ActivationFunctions;
            if(actFunc.Length != LayerCount - 1)
            {
                throw new ArgumentException($"There is a wrong amount of activation function {actFunc.Length}, instead of {LayerCount - 1}");
            }

            activationFuncs = actFunc;

            if (random == null)
                random = new Random();

            GaussianDistribution gd = new GaussianDistribution(random, hyperparameters.meanW, hyperparameters.stdW);
            weights = new MatrixAT1[LayerCount - 1];
            for (int w = 0; w < weights.Length; w++)
            {
                weights[w] = new MatrixAT1(neuronCounts[w + 1], neuronCounts[w]);
                for (int r = 0; r < weights[w].Rows; r++)
                {
                    for (int c = 0; c < weights[w].Columns; c++)
                    {
                        weights[w][r, c] = gd.GetNext();
                    }
                }
            }

            zs = new MatrixAT1[LayerCount - 1];
            biases = new MatrixAT1[LayerCount - 1];
            for (int b = 0; b < biases.Length; b++)
            {
                biases[b] = new MatrixAT1(neuronCounts[b + 1], 1);
                zs[b] = new MatrixAT1(neuronCounts[b + 1], 1);
            }

            lossFunction = hyperparameters.LossFunction;

            ases = new MatrixAT1[LayerCount];

            for (int a = 0; a < ases.Length; a++)
            {
                ases[a] = new MatrixAT1(neuronCounts[a], 1);
            }

            GradientStepStrategy = hyperparameters.GradientStepStrategy;
        }

        public override double[] Run(double[] input)
        {
            if (input.Length != neuronCounts[0])
                throw new ArgumentException($"Wrong numbers of arguments in input - {input.Length} (expected {neuronCounts[0]})");

            ases[0] = new MatrixAT1(input);

            for (int layer = 1; layer < LayerCount; layer++)
            {
                zs[layer - 1] = new MatrixAT1(neuronCounts[layer], 1);
                ases[layer] = new MatrixAT1(neuronCounts[layer], 1);


                MatrixAT1 a_Lminus1 = ases[layer - 1];

                zs[layer - 1] = weights[layer - 1] * a_Lminus1 + biases[layer - 1];

                for (int j = 0; j < neuronCounts[layer]; j++)
                {
                    ases[layer][j, 0] = activationFuncs[layer - 1].Compute(zs[layer - 1][j, 0]); 
                }
            }

            double[] result = new double[neuronCounts[LayerCount - 1]];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = ases[LayerCount - 1][i, 0];
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

                    for (int inputInd = startIndInc; inputInd < endIndExc; inputInd++)
                    {
                        (MatrixAT1[] weightGrad, MatrixAT1[] biasesGrad, MatrixAT1[] activationsGrad) = InitialiseComponents();
                        for (int layer = LayerCount - 1; layer >= 1; layer--)
                        {
                            if(layer == LayerCount - 1)
                            {
                                MatrixAT1 currOutput = new MatrixAT1(Run(inputs[inputInd]));

                                MatrixAT1 goodOutput = new MatrixAT1(outputs[inputInd]);

                                averageDiff += lossFunction.Compute(currOutput, goodOutput);

                                activationsGrad[layer] = CalculateLastLayerLossGradient(currOutput, goodOutput);
                                ;
                            }
                            else
                            {
                                activationsGrad[layer] = CalculateActivationGradient(layer, biasesGrad[layer]);
                            }

                            biasesGrad[layer - 1] = CalculateBiasGradientForLayer(layer, activationsGrad[layer]);
                            weightGrad[layer - 1] = CalculateWeightGradientForLayer(layer, biasesGrad[layer - 1]);
                        }

                        for (int i = 0; i < weightGrad.Length; i++)
                        {
                            weightGradChange[i] += weightGrad[i];
                            biasesGradChange[i] += biasesGrad[i];
                        }
                        
                    }
                    averageDiff /= n;
                    for (int i = 0; i < weightGradChange.Length; i++)
                    {
                        weightGradChange[i] = 1d / n * weightGradChange[i];
                        biasesGradChange[i] = 1d / n * biasesGradChange[i];
                    }

                    for (int w = 0; w < weights.Length; w++)
                    {
                        double gradientVelocity = GradientStepStrategy.GetStepSize(averageDiff);
                        weights[w] = weights[w] - gradientVelocity * weightGradChange[w];
                        biases[w] = biases[w] - gradientVelocity * biasesGradChange[w];
                    }

                    ;
                }
            }
        }
        (MatrixAT1[], MatrixAT1[], MatrixAT1[]) InitialiseComponents()
        {
            MatrixAT1[] weightGrad = new MatrixAT1[weights.Length];
            for (int i = 0; i < weightGrad.Length; i++)
            {
                weightGrad[i] = new MatrixAT1(weights[i].Rows, weights[i].Columns);
            }

            MatrixAT1[] biasGrad = new MatrixAT1[biases.Length];
            for (int i = 0; i < biasGrad.Length; i++)
            {
                biasGrad[i] = new MatrixAT1(biases[i].Rows, biases[i].Columns);
            }

            MatrixAT1[] actGrad = new MatrixAT1[ases.Length];
            for (int i = 0; i < actGrad.Length; i++)
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
                ag_L[i, 0] = lossFunction.ComputeDerivative(current[i, 0], expected[i, 0]);
            }

            return ag_L;
        }

        //TODO - B - Nomenclature of functions
        MatrixAT1 CalculateActivationGradient(int layer, MatrixAT1 biasLplus1)
        {
            MatrixAT1 weightsLplus1 = weights[layer];
            MatrixAT1 ag_L = new MatrixAT1(neuronCounts[layer], 1);

            for (int k = 0; k < ag_L.Rows; k++)
            {
                double sum = 0;
                for (int j = 0; j < biasLplus1.Rows; j++)
                {
                    sum += weightsLplus1[j, k] * biasLplus1[j, 0];
                }
                ag_L[k, 0] = sum;
            }

            return ag_L;
        }

        MatrixAT1 CalculateBiasGradientForLayer(int layer, MatrixAT1 ag_L)
        {
            MatrixAT1 bg_L = new MatrixAT1(neuronCounts[layer], 1);

            MatrixAT1 z_L = zs[layer - 1];

            var actF = activationFuncs[layer - 1];

            for (int j = 0; j < bg_L.Rows; j++)
            {
                bg_L[j, 0] = actF.ComputeDerivative(z_L[j, 0]) * ag_L[j, 0];
            }

            return bg_L;
        }

        MatrixAT1 CalculateWeightGradientForLayer(int layer, MatrixAT1 bg_L)
        {
            MatrixAT1 a_Lminus1 = ases[layer - 1];
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

        public override double[] Test(double[][] inputs, double[][] outputs)
        {
            throw new NotImplementedException();
        }
    }
}
