using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class SampleWithoutReplacement
    {
        //TODO - B - test
        public List<double> Samples;
        public Random Random;
        public SampleWithoutReplacement(List<double> samples, Random random)
        {
            Samples = samples;
            Random = random;
        }

        public bool GetNext(out double result)
        {
            result = double.MinValue;

            if (Samples.Count == 0)
                return false;

            int ind = Random.Next(Samples.Count);
            result = Samples[ind];

            Samples.RemoveAt(ind);

            return true;
        }
    }
}
