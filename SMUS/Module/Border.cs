using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class Border : Module, IModule
    {
        private readonly Sprite sprite;

        public Border(Locks locks, RenderWindow window) : base(locks, window)
        {
            Texture tex = new Texture(1,1);
            sprite = new Sprite(tex)
            {
                Color = Color.Black
            };
           
        }

        public override void Update()
        {
            //Draw border.

            //Top
            sprite.Position = new Vector2f(0,0);
            sprite.Scale = new Vector2f(Window.Size.X, 1);
            sprite.Draw(Window, RenderStates.Default);
            //Bottom
            sprite.Position = new Vector2f(0, Window.Size.Y-1);
            sprite.Draw(Window, RenderStates.Default);
            //Left
            sprite.Position = new Vector2f(0, 0);
            sprite.Scale = new Vector2f(1, Window.Size.Y);
            sprite.Draw(Window, RenderStates.Default);
            //Right
            sprite.Position = new Vector2f(Window.Size.X-1, 0);
            sprite.Draw(Window, RenderStates.Default);
        }
    }
}
