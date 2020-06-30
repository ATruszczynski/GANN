using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.SamplingStrategies
{
    public abstract class SamplingStrategy
    {
        //TODO - B - move random inside?
        public abstract Chromosome Sample(Chromosome[] population, double[] fitnesses, Random random);
    }
}
