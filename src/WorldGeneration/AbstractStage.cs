using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration
{
    internal abstract class AbstractStage : IStage
    {
        public StageLevel level { get; set; }

        public int height => 16;

        public int width => 16;

        //TODO this variable needs to be communicated to the chunk
        public int chunkSize => 16;

        public IChunk[][]? chunks { get; set; }

        public abstract void Initalize();

        protected void Initalize(Type chunkType, StageLevel level)
        {
            this.level = level;
            try
            {
                chunks = new IChunk[height][];
                for (int y = 0; y < height; y++)
                {
                    chunks[y] = new IChunk[width];
                    for (int x = 0; x < width; x++)
                    {
                        chunks[y][x] = Activator.CreateInstance(chunkType) as IChunk ?? throw new Exception();
                        chunks[y][x]?.Intialize(x, y);
                    }
                }
            }catch(Exception ex)
            {
                throw;
            }
            
        }

        public abstract void LoadContent(GraphicsDeviceManager? _graphics);

        public void Draw(SpriteBatch? _spriteBatch, GameTime gameTime)
        {
            for (int y = 0; y < chunks?.Length; y++)
            {
                for (int x = 0; x < chunks[y].Length; x++)
                {
                    chunks[y][x].Draw(_spriteBatch,gameTime, x, y);
                }
            }

        }

        public void Update(GameTime gameTime)
        {
            for (int y = 0; y < chunks?.Length; y++) {
                for (int x = 0; x < chunks[y].Length; x++)
                {
                    chunks[y][x].Update(gameTime, x,y);
                }
            }
        }
    }
}
