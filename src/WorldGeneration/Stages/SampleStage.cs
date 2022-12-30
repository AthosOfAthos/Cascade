using Cascade.src.WorldGeneration.Chunks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration.Stages
{
    internal class SampleStage : AbstractStage
    {
        public override void Initalize()
        {
            Initalize(typeof(SampleChunk), StageLevel.level1);
        }

        public override void LoadContent(GraphicsDeviceManager? _graphics)
        {
            //TODO - load the tileset
        }
    }
}
