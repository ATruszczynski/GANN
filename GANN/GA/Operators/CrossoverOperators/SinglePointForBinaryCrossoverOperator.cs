using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.Operators.CrossoverOperators
{
    public class SinglePointForBinaryCrossoverOperator : CrossoverOperator
    {
        //TODO - C - drop random as arguemt? Use params instead?
        public override (Chromosome, Chromosome) Crossover(Chromosome ch1, Chromosome ch2, Random random)
        {
            BinaryChromosome c1 = (BinaryChromosome)ch1;
            BinaryChromosome c2 = (BinaryChromosome)ch2;

            var ar1 = new bool[c1.Array.Length];
            var ar2 = new bool[c2.Array.Length];

            int div = random.Next(1, ar1.Length - 1);
            for(int i = 0; i < div; i++)
            {
                ar1[i] = c1.Array[i];
                ar2[i] = c2.Array[i];
            }
            for (int i = div; i < ar1.Length; i++)
            {
                ar1[i] = c2.Array[i];
                ar2[i] = c1.Array[i];
            }

            BinaryChromosome res1 = new BinaryChromosome(ar1);
            BinaryChromosome res2 = new BinaryChromosome(ar2);

            return (res1, res2);
        }
    }
}
