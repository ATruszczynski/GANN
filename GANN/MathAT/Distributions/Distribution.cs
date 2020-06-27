using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public abstract class Distribution
    {
        //TODO - C - shoudl it rake random in constuctor?
        public abstract double GetNext(Random random);
    }
}
