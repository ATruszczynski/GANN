using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
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
            double result = cycleList[ind];
            ind = (ind + 1) % cycleList.Length;
            return result;
        }
    }
}
