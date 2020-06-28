using GANN.MathAT;
using GANN.NN;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GANN.MathAT.ActFuns;

namespace NeuralNetworkExperiments
{
    public class TestGenerator
    {
        public static (double[][], double[][]) TTT(int number)
        {
            double[][] inputs = new double[number][];
            double[][] outputs = new double[number][];

            for (int i = 0; i < number; i++)
            {
                inputs[i] = new double[9];
                outputs[i] = new double[4]; //0 - remis, 1 - krzyżyk, 2 - kółko
            }

            Random r = new Random(1001);

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    inputs[i][j] = r.Next(3)/2;
                }

                bool cross = CheckForWinning(inputs[i], 1);
                bool circle = CheckForWinning(inputs[i], 2);

                if (cross)
                    outputs[i][1] = 1;
                else if (circle)
                    outputs[i][2] = 1;
                else if (cross && circle)
                    outputs[i][3] = 1;
                else
                    outputs[i][0] = 1;
            }

            return (inputs, outputs);
        }

        static bool CheckForWinning(double[] b, double fig)
        {
            int[][] winningCom = new int[8][]
            {
                new int[] { 0, 1, 2},
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            for (int i = 0; i < winningCom.Length; i++)
            {
                var d = winningCom[i];
                if (b[d[0]] == fig && b[d[1]] == fig && b[d[2]] == fig)
                    return true;
            }

            return false;
        }

        public static void TestScenario1()
        {

            int num = 2000;
            (var i, var o) = TestGenerator.TTT(num);

            ANN nn = new ANN(new Hyperparameters(new int[] { 9, 4, 4, 4 }), new Random(1001));
                 //(
                 // new int[] { 9, 4, 4, 4 },
                 // //new Func<double, double>[] { Relu, Sigma },
                 // //new Func<double, double>[] { DerRelu, DerSigma },
                 // //new int[] { 9, 20, 20, 4 },
                 // new Func<double, double>[] { Relu, Relu, Sigma },
                 // new Func<double, double>[] { DerRelu, Relu, DerSigma },
                 // //new Func<double, double>[] { Sigma, Sigma, Sigma },
                 // //new Func<double, double>[] { DerSigma, DerSigma, DerSigma },
                 // null,
                 // DerLoss
                 //);

            nn.Train(i, o, 20, 100);

            var res = nn.Run(new double[] { 0, 0, 0, 1, 2, 1, 2, 2, 2 });
            Console.WriteLine($"{res[0]},{res[1]},{res[2]},{res[3]}");
        }

        public static void TestScenario2()
        {
            ANN nn = new ANN(new Hyperparameters(new int[] { 9, 4, 4, 4 }), new Random(1001));

            nn.weights[0] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.biases[0] = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 });

            nn.Train
                (
                new double[][] { new double[] { 2, 1 } },
                new double[][] { new double[] { 1, 0 } },
                2,
                1
                );
        }
    }
}
