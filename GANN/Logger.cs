using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GANN
{
    public class Logger
    {
        static string path = "log.txt";
        static StreamWriter sw;
        static Random random = new Random(1001);

        public static void Log(string id, string message)
        {
            if (sw == null)
                sw = new StreamWriter(path);

            sw.WriteLine(id + " - " + message);
            if (random.Next(15) == 0)
                sw.Flush();
        }
    }
}
