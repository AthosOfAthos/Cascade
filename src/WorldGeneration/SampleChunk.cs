using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration
{
    internal class SampleChunk : IChunk
    {
        public bool isDirty { get; set; }

        public void Intialize(int x, int y)
        {
            //TODO - use whatever the algorithm is to assign the block level values
            isDirty= true;
        }

        public void Draw(SpriteBatch? _spriteBatch, GameTime gametime, int x, int y)
        {
            if(isDirty)
            {
                isDirty= false;
            }
            //TODO
        }



        public void Update(GameTime gameTime, int x, int y)
        {
            //TODO
        }
    }
}
