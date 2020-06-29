using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class DiscreteRange: Range
    {
        public int Min;
        public int Max;
        public DiscreteRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override bool IsInRange(object value)
        {
            //TODO - B - test
            int val = (int)value;
            return Min <= val && val < Max;
        }
    }
}
