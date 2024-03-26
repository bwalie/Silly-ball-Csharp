using System;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

namespace PingPong
{
    public class PingPong : PhysicsGame
    {
        static string[] line = 
        {
            "               ",
            "               ",
            "               ",
            "  X   X        ",
            "X              ",
            "            *  ",
            "    X      X   ",
            "          X    X",
            "               ",
            "               ",
            "               ",
            "               ",
            "  X   X        ",
            "X              ",
            "            *  ",
            "    X      X   ",
            "               ",
            "               ",
            "               ",
            "               ",
            "   *           ",
            "               ",
            "    X      X   ",
            "X              ",
            "               ",
            "         X    X",
        };
        int tileWidth;
        int tileHeight;

        public override void Begin()
        {
            tileWidth = (int)(Screen.Width / line[0].Length);
            tileHeight = (int)(Screen.Height / line.Length);
            PhysicsObject ball = new PhysicsObject(40.0, 40.0);
            
            Add(ball);
            ball.Shape = Shape.Circle;

            Keyboard.Listen(Key.Up, ButtonState.Pressed, MoveBall, "Move Up", ball, new Vector(0, 500));
            Keyboard.Listen(Key.Left, ButtonState.Pressed, MoveBall, "Move Left", ball, new Vector(-500, 0));
            Keyboard.Listen(Key.Down, ButtonState.Pressed, MoveBall, "Move Down", ball, new Vector(0, -500));
            Keyboard.Listen(Key.Right, ButtonState.Pressed, MoveBall, "Move Right", ball, new Vector(500, 0));
            Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, null);

            TileMap tiles = TileMap.FromStringArray(line);
            tiles.SetTileMethod('X', CreateGalaxy);
            tiles.SetTileMethod('*', CreateSombrero);
            tiles.Execute(tileWidth, tileHeight);

            Level.CreateBorders();
            Camera.ZoomToLevel();

            Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Exit Game");
        }

        void CreateGalaxy(Vector location, double width, double height)
        {
            PhysicsObject galaxy = PhysicsObject.CreateStaticObject(tileWidth, tileHeight);
            galaxy.Position = location;
            galaxy.Color = Color.Black;
            Add(galaxy);
        }

        void CreateSombrero(Vector location, double width, double height)
        {
            PhysicsObject sombrero = PhysicsObject.CreateStaticObject(tileWidth, tileHeight);
            sombrero.Position = location;
            sombrero.Color = Color.Yellow;
            Add(sombrero);
            AddCollisionHandler(sombrero, CollisionWithSombrero);
        }

        void CollisionWithSombrero(PhysicsObject sombrero, PhysicsObject target)
        {
            PlaySound("exp");
        }

        void MoveBall(PhysicsObject ball, Vector force)
        {
            ball.Hit(force);
        }
    }
}
