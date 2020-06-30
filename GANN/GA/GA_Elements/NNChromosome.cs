using GANN.NN;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.GA_Elements
{
    public class NNChromosome : Chromosome
    {
        public ANN NeuralNetwork;
        public NNChromosome()
        {

        }

        public NNChromosome(ANN nn)
        {
            NeuralNetwork = nn;
        }
        public override Chromosome DeepCopy()
        {
            //TODO - B - deep copies are not having deep copy of random
            return new NNChromosome(new ANN(NeuralNetwork.Hyperparameters, NeuralNetwork.Random));
        }
    }
}
