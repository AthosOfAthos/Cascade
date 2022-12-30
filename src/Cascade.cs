using Cascade.src.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cascade;

public class Cascade: Game {
    static void Main(string[] args) {
        Game game = new Cascade();
        game.Run();
    }
    
    private GraphicsDeviceManager _graphics;

    private List<IManager> _managers;
    
	public Cascade() {
        _graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
        IsMouseVisible = true;

        //Manager content, this should be redone with dependency injection
        _managers = new List<IManager> { 
            new EntityManager(),
            new WorldManager()
        };
	}

    protected override void Initialize()
    {
        _managers.ForEach(manager => manager.Initialize(this));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _managers.ForEach(manager => manager.LoadContent());
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
        _managers.ForEach(manager => manager.Update(gameTime));
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);
        _managers.ForEach(manager => manager.Draw(gameTime));
        
        base.Draw(gameTime);
    }
}
