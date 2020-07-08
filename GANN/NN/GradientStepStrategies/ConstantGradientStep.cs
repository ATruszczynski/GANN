using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public class ConstantGradientStep : GradientStepStrategy
    {
        public double stepSize = 1;

        public ConstantGradientStep(double ss = 1)
        {
            stepSize = ss;
        }

        public override GradientStepStrategy DeepCopy()
        {
            return new ConstantGradientStep(stepSize);
        }

        
        public override int CompareTo(object obj)
        {
            ConstantGradientStep tmp;
            try
            {
                tmp = (ConstantGradientStep)obj;
            }
            catch
            {
                return int.MinValue;
            }
            return tmp.stepSize.CompareTo(stepSize);
        }

        public override string ToString()
        {
            return $"CS{stepSize}";
        }

        public override (MatrixAT1[], MatrixAT1[]) GetStepSize(double avDiff, MatrixAT1[] updateW, MatrixAT1[] updateB)
        {
            MatrixAT1[] resW = new MatrixAT1[updateW.Length];
            MatrixAT1[] resB = new MatrixAT1[updateB.Length];
            for (int i = 1; i < updateW.Length; i++)
            {
                resW[i] = stepSize * updateW[i];
                resB[i] = stepSize * updateB[i];
            }
            return (resW, resB);
        }
    }
}
