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
    internal abstract class AbstractStage : IStage
    {
        public StageLevel level { get; set; }

        public int height => 8;

        public int width => 8;

        public int chunkSize => 32;

        public IChunk[,]? chunks { get; set; }

        public abstract void Initalize();

        protected void Initalize(Type chunkType, StageLevel level)
        {
            this.level = level;
            chunks = new IChunk[height,width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    chunks[y,x] = Activator.CreateInstance(chunkType) as IChunk ?? throw new Exception();
                    chunks[y,x]?.Intialize(chunkSize, x, y);
                }
            }


        }

        public abstract void LoadContent(GraphicsDeviceManager? _graphics);

        public virtual void Draw(SpriteBatch? _spriteBatch, GameTime gameTime)
        {
            _spriteBatch?.Begin();
            for (int y = 0; y < chunks?.GetLength(0); y++)
            {
                for (int x = 0; x < chunks.GetLength(1); x++)
                {
                    //Around here camera needs to be passed in
                    chunks[y,x].Draw(_spriteBatch, gameTime, x, y);
                }
            }
            _spriteBatch?.End();

        }

        public virtual void Update(GameTime gameTime)
        {
            for (int y = 0; y < chunks?.GetLength(0); y++)
            {
                for (int x = 0; x < chunks.GetLength(1); x++)
                {
                    chunks[y,x].Update(gameTime, x, y);
                }
            }
        }
    }
}
