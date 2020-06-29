using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class Utility
    {
        //TODO - B - test
        public static int Roulette(double[] values, Random random)
        {
            double p = random.NextDouble();

            int ind = 0;
            for (ind = 0; ind < values.Length; ind++)
            {
                if (p < values[ind])
                    break;
            }

            return ind;
        }

        public static double[] NormalisedCumulativeSum(double[] values, double offset = 0)
        {
            double[] result = new double[values.Length];
            result[0] = values[0] + offset;

            for (int i = 1; i < result.Length; i++)
            {
                result[i] = result[i - 1] + values[i] + offset;
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= result[result.Length - 1];
            }

            return result;
        }

        public static int[] ArrayZeroToValue(int valueEx)
        {
            int[] result = new int[valueEx];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = i;
            }

            return result;
        }
    }
}
