using GANN.GA.GA_Elements;
using GANN.GA.Operators.MutationOperators;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class MockMO : MutationOperator
    {
        public override Chromosome Mutate(Chromosome m, Random radoms)
        {
            return m;
        }
    }
}
