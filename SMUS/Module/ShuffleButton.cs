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
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X/4 - 10, Program.Window.Size.Y - button.sprite.Texture.Size.Y*1.5f);
            button.sprite.Color = new Color(255,255,255,100);
            
            button.OnPress += () =>
            {
                Audio.Shuffle = !Audio.Shuffle;
                button.sprite.Color = Audio.Shuffle ? Config.Colors.Buttons : new Color(255,255,255,100);
            };
        }

        public override void Update()
        {
            button.Draw(true);
        }
    }
}
