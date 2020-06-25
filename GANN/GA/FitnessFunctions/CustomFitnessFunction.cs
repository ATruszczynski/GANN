using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.FitnessFunctions
{
    public class CustomFitnessFunction : FitnessFunction
    {
        //TODO - C - nomenclature
        Func<Chromosome, object[], double> FF;

        public CustomFitnessFunction(Func<Chromosome, object[], double> func)
        {
            FF = func;
        }

        public override double ComputeFitness(Chromosome c, params object[] args)
        {
            return FF.Invoke(c, args);
        }
    }
}
