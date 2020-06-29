using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.MathAT
{
    public class ActFuns
    {
        public static double Relu(double arg)
        {
            return Max(0, arg);
        }

        public static double DerRelu(double arg)
        {
            double result = 0;

            if (arg > 0)
                result = 1;

            return result;
        }

        public static double Sigma(double arg)
        {
            return 1d / (1 + Exp(-arg));
        }

        public static double DerSigma(double arg)
        {
            return Sigma(arg) * (1 - Sigma(arg));
        }

        public static double DerLoss(double act, double y)
        {
            return 2 * (act - y);
        }
        public static double DerLoss2(double act, double y)
        {
            return (act - y);
        }
    }
}
