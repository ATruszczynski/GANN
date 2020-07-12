using GANN.MathAT;
using GANN.NN;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
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
            Random random = new Random(1001);
            for (int i = 5; i >= 0; i--)
            {
                Console.WriteLine(random.Next());
            }
            random = new Random(1001);
            for (int i = 5; i >= 0; i--)
            {
                Console.WriteLine(random.Next(1,4));
            }



            (var trainInputs, var trainOutputs, var testInputs, var testOutputs) = GreyScaleImageLoader.LoadToGreyScale(@"C:\Users\aleks\Desktop\Sign-Language-Digits-Dataset-master\Sign-Language-Digits-Dataset-master\Dataset");
            ANN ann = new ANN(new Hyperparameters(10000, 10, new int[] { 200, 100 },
                    intActFuns: new ActivationFunction[] { new Relu(), new Relu() },
                    aggregateActFunc: new Softmax(),
                    lossFunc: new CrossEntropy(),
                    gradStep: new MomentumStrategy(0.00001, 0.01)));
            ann.Train(trainInputs, trainOutputs,10, 50);
            //ann.ModelToFile("desu.txt");
            Utility.WriteArary(ann.Test(testInputs, testOutputs, "desu.txt"));
        }
    }
}
