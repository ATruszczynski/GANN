using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ActivationFunctions
{
    public abstract class ActivationFunction: IComparable
    {
        public abstract int CompareTo(object obj);
        public abstract double Compute(int ind, MatrixAT1 zs);
        public abstract double ComputeDerivative(int ind, MatrixAT1 zs);
        public abstract ActivationFunction DeepCopy();
    }
}
