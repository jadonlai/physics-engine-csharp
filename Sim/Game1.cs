using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat;
using Flat.Graphics;
using Flat.Input;
using Physics;
using System.Collections.Generic;
using System.Text.Unicode;

namespace Sim;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private Screen screen;
    private Sprites sprites;
    private Shapes shapes;
    private Camera camera;

    private List<Body> bodyList;

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

        camera.GetExtents(out float left, out float right, out float bottom, out float top);

        bodyList = new List<Body>();
        int bodyCount = 10;
        const float padding = 20f;

        for (int i = 0; i < bodyCount; i++) {
            int type = RandomHelper.RandomInteger(0, 2);

            Body body = null;

            float x = RandomHelper.RandomSingle(left + padding, right - padding);
            float y = RandomHelper.RandomSingle(bottom + padding, top - padding);

            if (type == (int) ShapeType.Circle) {
                if (!Body.CreateCircleBody(3f, new Vector(x, y), 2f, false, 0.5f, out body, out string errorMessage)) {
                    throw new Exception();
                }
            } else if (type == (int) ShapeType.Box) {
                if (!Body.CreateBoxBody(3f, 3f, new Vector(x, y), 2f, false, 0.5f, out body, out string errorMessage)) {
                    throw new Exception();
                }
            } else {
                throw new Exception("unknown type");
            }

            bodyList.Add(body);
        }

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
        for (int i = 0; i < bodyList.Count; i++) {
            Body body = bodyList[i];
            Vector2 position = Converter.ToVector2(body.Position);
            if (body.ShapeType is ShapeType.Circle) {
                shapes.DrawCircle(position, body.Radius, 26, Color.White);
            } else if (body.ShapeType is ShapeType.Box) {
                shapes.DrawBox(position, body.Width, body.Height, Color.White);
            }
        }
        shapes.End();

        screen.Unset();
        screen.Present(sprites);

        base.Draw(gameTime);
    }
}
