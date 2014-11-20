using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    internal class Border : Module, IModule
    {
        private const int borderSize = 2;
        private readonly Sprite sprite;

        public Border()
        {
            var tex = new Texture(Program.AssPath + "/Resources/Textures/blank.png");
            sprite = new Sprite(tex)
            {
                Color = Config.Colors["border"],
                Position = new Vector2f(0, 0)
            };
        }

        public override void Update()
        {
            //TODO: Draw and render this as a texture rather than manually.

            sprite.Color = Config.Colors["shadow"];
            //Top shadow
            sprite.Scale = new Vector2f(Program.Window.Size.X, borderSize);
            sprite.Position = new Vector2f(0, 1);
            sprite.Draw(Program.Window, RenderStates.Default);
            //Right shadow
            sprite.Scale = new Vector2f(borderSize, Program.Window.Size.Y);
            sprite.Position = new Vector2f(Program.Window.Size.X-borderSize-1, 0);
            sprite.Draw(Program.Window, RenderStates.Default);

            sprite.Color = Config.Colors["border"];
            //Bottom           
            sprite.Scale = new Vector2f(Program.Window.Size.X, borderSize);
            sprite.Position = new Vector2f(0, Program.Window.Size.Y - borderSize);
            sprite.Draw(Program.Window, RenderStates.Default);
            //Top
            sprite.Position = new Vector2f(0, 0);
            sprite.Scale = new Vector2f(Program.Window.Size.X, borderSize);
            sprite.Draw(Program.Window, RenderStates.Default);
            //Left
            sprite.Position = new Vector2f(0, 0);
            sprite.Scale = new Vector2f(borderSize, Program.Window.Size.Y);
            sprite.Draw(Program.Window, RenderStates.Default);
            //Right
            sprite.Position = new Vector2f(Program.Window.Size.X - borderSize, 0);
            sprite.Draw(Program.Window, RenderStates.Default);
        }
    }
}