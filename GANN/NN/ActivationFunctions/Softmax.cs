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
            //TODO - A - test
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
            //TODO - A - test
            var zs2 = zs.DeepCopy();
            double max = double.MinValue;
            for (int i = 0; i < zs2.Rows; i++)
            {
                if (zs2[i, 0] > max)
                    max = zs2[i, 0];
            }

            for (int i = 0; i < zs2.Rows; i++)
            {
                zs2[i, 0] -= max;
            }

            double z_i = Exp(zs2[ind, 0]);
            double sum = 0;

            for (int k = 0; k < zs2.Rows; k++)
            {
                double z_k = zs2[k, 0];
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
            //TODO - A - test
            return new Softmax();
        }

        public override string ToString()
        {
            return "SM";
        }
    }
}
