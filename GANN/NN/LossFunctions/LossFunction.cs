using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.LossFunctions
{
    public abstract class LossFunction : IComparable
    {
        public abstract int CompareTo(object obj);
        public abstract double Compute(MatrixAT1 output, MatrixAT1 expected);
        public abstract double ComputeDerivative(double output, double expected);
        public abstract LossFunction DeepCopy();
        public abstract override string ToString();
    }
}
