﻿using GANN.NN;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.GA_Elements
{
    public class NNChromosome : Chromosome
    {
        public NeuralNetwork NeuralNetwork;
        public override Chromosome DeepCopy()
        {
            //TODO - A - implement
            throw new NotImplementedException();
        }
    }
}
