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
using UnitTests.UtilityClasses;
using System.Collections.Generic;
using System.Linq;
using static GANN.MathAT.TestGenerator;

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
                    2,
                    2,
                    new int[] { 2 },
                    intActFuns: new ActivationFunction[] { new Relu() },
                    aggregateActFunc: new Relu()
                ));

            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 2 }, { 2, -1 } });
            nn.weights[2] = new MatrixAT1(new double[,] { { 3, 0 }, { 1, 2 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 0 } });
            nn.biases[2] = new MatrixAT1(new double[,] { { 2 }, { -2 } });

            var resutl = nn.Run(new double[] { 2, 1 }, out _, out _);

            Assert.AreEqual(17, resutl[0]);
            Assert.AreEqual(9, resutl[1]);
        }

        //TODO - B - fix those tests
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void NN_MismatchedInput()
        //{
        //    ANN nn = new ANN(new Hyperparameters
        //        (
        //            new int[] { 2, 2, 2 },
        //            intActFuns: new ActivationFunction[] { new Relu(), new Relu() }
        //        ));

        //    nn.Train
        //         (
        //         new double[][] { new double[] { 2, 1, 3 } },
        //         new double[][] { new double[] { 1, 0 } },
        //         1,
        //         1
        //         );
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void NN_MismatchedOutput()
        //{
        //    ANN nn = new ANN(new Hyperparameters
        //        (
        //            new int[] { 2, 2, 2 },
        //            intActFuns: new ActivationFunction[] { new Relu(), new Relu() }
        //        ));

        //    nn.Train
        //         (
        //         new double[][] { new double[] { 2, 1 } },
        //         new double[][] { new double[] { 1, 0, 1 } },
        //         1,
        //         1
        //         );
        //}

        [TestMethod]
        public void NNTrain_SingleInput()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    2,
                    2,
                    new int[] { 2 },
                    intActFuns: new ActivationFunction[] { new Relu() },
                    aggregateActFunc: new Relu()
                ));

            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.weights[2] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.biases[2] = new MatrixAT1(new double[,] { { 1 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 }, out _, out _);

            Assert.AreEqual(9, resutl[0]);
            Assert.AreEqual(9, resutl[1]);

            nn.Train
                (
                new double[][] { new double[] { 2, 1 } },
                new double[][] { new double[] { 1, 0 } },
                1,
                1
                );

            resutl = nn.Run(new double[] { 2, 1 }, out _, out _);

            Assert.IsTrue(MatrixAT1.Compare(nn.weights[1], new MatrixAT1(new double[,] { { -67, -33 }, { -67, -33 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.weights[2], new MatrixAT1(new double[,] { { -63, -63 }, { -71, -71 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.biases[1], new MatrixAT1(new double[,] { { -33 }, { -33 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.biases[2], new MatrixAT1(new double[,] { { -15 }, { -17 } })));

            Assert.AreEqual(0, resutl[0]);
            Assert.AreEqual(0, resutl[1]);
        }
        
        [TestMethod]
        public void NNTrain_SingleInput2()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    2,
                    2,
                    new int[] { 1 },
                    intActFuns: new ActivationFunction[] { new Relu()},
                    aggregateActFunc: new Relu()
                ));

            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 2 } });
            nn.weights[2] = new MatrixAT1(new double[,] { { -1 }, { 3 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { -1 } });
            nn.biases[2] = new MatrixAT1(new double[,] { { 2 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 }, out _, out _);

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
            Assert.IsTrue(MatrixAT1.Compare(nn.weights[1], new MatrixAT1(new double[,] { { -119, -58 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.weights[2], new MatrixAT1(new double[,] { { -1 }, { -57 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.biases[1], new MatrixAT1(new double[,] { { -61 } })));
            Assert.IsTrue(MatrixAT1.Compare(nn.biases[2], new MatrixAT1(new double[,] { { 2 }, { -19 } })));

            resutl = nn.Run(new double[] { 2, 1 }, out _, out _);

            Assert.AreEqual(2, resutl[0]);
            Assert.AreEqual(0, resutl[1]);
        }

        [TestMethod]
        public void NNRun2()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    2,
                    2,
                    new int[] { 2 },
                    intActFuns: new ActivationFunction[] { new Relu() },
                    aggregateActFunc: new Sigma()
                ));

            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.weights[2] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.biases[2] = new MatrixAT1(new double[,] { { 1 }, { 1 } });

            var resutl = nn.Run(new double[] { 2, 1 }, out _, out _);
            var s = new Sigma();
            Assert.AreEqual(s.Compute(0, new MatrixAT1(new double[,] { { 9 } })), resutl[0]);
            Assert.AreEqual(s.Compute(0, new MatrixAT1(new double[,] { { 9 } })), resutl[1]);
        }

        [TestMethod]
        public void NNTrain_MultipleInputs()
        {
            //TODO - B - develop
            ANN nn = new ANN(new Hyperparameters
                (
                    2,
                    2,
                    new int[] { 3 },
                    intActFuns: new ActivationFunction[] { new Relu()},
                    aggregateActFunc: new Relu()
                ));

            //nn.normaliseOutput = false;
            nn.weights[1] = new MatrixAT1(new double[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } });
            nn.weights[2] = new MatrixAT1(new double[,] { { 1, 1, 1 }, { 1, 1, 1 } });
            nn.biases[1] = new MatrixAT1(new double[,] { { 1 }, { 1 }, { 1 } });
            nn.biases[2] = new MatrixAT1(new double[,] { { 1 }, { 1 } });
            nn.masDeg = 1;
            nn.Train
                (
                new double[][] { new double[] { 2, 1 }, new double[] { 1, 2 }, new double[] { 3, 4 } },
                new double[][] { new double[] { 1, 0 }, new double[] { 0, 1 }, new double[] { 0, 1 } },
                2,
                2
                );

            var res = nn.Run(new double[] { 1, 1 }, out _, out _);
            ;
        }

        [TestMethod]
        //Comparison with results here:
        //https://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/
        public void SigmaComparison()
        {
            ANN nn = new ANN(new Hyperparameters
                (
                    2,
                    2,
                    new int[] { 2 },
                    intActFuns: new ActivationFunction[] { new Sigma() },
                    aggregateActFunc: new Sigma(),
                    lossFunc: new QuadDiff(0.5)
                ));

            nn.GradientStepStrategy = new ConstantGradientStep(0.5);

            nn.weights[1] = new MatrixAT1(new double[,] { { 0.15, 0.20 }, { 0.25, 0.3 } });
            nn.weights[2] = new MatrixAT1(new double[,] { { 0.40, 0.45 }, { 0.50, 0.55 } });

            nn.biases[1] = new MatrixAT1(new double[,] { { 0.35 }, { 0.35 } });
            nn.biases[2] = new MatrixAT1(new double[,] { { 0.6 }, { 0.6 } });

            var input = new double[] { 0.05, 0.1 };

            var result = nn.Run(input, out MatrixAT1[] ases, out MatrixAT1[] zs);

            double eps = 0.00001;

            Assert.AreEqual(input[0], ases[0][0, 0]);
            //Assert.AreEqual(input[1], zs[1][1, 0]);
            //Assert.AreEqual(input[0], zs[1][0, 0]);
            Assert.AreEqual(input[1], ases[0][1, 0]);

            Assert.AreEqual(0.3775, zs[1][0,0]);
            Assert.IsTrue(CloseCompare(0.593269992, ases[1][0, 0], eps));
            Assert.IsTrue(CloseCompare(0.596884378, ases[1][1, 0], eps));

            Assert.IsTrue(CloseCompare(1.105905967, zs[2][0,0], eps));
            Assert.IsTrue(CloseCompare(0.75136507, ases[2][0, 0], eps));
            Assert.IsTrue(CloseCompare(0.772928465, ases[2][1, 0], eps));

            Assert.IsTrue(CloseCompare(0.75136507, result[0], eps));
            Assert.IsTrue(CloseCompare(0.772928456, result[1], eps));

            nn.Train
                (
                new double[][] { new double[] { 0.05, 0.1 } },
                new double[][] { new double[] { 0.01, 0.99 } },
                1,
                1
                );

            Assert.IsTrue(CloseCompare(0.35891648, nn.weights[2][0, 0], eps));
            Assert.IsTrue(CloseCompare(0.408666186, nn.weights[2][0, 1], eps));
            Assert.IsTrue(CloseCompare(0.511301270, nn.weights[2][1, 0], eps));
            Assert.IsTrue(CloseCompare(0.561370121, nn.weights[2][1, 1], eps));

            Assert.IsTrue(CloseCompare(0.149780716, nn.weights[1][0,0], eps));
            Assert.IsTrue(CloseCompare(0.19956143, nn.weights[1][0,1], eps));
            Assert.IsTrue(CloseCompare(0.24975114, nn.weights[1][1,0], eps));
            Assert.IsTrue(CloseCompare(0.29950229, nn.weights[1][1,1], eps));

        }

        public static bool CloseCompare(double v1, double v2, double eps)
        {
            //TODO - B - move to testing utility and test
            return Abs(v1 - v2) <= eps;
        }

        [TestMethod]
        public void NNFromHPNoInternalLayers()
        {
            PseudoRandom pr = new PseudoRandom(0);
            Hyperparameters hp = new Hyperparameters(2,2, mw: 1);
            ANN nn = new ANN(hp, pr);

            Assert.AreEqual(2, nn.neuronCounts.Length);
            Assert.AreEqual(2, nn.neuronCounts[0]);
            Assert.AreEqual(2, nn.neuronCounts[1]);

            Assert.AreEqual(2, nn.activationFuncs.Length);
            Assert.IsNull(nn.activationFuncs[0]);
            Assert.AreEqual(0, nn.activationFuncs[1].CompareTo(new Sigma()));

            Assert.AreEqual(2, nn.weights.Length);
            Assert.IsNull(nn.weights[0]);
            Assert.AreEqual(2, nn.weights[1].Rows);
            Assert.AreEqual(2, nn.weights[1].Columns);
            Assert.AreEqual(1, nn.weights[1][0,0]);
            Assert.AreEqual(1, nn.weights[1][0,1]);
            Assert.AreEqual(1, nn.weights[1][1,0]);
            Assert.AreEqual(1, nn.weights[1][1,1]);

            Assert.AreEqual(2, nn.biases.Length);
            Assert.IsNull(nn.biases[0]);
            Assert.AreEqual(2, nn.biases[1].Rows);
            Assert.AreEqual(1, nn.biases[1].Columns);
            Assert.AreEqual(0, nn.biases[1][0, 0]);
            Assert.AreEqual(0, nn.biases[1][1, 0]);

            Assert.AreEqual(0, nn.LossFunction.CompareTo(new QuadDiff()));
            Assert.AreEqual(0, nn.GradientStepStrategy.CompareTo(new ConstantGradientStep()));
        }

        [TestMethod]
        public void NNFromHP1InternalLayer()
        {
            PseudoRandom pr = new PseudoRandom(0);
            Hyperparameters hp = new Hyperparameters(2, 2, new int[] { 1 }, new ActivationFunction[] { new Sigma() }, new Relu(), new QuadDiff(22), new ConstantGradientStep(0.5), 1, 1);
            ANN nn = new ANN(hp, pr);

            Assert.AreEqual(3, nn.neuronCounts.Length);
            Assert.AreEqual(2, nn.neuronCounts[0]);
            Assert.AreEqual(1, nn.neuronCounts[1]);
            Assert.AreEqual(2, nn.neuronCounts[2]);

            Assert.AreEqual(3, nn.activationFuncs.Length);
            Assert.IsNull(nn.activationFuncs[0]);
            Assert.AreEqual(0, nn.activationFuncs[1].CompareTo(new Sigma()));
            Assert.AreEqual(0, nn.activationFuncs[2].CompareTo(new Relu()));

            Assert.AreEqual(3, nn.weights.Length);
            Assert.IsNull(nn.weights[0]);
            Assert.AreEqual(1, nn.weights[1].Rows);
            Assert.AreEqual(2, nn.weights[1].Columns);
            Assert.AreEqual(2, nn.weights[2].Rows);
            Assert.AreEqual(1, nn.weights[2].Columns);
            Assert.AreEqual(1, nn.weights[1][0, 0]);
            Assert.AreEqual(1, nn.weights[1][0, 1]);
            Assert.AreEqual(1, nn.weights[2][0, 0]);
            Assert.AreEqual(1, nn.weights[2][1, 0]);

            Assert.AreEqual(3, nn.biases.Length);
            Assert.IsNull(nn.biases[0]);
            Assert.AreEqual(1, nn.biases[1].Rows);
            Assert.AreEqual(1, nn.biases[1].Columns);
            Assert.AreEqual(2, nn.biases[2].Rows);
            Assert.AreEqual(1, nn.biases[2].Columns);
            Assert.AreEqual(0, nn.biases[1][0, 0]);
            Assert.AreEqual(0, nn.biases[2][0, 0]);
            Assert.AreEqual(0, nn.biases[2][1, 0]);

            Assert.AreEqual(0, nn.LossFunction.CompareTo(new QuadDiff(22)));
            Assert.AreEqual(0, nn.GradientStepStrategy.CompareTo(new ConstantGradientStep(0.5)));
        }

        [TestMethod]
        public void TestFuncTest()
        {
            MockANN mann = new MockANN();
            var outputs = new double[][]
            {
                new double[] { 1, 0, 0},
                new double[] { 1, 0, 0},
                new double[] { 1, 0, 0},
                new double[] { 0, 1, 0},
                new double[] { 0, 1, 0},
                new double[] { 0, 1, 0},
                new double[] { 0, 0, 1},
                new double[] { 0, 0, 1},
                new double[] { 0, 0, 1},
            };

            mann.runResults = new double[][]
            {
                new double[] { 0, 0.9, 0.2},
                new double[] { 0.1, 0.45, 1},
                new double[] { 0.4, 0, 1},
                new double[] { 0.2, 1, 0},
                new double[] { 0.9999, 1, 0},
                new double[] { 0, 0, 1},
                new double[] { 1, 0, 0.23},
                new double[] { 0, 0.5, 1},
                new double[] { 0, 0, 1},
            };

            mann.Test2(mann.runResults, outputs);

        }

        [TestMethod]
        public void DeteminismTest()
        {
            Random random = new Random(1001);
            (var trainInput, var trainOutput) = TestGenerator.CountIO(500, 10, random);
            (var testInput, var testOutput) = TestGenerator.CountIO(50, 10,  random);
            //(var trainInput, var trainOutput) = TestGenerator.GenerateReverseIO(10, random);
            //(var testInput, var testOutput) = TestGenerator.GenerateReverseIO(50, random);

            ANN nn;// = new ANN(new Hyperparameters(10, 11, new int[] { 100 }, mw: 0), random);
            //nn.masDeg = 1;

            List<List<double[]>> listRunResults = new List<List<double[]>>();
            var listTestResults = new List<List<double>>();

            int trainCount = 10;
            int testCount = 10;

            for (int i = 0; i < testCount; i++)
            {
                listRunResults.Add(new List<double[]>());
                listTestResults.Add(new List<double>());
            }

            for (int i = 0; i < trainCount; i++)
            {
                random = new Random(1001);
                nn = new ANN(new Hyperparameters(10, 11, new int[] { 100 }, mw: 0), random);
                nn.Train(trainInput, trainOutput, 5, 50);
                for (int j = 0; j < testCount; j++)
                {
                    listRunResults[i].Add(nn.Run(testInput[0], out _, out _));
                    listTestResults[i].Add(nn.Test(testInput, testOutput/*, "countconfusionmatrix.txt", "log2.txt"*/).Average());
                }
            }

            bool testOk = true;
            for (int i = 0; i < listTestResults.Count && testOk; i++)
            {
                var testResults = listTestResults[i];
                for (int j = 1; j < testResults.Count && testOk; j++)
                {
                    Assert.IsTrue(NNUT.CloseCompare(testResults[j - 1], testResults[j], 1e-6));
                }
            }

            for (int i = 1; i < listTestResults.Count && testOk; i++)
            {
                var testResult1 = listTestResults[i - 1];
                var testResult2 = listTestResults[i];
                for (int j = 0; j < testResult1.Count && testOk; j++)
                {
                    Assert.IsTrue(NNUT.CloseCompare(testResult1[j], testResult2[j], 1e-6));
                }
            }


            bool runOk = true;
            for (int i = 0; i < listRunResults.Count && runOk; i++)
            {
                var runResults = listRunResults[i];
                for (int j = 1; j < runResults.Count && runOk; j++)
                {
                    var runResult1 = runResults[j - 1];
                    var runResult2 = runResults[j];
                    for (int k = 0; k < runResult1.Length && runOk; k++)
                    {
                        Assert.IsTrue(NNUT.CloseCompare(runResult1[k], runResult2[k], 1e-6));
                    }
                }
            }

            for (int i = 1; i < listRunResults.Count && runOk; i++)
            {
                var runResult1 = listRunResults[i - 1];
                var runResult2 = listRunResults[i];
                for (int j = 0; j < runResult1.Count && runOk; j++)
                {
                    Assert.IsTrue(NNUT.CloseCompare(runResult1[j][0], runResult2[j][0], 1e-6));
                }
            }

        }

        [TestMethod]
        public void NNTestTest()
        {
            Random random = new Random(1001);
            (var trainInput, var trainOutput) = CountIO(10, 3, random);
            (var testInput, var testOutput) = CountIO(200, 3, random);
            //(var trainInput, var trainOutput) = GenerateReverseIO(500, random);
            //(var testInput, var testOutput) = GenerateReverseIO(200, random);

            int networsk = 1;
            int reps = 1;

            ANN nn = null;
            for (int i = 0; i < networsk; i++)
            {
                nn = new ANN(new Hyperparameters(testInput[0].Length, testOutput[0].Length, inNeuronCounts: new int[] { 8 }));
                nn.weights[1] = new MatrixAT1(new double[,] { { -1, -1, -1 },
                                                              { -1, -1, 1 },
                                                              { -1, 1, -1 },
                                                              { -1, 1, 1 },
                                                              { 1, -1, -1 },
                                                              { 1, -1, 1 },
                                                              { 1, 1, -1 },
                                                              { 1, 1, 1 },
                });

                nn.biases[1] = new MatrixAT1(new double[,] {
                                                              { 1 },
                                                              { 0 },
                                                              { 0 },
                                                              { -1 },
                                                              { 0 },
                                                              { -1 },
                                                              { -1 },
                                                              { -2 },
                });
                nn.weights[2] = new MatrixAT1(new double[,] { { 1,   0,   0,   0,   0,   0,   0,   0},
                                                              { 0 ,  1 ,  1  , 0 ,  1 ,  0 ,  0 ,  0},
                                                              { 0 ,  0 ,  0,   1,   0,   1,   1,   0},
                                                              { 0 ,  0 ,  0,   0,   0,   0,   0,   1},

                });

                nn.biases[2] = new MatrixAT1(new double[,] {
                                                              { 0 },
                                                              { 0 },
                                                              { 0 },
                                                              { 0 }
                });

                Assert.AreEqual(1d, nn.Test(testInput, testOutput).Average());
            }
        }

        [TestMethod]
        public void CountTest()
        {
            Random random = new Random(1001);
            (var trainInput, var trainOutput) = CountIO(1000, 3, random);
            (var testInput, var testOutput) = CountIO(200, 3, random);
            //(var trainInput, var trainOutput) = GenerateReverseIO(500, random);
            //(var testInput, var testOutput) = GenerateReverseIO(200, random);

            int networsk = 1;
            int reps = 1;

            ANN nn = null;
            for (int i = 0; i < networsk; i++)
            {
                random = new Random(1001);
                nn = new ANN(new Hyperparameters(trainInput[0].Length, trainOutput[0].Length, inNeuronCounts: new int[] { 8 }, gradStep: new ConstantGradientStep(0.01)), random);

                nn.Train(trainInput, trainOutput, 10, 50);
            }
            Assert.AreEqual(1d, nn.Test(testInput, testOutput/*, "countconfusionmatrix.txt", "log2.txt"*/).Average());
        }
    }
}
