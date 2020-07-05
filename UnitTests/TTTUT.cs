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

        [TestMethod]
        public void CountTest()
        {
            PseudoRandom pr = new PseudoRandom
                (
                    4, 2, 7, 1, 3,
                    2, 4, 5,
                    7, 1, 2, 1, 0, 4, 2, 1,
                    0,
                    10, 7, 8, 1, 2, 5, 2, 1, 0, 1, 0, 0
                );
            (var inputs, var outputs) = TestGenerator.CountIO(5, pr);

            List<List<int>> tin = new List<List<int>>(); 
            List<List<int>> tout = new List<List<int>>(); 

            List<int> expectedOnes = new List<int>() { 2, 8, 1, 5 };
            List<int> expectedOnesOut = new List<int>() { 4 };
            tin.Add(expectedOnes);
            tout.Add(expectedOnesOut);

            expectedOnes = new List<int>() { 4, 6 };
            expectedOnesOut = new List<int>() { 2 };
            tin.Add(expectedOnes);
            tout.Add(expectedOnesOut);

            expectedOnes = new List<int>() { 0, 1, 2, 3, 5, 6, 8 };
            expectedOnesOut = new List<int>() { 7 };
            tin.Add(expectedOnes);
            tout.Add(expectedOnesOut);

            expectedOnes = new List<int>() { };
            expectedOnesOut = new List<int>() { 0 };
            tin.Add(expectedOnes);
            tout.Add(expectedOnesOut);

            expectedOnes = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            expectedOnesOut = new List<int>() { 10 };
            tin.Add(expectedOnes);
            tout.Add(expectedOnesOut);

            for (int i = 0; i < tin.Count; i++)
            {
                var tinn = tin[i];
                var toutt = tout[i];

                for (int j = 0; j < tinn.Count; j++)
                {
                    if (tinn.Contains(j))
                        Assert.AreEqual(1, inputs[i][j]);
                    else
                        Assert.AreEqual(0, inputs[i][j]);
                }

                for (int j = 0; j < toutt.Count; j++)
                {
                    if (toutt.Contains(j))
                        Assert.AreEqual(1, outputs[i][j]);
                    else
                        Assert.AreEqual(0, outputs[i][j]);
                }
            }
            
            
        }
    }
}
