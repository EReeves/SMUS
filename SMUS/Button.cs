using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class Button
    {
        public readonly Sprite sprite;
        public event Action OnPress;

        public Button(string path)
        {
            sprite = new Sprite(new Texture(path));
            Program.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button != Mouse.Button.Left) return;

            if (MouseInBounds() && Program.WindowFocused)
            {
                if(OnPress != null)
                OnPress.Invoke();
            }
        }

        private bool MouseInBounds()
        {
            Vector2i e = Mouse.GetPosition(Program.Window);

            return e.X >= sprite.Position.X && e.Y >= sprite.Position.Y &&
                   e.X <= sprite.Position.X + sprite.Texture.Size.X &&
                   e.Y <= sprite.Position.Y + sprite.Texture.Size.Y;
        }

        public void Draw(bool shadow)
        {
            if (!shadow)
            {
                sprite.Draw(Program.Window, RenderStates.Default);
                return;
            }
            else
            {
                //Shadow.
                Color def = sprite.Color;
                sprite.Color = Config.Colors.Shadow;
                sprite.Position += new Vector2f(0,1);
                sprite.Draw(Program.Window, RenderStates.Default);

                //Sprite
                sprite.Color = def;
                sprite.Position -= new Vector2f(0, 1);
                sprite.Draw(Program.Window, RenderStates.Default);
            }
        }
    }
}
