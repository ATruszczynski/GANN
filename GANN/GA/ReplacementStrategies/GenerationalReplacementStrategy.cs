using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.ReplacementStrategies
{
    public class GenerationalReplacementStrategy : ReplacementStrategy
    {
        public override Chromosome[] Replace(Chromosome[] oldPopulation, Chromosome[] newPopulation)
        {
            return newPopulation;
        }
    }
}
