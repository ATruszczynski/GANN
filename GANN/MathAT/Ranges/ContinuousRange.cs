using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class ContinuousRange: Range
    {
        //TODO - B - test
        public double Min { get => RangeGenerator.Min; }
        public double Max { get => RangeGenerator.Max; }
        public ContinuousRangeDistribution RangeGenerator;
        public ContinuousRange(ContinuousRangeDistribution rangeGenerator)
        {
            RangeGenerator = rangeGenerator;
        }

        public override bool IsInRange(object value)
        {
            double val = (double)value;

            return Min <= val && val < Max;
        }
    }
}
