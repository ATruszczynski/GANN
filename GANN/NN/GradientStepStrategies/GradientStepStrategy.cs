using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public abstract class GradientStepStrategy : IComparable
    {
        //TODO - A - what parameters should that take?
        public abstract double GetStepSize(double avDiff);
        public abstract GradientStepStrategy DeepCopy();
        public abstract int CompareTo(object obj);
        public abstract override string ToString();
    }
}
