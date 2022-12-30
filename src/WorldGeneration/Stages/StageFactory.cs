using Cascade.src.WorldGeneration.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration.Stages
{
    internal class StageFactory : IStageFactory
    {
        public IStage GenerateStage()
        {
            return new SampleStage();
        }

        public void Initialize(Game game)
        {
            TileIndex.Initalize(game);
        }
    }
}
