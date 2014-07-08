using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class RepeatButton : Module
    {
        private readonly Button button;

        public RepeatButton()
        {
            button = new Button(Directory.GetCurrentDirectory() + "/Resources/Textures/repeat.png");
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X / 4, button.sprite.Texture.Size.Y/2);
            button.sprite.Color = Config.Colors.ButtonsFaded;

            button.OnPress += () =>
            {
                bool repeat = Audio.NextState == Audio.State.Repeat;
                Audio.NextState = !repeat ? Audio.State.Repeat : Audio.State.Next;
                button.sprite.Color = !repeat ? Config.Colors.Buttons : Config.Colors.ButtonsFaded;
            };
        }

        public override void Update()
        {
            if (Audio.NextState != Audio.State.Repeat)
                button.sprite.Color = Config.Colors.ButtonsFaded;
            button.Draw(true);
        }
    }
}
