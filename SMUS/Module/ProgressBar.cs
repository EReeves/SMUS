using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    internal class ProgressBar : Module
    {
        private readonly Sprite sprite;

        public ProgressBar()
        {
            var tex = new Texture(Directory.GetCurrentDirectory() + "/Resources/Textures/blank.png");
            sprite = new Sprite(tex)
            {
                Position = new Vector2f(0, 0),
                Color = Config.Colors.ProgressBar
            };
        }

        public override void Update()
        {
            DrawBar();
        }

        public void DrawBar()
        {
            //Calculate and draw progress bar.
            if (Audio.Current == null || Audio.Current.PlayPosition == 0) return;

            float percentage = Audio.Current.PlayLength / (float)Audio.Current.PlayPosition;
            sprite.Scale = new Vector2f(Program.Window.Size.X / percentage, Program.Window.Size.Y);
            sprite.Draw(Program.Window, RenderStates.Default);
        }
    }
}