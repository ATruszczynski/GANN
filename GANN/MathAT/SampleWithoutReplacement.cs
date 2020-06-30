using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class SampleWithoutReplacement<T>
    {
        //TODO - B - test
        public List<T> Samples;
        public Random Random;
        public SampleWithoutReplacement(T[] samples, Random random)
        {
            Samples = new List<T>();
            foreach (var sample in samples)
            {
                Samples.Add(sample);
            }
            Random = random;
        }
        public SampleWithoutReplacement(List<T> samples, Random random)
        {
            Samples = samples;
            Random = random;
        }

        public bool GetNext(out T result)
        {
            result = default;

            if (Samples.Count == 0)
                return false;

            int ind = Random.Next(Samples.Count);
            result = Samples[ind];

            Samples.RemoveAt(ind);

            return true;
        }
    }
}
