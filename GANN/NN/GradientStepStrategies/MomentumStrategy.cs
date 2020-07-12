using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public class MomentumStrategy : GradientStepStrategy
    {
        public MatrixAT1[] WeightMom;
        public MatrixAT1[] BiasMom;

        public double MomDecay;
        public double StepSize;

        public MomentumStrategy(double stepSize, double momDecay)
        {
            MomDecay = momDecay;
            StepSize = stepSize;
        }

        public override int CompareTo(object obj)
        {
            MomentumStrategy ms = null;
            try
            {
                ms = (MomentumStrategy)obj;
            }
            catch
            {
                return int.MinValue;
            }

            int comp = MomDecay.CompareTo(ms.MomDecay);
            if (comp != 0)
                return comp;

            return StepSize.CompareTo(ms.StepSize);
        }

        public override GradientStepStrategy DeepCopy()
        {
            return new MomentumStrategy(StepSize, MomDecay);
        }

        public override (MatrixAT1[], MatrixAT1[]) GetStepSize(double avDiff, MatrixAT1[] updateW, MatrixAT1[] updateB)
        {
            if(WeightMom == null)
            {
                WeightMom = new MatrixAT1[updateW.Length];
                BiasMom = new MatrixAT1[updateB.Length];
                for (int i = 1; i < updateW.Length; i++)
                {
                    WeightMom[i] = new MatrixAT1(updateW[i].Rows, updateW[i].Columns);
                    BiasMom[i] = new MatrixAT1(updateB[i].Rows, updateB[i].Columns);
                }
            }
            else
            {
                for (int i = 1; i < WeightMom.Length; i++)
                {
                    WeightMom[i] = (1 - MomDecay) * WeightMom[i];
                    BiasMom[i] = (1 - MomDecay) * BiasMom[i];
                }
            }
            MatrixAT1[] resw = new MatrixAT1[WeightMom.Length];
            MatrixAT1[] resb = new MatrixAT1[WeightMom.Length];
            for (int i = 1; i < updateW.Length; i++)
            {
                WeightMom[i] += StepSize * updateW[i];
                resw[i] = WeightMom[i].DeepCopy();
                BiasMom[i] += StepSize * updateB[i];
                resb[i] = BiasMom[i].DeepCopy();
            }

            return (resw, resb);
        }

        public override string ToString()
        {
            return $"MS{StepSize}-{MomDecay}";
        }
    }
}
