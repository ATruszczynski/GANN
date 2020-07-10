using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;
using GANN.MathAT;

namespace GANN.MathAT.Ranges
{
    public class ContinuousRange: Range
    {
        public double Min { get => RangeGenerator.Min; }
        public double Max { get => RangeGenerator.Max; }
        public ContinuousRangeDistribution RangeGenerator;
        //TODO - B - GetNeighbour is uniform only!
        public ContinuousRange(ContinuousRangeDistribution rangeGenerator)
        {
            RangeGenerator = rangeGenerator;
        }

        public override bool IsInRange(object value)
        {
            double val = Utility.TryCastToDouble(value);

            return Min <= val && val < Max;
        }

        public override object GetNext()
        {
            return RangeGenerator.GetNext();
        }

        public override object GetNeighbour(object obj)
        {
            //TODO - A - test
            double val = Utility.TryCastToDouble(obj);
            double neighRadius = neighbourTol * (Max - Min);

            return Utility.NeighbourOnCircleCont(val, neighRadius, Max, Min, RangeGenerator.Random);
        }
    }
}
