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
        public NeuralNetwork NeuralNetwork;

        public GANN(GeneticAlgorithm ga, NeuralNetwork nn)
        {
            GeneticAlgorithm = ga;
            NeuralNetwork = nn;
        }
    }
}
