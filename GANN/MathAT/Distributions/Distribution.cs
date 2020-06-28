using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public abstract class Distribution
    {
        public Random Random;
        public Distribution(Random random = null)
        {
            if (random != null)
                Random = random;
            else
                Random = new Random();
        }
        //TODO - C - shoudl it rake random in constuctor?
        public abstract double GetNext();
    }
}
