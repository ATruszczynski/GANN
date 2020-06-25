using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.GA_Elements
{
    //TODO - C - ArrayChromosome class?
    public class BinaryChromosome : Chromosome
    {
        public bool[] Array;

        public BinaryChromosome(bool[] array)
        {
            Array = array;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Array.Length; i++)
            {
                result += (Array[i] == true) ? "1" : "0";
                if (i == Array.Length - 1)
                    result += ",";
            }

            return result;
        }

        public override Chromosome DeepCopy()
        {
            bool[] array = new bool[Array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Array[i];
            }
            BinaryChromosome bc = new BinaryChromosome(array);

            return bc;
        }
    }
}
