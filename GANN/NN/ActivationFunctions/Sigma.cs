﻿using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace GANN.NN.ActivationFunctions
{
    public class Sigma : ActivationFunction
    {
        public override double Compute(double arg)
        {
            //TODO - B - test
            return 1d / (1 + Exp(-arg));
        }

        public override double ComputeDerivative(double arg)
        {
            //TODO - B - test
            return Compute(arg) * (1 - Compute(arg));
        }

        public override ActivationFunction DeepCopy()
        {
            //TODO - B - test
            return new Sigma();
        }
        public override int CompareTo(object obj)
        {
            //TODO - B - test
            Sigma tmp;
            try
            {
                tmp = (Sigma)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return 0;
        }
    }
}
