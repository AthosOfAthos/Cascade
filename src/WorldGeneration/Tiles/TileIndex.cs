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
        public static readonly string Blank = "Blank";
        public static readonly string Sample = "Sample";

        private static Game? game;

        private static bool isInitalized;

        private static Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();

        public static ITile GetTile(string name)
        {
            Type type;
            switch (name)
            {
                case "Sample":
                    type = typeof(SampleTile);
                    break;
                case "Blank":
                default:
                    type = typeof(BlankTile);
                    break;

            }
            return (ITile)Activator.CreateInstance(type) ?? throw new Exception();
        }

        public static void Initalize(Game game)
        {
            if (!isInitalized)
            {
                TileIndex.game = game;
            }
            isInitalized = true;

        }
        public static void getTexture(string name, out Texture2D texture)
        {
            bool success = textureDictionary.TryGetValue(name, out texture);
            if (!success || texture == null)
            {
                texture = game?.Content.Load<Texture2D>(name) ?? throw new Exception();

                textureDictionary.Add(name, texture);
            }
        }
    }
}
