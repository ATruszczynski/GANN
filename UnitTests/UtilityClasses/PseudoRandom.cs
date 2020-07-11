using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class PseudoRandom: Random
    {
        //TODO - D - kinda stupid implementation
        double[] cycleList;
        int ind = 0;
        //public PseudoRandom(double[] values)
        //{
        //    cycleList = new double[values.Length];
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        cycleList[i] = values[i];
        //    }
        //}

        public PseudoRandom(params double[] values)
        {
            cycleList = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                cycleList[i] = values[i];
            }
        }

        public override int Next(int minValue, int maxValue)
        {
            int v = Next();
            if (v < minValue || v >= maxValue)
                throw new ArgumentException("Not in range");
            return v;
        }

        public override int Next(int maxValue)
        {
            int v = Next();
            if (v >= maxValue)
                throw new ArgumentException("Not in range");
            return v;
        }

        public override int Next()
        {
            return (int)InternalNext();
        }

        public override double NextDouble()
        {
            return InternalNext(true);
        }

        double InternalNext(bool isFloat = false)
        {
            //TODO - B - test not in ranges
            double result = cycleList[ind];
            if (isFloat && (result < 0 || result >= 1))
                throw new ArgumentException("Not in range");
            ind = (ind + 1) % cycleList.Length;
            return result;
        }
    }
}
