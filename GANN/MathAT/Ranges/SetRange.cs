using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public class SetRange<T>: Range where T : IComparable, IDeepCopyable
    {
        public DiscreteDistribution Distribution;
        public T[] Values;

        public SetRange(T[] values, DiscreteDistribution rangeGenerator)
        {
            Distribution = rangeGenerator;
            Values = values;
        }

        public override object GetNeighbour(object obj)
        {
            if (Values.Length < 2)
                throw new ArgumentException($"Operation doesnt make sense with {Values.Length} elements to choose from");
            T val = (T)obj;
            int ind = 0;
            for (; ind < Values.Length; ind++)
            {
                if (Values[ind].CompareTo(val) == 0)
                    break;
            }

            int radius = (int)Math.Ceiling(Values.Length * neighbourTol);

            return Values[Utility.NeighbourOnCircleDisc(ind, radius, 0, Values.Length, Distribution.Random)];
        }

        public override object GetNext()
        {
            return Values[(int)Distribution.GetNext()].DeepCopy();
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
