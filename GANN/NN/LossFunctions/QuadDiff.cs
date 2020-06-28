using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN.LossFunctions
{
    public class QuadDiff : LossFunction
    {
        //TODO - B - use vector class
        public double d;
        public QuadDiff(double dd = 1)
        {
            d = dd;
        }
        public override double Compute(MatrixAT1 m1, MatrixAT1 m2)
        {
            //TODO - B - test
            double sum = 0;

            for (int i = 0; i < m1.Rows; i++)
            {
                sum += Pow(m1[i,0] - m2[i,0], 2);
            }

            return d*sum;
        }

        public override double ComputeDerivative(double output, double expected)
        {
            //TODO - B - test
            return 2 * d * (output - expected);
        }
    }
}
