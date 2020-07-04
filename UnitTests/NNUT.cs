using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GANN.NN;
using static GANN.MathAT.ActFuns;
using GANN.MathAT;
using static System.Math;
using GANN.NN.Parameters;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;

namespace UnitTests
{
    [TestClass]
    public class NNUT
    {
        //TODO - B - iterate over UTs
        [TestMethod]
        public void NNRun()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 2, 2 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu() }
                ));

            nn.Layers[1].weights = new MatrixAT1(new double[,] { { 1, 2 }, { 2, -1 } });
            nn.Layers[2].weights = new MatrixAT1(new double[,] { { 3, 0 }, { 1, 2 } });
            nn.Layers[1].biases = new MatrixAT1(new double[,] { { 1 }, { 0 } });
            nn.Layers[2].biases = new MatrixAT1(new double[,] { { 2 }, { -2 } });

            var resutl = nn.Run(new double[] { 2, 1 });

            Assert.AreEqual(17, resutl[0]);
            Assert.AreEqual(9, resutl[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NN_MismatchedInput()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 2, 2 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu() }
                ));

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
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 2, 2 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu() }
                ));

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
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 2, 2 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu() }
                ));

            nn.Layers[1].weights = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.Layers[2].weights = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.Layers[1].biases = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.Layers[2].biases = new MatrixAT1(new double[,] { { 1 }, { 1 } });

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

            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[1].weights, new MatrixAT1(new double[,] { { -67, -33 }, { -67, -33 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[2].weights, new MatrixAT1(new double[,] { { -63, -63 }, { -71, -71 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[1].biases, new MatrixAT1(new double[,] { { -33 }, { -33 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[2].biases, new MatrixAT1(new double[,] { { -15 }, { -17 } })));

            Assert.AreEqual(0, resutl[0]);
            Assert.AreEqual(0, resutl[1]);
        }
        
        [TestMethod]
        public void NNTrain_SingleInput2()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 1, 2 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu() }
                ));

            nn.Layers[1].weights = new MatrixAT1(new double[,] { { 1, 2 } });
            nn.Layers[2].weights = new MatrixAT1(new double[,] { { -1 }, { 3 } });
            nn.Layers[1].biases = new MatrixAT1(new double[,] { { -1 } });
            nn.Layers[2].biases = new MatrixAT1(new double[,] { { 2 }, { 1 } });

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

            //TODO - C - calculate manually scores (they are from debugging)
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[1].weights, new MatrixAT1(new double[,] { { -119, -58 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[2].weights, new MatrixAT1(new double[,] { { -1 }, { -57 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[1].biases, new MatrixAT1(new double[,] { { -61 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.Layers[2].biases, new MatrixAT1(new double[,] { { 2 }, { -19 } })));

            resutl = nn.Run(new double[] { 2, 1 });

            Assert.AreEqual(2, resutl[0]);
            Assert.AreEqual(0, resutl[1]);
        }

        [TestMethod]
        public void NNTrain_MultipleInputs()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 3, 2 },
                    actFuns: new ActivationFunction[] { new Relu(), new Relu() }
                ));

            //nn.normaliseOutput = false;
            nn.Layers[1].weights = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } });
            nn.Layers[2].weights = new MatrixAT1(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            nn.Layers[1].biases = new MatrixAT1(new double[,] { { 1 }, { 1 }, { 1 } });
            nn.Layers[2].biases = new MatrixAT1(new double[,] { { 1 }, { 1 } });

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

        [TestMethod]
        //Comparison with results here:
        //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
        public void SigmaComparison()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    new int[] { 2, 2, 2 },
                    actFuns: new ActivationFunction[] { new Sigma(), new Sigma() },
                    lossFunc: new QuadDiff(0.5)
                ));

            nn.GradientStepStrategy = new ConstantGradientStep(0.5);

            nn.Layers[1].weights = new MatrixAT1(new double[,] { { 0.15, 0.20 }, { 0.25, 0.3 } });
            nn.Layers[2].weights = new MatrixAT1(new double[,] { { 0.40, 0.45 }, { 0.50, 0.55 } });

            nn.Layers[1].biases = new MatrixAT1(new double[,] { { 0.35 }, { 0.35 } });
            nn.Layers[2].biases = new MatrixAT1(new double[,] { { 0.6 }, { 0.6 } });

            var input = new double[] { 0.05, 0.1 };

            var result = nn.Run(input);

            double eps = 0.00001;

            Assert.AreEqual(input[0], nn.Layers[0].ases[0, 0]);
            //Assert.AreEqual(input[1], nn.Layers[1].zs[1, 0]);
            //Assert.AreEqual(input[0], nn.Layers[1].zs[0, 0]);
            Assert.AreEqual(input[1], nn.Layers[0].ases[1, 0]);

            Assert.AreEqual(0.3775, nn.Layers[1].zs[0,0]);
            Assert.IsTrue(CloseCompare(0.593269992, nn.Layers[1].ases[0, 0], eps));
            Assert.IsTrue(CloseCompare(0.596884378, nn.Layers[1].ases[1, 0], eps));

            Assert.IsTrue(CloseCompare(1.105905967, nn.Layers[2].zs[0,0], eps));
            Assert.IsTrue(CloseCompare(0.75136507, nn.Layers[2].ases[0, 0], eps));
            Assert.IsTrue(CloseCompare(0.772928465, nn.Layers[2].ases[1, 0], eps));

            Assert.IsTrue(CloseCompare(0.75136507, result[0], eps));
            Assert.IsTrue(CloseCompare(0.772928456, result[1], eps));

            nn.Train
                (
                new double[][] { new double[] { 0.05, 0.1 } },
                new double[][] { new double[] { 0.01, 0.99 } },
                1,
                1
                );

            Assert.IsTrue(CloseCompare(0.35891648, nn.Layers[2].weights[0, 0], eps));
            Assert.IsTrue(CloseCompare(0.408666186, nn.Layers[2].weights[0, 1], eps));
            Assert.IsTrue(CloseCompare(0.511301270, nn.Layers[2].weights[1, 0], eps));
            Assert.IsTrue(CloseCompare(0.561370121, nn.Layers[2].weights[1, 1], eps));

            Assert.IsTrue(CloseCompare(0.149780716, nn.Layers[1].weights[0,0], eps));
            Assert.IsTrue(CloseCompare(0.19956143, nn.Layers[1].weights[0,1], eps));
            Assert.IsTrue(CloseCompare(0.24975114, nn.Layers[1].weights[1,0], eps));
            Assert.IsTrue(CloseCompare(0.29950229, nn.Layers[1].weights[1,1], eps));

        }

        public static bool CloseCompare(double v1, double v2, double eps)
        {
            //TODO - B - move to testing utility and test
            return Abs(v1 - v2) <= eps;
        }
    }
}
