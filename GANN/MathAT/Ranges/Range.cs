using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public abstract class Range
    {
        public abstract bool IsInRange(object value);
        public abstract object GetNext();
    }
}
