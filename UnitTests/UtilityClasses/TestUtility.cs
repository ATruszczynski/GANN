using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.UtilityClasses
{
    class TestUtility
    {
        public static bool CompareArrays<T>(T[] array1, params T[] array2) where T: IComparable
        {
            bool result = true;

            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i].CompareTo(array2[i]) != 0)
                    return false;
            }

            return result;
        }

    }
}
