﻿using Cascade.src.WorldGeneration.Chunks;
using Microsoft.Xna.Framework;
using Cascade.src.Turing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cascade.src.WorldGeneration.Stages
{
    internal class SampleStage : AbstractStage
    {
        private Turing.Turing2? turing;
        private double[,] res;
        public override void Initalize()
        {
            Initalize(typeof(SampleChunk), StageLevel.level1);
            int trueHeight = chunkSize * height;
            int trueWidth = chunkSize * width;
            turing = new(trueWidth, trueHeight);
           
 
            
            //Should do all of the atom indexing for the stage here 
        }

        public override void LoadContent(GraphicsDeviceManager? _graphics)
        {
            //TODO - load the tileset
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            res = turing?.PerfPattern(turing.GetScales()) ?? new double[height,width];
            //res = new double[chunkSize * chunkSize * height * width];
            Random rnd = new Random();
            for (int i = 0; i < res.Length; i++)
            {
                //res[i] = rnd.NextDouble();
            }
            for (int i = 0; i<chunks?.GetLength(0); i++)//height
            {
                for(int j=0; j<chunks.GetLength(1); j++)//width
                {

                    ((SampleChunk)chunks[i,j]).Notify(i*chunkSize,j*chunkSize, res);
                }
            }
            

        }
    }
}
