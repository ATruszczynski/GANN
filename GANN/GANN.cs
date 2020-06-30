using GANN.GA;
using GANN.NN;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN
{
    class GANN
    {
        public GeneticAlgorithm GeneticAlgorithm;
        public double[][] trainInput;
        public double[][] trainOutput;
        public double[][] testInput;
        public double[][] testOutput;

        public GANN(GeneticAlgorithm ga)
        {
            GeneticAlgorithm = ga;
        }
    }
}
