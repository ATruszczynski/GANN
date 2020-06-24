using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN
{
    public abstract class NeuralNetwork
    {
        //TODO more general input/output
        public abstract void Train(List<List<double>> inputs, List<List<double>> outputs, int epochs, int batchSize);
        public abstract List<double> Run(List<double> input);
    }
}
