using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.NN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;

namespace UnitTests.NNElems
{
    [TestClass]
    public class FFUT
    {
        [TestMethod]
        public void NNFitnessFuncTest()
        {
            MockANN mnn = new MockANN();
            NNFitnessFunc nnff = new NNFitnessFunc();
            Assert.AreEqual(0.5, nnff.ComputeFitness(new NNChromosome(mnn)));
        }
    }
}
