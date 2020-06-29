using GANN.MathAT;
using GANN.MathAT.Distributions;
using GANN.NN.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.Other
{
    public class Layer
    {
        public MatrixAT1 weights;
        public MatrixAT1 biases;
        public ActivationFunction ActivationFunction;
        public MatrixAT1 zs;
        public int Count;
        public MatrixAT1 ases;

        public Layer(int counts) //basically empty layer. Used for input layer
        {
            Count = counts;
            ases = new MatrixAT1(Count, 1);
        }

        public Layer(int counts, int prevCount, ActivationFunction actFun, Distribution dist):this(counts)
        {
            //TODO - B - test
            weights = new MatrixAT1(Count, prevCount);
            biases = new MatrixAT1(Count, 1);
            ActivationFunction = actFun;

            for (int r = 0; r < weights.Rows; r++)
            {
                for (int c = 0; c < weights.Columns; c++)
                {
                    weights[r, c] = dist.GetNext();
                }
            }
        }
    }
}
