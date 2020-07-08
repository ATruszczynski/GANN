using GANN.NN;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.GA_Elements
{
    public class NNChromosome : Chromosome
    {
        //TODO - A - deep copy of NN?
        public Hyperparameters Hyperparameters;
        public NNChromosome()
        {

        }

        public NNChromosome(Hyperparameters hyperparameters)
        {
            Hyperparameters = hyperparameters;
        }
        public override Chromosome DeepCopy()
        {
            //TODO - B - deep copies are not having deep copy of random
            return new NNChromosome(Hyperparameters.DeepCopy());
        }

        public override string ToString()
        {
            return Hyperparameters.ToString();
        }
    }
}
