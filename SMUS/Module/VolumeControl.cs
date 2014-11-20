using System;
using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class VolumeControl : Module
    {
        private readonly Sprite sprite;
        private const int width = 10;
        private float linearVolume = 0.8f;

        public VolumeControl()
        {
            var tex = new Texture(Program.AssPath + "/Resources/Textures/blank.png");
            sprite = new Sprite(tex)
            {
                Color = Config.Colors["volume"],
                Position = new Vector2f(Program.Window.Size.X - width, 0),
                Scale = new Vector2f(width,Program.Window.Size.Y)
            };

            Audio.Engine.SoundVolume = 0.8f;

            Program.Window.MouseWheelMoved += Window_MouseWheelMoved;
        }

        public override void Update()
        {
            float percent = ((float)Math.Exp(Audio.Engine.SoundVolume) - 1)/((float)Math.E - 1);
            sprite.Position = new Vector2f(sprite.Position.X,-1 + Program.Window.Size.Y - Program.Window.Size.Y * percent);
            sprite.Draw(Program.Window, RenderStates.Default);
        }

        private void Window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            if (!Mouse.IsButtonPressed(Mouse.Button.Right)) return;
         
            switch (e.Delta)
            {
                case 0:
                    break;
                case 1:
                    Up(0.05f);
                    break;
                case -1:
                    Down(0.05f);
                    break;
            }

            float percent = ((float)Math.Exp(linearVolume) - 1) / ((float)Math.E - 1);
            Audio.Engine.SoundVolume = percent;
        }

        public void Up(float amount)
        {
            if (!(Audio.Engine.SoundVolume < 1f)) return;
            linearVolume += amount;
            RecalculateVolume();
        }

        public void Down(float amount)
        {
            if (!(Audio.Engine.SoundVolume > 0f)) return;
            linearVolume -= amount;
            RecalculateVolume();
        }

        private void RecalculateVolume()
        {

            float percent = ((float)Math.Exp(linearVolume) - 1) / ((float)Math.E - 1);
            Audio.Engine.SoundVolume = percent;
        }
    }
}
