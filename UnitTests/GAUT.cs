using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.ReplacementStrategies;
using GANN.GA.SamplingStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class GAUT
    {
        //TODO - C - Move and test utility functions
        //TODO - B - test matrix compare functions
        [TestMethod]
        public void InterFFTest()
        {
            FitnessFunction ff = new InterchangableBinaryFF();

            Assert.AreEqual(5, ff.ComputeFitness(new BinaryChromosome(new bool[] { true, false, true, false, true})));
            Assert.AreEqual(3, ff.ComputeFitness(new BinaryChromosome(new bool[] { true, true, false, false, true})));
            Assert.AreEqual(0, ff.ComputeFitness(new BinaryChromosome(new bool[] { false, true, false, true, false})));
        }

        [TestMethod]
        public void SPBCrossover()
        {
            CrossoverOperator co = new SinglePointForBinaryCrossoverOperator();
            BinaryChromosome b1 = new BinaryChromosome(new bool[] { true, true, true });
            BinaryChromosome b2 = new BinaryChromosome(new bool[] { false, false, false });

            PseudoRandom pr = new PseudoRandom(new double[] { 1 });

            (var r1, var r2) = co.Crossover(b1, b2, pr);

            var rr1 = r1 as BinaryChromosome;
            var rr2 = r2 as BinaryChromosome;

            Assert.AreEqual(true, rr1.Array[0]);
            Assert.AreEqual(false, rr1.Array[1]);
            Assert.AreEqual(false, rr1.Array[2]);
            Assert.AreEqual(false, rr2.Array[0]);
            Assert.AreEqual(true, rr2.Array[1]);
            Assert.AreEqual(true, rr2.Array[2]);
        }

        [TestMethod]
        public void SPBMutation()
        {
            MutationOperator co = new SinglePointBinaryMutation();
            BinaryChromosome b1 = new BinaryChromosome(new bool[] { true, true, true });

            PseudoRandom pr = new PseudoRandom(new double[] { 1 });

            var r1 = co.Mutate(b1, pr);

            var rr1 = r1 as BinaryChromosome;

            Assert.AreEqual(true, rr1.Array[0]);
            Assert.AreEqual(false, rr1.Array[1]);
            Assert.AreEqual(true, rr1.Array[2]);
        }

        [TestMethod]
        public void GenerationalReplacement()
        {
            ReplacementStrategy rs = new GenerationalReplacementStrategy();

            BinaryChromosome[] pop1 = new BinaryChromosome[] { new BinaryChromosome(new bool[] { true }), new BinaryChromosome(new bool[] { true })  };
            BinaryChromosome[] pop2 = new BinaryChromosome[] { new BinaryChromosome(new bool[] { false }), new BinaryChromosome(new bool[] { false })  };

            Chromosome[] pop = rs.Replace(pop1, pop2);

            Assert.AreEqual(2, pop.Length);
            Assert.AreEqual(false, (pop[0] as BinaryChromosome).Array[0]);
            Assert.AreEqual(false, (pop[1] as BinaryChromosome).Array[0]);
        }

        [TestMethod]
        public void BCDP()
        {
            BinaryChromosome bc = new BinaryChromosome(new bool[] { true, false });
            var bc2 = (BinaryChromosome)bc.DeepCopy();

            Assert.AreEqual(true, bc.Array[0]);
            Assert.AreEqual(false, bc.Array[1]);
            Assert.AreEqual(true, bc2.Array[0]);
            Assert.AreEqual(false, bc2.Array[1]);

            bc.Array[0] = false;

            Assert.AreEqual(false, bc.Array[0]);
            Assert.AreEqual(false, bc.Array[1]);
            Assert.AreEqual(true, bc2.Array[0]);
            Assert.AreEqual(false, bc2.Array[1]);
        }

        [TestMethod]
        public void RouletteSampling()
        {
            SamplingStrategy ss = new RouletteSamplingStrategy();
            BinaryChromosome[] pop1 = new BinaryChromosome[] { new BinaryChromosome(new bool[] { true, false }), new BinaryChromosome(new bool[] { true, true }), new BinaryChromosome(new bool[] { false, false }) };
            FitnessFunction ff = new InterchangableBinaryFF();
            PseudoRandom pr = new PseudoRandom(0.2,0.6, 0.8);

            var s1 = (BinaryChromosome)ss.Sample(pop1, ff, pr);
            var s2 = (BinaryChromosome)ss.Sample(pop1, ff, pr);
            var s3 = (BinaryChromosome)ss.Sample(pop1, ff, pr);

            Assert.AreEqual(true, s1.Array[0]);
            Assert.AreEqual(false, s1.Array[1]);
            Assert.AreEqual(true, s2.Array[0]);
            Assert.AreEqual(true, s2.Array[1]);
            Assert.AreEqual(false, s3.Array[0]);
            Assert.AreEqual(false, s3.Array[1]);

            BinaryChromosome[] pop = new BinaryChromosome[] { new BinaryChromosome(new bool[] { true, true}) };

            var s4 = (BinaryChromosome)ss.Sample(pop, ff, pr);
            Assert.AreEqual(true, s4.Array[0]);
            Assert.AreEqual(true, s4.Array[1]);

            BinaryChromosome[] pop3 = new BinaryChromosome[] { new BinaryChromosome(new bool[] { true, true }), new BinaryChromosome(new bool[] { false, true }) };
            pr = new PseudoRandom(0.9999);

            var s5 = (BinaryChromosome)ss.Sample(pop3, ff, pr);
            Assert.AreEqual(false, s5.Array[0]);
            Assert.AreEqual(true, s5.Array[1]);

        }
    }
}
