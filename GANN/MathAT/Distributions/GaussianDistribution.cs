using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class GaussianDistribution: ContinuousDistributon
    {
        public double Mean = 0;
        public double Std = 1;
        public GaussianDistribution(Random random, double mean = 0, double std = 1): base(random)
        {
            Mean = mean;
            Std = std;
        }

        public override double GetNext()
        {
            //TODO - A - test dkh
            //TODO - B - why does this work?
            //Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - Random.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - Random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         Mean + Std * randStdNormal; //random normal(mean,stdDev^2)}
            return randNormal;
        }

    }
}
