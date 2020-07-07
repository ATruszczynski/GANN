using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GANN
{
    public class Logger
    {
        //TODO - 0 - find first thing that change; write wg to ext array
        static string path = "log.txt";
        static StreamWriter sw;
        static Random random = new Random(1001);
        static object llcok = new object();

        public static void Log(string id, string message)
        {
            lock (llcok)
            {
                if (sw == null)
                    sw = new StreamWriter(path);

                sw.WriteLine(id + " - " + message);
                if (random.Next(15) == 0)
                    sw.Flush();
            }
        }

        public static void FlushClose()
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
}
