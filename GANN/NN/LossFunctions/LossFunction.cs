using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.LossFunctions
{
    public abstract class LossFunction
    {
        //TODO - A - reimplement loss
        public abstract double Compute(MatrixAT1 m1, MatrixAT1 m2);
        public abstract double ComputeDerivative(double output, double expected);
    }
}
