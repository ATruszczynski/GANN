using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.ReplacementStrategies;
using GANN.GA.SamplingStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA
{
    public abstract class GeneticAlgorithm
    {
        public double crossoverProbability;
        public double mutationProbability;

        CrossoverOperator CrossoverOperator;
        MutationOperator MutationOperator;
        SamplingStrategy SamplingStrategy;
        ReplacementStrategy ReplacementStrategy;


        //public abstract Run();
    }
}
