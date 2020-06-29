using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class UniformContinuousDistribution : ContinuousRangeDistribution
    {
        //TODO - D - parameter names suck
        public UniformContinuousDistribution(Random random, double minn, double maxx): base(random, minn, maxx)
        {

        }

        public override double GetNext()
        {
            //TODO - B - test
            double num = Random.NextDouble();

            num *= Max - Min;
            num += Min;

            return num;
        }
    }
}
