using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN
{
    class ANN : NeuralNetwork
    {
        //TODO - D - remove needless pubilc
        public MatrixAT1[] weights;
        public MatrixAT1[] biases;
        public Func<double,double>[] activationFuncs;
        public Func<double,double>[] derActivationFuncs;
        public Func<double, double, double> lossFunction;
        public Func<double, double, double> derLossFunction;
        public MatrixAT1[] zs;
        public MatrixAT1[] ases;
        public int[] neuronCounts;
        public int LayerCount { get => neuronCounts.Length; }

        //TODO - B - custom edges
        //TODO - A - is this suitable for GA
        public ANN(int[] neurons, Func<double, double>[] actFunc, Func<double, double>[] derActFunc, Func<double, double, double> lossF, Func<double, double, double> derLossF)
        {
            neuronCounts = new int[neurons.Length];
            for (int i = 0; i < neurons.Length; i++)
            {
                neuronCounts[i] = neurons[i];
            }

            if(actFunc.Length != LayerCount - 1)
            {
                throw new ArgumentException($"There is a wrong amount of activation function {actFunc.Length}, instead of {LayerCount - 1}");
            }

            if (derActFunc.Length != LayerCount - 1)
            {
                throw new ArgumentException($"There is a wrong amount of derivations of activation functions {derActFunc.Length}, instead of {LayerCount - 1}");
            }

            activationFuncs = actFunc;
            derActivationFuncs = derActFunc;

            weights = new MatrixAT1[LayerCount - 1];
            biases = new MatrixAT1[LayerCount - 1];

            lossFunction = lossF;
            derLossFunction = derLossF;

            zs = new MatrixAT1[LayerCount];
            ases = new MatrixAT1[LayerCount];
        }

        public override double[] Run(double[] input)
        {
            if (input.Length != neuronCounts[0])
                throw new ArgumentException($"Wrong numbers of arguments in input - {input.Length} (expected {neuronCounts[0]})");

            ases[0] = new MatrixAT1(input);
            zs[0] = new MatrixAT1(input);

            for (int layer = 1; layer < LayerCount; layer++)
            {
                zs[layer] = new MatrixAT1(neuronCounts[layer], 1);
                ases[layer] = new MatrixAT1(neuronCounts[layer], 1);


                MatrixAT1 a_Lminus1 = ases[layer - 1];

                zs[layer] = weights[layer - 1] * a_Lminus1 + biases[layer - 1];

                for (int j = 0; j < neuronCounts[layer]; j++)
                {
                    ases[layer][j, 1] = activationFuncs[layer - 1](zs[layer][j, 1]); 
                }
            }

            double[] result = new double[neuronCounts[LayerCount - 1]]; //źle?

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = ases[LayerCount - 1][i, 1];
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
                    (MatrixAT1[] weightGrad, MatrixAT1[] biasesGrad, MatrixAT1[] activationsGrad) = InitialiseComponents();

                    int startIndInc = b * batchSize;
                    int endIndExc = Min(inputs.Length, (b + 1) * batchSize);
                    int n = endIndExc - startIndInc;

                    for (int inputInd = startIndInc; inputInd < endIndExc; inputInd++)
                    {
                        for (int layer = LayerCount - 1; layer >= 1; layer--)
                        {
                            if(layer == LayerCount - 1)
                            {
                                MatrixAT1 currOutput = new MatrixAT1(Run(inputs[inputInd]));

                                MatrixAT1 goodOutput = new MatrixAT1(outputs[inputInd]);

                                activationsGrad[layer] = CalculateLastLayerLossGradient(currOutput, goodOutput);

                            }
                            else
                            {
                                activationsGrad[layer] = CalculateActivationGradient(layer);
                            }

                            biasesGrad[layer - 1] = CalculateBiasGradientForLayer(layer, activationsGrad[layer]);
                            weightGrad[layer - 1] = CalculateWeightGradientForLayer(layer, biasesGrad[layer - 1]);
                        }
                    }
                }
            }

            throw new NotImplementedException();
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
            MatrixAT1 ag_L = new MatrixAT1(current.Rows, 1);

            for (int i = 0; i < ag_L.Rows; i++)
            {
                ag_L[i, 1] = derLossFunction(current[i, 1], expected[i, 1]);
            }

            return ag_L;
        }

        //TODO - B - Nomenclature of functions
        MatrixAT1 CalculateActivationGradient(int layer)
        {
            MatrixAT1 weightsLplus1 = weights[layer];
            MatrixAT1 biasLplus1 = biases[layer];
            MatrixAT1 ag_L = new MatrixAT1(neuronCounts[layer], 1);

            for (int k = 0; k < ag_L.Rows; k++)
            {
                double sum = 0;
                for (int j = 0; j < biasLplus1.Rows; j++)
                {
                    sum += weightsLplus1[j, k] * biasLplus1[j, 1];
                }
                ag_L[k, 1] = sum;
            }

            return ag_L;
        }

        MatrixAT1 CalculateBiasGradientForLayer(int layer, MatrixAT1 ag_L)
        {
            MatrixAT1 bg_L = new MatrixAT1(neuronCounts[layer - 1], 1);

            MatrixAT1 z_L = zs[layer];

            var derAct = derActivationFuncs[layer - 1];

            for (int j = 0; j < bg_L.Rows; j++)
            {
                bg_L[j, 1] = derAct(z_L[j, 1]) * ag_L[j, 1];
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
                    wg_L[j, k] = a_Lminus1[k, 1] * bg_L[j, 1];
                }
            }

            return wg_L;
        }
    }
}
