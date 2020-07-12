using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    //TODO - C - logger
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
            Console.WriteLine(ArrayToString(array, sep));
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

        public static double ArrayAverage(double[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum / array.Length;
        }

        public static string ArrayToString<T>(T[] array, char sep = ',')
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString();
                if (i != array.Length - 1)
                    result += sep;
            }
            return result;
        }

        public static double NeighbourOnCircleCont(double curr, double radius, double min, double max, Random random)
        {
            //TODO - C - not too close to orioginal value?
            var val = 2*(random.NextDouble() - 0.5);
            var step = val * radius;

            var cand = curr + step;

            var range = max - min;

            while (cand < min)
                cand += range;

            while (cand >= max)
                cand -= range;

            return cand;
        }

        public static int NeighbourOnCircleDisc(int curr, int radius, int min, int max, Random random)
        {
            if (radius == 0)
                throw new ArgumentException("Operation doesn't make sense with radius 0");
            //TODO - A - test for random
            double p = random.NextDouble();
            int cand;
            if(p < 0.5)
            {
                cand = random.Next(curr - radius, curr);
            }
            else
            {
                cand = random.Next(curr + 1, curr + radius + 1);
            }

            var range = max - min;

            while (cand < min)
                cand += range;
            while (cand >= max)
                cand -= range;

            return cand;
        }
    }
}
