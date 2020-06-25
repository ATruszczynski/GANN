using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.SamplingStrategies
{
    public abstract class SamplingStrategy
    {
        public abstract Chromosome Sample(Chromosome[] population);
    }
}
