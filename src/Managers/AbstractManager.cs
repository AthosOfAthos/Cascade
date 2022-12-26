using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.Managers
{
    internal abstract class AbstractManager : IManager
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private Game _game { get; set; }

        public virtual void Initialize(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public virtual void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        public virtual void Update(GameTime gameTime)
        {
            //TODO
        }

        public virtual void Draw(GameTime gameTime)
        {
            //TODO
        }

    }
}
