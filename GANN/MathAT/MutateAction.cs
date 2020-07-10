using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public enum MutateAction
    {
        AddLayer,
        RemoveLayer,
        ChangeNeuronCount,
        ChangeActFunc,
        ChangeLossFunction,
        ChangeGradientStrat
    }
}
