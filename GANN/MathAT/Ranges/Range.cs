using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public abstract class Range
    {
        public double neighbourTol = 0.1;
        public abstract bool IsInRange(object value);
        public abstract object GetNext();
        public abstract object GetNeighbour(object obj);
    }
}
