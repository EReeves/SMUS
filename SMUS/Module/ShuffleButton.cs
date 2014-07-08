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
    class ShuffleButton : Module
    {
        private readonly Button button;

        public ShuffleButton()
        {
            button = new Button(Directory.GetCurrentDirectory() + "/Resources/Textures/shuffle.png");
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X / 4, Program.Window.Size.Y - (button.sprite.Texture.Size.Y + button.sprite.Texture.Size.Y / 2));
            button.sprite.Color = Config.Colors.ButtonsFaded;
            
            button.OnPress += () =>
            {
                bool shuffle = Audio.NextState == Audio.State.Shuffle;
                Audio.NextState = !shuffle ? Audio.State.Shuffle : Audio.State.Next;
                button.sprite.Color = !shuffle ? Config.Colors.Buttons : Config.Colors.ButtonsFaded;
            };
        }

        public override void Update()
        {
            if(Audio.NextState != Audio.State.Shuffle)
                button.sprite.Color = Config.Colors.ButtonsFaded;

            button.Draw(true);
        }
    }
}
