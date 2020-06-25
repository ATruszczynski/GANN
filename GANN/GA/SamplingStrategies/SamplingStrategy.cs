using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.SamplingStrategies
{
    public abstract class SamplingStrategy
    {
        /// <summary>
        /// Returns a deep copy of elemnent in population
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        public abstract Chromosome Sample(Chromosome[] population, FitnessFunction fitnessFunction);
    }
}
