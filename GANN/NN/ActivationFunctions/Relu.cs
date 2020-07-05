using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ActivationFunctions
{
    public class Relu : ActivationFunction
    {

        public override double Compute(double arg)
        {
            return Math.Max(0, arg);
        }

        public override double ComputeDerivative(double arg)
        {
            double result = 0;

            if (arg > 0)
                result = 1;

            return result;
        }

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
    }
}
