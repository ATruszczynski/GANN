using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.FitnessFunctions
{
    public class InterchangableBinaryFF : FitnessFunction
    {
        public override double ComputeFitness(Chromosome c, params object[] args)
        {
            {
                BinaryChromosome b = (BinaryChromosome)c;
                double sum = 0;
                for (int i = 0; i < b.Array.Length; i++)
                {
                    if (i % 2 == 0 && b.Array[i] == true)
                        sum++;
                    if (i % 2 == 1 && b.Array[i] == false)
                        sum++;
                }
                return sum;
            }
        }
    }
}
