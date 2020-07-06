using GANN.MathAT;
using GANN.NN;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN
{
    public class Tester
    {
        double[][] inputs;
        double[][] outputs;

        public static double Test(int reps, ANN nn,  double[][] inputs, double[][] outputs)
        {
            List<double> scores = new List<double>();
            for (int i = 0; i < reps; i++)
            {
                var res = Utility.ArrayAverage(nn.Test(inputs, outputs));
                scores.Add(res);
            }
            return Utility.ArrayAverage(scores.ToArray());
        }
    }
}
