using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class SetRange<T>: Range where T : IComparable
    {
        public DiscreteDistribution Distribution;
        public T[] Values;

        public SetRange(T[] values, DiscreteDistribution rangeGenerator)
        {
            Distribution = rangeGenerator;
            Values = values;
        }

        public override object GetNext()
        {
            return Values[(int)Distribution.GetNext()];
        }

        public override bool IsInRange(object value)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i].CompareTo(value) == 0)
                    return true;
            }
            return false;
        }
    }
}
