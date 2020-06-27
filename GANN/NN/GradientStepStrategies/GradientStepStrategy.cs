﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public abstract class GradientStepStrategy
    {
        public abstract double GetStepSize();
    }
}
