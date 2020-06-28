﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.ActivationFunctions
{
    public class Relu : ActivationFunction
    {
        public override double Compute(double arg)
        {
            //TODO - B - test
            return Math.Max(0, arg);
        }

        public override double ComputeDerivative(double arg)
        {
            //TODO - B - test
            double result = 0;

            if (arg > 0)
                result = 1;

            return result;
        }
    }
}