using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GANN.NN;
using static GANN.MathAT.ActFuns;
using GANN.MathAT;
using static System.Math;

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
        [ExpectedException(typeof(ArgumentException))]
        public void NN_MismatchedInput()
        {
            ANN nn = new ANN
               (
                new int[] { 2, 2, 2 },
                new Func<double, double>[] { Relu, Relu },
                new Func<double, double>[] { DerRelu, DerRelu },
                null,
                DerLoss
               );
            
            nn.Train
                 (
                 new double[][] { new double[] { 2, 1, 3 } },
                 new double[][] { new double[] { 1, 0 } },
                 1,
                 1
                 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NN_MismatchedOutput()
        {
            ANN nn = new ANN
               (
                new int[] { 2, 2, 2 },
                new Func<double, double>[] { Relu, Relu },
                new Func<double, double>[] { DerRelu, DerRelu },
                null,
                DerLoss
               );

            nn.Train
                 (
                 new double[][] { new double[] { 2, 1 } },
                 new double[][] { new double[] { 1, 0, 1 } },
                 1,
                 1
                 );
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

            Assert.AreEqual(9, resutl[0]);
            Assert.AreEqual(9, resutl[1]);

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
            Assert.AreEqual(10, resutl[1]);

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
        //TODO - 0 - normalisation of input only entire input set-wise
        [TestMethod]
        //Comparison with results here:
        //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
        public void SigmaComparison()
        {
            ANN nn = new ANN
                (
                 new int[] { 2, 2, 2 },
                 new Func<double, double>[] { Sigma, Sigma },
                 new Func<double, double>[] { DerSigma, DerSigma },
                 null,
                 DerLoss2
                );

            nn.gradientVelocity = 0.5;

            nn.weights[0] = new MatrixAT1(new double[,] { { 0.15, 0.20 }, { 0.25, 0.3 } });
            nn.weights[1] = new MatrixAT1(new double[,] { { 0.40, 0.45 }, { 0.50, 0.55 } });

            nn.biases[0] = new MatrixAT1(new double[,] { { 0.35 }, { 0.35 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 0.6 }, { 0.6 } });

            var input = new double[] { 0.05, 0.1 };

            var result = nn.Run(input);

            double eps = 0.00001;

            Assert.AreEqual(input[0], nn.ases[0][0, 0]);
            Assert.AreEqual(input[1], nn.zs[0][1, 0]);
            Assert.AreEqual(input[0], nn.zs[0][0, 0]);
            Assert.AreEqual(input[1], nn.ases[0][1, 0]);

            Assert.AreEqual(0.3775, nn.zs[1][0,0]);
            Assert.IsTrue(CloseCompare(0.593269992, nn.ases[1][0, 0], eps));
            Assert.IsTrue(CloseCompare(0.596884378, nn.ases[1][1, 0], eps));

            Assert.IsTrue(CloseCompare(1.105905967, nn.zs[2][0,0], eps));
            Assert.IsTrue(CloseCompare(0.75136507, nn.ases[2][0, 0], eps));
            Assert.IsTrue(CloseCompare(0.772928465, nn.ases[2][1, 0], eps));

            Assert.IsTrue(CloseCompare(0.75136507, result[0], eps));
            Assert.IsTrue(CloseCompare(0.772928456, result[1], eps));

            nn.Train
                (
                new double[][] { new double[] { 0.05, 0.1 } },
                new double[][] { new double[] { 0.01, 0.99 } },
                1,
                1
                );

            Assert.IsTrue(CloseCompare(0.35891648, nn.weights[1][0, 0], eps));
            Assert.IsTrue(CloseCompare(0.408666186, nn.weights[1][0, 1], eps));
            Assert.IsTrue(CloseCompare(0.511301270, nn.weights[1][1, 0], eps));
            Assert.IsTrue(CloseCompare(0.561370121, nn.weights[1][1, 1], eps));

            Assert.IsTrue(CloseCompare(0.149780716, nn.weights[0][0,0], eps));
            Assert.IsTrue(CloseCompare(0.19956143, nn.weights[0][0,1], eps));
            Assert.IsTrue(CloseCompare(0.24975114, nn.weights[0][1,0], eps));
            Assert.IsTrue(CloseCompare(0.29950229, nn.weights[0][1,1], eps));

        }

        bool CloseCompare(double v1, double v2, double eps)
        {
            return Abs(v1 - v2) <= eps;
        }
    }
}
