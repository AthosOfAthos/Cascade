using Cascade.src.WorldGeneration.Chunks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration.Stages
{
    internal interface IStage
    {
        StageLevel level { get; }
        int height { get; }
        int width { get; }
        int chunkSize { get; }
        IChunk[,]? chunks { get; }

        void Initalize();
        void LoadContent(GraphicsDeviceManager? _graphics);
        void Draw(SpriteBatch? _spriteBatch, GameTime gameTime);
        void Update(GameTime gameTime);
    }

    public enum StageLevel
    {
        level1,
        level2,
        level3
    }
}
