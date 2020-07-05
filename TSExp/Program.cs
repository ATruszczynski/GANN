using GANN.MathAT;
using GANN.NN;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSExp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO - A - NN doesnt work with neuron counts only
            (var trainInputs, var trainOutputs, var testInputs, var testOutputs) = GreyScaleImageLoader.LoadToGreyScale(@"C:\Users\aleks\Desktop\Sign-Language-Digits-Dataset-master\Sign-Language-Digits-Dataset-master\Dataset");
            ANN ann = new ANN(new Hyperparameters(10000, 10, new int[] { 1000, 100 }, sw: 0.25,
                    intActFuns: new ActivationFunction[] { new Relu(), new Relu(), new Relu() },
                    aggregateActFunc: new Sigma(),
                    gradStep: new ConstantGradientStep(0.001)));
            ann.Train(trainInputs, trainOutputs,5, 100);
            //ann.ModelToFile("desu.txt");
            Utility.WriteArary(ann.Test(testInputs, testOutputs, "desu.txt"));
        }
    }
}
