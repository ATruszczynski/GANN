using GANN.GA.GA_Elements;
using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class MockChromosome : Chromosome
    {
        public MatrixAT1 m; 

        public MockChromosome()
        {

        }
        public MockChromosome(MatrixAT1 ma1)
        {
            m = ma1.DeepCopy();
        }
        public override Chromosome DeepCopy()
        {
            return new MockChromosome();
        }
    }
}
