using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.NN.GradientStepStrategies
{
    public class MomentumStrategy : GradientStepStrategy
    {
        MatrixAT1[] WeightMom;
        MatrixAT1[] BiasMom;

        double MomDecay;
        double StepSize;

        public MomentumStrategy(double stepSize, double momDecay)
        {
            MomDecay = momDecay;
            StepSize = stepSize;
        }

        public override int CompareTo(object obj)
        {
            //TODO - A - test
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
            //TODO - A - test
            return new MomentumStrategy(StepSize, MomDecay);
        }

        public override (MatrixAT1[], MatrixAT1[]) GetStepSize(double avDiff, MatrixAT1[] updateW, MatrixAT1[] updateB)
        {
            //TODO - A - test
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

            for (int i = 1; i < updateW.Length; i++)
            {
                WeightMom[i] += StepSize * updateW[i];
                BiasMom[i] += StepSize * updateB[i];
            }

            return (WeightMom, BiasMom);
        }

        public override string ToString()
        {
            //TODO - A - test
            return $"MS{StepSize}-{MomDecay}";
        }
    }
}
