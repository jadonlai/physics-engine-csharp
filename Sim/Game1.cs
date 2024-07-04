using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat;
using Flat.Graphics;
using Flat.Input;
using Physics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Collections.Specialized;

namespace Sim;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private Screen screen;
    private Sprites sprites;
    private Shapes shapes;
    private Camera camera;

    private Vector vectorA = new Vector(12f, 20f);

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this)
        {
            SynchronizeWithVerticalRetrace = true
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;

        const double UpdatesPerSecond = 60d;
        TargetElapsedTime = TimeSpan.FromTicks((long) Math.Round(TimeSpan.TicksPerSecond / UpdatesPerSecond));
    }

    protected override void Initialize()
    {
        FlatUtil.SetRelativeBackBufferSize(graphics, 0.5f);

        screen = new Screen(this, 1280, 768);
        sprites = new Sprites(this);
        shapes = new Shapes(this);
        camera = new Camera(screen)
        {
            Zoom = 8
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        FlatKeyboard keyboard = FlatKeyboard.Instance;
        FlatMouse mouse = FlatMouse.Instance;

        keyboard.Update();
        mouse.Update();

        if (keyboard.IsKeyAvailable)
        {
            if (keyboard.IsKeyClicked(Keys.Escape))
            {
                Exit();
            }

            if (keyboard.IsKeyClicked(Keys.Up))
            {
                camera.IncZoom();
            }

            if (keyboard.IsKeyClicked(Keys.Down))
            {
                camera.DecZoom();
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        screen.Set();
        GraphicsDevice.Clear(new Color(50, 60, 70));

        Vector normalized = PhysicsMath.Normalize(vectorA);

        shapes.Begin(camera);
        shapes.DrawCircle(Vector2.Zero, 1f, 24, Color.Blue);
        shapes.DrawLine(Vector2.Zero, Converter.ToVector2(vectorA), Color.White);
        shapes.DrawLine(Vector2.Zero, Converter.ToVector2(normalized), Color.Green);
        shapes.End();

        screen.Unset();
        screen.Present(sprites);

        base.Draw(gameTime);
    }
}
