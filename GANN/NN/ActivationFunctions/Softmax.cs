using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        public override double ComputeDerivative(int ind, MatrixAT1 zs)
        {
            throw new NotImplementedException();
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
