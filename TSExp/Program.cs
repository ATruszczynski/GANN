using GANN.MathAT;
using GANN.NN;
using GANN.NN.ActivationFunctions;
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
            ANN ann = new ANN(new Hyperparameters(new int[] { 10000, 1000, 10 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu(), new Sigma() }));
            ann.Train(trainInputs, trainOutputs,5, 100);
            
            Utility.WriteArary(ann.Test(testInputs, testOutputs));
        }
    }
}
