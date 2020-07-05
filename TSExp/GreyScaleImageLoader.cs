using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TSExp
{
    class GreyScaleImageLoader
    {
        public static (double[][] trainInputs, double[][] trainOutputs, double[][] testInputs, double[][] testOutputs) LoadToGreyScale(string path)
        {
            Random random = new Random(1001);
            List<double[]> trainInputs = new List<double[]>();
            List<double[]> trainOutputs = new List<double[]>();
            List<double[]> testInputs = new List<double[]>();
            List<double[]> testOutputs = new List<double[]>();

            var directories = Directory.GetDirectories(path);

            foreach(var dir in directories)
            {
                int num = int.Parse(Path.GetFileName(dir));
                Parallel.ForEach(Directory.GetFiles(dir), file => 
                {
                //foreach(var file in Directory.GetFiles(dir))
                //{
                    if (random.Next(1) == 0)
                    {
                        Bitmap img = new Bitmap(file);
                        int wid = img.Width;
                        int hei = img.Height;
                        if (wid * hei != 10000)
                            return;
                        double[] inp = new double[wid * hei];
                        for (int w = 0; w < wid; w++)
                        {
                            for (int h = 0; h < hei; h++)
                            {
                                inp[w + h * wid] = ToGrey01(img.GetPixel(w, h));
                            }
                        }
                        double[] outt = new double[10];
                        outt[num] = 1;
                        if (random.Next(10) == 0)
                        {
                            testInputs.Add(inp);
                            testOutputs.Add(outt);
                        }
                        else
                        {
                            trainInputs.Add(inp);
                            trainOutputs.Add(outt);
                        }
                    }
                });
        }

            return (trainInputs.ToArray(), trainOutputs.ToArray(), testInputs.ToArray(), testOutputs.ToArray());
        }

        public static double ToGrey01(Color color)
        {
            return (color.R + color.G + color.B) / (3*255d);
        }
    }
}
