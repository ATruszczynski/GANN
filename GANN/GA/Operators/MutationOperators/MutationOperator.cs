using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.Operators.MutationOperators
{
    public abstract class MutationOperator
    {
        public abstract Chromosome Mutate(Chromosome m, Random radoms);
    }
}
