using Cascade.src.Managers;
using Cascade.src.Turing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace Cascade;

public class Cascade: Game {
    static void Main(string[] args) {
        Game game = new Cascade();
        game.Run();
        //Turing t = new Turing(250, 250);
        //double[] outvar = t.NextPattern(t.GetScales()).GetAwaiter().GetResult();
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
        _graphics.ApplyChanges();
        _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _managers.ForEach(manager => manager.LoadContent());
        base.LoadContent();
    }
    Stopwatch sw;
    protected override void Update(GameTime gameTime)
    {


        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
        _managers.ForEach(manager => manager.Update(gameTime));
        
        base.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        if (sw == null)
        {
            sw = new Stopwatch();
        }
        else
        {
            sw.Stop();
            long a = sw.ElapsedMilliseconds;
        }
        Stopwatch s2 = new Stopwatch();
        s2.Start();
        //GraphicsDevice.Clear(Color.Gray);
        _managers.ForEach(manager => manager.Draw(gameTime));
        
        base.Draw(gameTime);
        s2.Stop();
        long b = s2.ElapsedMilliseconds;
        sw.Restart();
    }
}
