using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class MockFF : FitnessFunction
    {
        public override double ComputeFitness(Chromosome c, params object[] args)
        {
            var m = (MockChromosome)c;
            if (m.m != null)
                throw new ArgumentException("Not null m");
            m.m = new MatrixAT1(new double[,] { { 1 } });
            return 1;
        }
    }
}
