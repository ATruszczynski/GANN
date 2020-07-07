using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN.ActivationFunctions
{
    public class Sigma : ActivationFunction
    {
        public override ActivationFunction DeepCopy()
        {
            return new Sigma();
        }
        public override int CompareTo(object obj)
        {
            Sigma tmp;
            try
            {
                tmp = (Sigma)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return 0;
        }

        double InternalCompute(double arg)
        {
            return 1d / (1 + Exp(-arg));
        }

        public override double Compute(int ind, MatrixAT1 zs)
        {
            return InternalCompute(zs[ind, 0]);
        }

        public override double ComputeDerivative(int ind, MatrixAT1 zs)
        {
            double arg = zs[ind, 0];
            return InternalCompute(arg) * (1 - InternalCompute(arg));
        }

        public override string ToString()
        {
            return "SI";
        }
    }
}
