using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT
{
    public class GaussianDistribution
    {
        public double Mean = 0;
        public double Std = 1;
        Random random;
        public GaussianDistribution(int? seed = null, double mean = 0, double std = 1)
        {
            if (seed == null)
                random = new Random();
            else
                random = new Random(seed.Value);
            Mean = mean;
            Std = std;
        }

        public double Next()
        {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         Mean + Std * randStdNormal; //random normal(mean,stdDev^2)}
            return randNormal;
        }

    }
}
