using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Cascade.src.Turing
{
    public class Turing
    {
        private readonly int width;
        private readonly int height;
        private readonly int size;

        private double[] pattern;

        public Turing(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.size = width * height;
            this.pattern = new double[size];
            InitalizePattern();
        }
        public List<Scale> GetScales()
        {
            List<Scale> scales = new()
            {
                new Scale(10,200,0.05),
                new Scale(10,200,0.05),
                new Scale(10,200,0.05),
            };
            return scales;
        }

        public async Task<double[]> NextPattern(List<Scale> scales)
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
        }
        private async Task<double[]> Blur(double radius, double[] pattern)
        {
            double[] partial = await BlurHorizontal(radius, pattern);
            return await BlurVertial(radius, partial);
        }
        private async Task<double[]> BlurHorizontal(double radius, double[] pattern)
        {
            double[] destination = new double[size];
            for (int y = 0; y < height; y++)
            {
                double sum = 0;
                double span = radius + 1;
                for (int x = 0; x <= radius; x++)
                {
                    Console.WriteLine(x + y * width);
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
            await Task.Run(() => { });
            return destination;
        }
        private async Task<double[]> BlurVertial(double radius, double[] pattern)
        {
            double[] destination = new double[size];
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
            await Task.Run(() => { });
            return destination;
        }
    }
}
