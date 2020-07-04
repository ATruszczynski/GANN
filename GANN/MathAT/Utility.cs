using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    //TODO - A - logger
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

        public static int HighestValueIndInArray(double[] array)
        {
            //TODO - B - test
            int result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > array[result])
                    result = i;
            }
            return result;
        }

        public static int[] ClassCounts(double[][] outputs)
        {
            //TODO - B - test
            int[] result = new int[outputs[0].Length];

            for (int i = 0; i < outputs.Length; i++)
            {
                result[HighestValueIndInArray(outputs[i])]++;
            }

            return result;
        }

        public static void WriteArary<T>(T[] array, char sep = ',')
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString();
                if (i != array.Length - 1)
                    result += sep;
            }
            Console.WriteLine(result);
        }

        public static double TryCastToDouble(object value)
        {
            //TODO - B - test
            double result = 0;

            try
            {
                result = (double)value;
            }
            catch
            {
                try
                {
                    result = (double)((int)value);
                }
                catch
                {
                    throw new ArgumentException($"Can't convert {value} to double");
                }
            }

            return result;
        }

        public static int TryCastToInt(object value)
        {
            //TODO - B - test
            int result;

            try
            {
                result = (int)value;
            }
            catch
            {
                try
                {
                    result = (int)((double)value);
                }
                catch
                {
                    //TODO - A - add gradient to mutation
                    throw new ArgumentException($"Can't convert {value} to integer");
                }
            }

            return result;
        }
    }
}
