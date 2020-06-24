using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN
{
    class ANN : NeuralNetwork
    {
        //TODO consistent nomenclature of layers and its count (does first layer count as layer?)
        //TODO layer count variable?
        //TODO arrays instead of lists
        //TODO clean up mess with layers count
        List<MatrixAT1> weights;
        List<MatrixAT1> biases;
        List<Func<double,double>> activationFuncs; 
        List<Func<double,double>> derActivationFuncs;
        Func<double, double, double> lossFunction;
        Func<double, double, double> derLossFunction;
        List<MatrixAT1> zs;
        List<MatrixAT1> ases;

        public ANN(List<int> neurons, List<Func<double, double>> actFunc, List<Func<double, double>> derActFunc)
        {
            //TODO implement
            throw new NotImplementedException();
        }

        public override List<double> Run(List<double> input)
        {
            for (int i = 0; i < weights.Count; i++)
            {
                zs[i] = new MatrixAT1(biases[i].Rows, 1);
                ases[i] = new MatrixAT1(biases[i].Rows, 1);

                MatrixAT1 a_Lminus1;
                if (i == 0)
                {
                    a_Lminus1 = new MatrixAT1(input.ToArray());
                    a_Lminus1.Transpose(); //TODO add arguemnt to constructor to avoid that
                }
                else
                    a_Lminus1 = ases[i - 1];

                var weight = weights[i];

                zs[i] = weight * a_Lminus1 + biases[i];

                for (int j = 0; j < zs[i].Rows; j++)
                {
                    ases[i][j, 1] = activationFuncs[i](zs[i][j, 1]); 
                }
            }

            List<double> resutl = new List<double>();

            for (int i = 0; i < ases[weights.Count - 1].Rows; i++)
            {
                resutl.Add(ases[weights.Count - 1][i, 1]);
            }

            return resutl;
        }

        public override void Train(List<List<double>> inputs, List<List<double>> outputs, int epochs, int batchSize)
        {
            double batches = Ceiling((double)inputs.Count / (double)batchSize);
            for (int e = 0; e < epochs; e++)
            {
                for (int b = 0; b < batches; b++)
                {
                    (MatrixAT1[] weightGrad, MatrixAT1[] biasesGrad, MatrixAT1[] activationsGrad) = InitialiseComponents();

                    int startIndInc = b * batchSize;
                    int endIndExc = Min(inputs.Count, (b + 1) * batchSize);
                    int n = endIndExc - startIndInc;

                    for (int inputInd = startIndInc; inputInd < endIndExc; inputInd++)
                    {
                        for (int layer = weights.Count - 1; layer >= 1; layer--)
                        {
                            if(layer == weights.Count - 1)
                            {
                                MatrixAT1 currOutput = new MatrixAT1(Run(inputs[inputInd]).ToArray());
                                currOutput.Transpose();

                                MatrixAT1 goodOutput = new MatrixAT1(outputs[inputInd].ToArray());
                                goodOutput.Transpose();

                                activationsGrad[layer] = CalculateLastLayerLossGradient(currOutput, goodOutput);

                            }
                            else
                            {

                            }

                            biasesGrad[layer] = CalculateBiasGradientForLayer(layer, activationsGrad[layer]);
                            weightGrad[layer] = CalculateWeightGradientForLayer(layer, biasesGrad[layer]);
                        }
                    }
                }
            }

            throw new NotImplementedException();
        }
        //TODO Differentiate between gradients and values in argument names
        (MatrixAT1[], MatrixAT1[], MatrixAT1[]) InitialiseComponents()
        {
            MatrixAT1[] weightGrad = new MatrixAT1[weights.Count];
            for (int i = 0; i < weightGrad.Length; i++)
            {
                weightGrad[i] = new MatrixAT1(weights[i].Rows, weights[i].Columns);
            }

            MatrixAT1[] biasGrad = new MatrixAT1[biases.Count];
            for (int i = 0; i < biasGrad.Length; i++)
            {
                biasGrad[i] = new MatrixAT1(biases[i].Rows, biases[i].Columns);
            }

            MatrixAT1[] acts = new MatrixAT1[activationFuncs.Count];
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i] = new MatrixAT1(acts.Length, 1);
            }

            return (weightGrad, biasGrad, acts);
        }

        MatrixAT1 CalculateLastLayerLossGradient(MatrixAT1 current, MatrixAT1 demanded)
        {
            MatrixAT1 a_l = new MatrixAT1(current.Rows, 1);

            for (int i = 0; i < a_l.Rows; i++)
            {
                a_l[i, 1] = derLossFunction(current[i, 1], demanded[i, 1]);
            }

            return a_l;
        }

        //TODO Nomenclature of functions
        MatrixAT1 CalculateActivationGradient(int layer, MatrixAT1 biasLplus1)
        {
            MatrixAT1 outWeights = weights[layer + 1];
            MatrixAT1 outBiases = biases[layer + 1];
            MatrixAT1 a_L = new MatrixAT1(biases[layer].Rows, 1);

            for (int k = 0; k < a_L.Rows; k++)
            {
                double sum = 0;
                for (int j = 0; j < biasLplus1.Rows; j++)
                {
                    sum += outWeights[j, k] * biasLplus1[j, 1];
                }
                a_L[k, 1] = sum;
            }

            return a_L;
        }

        //TODO explicit layer counts would be more readable?
        MatrixAT1 CalculateBiasGradientForLayer(int layer, MatrixAT1 a_L)
        {
            MatrixAT1 b_L = new MatrixAT1(a_L.Rows, 1);

            MatrixAT1 z_L = zs[layer];

            var derAct = derActivationFuncs[layer];

            for (int j = 0; j < a_L.Rows; j++)
            {
                b_L[j, 1] = derAct(z_L[j, 1]) * a_L[j, 1];
            }

            return b_L;
        }

        MatrixAT1 CalculateWeightGradientForLayer(int layer, MatrixAT1 b_L)
        {
            MatrixAT1 a_Lminus1 = ases[layer - 1];
            MatrixAT1 w_L = new MatrixAT1(b_L.Rows, a_Lminus1.Rows);

            for (int j = 0; j < w_L.Rows; j++)
            {
                for (int k = 0; k < w_L.Columns; k++)
                {
                    w_L[j, k] = a_Lminus1[k, 1] * b_L[j, 1];
                }
            }

            return w_L;
        }
    }
}
