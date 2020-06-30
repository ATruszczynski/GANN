using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class DiscreteRange: Range
    {
        //TODO - C - those ranges are horrendous
        public int Min { get => RangeGenerator.Min; }
        public int Max { get => RangeGenerator.Max; }
        public DiscreteRangeDistribution RangeGenerator;
        public DiscreteRange(DiscreteRangeDistribution rangeGenerator)
        {
            RangeGenerator = rangeGenerator;
        }

        public override bool IsInRange(object value)
        {
            //TODO - B - test
            int val = (int)value;
            return Min <= val && val < Max;
        }

        public override object GetNext()
        {
            return RangeGenerator.GetNext();
        }
    }
}
