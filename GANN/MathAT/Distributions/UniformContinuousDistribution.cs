using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class UniformContinuousDistribution : ContinuousDistributon
    {
        double min = 0;
        double max = 1;
        //TODO - D - parameter names suck
        public UniformContinuousDistribution(Random random, double minn, double maxx): base(random)
        {
            min = minn;
            max = maxx;
        }

        public override double GetNext()
        {
            //TODO - B - test
            double num = Random.NextDouble();

            num *= max - min;
            num -= min;

            return num;
        }
    }
}
