using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    //TODO - A - logger
    public class Utility
    {
        public static int Roulette(double[] values, Random random)
        {
            //TODO - B - validation
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

        //public static int[] ArrayZeroToValue(int valueEx)
        //{
        //    int[] result = new int[valueEx];

        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        result[i] = i;
        //    }

        //    return result;
        //}

        public static int HighestValueIndInArray(double[] array)
        {
            //TODO - B - validation
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
            //TODO - B - validation
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
            double result = 0;

            if (value is int)
                result = (int)value;
            else if (value is float)
                result = (float)value;
            else if (value is decimal)
                result = (double)(decimal)value;
            else if (value is double)
                result = (double)value;
            else
                throw new ArgumentException($"Can't convert {value} to double");

            return result;
        }

        public static int TryCastToInt(object value)
        {
            int result = 0;

            if (value is double)
                result = (int)(double)value;
            else if (value is float)
                result = (int)(float)value;
            else if (value is decimal)
                result = (int)(decimal)value;
            else if (value is int)
                result = (int)value;
            else
                throw new ArgumentException($"Can't convert {value} to int");

            return result;
        }

        public static List<int>[] SeparateForTasks(int startInd, int endInd, int parts)
        {
            var tmp = new List<int>[parts];
            for (int i = startInd; i < endInd; i++)
            {
                if (tmp[i % parts] == null)
                    tmp[i % parts] = new List<int>();
                tmp[i % parts].Add(i);
            }

            List<List<int>> d = new List<List<int>>();
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i] != null)
                {
                    d.Add(tmp[i]);
                }
            }

            return d.ToArray();
        }
    }
}
