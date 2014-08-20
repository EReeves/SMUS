using System;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Tools;
using SFML.Window;

namespace SMUS.Module
{
    internal class Button
    {
        public readonly BatchedSprite sprite;
        public readonly BatchedSprite shadowSprite;
        public event Action OnPress;

        public Button(BatchedSprite spr)
        {
            sprite = spr;
            shadowSprite = new BatchedSprite(spr.Position, spr.AtlasPosition, spr.ZOrder - 1);

            spr.Colour = Config.Colors["buttons"];
            shadowSprite.Colour = Config.Colors["shadow"];
            shadowSprite.Position += new Vector2f(0, 1);

            Program.SpriteBatch.Add(spr);
            Program.SpriteBatch.Add(shadowSprite);

            Program.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button != Mouse.Button.Left) return;

            if (MouseInBounds() && Program.WindowFocused)
            {
                if (OnPress != null)
                    OnPress.Invoke();
            }
        }

        private bool MouseInBounds()
        {
            Vector2i e = Mouse.GetPosition(Program.Window);

            return e.X >= sprite.Position.X && e.Y >= sprite.Position.Y &&
                   e.X <= sprite.Position.X + sprite.AtlasPosition.Width &&
                   e.Y <= sprite.Position.Y + sprite.AtlasPosition.Height;
        }
    }
}
