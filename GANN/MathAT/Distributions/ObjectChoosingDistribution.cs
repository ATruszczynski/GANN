using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class ObjectChoosingDistribution<T>
    {
        public DiscreteDistribution DiscDist;
        public T[] elements;
        public ObjectChoosingDistribution(Random random, T[] options, params double[] poss)
        {
            //TODO - B - test
            //TODO - B - validation
            if (poss.Length == 0)
                DiscDist = new UniformDiscreteDistribution(random, 0, options.Length);
            else
            {
                double[] indices = new double[poss.Length];
                for (int i = 0; i < indices.Length; i++)
                {
                    indices[i] = i;
                }
                DiscDist = new CustomDiscreteDistribution(random, indices, poss);
            }
            elements = options;
        }

        public T GetNext(Random random)
        {
            return elements[(int)DiscDist.GetNext()];
        }
    }
}
