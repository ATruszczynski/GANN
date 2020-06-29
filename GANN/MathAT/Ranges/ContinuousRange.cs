using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class ContinuousRange: Range
    {
        //TODO - B - test
        public double Min;
        public double Max;

        public ContinuousRange(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public override bool IsInRange(object value)
        {
            double val = (double)value;

            return Min <= val && val < Max;
        }
    }
}
