using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Cascade.src.Turing
{
    public class Turing
    {
        private readonly int width;
        private readonly int height;
        private readonly int size;

        private double[] pattern;
        Stopwatch stopwatch;
        int callcount;

        public Turing(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.size = width * height;
            this.pattern = new double[size];
            InitalizePattern();
            callcount = 0;
            stopwatch = Stopwatch.StartNew();
            
        }
        public List<Scale> GetScales()
        {
            List<Scale> scales = new()
            {
                //new Scale(100,200,0.5),
                new Scale(25,50,0.05),
                new Scale(10,200,0.05),
                new Scale(15,100,0.05),
            };
            return scales;
        }

        public double[] PerfPattern(List<Scale> scales)
        {
            NextPattern(scales).GetAwaiter().GetResult();
            callcount += 1;
            long perf = stopwatch.ElapsedMilliseconds / callcount;
            return pattern;
        }
        public async Task<double[]> NextPattern(List<Scale> scales)
        {
            
            List<Task<double[]>> scaleVariationTasks = new List<Task<double[]>>();
            List<double[]> scaleVariations = new List<double[]>();
            foreach (Scale scale in scales)
            {
                double[] patternCopy = new double[size];
                pattern.CopyTo(patternCopy, 0);
                Task < double[]> scaleVariationTask = NextPattern(scale, patternCopy); 
                scaleVariationTasks.Add(scaleVariationTask);
            }
            foreach (Task<double[]> scaleVariationTask in scaleVariationTasks)
            {
                scaleVariations.Add(await scaleVariationTask);
            }
            double[] combinedVariation = new double[size];
            double[] combinedIncrement = new double[size];
            foreach (double[] scaleVariation in scaleVariations)
            {
                double increment = scales.ElementAt(scaleVariations.IndexOf(scaleVariation)).Increment;
                for (int i = 0; i < size; i++)
                {
                    if (scaleVariation[i] > combinedVariation[i])
                    {
                        combinedVariation[i] = scaleVariation[i];
                        combinedIncrement[i] = scaleVariation[i] > 0 ? increment : -increment;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                pattern[i] += combinedIncrement[i];
            }
            NormalizePattern();
            return pattern;

        }
        /*
        public async Task<double[]> NextPatternOld(List<Scale> scales)
        {
            double[] activators;
            double[] inhibitors;
            double[] variations = new double[size];
            double[] increments = new double[size];
            foreach (Scale scale in scales)
            {
                Task<double[]> inhibit = Blur(scale.InhibitorRadius, pattern);
                Task<double[]> activate = Blur(scale.ActivatorRadius, pattern);
                inhibitors = await inhibit;
                activators = await activate;
                for (int i = 0; i < size; i++)
                {
                    double variation = activators[i] - inhibitors[i];
                    if (scale == scales.First() || Math.Abs(variation) > Math.Abs(variations[i]))
                    {
                        variations[i] = variation;
                        increments[i] = variation > 0 ? scale.Increment : -scale.Increment;
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                pattern[i] += increments[i];
            }
            NormalizePattern();
            return pattern;
        }*/

        public async Task<double[]> NextPattern(Scale scale, double[] patternCopy)
        {
            double[] activators;
            double[] inhibitors;
            double[] variations = new double[size];
            Task<double[]> inhibit = Task.Run(() => Blur(scale.InhibitorRadius, patternCopy));
            Task<double[]> activate = Task.Run(() => Blur(scale.ActivatorRadius, patternCopy));
            inhibitors = await inhibit;
            activators = await activate;
            for (int i = 0; i < size; i++)
            {
                double variation = activators[i] - inhibitors[i];
                variations[i] = variation;
            }
            return variations;

        }

        private void NormalizePattern()
        {
            double min = pattern.Min();
            double max = pattern.Max();
            double range = max - min;
            for (int i = 0; i < size; i++)
            {
                pattern[i] = (pattern[i] - min) / range;
            }
        }

        private void InitalizePattern()
        {
            //This is doing random right now, but it might be interesting to change
            Random random = new();
            for (int i = 0; i < size; i++)
            {
                pattern[i] = random.NextDouble();
            }
            for (int i = 0; i < size; i++)
            {
                if (i % 2 == 0)
                {
                    pattern[i] = 1;
                }
            }
        }
        private double[] Blur(double radius, double[] pattern)
        {

            double[] destination = new double[size];
            double[] partial = new double[size];
            BlurHorizontal(radius, pattern, ref partial);
            BlurVertial(radius, partial, ref destination);
            return destination;
        }
 
        private void BlurHorizontal(double radius, double[] pattern, ref double[] destination)
        {
            for (int y = 0; y < height; y++)
            {
                double sum = 0;
                double span = radius + 1;
                for (int x = 0; x <= radius; x++)
                {
                    sum += pattern[x + y * width];
                }
                destination[y * width] = sum / span;
                for (int x = 0; x < width; x++)
                {
                    if (x + radius < width)
                    {
                        sum += pattern[x + (int)radius + y * width];
                    }
                    else
                    {
                        span--;
                    }

                    if (x - radius - 1 > 0)
                    {
                        sum -= pattern[x - (int)radius - 1 + y * width];
                    }
                    else
                    {
                        span++;
                    }
                    destination[x + y * width] = sum / span;
                }
            }
            return;
        }
        private double[] BlurVertial(double radius, double[] pattern, ref double[] destination)
        {
            for (int x = 0; x < width; x++)
            {
                double sum = 0;
                double span = radius + 1;
                for (int y = 0; y <= radius; y++)
                {
                    sum += pattern[x + y * width];
                }
                destination[x] = sum / span;

                for (int y = 0; y < height; y++)
                {
                    if (y + radius < height)
                    {
                        sum += pattern[x + ((int)radius + y) * width];
                    }
                    else
                    {
                        span--;
                    }

                    if (y - radius - 1 > 0)
                    {
                        sum -= pattern[x + (y - (int)radius - 1) * width];
                    }
                    else
                    {
                        span++;
                    }
                    destination[x + y * width] = sum / span;
                }
            }

            return destination;
        }
    }
}
