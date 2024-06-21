using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat;
using Flat.Graphics;
using Flat.Input;
using Physics;

namespace Sim;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private Screen screen;
    private Sprites sprites;
    private Shapes shapes;
    private Camera camera;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.SynchronizeWithVerticalRetrace = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;

        const double UpdatesPerSecond = 60d;
        TargetElapsedTime = TimeSpan.FromTicks((long) Math.Round(TimeSpan.TicksPerSecond / UpdatesPerSecond));
    }

    protected override void Initialize()
    {
        float x1 = 0.05f;
        float y1 = 0.002f;
        float x2 = 0.001f;
        float y2 = 0.003f;

        Vector a = new Vector(x1, y1);
        Vector b = new Vector(x2, y2);

        Stopwatch watch = new Stopwatch();

        watch.Start();
        for (int i = 0; i < 1_000_000; i++)
        {
            // x1 += x2;
            // y1 += y2;
            a += b;
        }
        watch.Stop();

        // Console.WriteLine($"{x1}, {y1}");
        Console.WriteLine($"{a.X}, {a.Y}");
        Console.WriteLine($"Time: {watch.Elapsed.TotalMilliseconds}ms");

        FlatUtil.SetRelativeBackBufferSize(graphics, 0.5f);

        screen = new Screen(this, 1280, 768);
        sprites = new Sprites(this);
        shapes = new Shapes(this);
        camera = new Camera(screen);
        camera.Zoom = 5;

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

        shapes.Begin(camera);
        shapes.DrawCircle(0, 0, 32, 32, Color.White);
        shapes.End();

        screen.Unset();
        screen.Present(sprites);

        base.Draw(gameTime);
    }
}
