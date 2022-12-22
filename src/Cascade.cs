using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cascade;

public class Cascade: Game {
    static void Main(string[] args) {
        Game game = new Cascade();
        game.Run();
    }
    
    private GraphicsDeviceManager graphics;
    
	public Cascade() {
        graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
        IsMouseVisible = true;
	}

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
		
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
		GraphicsDevice.Clear(Color.Gray);
		
        base.Draw(gameTime);
    }
}
