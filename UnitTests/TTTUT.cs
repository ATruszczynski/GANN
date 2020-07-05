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

        [TestMethod]
        public void ReverseTest()
        {
            PseudoRandom pr = new PseudoRandom(0, 1, 1, 1, 0);
            (var inputs, var outputs) = TestGenerator.GenerateReverseIO(5, pr);

            Assert.AreEqual(1, inputs[0][0]);
            Assert.AreEqual(0, inputs[0][1]);
            Assert.AreEqual(0, inputs[1][0]);
            Assert.AreEqual(1, inputs[1][1]);
            Assert.AreEqual(0, inputs[2][0]);
            Assert.AreEqual(1, inputs[2][1]);
            Assert.AreEqual(0, inputs[3][0]);
            Assert.AreEqual(1, inputs[3][1]);
            Assert.AreEqual(1, inputs[4][0]);
            Assert.AreEqual(0, inputs[4][1]);
            Assert.AreEqual(0, outputs[0][0]);
            Assert.AreEqual(1, outputs[0][1]);
            Assert.AreEqual(1, outputs[1][0]);
            Assert.AreEqual(0, outputs[1][1]);
            Assert.AreEqual(1, outputs[2][0]);
            Assert.AreEqual(0, outputs[2][1]);
            Assert.AreEqual(1, outputs[3][0]);
            Assert.AreEqual(0, outputs[3][1]);
            Assert.AreEqual(0, outputs[4][0]);
            Assert.AreEqual(1, outputs[4][1]);
        }
    }
}
