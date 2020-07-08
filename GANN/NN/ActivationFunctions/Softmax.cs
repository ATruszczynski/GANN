using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN.ActivationFunctions
{
    public class Softmax : ActivationFunction
    {
        public override int CompareTo(object obj)
        {
            //TODO - B - test
            try
            {
                var sm = (Softmax)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return 0;
        }

        public override double Compute(int ind, MatrixAT1 zs)
        {
            double z_i = Exp(zs[ind, 0]);
            double sum = 0;

            for (int k = 0; k < zs.Rows; k++)
            {
                double z_k = zs[k, 0];
                sum += Exp(z_k);
            }

            return z_i/sum;
        }

        public override double ComputeDerivative(int ind, MatrixAT1 zs)
        {
            double val = Compute(ind, zs);
            return val * (1 - val);
        }

        public override ActivationFunction DeepCopy()
        {
            //TODO - B - test
            return new Softmax();
        }

        public override string ToString()
        {
            return "SM";
        }
    }
}
