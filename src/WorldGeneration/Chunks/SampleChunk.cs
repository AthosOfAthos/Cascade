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

        public string[][]? Tiles { get; set; }

        private int size = 32;

        public void Intialize(int size, int xloc, int yloc)
        {
            //TODO - use whatever the algorithm is to assign the block level values

            //for the sample its going to be a sine wave
            Tiles = new string[size][];
            for (int y = 0; y < size; y++)
            {
                Tiles[y] = new string[size];
                for (int x = 0; x < size; x++)
                {
                    if (Math.Sin(xloc) > 0 && Math.Sin(yloc)>0)
                    {
                        Tiles[y][x] = TileIndex.Sample;
                    }
                    else
                    {
                        Tiles[y][x] = TileIndex.Blank;
                    }

                }
            }

            isDirty = true;
        }

        public void Draw(SpriteBatch? _spriteBatch, GameTime gametime, int xloc, int yloc)
        {
            Texture2D tile;
            Texture2D old_tile = null;
            if (isDirty)
            {
                isDirty = true;
                for (int y = 0; y < Tiles?.Length; y++)
                {
                    for (int x = 0; x < Tiles[y].Length; x++)
                    {
                        TileIndex.getTexture(Tiles[x][y], out tile);
                        _spriteBatch?.Draw(tile, new Microsoft.Xna.Framework.Rectangle(xloc*size, yloc*size, size, size), Microsoft.Xna.Framework.Color.White);
                        bool b = old_tile == tile;
                        old_tile = tile;
                        
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
