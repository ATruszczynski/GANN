using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public class DecayingGradientStep : GradientStepStrategy
    {
        public double Starting;
        public double DecayRate;
        public double Current;
        public DecayingGradientStep(double starting = 1, double decayPerc = 0.001)
        {
            Starting = starting;
            Current = starting;
            DecayRate = decayPerc;
        }
        public override int CompareTo(object obj)
        {
            //TODO - B - what should it return on different Current
            DecayingGradientStep res;
            try
            {
                res = (DecayingGradientStep)obj;
            }
            catch
            {
                return int.MinValue;
            }

            if (res.DecayRate != DecayRate)
                return int.MinValue;
            if (res.Starting != Starting)
                return int.MinValue;

            return 0;
        }

        public override GradientStepStrategy DeepCopy()
        {
            return new DecayingGradientStep(Starting, DecayRate);
        }

        public override double GetStepSize(double avDiff)
        {
            double result = Current;
            Current *= (1 - DecayRate);
            return result;
        }

        public override string ToString()
        {
            return $"DS{Starting}-{DecayRate}";
        }
    }
}
