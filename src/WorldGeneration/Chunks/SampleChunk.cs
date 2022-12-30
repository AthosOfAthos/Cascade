using Cascade.src.WorldGeneration.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;

namespace Cascade.src.WorldGeneration.Chunks
{
    internal class SampleChunk : IChunk
    {
        public bool isDirty { get; set; }

        public ITile[][]? Tiles { get; set; }

        public void Intialize(int size, int xloc, int yloc)
        {
            //TODO - use whatever the algorithm is to assign the block level values

            //for the sample its going to be a sine wave

            Tiles = new ITile[size][];
            for (int y = 0; y < size; y++)
            {
                Tiles[y] = new ITile[size];
                for (int x = 0; x < size; x++)
                {
                    if (Math.Sin(xloc) > 0)
                    {
                        Tiles[y][x] = new SampleTile();
                    }
                    else
                    {
                        Tiles[y][x] = new BlankTile();
                    }

                }
            }

            isDirty = true;
        }

        public void Draw(SpriteBatch? _spriteBatch, GameTime gametime, int xloc, int yloc)
        {
            Console.WriteLine("A");
            if (isDirty)
            {
                isDirty = false;
                for (int y = 0; y < Tiles?.Length; y++)
                {
                    for (int x = 0; x < Tiles[y].Length; x++)
                    {
                        Console.WriteLine("Got this far");
                        Texture2D tile = TileIndex.getTexture(Tiles[x][y].Name);
                        _spriteBatch?.Draw(tile, new Microsoft.Xna.Framework.Rectangle(xloc, yloc, 16, 16), Microsoft.Xna.Framework.Color.White);

                    }
                }
            }
            //TODO
        }



        public void Update(GameTime gameTime, int x, int y)
        {
            //TODO
        }
    }
}
