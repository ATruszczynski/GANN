using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.Operators.CrossoverOperators
{
    public abstract class CrossoverOperator
    {
        public abstract Chromosome Crossover(Chromosome ch1, Chromosome ch2);
    }
}
