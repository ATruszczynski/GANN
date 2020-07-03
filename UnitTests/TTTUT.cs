using GANN.MathAT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;
using static UnitTests.UtilityClasses.TestUtility;

namespace UnitTests
{
    [TestClass]
    public class TTTUT
    {

        [TestMethod]
        public void TTT1()
        {
            PseudoRandom pr = new PseudoRandom(0,0,0,1,1,1,2,2,2);
            (var inputs, var outputs) = TestGenerator.TTT1(1, pr);
            Assert.IsTrue(CompareArrays(inputs[0], 0, 0, 0, 0.5, 0.5, 0.5, 1, 1, 1));
            Assert.IsTrue(CompareArrays(outputs[0], 0, 0, 0, 1));
        }
    }
}
