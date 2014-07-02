using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class Border : Module, IModule
    {
        private readonly Sprite sprite;
        private const int borderSize = 2;

        public Border(Locks locks, RenderWindow window) : base(locks, window)
        {
            Texture tex = new Texture(Directory.GetCurrentDirectory() + "/Resources/Textures/blank.png");
            sprite = new Sprite(tex)
            {
                Color = new Color(120,90,70),
                Position = new Vector2f(0,0)
            };
        }

        public override void Update()
        {
            //Draw border.

            sprite.Color = new Color(40, 30, 20);
            //Top shadow
            sprite.Scale = new Vector2f(Window.Size.X, borderSize);
            sprite.Position = new Vector2f(0, 1);
            sprite.Draw(Window, RenderStates.Default);
            //Left shadow
            sprite.Scale = new Vector2f(borderSize, Window.Size.Y);
            sprite.Position = new Vector2f(1, 0);
            sprite.Draw(Window, RenderStates.Default);

            sprite.Color = new Color(120, 90, 70);
            //Bottom           
            sprite.Scale = new Vector2f(Window.Size.X, borderSize);
            sprite.Position = new Vector2f(0, Window.Size.Y - borderSize);
            sprite.Draw(Window, RenderStates.Default);
            //Top
            sprite.Position = new Vector2f(0, 0);
            sprite.Scale = new Vector2f(Window.Size.X, borderSize);
            sprite.Draw(Window, RenderStates.Default);
            //Left
            sprite.Position = new Vector2f(0, 0);
            sprite.Scale = new Vector2f(borderSize, Window.Size.Y);
            sprite.Draw(Window, RenderStates.Default);
            //Right
            sprite.Position = new Vector2f(Window.Size.X - borderSize, 0);
            sprite.Draw(Window, RenderStates.Default);
        }
    }
}
