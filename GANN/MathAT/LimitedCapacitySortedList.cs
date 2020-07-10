using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class LimitedCapacitySortedList<T>
    {
        List<(double, T)> list;
        int Max;

        public LimitedCapacitySortedList(int max)
        {
            list = new List<(double, T)>();
            Max = max;
        }

        public void Add(double score, T add)
        {
            //TODO - A - test
            list.Add((score, add));
            list.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            while (list.Count > Max)
                list.RemoveAt(list.Count - 1);
        }

        public List<T> ExtractList()
        {
            //TODO - A - test
            var result = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(list[i].Item2);
            }
            return result;
        }
    }
}
