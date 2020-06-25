using GANN.NN;
using System;
using static GANN.MathAT.ActFuns;

namespace NeuralNetworkExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 1000;
            (var i, var o) = TestGenerator.TTT(num);
            //for (int ii = 0; ii < num; ii++)
            //{
            //    var b = i[ii];
            //    var win = o[ii];
            //    string result = "";
            //    for (int j = 0; j < b.Length; j++)
            //    {
            //        result += b[j];
            //        if ((j + 1) % 3 == 0)
            //            result += Environment.NewLine;
            //    }

            //    result += Environment.NewLine;

            //    for (int j = 0; j < win.Length; j++)
            //    {
            //        result += win[j];
            //        if (j != win.Length - 1)
            //            result += ",";
            //    }

            //    result += "------------------" + Environment.NewLine;

            //    Console.WriteLine(result);
            //}

            ANN nn = new ANN
                (
                 new int[] { 9, 4, 4, 4 },
                 new Func<double, double>[] { Relu, Relu, Relu },
                 new Func<double, double>[] { DerRelu, DerRelu, DerRelu },
                 null,
                 DerLoss
                );

            nn.Train(i, o, 5, 100);

            var res = nn.Run(new double[] { 0, 0, 0, 1, 1, 1, 0, 0, 0 });
            Console.WriteLine($"{res[0]},{res[1]},{res[2]},{res[3]}");
        }
    }
}
