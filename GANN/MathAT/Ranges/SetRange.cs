using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class SetRange<T> : Range
    {
        public DiscreteDistribution Distribution;
        public T[] Values;

        public SetRange(T[] values, DiscreteDistribution rangeGenerator)
        {
            Distribution = rangeGenerator;
            Values = values;
        }

        //TODO - B - test

        public override object GetNext()
        {
            return Values[(int)Distribution.GetNext()];
            throw new NotImplementedException();
        }

        public override bool IsInRange(object value)
        {
            //TODO - A - implement
            throw new NotImplementedException();
        }
    }
}
