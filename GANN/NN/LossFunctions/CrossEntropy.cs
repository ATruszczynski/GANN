using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN.LossFunctions
{
    public class CrossEntropy : LossFunction
    {
        public override int CompareTo(object obj)
        {
            try
            {
                CrossEntropy e = (CrossEntropy)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return 0;
        }

        public override double Compute(MatrixAT1 output, MatrixAT1 expected)
        {
            double ce = 0;

            for (int i = 0; i < output.Rows; i++)
            {
                double e = expected[i, 0];
                double o = output[i, 0];
                ce += e * Log(o);
            }

            return -ce;
        }

        public override double ComputeDerivative(double output, double expected)
        {
            double der = -expected/output;
            if (double.IsNaN(der))
                throw new ArgumentException("Nan value");
            return der;
        }

        public override LossFunction DeepCopy()
        {
            return new CrossEntropy();
        }

        public override string ToString()
        {
            return "CE";
        }
    }
}
