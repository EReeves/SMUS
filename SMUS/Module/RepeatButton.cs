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
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X / 4 - 10, Program.Window.Size.Y - 5 - button.sprite.Texture.Size.Y * 3f);
            button.sprite.Color = new Color(255, 255, 255, 100);

            button.OnPress += () =>
            {
                bool repeat = Audio.NextState == Audio.State.Repeat;
                Audio.NextState = !repeat ? Audio.State.Repeat : Audio.State.Next;
                button.sprite.Color = !repeat ? Config.Colors.Buttons : new Color(255, 255, 255, 100);
            };
        }

        public override void Update()
        {
            if (Audio.NextState != Audio.State.Repeat)
                button.sprite.Color = new Color(255, 255, 255, 100);

            button.Draw(true);
        }
    }
}
