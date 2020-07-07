using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ActivationFunctions
{
    public class Relu : ActivationFunction
    {
        public override ActivationFunction DeepCopy()
        {
            return new Relu();
        }
        public override int CompareTo(object obj)
        {
            Relu tmp;
            try
            {
                tmp = (Relu)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return 0;
        }

        public override double Compute(int ind, MatrixAT1 zs)
        {
            return Math.Max(0, zs[ind,0]);
        }

        public override double ComputeDerivative(int ind, MatrixAT1 zs)
        {
            double result = 0;

            if (zs[ind, 0] > 0)
                result = 1;

            return result;
        }

        public override string ToString()
        {
            return "RL";
        }
    }
}
