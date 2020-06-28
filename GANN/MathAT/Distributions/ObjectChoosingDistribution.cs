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
                DiscDist = new CustomDiscreteDistribution(random, poss);
            elements = options;
        }

        public T GetNext(Random random)
        {
            return elements[(int)DiscDist.GetNext()];
        }
    }
}
