using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration.Tiles
{
    internal static class TileIndex
    {


        private static Game? game;

        private static bool isInitalized;

        private static Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();

        public static void Initalize(Game game)
        {
            if (!isInitalized)
            {
                TileIndex.game = game;
            }
            isInitalized = true;

        }
        public static Texture2D getTexture(string name)
        {
            bool success = textureDictionary.TryGetValue(name, out Texture2D? texture);
            if (!success || texture == null)
            {
                texture = game?.Content.Load<Texture2D>(name) ?? throw new Exception();

                textureDictionary.Add(name, texture);
            }
            return texture;
        }
    }
}
