using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public class ConstantGradientStep : GradientStepStrategy
    {
        public double stepSize = 1;

        public ConstantGradientStep(double ss)
        {
            stepSize = ss;
        }

        public override GradientStepStrategy DeepCopy()
        {
            //TODO - B - test
            return new ConstantGradientStep(stepSize);
        }

        public override double GetStepSize(double avDiff)
        {
            //TODO - B - test
            return stepSize;
        }
    }
}
