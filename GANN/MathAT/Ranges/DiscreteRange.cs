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
            //TODO - A - this should fail for doubles not natural
            double vald = Utility.TryCastToDouble(value);
            if (Math.Ceiling(vald) != vald && Math.Floor(vald) != vald)
                return false;
            int val = Utility.TryCastToInt(value);
            return Min <= val && val < Max;
        }

        public override object GetNext()
        {
            return (int)RangeGenerator.GetNext();
        }

        public override object GetNeighbour(object obj)
        {
            //TODO - B - test
            var val = Utility.TryCastToInt(obj);
            int radius = (int)Math.Ceiling((Max - Min) / neighbourTol);

            return Utility.NeighbourOnCircleDisc(val, radius, Max, Min, RangeGenerator.Random);
        }
    }
}
