using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    //TODO - A - make fitness func resitant to exceptions
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
