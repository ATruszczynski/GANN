using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    class PseudoRandom: Random
    {
        //TODO - D - kinda stupid implementation
        List<double> cycleList;
        int ind = 0;
        public PseudoRandom(List<double> values)
        {
            cycleList = new List<double>();
            for (int i = 0; i < values.Count; i++)
            {
                cycleList.Add(values[i]);
            }
        }

        public override int Next(int minValue, int maxValue)
        {
            return Next();
        }

        public override int Next(int maxValue)
        {
            return Next();
        }

        public override int Next()
        {
            return (int)NextDouble();
        }

        public override double NextDouble()
        {
            ind = (ind + 1) % cycleList.Count;
            return cycleList[ind];
        }
    }
}
