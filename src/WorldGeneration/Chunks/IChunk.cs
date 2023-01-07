using Cascade.src.WorldGeneration.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration.Chunks
{
    internal interface IChunk
    {
        string[,]? Tiles { get; }
        bool isDirty { get; set; }
        void Intialize(int size, int x, int y);
        void Update(GameTime gameTime, int x, int y);

        void Draw(SpriteBatch? _spriteBatch, GameTime gametime, int x, int y);
    }
}
