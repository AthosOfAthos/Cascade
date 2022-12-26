using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.Managers
{
    internal interface IManager
    {
        public void Initialize(Game game);

        public void LoadContent();

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime);
    }
}
