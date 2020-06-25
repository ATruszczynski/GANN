using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.Operators.MutationOperators
{
    public class SinglePointBinaryMutation : MutationOperator
    {
        public override Chromosome Mutate(Chromosome m, Random radoms)
        {
            BinaryChromosome bc = (BinaryChromosome)m;
            int ind = radoms.Next(bc.Array.Length);

            bc.Array[ind] = !bc.Array[ind];

            return bc;
        }
    }
}
