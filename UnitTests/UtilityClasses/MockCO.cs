using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class MockCO : CrossoverOperator
    {
        public override (Chromosome, Chromosome) Crossover(Chromosome ch1, Chromosome ch2, Random random)
        {
            return (ch2, ch1);
        }
    }
}
