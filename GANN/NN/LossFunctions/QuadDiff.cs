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
        public override double Compute(MatrixAT1 output, MatrixAT1 expected)
        {
            double sum = 0;

            for (int i = 0; i < output.Rows; i++)
            {
                sum += Pow(output[i,0] - expected[i,0], 2);
            }

            return d*sum;
        }

        public override double ComputeDerivative(double output, double expected)
        {
            return 2 * d * (output - expected);
        }

        public override LossFunction DeepCopy()
        {
            return new QuadDiff(d);
        }

        public override int CompareTo(object obj)
        {
            QuadDiff tmp;
            try
            {
                tmp = (QuadDiff)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return tmp.d.CompareTo(d);
        }
    }
}
