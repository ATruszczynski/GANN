using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.ReplacementStrategies
{
    public abstract class ReplacementStrategy
    {
        public abstract Chromosome[] Replace(Chromosome[] oldPopulation, Chromosome[] newPopulation);
    }
}
