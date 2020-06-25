using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                outputs[i] = new double[3]; //0 - remis, 1 - krzyżyk, 2 - kółko
            }

            Random r = new Random();

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    inputs[i][j] = r.Next(3);
                }

                bool cross = CheckForWinning(inputs[i], 1);
                bool circle = CheckForWinning(inputs[i], 2);

                if (cross)
                    outputs[i][1] = 1;
                else if (circle)
                    outputs[i][2] = 1;
                else
                    outputs[i][0] = 1;
            }

            return (inputs, outputs);
        }

        bool static CheckForWinning(double[] b, double fig)
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
    }
}
