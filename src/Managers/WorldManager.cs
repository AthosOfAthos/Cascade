using Cascade.src.WorldGeneration;
using Cascade.src.WorldGeneration.Stages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cascade.src.Managers
{
    internal class WorldManager : AbstractManager
    {
        private IStageFactory _stageFactory = new StageFactory();
        private List<IStage> _stages = new List<IStage>();

        private IStage? _currentStage;

        public override void Initialize(Game game)
        {
            _stageFactory.Initialize(game);
            _stages.Add(_stageFactory.GenerateStage());
            _currentStage = _stages.First();
            _currentStage.Initalize();
            base.Initialize(game);
        }

        public override void LoadContent()
        {
            
            
            _currentStage.LoadContent(_graphics);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _currentStage?.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _currentStage?.Draw(_spriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
