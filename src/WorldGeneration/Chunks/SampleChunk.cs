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

        public string[,]? Tiles { get; set; }

        private int tileSize = 8;
        private int chunkSize;

        public void Intialize(int size, int xloc, int yloc)
        {
            //TODO - use whatever the algorithm is to assign the block level values

            //for the sample its going to be a sine wave
            chunkSize = size; 
            Tiles = new string[size,size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x == 0 || y ==0 || y==size-1 || x==size-1)
                    {
                        Tiles[y,x] = TileIndex.Sample;
                    }
                    else
                    {
                        Tiles[y,x] = TileIndex.Blank;
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
                isDirty = false;
                for (int y = 0; y < Tiles?.GetLength(0); y++)
                {
                    for (int x = 0; x < Tiles?.GetLength(1); x++)
                    {
                        TileIndex.getTexture(Tiles[y,x], out tile);
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

        internal void Notify(int i, int j, double[,]? res)
        {
            isDirty = true;
            for (int y = 0; y < Tiles?.GetLength(0); y++)
            {
                for (int x = 0; x < Tiles.GetLength(1); x++)
                {
                    int adj = (int)Math.Round((res[i+y,j+x])*5);
                    switch (adj)
                    {
                        default:
                        case 0:
                            Tiles[y,x] = TileIndex.Red;
                            break;
                        case 1:
                            Tiles[y,x] = TileIndex.Orange;
                            break;
                        case 2:
                            Tiles[y,x] = TileIndex.Yellow;
                            break;
                        case 3:
                            Tiles[y,x] = TileIndex.Green;
                            break;
                        case 4:
                            Tiles[y,x] = TileIndex.Blue;
                            break;
                        case 5:
                            Tiles[y,x] = TileIndex.Purple;
                            break;
                    }
                    //res[yloc * chunkSize + y][xloc * chunkSize + x] = -1;

                }
            }
        }
    }
}
