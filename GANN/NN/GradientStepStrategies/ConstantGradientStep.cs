using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public class ConstantGradientStep : GradientStepStrategy
    {
        public double stepSize = 1;

        public ConstantGradientStep(double ss = 1)
        {
            stepSize = ss;
        }

        public override GradientStepStrategy DeepCopy()
        {
            return new ConstantGradientStep(stepSize);
        }

        public override double GetStepSize(double avDiff)
        {
            return stepSize;
        }
        public override int CompareTo(object obj)
        {
            ConstantGradientStep tmp;
            try
            {
                tmp = (ConstantGradientStep)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return tmp.stepSize.CompareTo(stepSize);
        }
    }
}
