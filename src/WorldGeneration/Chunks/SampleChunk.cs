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

        private int tileSize = 32;
        private int chunkSize;

        public void Intialize(int size, int xloc, int yloc)
        {
            //TODO - use whatever the algorithm is to assign the block level values

            //for the sample its going to be a sine wave
            chunkSize = size; 
            Tiles = new string[size][];
            for (int y = 0; y < size; y++)
            {
                Tiles[y] = new string[size];
                for (int x = 0; x < size; x++)
                {
                    if (x == 0 || y ==0 || y==size-1 || x==size-1)
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
                        _spriteBatch?.Draw(tile, new Microsoft.Xna.Framework.Rectangle(xloc*chunkSize*tileSize+x*tileSize, yloc*chunkSize*tileSize+y*tileSize, tileSize, tileSize), Microsoft.Xna.Framework.Color.White);
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
