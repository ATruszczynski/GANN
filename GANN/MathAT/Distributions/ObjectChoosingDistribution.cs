using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class ObjectChoosingDistribution<T>
    {
        public DiscreteDistribution DiscDist;
        public T[] elements;
        public ObjectChoosingDistribution(Random random, T[] options)
        {
            //TODO - B - test
            //TODO - B - validation
            DiscDist = new UniformDiscreteDistribution(random, 0, options.Length);
            elements = options;
        }

        public T GetNext(Random random)
        {
            return elements[(int)DiscDist.GetNext()];
        }
    }
}
