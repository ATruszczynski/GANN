using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ActivationFunctions
{
    public abstract class ActivationFunction: IComparable
    {
        public abstract int CompareTo(object obj);
        public abstract double Compute(double arg);
        public abstract double ComputeDerivative(double arg);
        public abstract ActivationFunction DeepCopy();
    }
}
