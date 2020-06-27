using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ActivationFunctions
{
    public abstract class ActivationFunction
    {
        //TODO - A - reimplement act func
        public abstract double Compute(double arg);
        public abstract double ComputeDerivative(double arg);
    }
}
