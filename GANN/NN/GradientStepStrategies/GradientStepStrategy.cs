using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public abstract class GradientStepStrategy : IComparable
    {
        //TODO - A - what parameters should that take?
        public abstract (MatrixAT1[], MatrixAT1[]) GetStepSize(double avDiff, MatrixAT1[] updateW, MatrixAT1[] updateB);
        public abstract GradientStepStrategy DeepCopy();
        public abstract int CompareTo(object obj);
        public abstract override string ToString();
    }
}
