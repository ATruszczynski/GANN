using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.FitnessFunctions
{
    public abstract class FitnessFunction
    {
        //TODO - C - add params everywhere
        public abstract double ComputeFitness(Chromosome c, params object[] args);
    }
}
