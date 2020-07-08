using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    class MomentumStrategy : GradientStepStrategy
    {
        public override int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public override GradientStepStrategy DeepCopy()
        {
            throw new NotImplementedException();
        }

        public override (MatrixAT1[], MatrixAT1[]) GetStepSize(double avDiff, MatrixAT1[] updateW, MatrixAT1[] updateB)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
