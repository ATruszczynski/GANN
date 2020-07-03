using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class UniformContinuousRangeDistribution : ContinuousRangeDistribution
    {
        //TODO - D - parameter names suck
        public UniformContinuousRangeDistribution(Random random, double minn, double maxx): base(random, minn, maxx)
        {

        }

        public override double GetNext()
        {
            double num = Random.NextDouble();

            num *= Max - Min;
            num += Min;

            return num;
        }
    }
}
