using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GANN.NN;
using static GANN.MathAT.ActFuns;
using GANN.MathAT;

namespace UnitTests
{
    [TestClass]
    public class NNUT
    {
        [TestMethod]
        public void NNRun()
        {
            ANN nn = new ANN
                (
                 new int[] { 2, 2, 2 },
                 new Func<double, double>[] { Relu, Relu },
                 new Func<double, double>[] { DerRelu, DerRelu },
                 null,
                 null
                );

            nn.weights[0] = new MatrixAT1(new double[,] { { 1, 2 }, { 2, -1 } });
            nn.weights[1] = new MatrixAT1(new double[,] { { 3, 0 }, { 1, 2 } });
            nn.biases[0] = new MatrixAT1(new double[,] { { 1 }, { 0 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 2 }, { -2 } });

            var resutl = nn.Run(new double[] { 2, 1 }, false);

            Assert.AreEqual(17, resutl[0]);
            Assert.AreEqual(9, resutl[1]);
        }

        [TestMethod]
        public void NNTrain_SingleInput()
        {
            //TODO - A - not good enough test; should have different weights
            ANN nn = new ANN
                (
                 new int[] { 2, 2, 2 },
                 new Func<double, double>[] { Relu, Relu },
                 new Func<double, double>[] { DerRelu, DerRelu },
                 null,
                 DerLoss
                );

            nn.weights[0] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.biases[0] = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 });

            Assert.AreEqual(0.5, resutl[0]);
            Assert.AreEqual(0.5, resutl[1]);

            nn.Train
                (
                new double[][] { new double[] { 2, 1 } },
                new double[][] { new double[] { 1, 0 } },
                1,
                1
                );

            resutl = nn.Run(new double[] { 2, 1 });

            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.weights[0], new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } })));
            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.weights[1], new MatrixAT1(new double[,] { { 5, 5 }, { -3, -3 } })));
            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.biases[0], new MatrixAT1(new double[,] { { 1 }, { 1 } })));
            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.biases[1], new MatrixAT1(new double[,] { { 2 }, { 0 } })));

            Assert.AreEqual(1, resutl[0]);
            Assert.AreEqual(0, resutl[1]);
        }

        [TestMethod]
        public void NNTrain_SingleInput2()
        {
            //TODO - A - not good enough test; should have different weights
            ANN nn = new ANN
                (
                 new int[] { 2, 1, 2 },
                 new Func<double, double>[] { Relu, Relu },
                 new Func<double, double>[] { DerRelu, DerRelu },
                 null,
                 DerLoss
                );

            nn.weights[0] = new MatrixAT1(new double[,] { { 1, 2 } });
            nn.weights[1] = new MatrixAT1(new double[,] { { -1 }, { 3 } });
            nn.biases[0] = new MatrixAT1(new double[,] { { -1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 2 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 });

            Assert.AreEqual(0, resutl[0]);
            Assert.AreEqual(1, resutl[1]);

            nn.Train
                (
                new double[][] { new double[] { 2, 1 } },
                new double[][] { new double[] { 1, 0 } },
                1,
                1
                );


            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.weights[0], new MatrixAT1(new double[,] { { -11, -4 } })));
            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.weights[1], new MatrixAT1(new double[,] { { -1 }, { -3 } })));
            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.biases[0], new MatrixAT1(new double[,] { { -7 } })));
            Assert.IsTrue(MatrixUT.CompareMatrixes(nn.biases[1], new MatrixAT1(new double[,] { { 2 }, { -1 } })));

            resutl = nn.Run(new double[] { 2, 1 });

            Assert.AreEqual(1, resutl[0]);
            Assert.AreEqual(0, resutl[1]);
        }

        [TestMethod]
        public void NNTrain_MultipleInputs()
        {
            ANN nn = new ANN
                (
                 new int[] { 2, 3, 2 },
                 new Func<double, double>[] { Relu, Relu },
                 new Func<double, double>[] { DerRelu, DerRelu },
                 null,
                 DerLoss
                );

            //nn.normaliseOutput = false;
            nn.weights[0] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } });
            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            nn.biases[0] = new MatrixAT1(new double[,] { { 1 }, { 1 }, { 1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 1 } });

            nn.Train
                (
                new double[][] { new double[] { 2, 1 }, new double[] { 1, 2 }, new double[] { 3, 4 } },
                new double[][] { new double[] { 1, 0 }, new double[] { 0, 1 }, new double[] { 0, 1 } },
                2,
                2
                );

            var res = nn.Run(new double[] { 1, 1 });
            ;
        }

    }
}
