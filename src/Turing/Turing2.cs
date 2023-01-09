using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cascade.src.Turing
{
    public class Turing2
    {
        private double[,] pattern;
        readonly int height;
        readonly int width;

        public Turing2(int height, int width)
        {
            this.height = height;
            this.width = width;
            pattern = new double[height,width];
            InitalizePattern();
            
        }
        public List<Scale> GetScales()
        {
            List<Scale> scales = new()
            {
                new Scale(100,100,0.1),
                new Scale(25,50,0.05),
                //new Scale(10,200,0.05),
                new Scale(15,100,0.05),
            };
            return scales;
        }

        public double[,] PerfPattern(List<Scale> scales)
        {
            NextPattern(scales).GetAwaiter().GetResult();
            return pattern;
        }
        public async Task<double[,]> NextPattern(List<Scale> scales)
        {
            
            List<Task<double[,]>> scaleVariationTasks = new();
            List<double[,]> scaleVariations = new();
            for (int i = 0; i < scales.Count; i++)
            {
                Scale scale = scales[i];
                scaleVariationTasks.Add(NextPattern(scale, pattern));
            }
            for (int i = 0; i < scaleVariationTasks.Count; i++)
            {
                Task<double[,]> scaleVariationTask = scaleVariationTasks[i];
                scaleVariations.Add(await scaleVariationTask);
            }
            double[,] variations = new double[height,width];
            double[,] increments = new double[height,width];
            for (int i = 0; i < scaleVariations.Count; i++)
            {
                double[,] scaleVariation = scaleVariations[i];
                double increment = scales[i].Increment;
                for (int y = 0; y < scaleVariation.GetLength(0); y++)
                {
                    for (int x = 0; x < scaleVariation.GetLength(1); x++)
                    {
                        if (scaleVariation[y,x] > variations[y,x])
                        {
                            variations[y, x] = scaleVariation[y, x];
                            increments[y,x] = scaleVariation[y, x] > 0 ? increment : -increment;
                        }
                    }
                }
            }

            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    pattern[y, x] += increments[y, x];
                }
            }
            NormalizePattern();
            return pattern;

        }

        public async Task<double[,]> NextPattern(Scale scale, double[,] pattern)
        {
            double[,] activators;
            double[,] inhibitors;
            double[,] variations = new double[height,width];
            Task<double[,]> inhibit = Task.Run(() => Blur(scale.InhibitorRadius, pattern));
            Task<double[,]> activate = Task.Run(() => Blur(scale.ActivatorRadius, pattern));
            inhibitors = await inhibit;
            activators = await activate;
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    variations[y,x] = activators[y,x] - inhibitors[y,x];
                }
            }
            return variations;

        }

        private void NormalizePattern()
        {
            double min = 1;
            double max = 0;
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    max = pattern[y, x] < max ? max : pattern[y, x];
                    min = pattern[y, x] > min ? min : pattern[y, x];
                }
            }
            double range = max - min;
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    pattern[y, x] = (pattern[y, x] -min) / range;
                }
            }
        }

        private void InitalizePattern()
        {
            //This is doing random right now, but it might be interesting to change
            Random random = new();
            for(int y = 0; y<pattern.GetLength(0); y++)
            {
                for (int x = 0; x<pattern.GetLength(1); x++)
                {
                    pattern[y,x] = random.NextDouble();
                }
            }
        }
        private double[,] Blur(int radius, double[,] pattern)
        {

            double[,] destination = new double[height,width];
            double[,] partial = new double[height,width];
            BlurHorizontal(radius, pattern, ref partial);
            BlurVertial(radius, partial, ref destination);
            return destination;
        }
 
        private void BlurHorizontal(int radius, double[,] pattern, ref double[,] destination)
        {
            for (int y = 0; y < pattern.GetLength(0); y++)
            {
                double sum = 0;
                double span = radius + 1;
                for (int x = 0; x <= radius; x++)
                {
                    sum += pattern[y,x];
                }
                destination[y,0] = sum / span;
                for (int x = 0; x < pattern.GetLength(1); x++)
                {
                    if (x + radius < width)
                    {
                        sum += pattern[y, x + radius];
                    }
                    else
                    {
                        span--;
                    }

                    if (x - radius - 1 > 0)
                    {
                        sum -= pattern[y, x - radius - 1];
                    }
                    else
                    {
                        span++;
                    }
                    destination[y,x] = sum / span;
                }
            }
            return;
        }
        private double[,] BlurVertial(int radius, double[,] pattern, ref double[,] destination)
        {
            for (int x = 0; x < pattern.GetLength(1); x++)
            {
                double sum = 0;
                double span = radius + 1;
                for (int y = 0; y <= radius; y++)
                {
                    sum += pattern[y,x];
                }
                destination[0,x] = sum / span;

                for (int y = 0; y < pattern.GetLength(0); y++)
                {
                    if (y + radius < height)
                    {
                        sum += pattern[radius + y,x];
                    }
                    else
                    {
                        span--;
                    }

                    if (y - radius - 1 > 0)
                    {
                        sum -= pattern[y - radius - 1,x];
                    }
                    else
                    {
                        span++;
                    }
                    destination[y,x] = sum / span;
                }
            }

            return destination;
        }
    }
}
