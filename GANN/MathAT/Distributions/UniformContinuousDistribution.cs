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
        public UniformContinuousDistribution(double minn, double maxx)
        {
            min = minn;
            max = maxx;
        }

        public override double GetNext(Random random)
        {
            //TODO - B - test
            double num = random.NextDouble();

            num *= max - min;
            num -= min;

            return num;
        }
    }
}
