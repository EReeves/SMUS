using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Tools;
using SFML.Window;

namespace SMUS.Module
{
    class ShuffleButton : Module
    {
        private readonly Button button;

        public ShuffleButton()
        {
            button = new Button(new BatchedSprite(new Vector2f(0,0), Program.AtlasData["shuffle"], 4 ));
            button.sprite.Position = new Vector2f(Program.Window.Size.X - Program.Window.Size.X / 4, Program.Window.Size.Y - (button.sprite.AtlasPosition.Height + button.sprite.AtlasPosition.Height/ 2));
            button.sprite.Colour = Config.Colors.ButtonsFaded;
            button.shadowSprite.Position = button.sprite.Position += new Vector2f(0, 1);
            
            button.OnPress += () =>
            {
                bool shuffle = Audio.NextState == Audio.State.Shuffle;
                Audio.NextState = !shuffle ? Audio.State.Shuffle : Audio.State.Next;
                button.sprite.Colour = !shuffle ? Config.Colors.Buttons : Config.Colors.ButtonsFaded;
                Program.SpriteBatch.CalculateVertices();
            };
        }

        public override void Update()
        {
            if(Audio.NextState != Audio.State.Shuffle)
                button.sprite.Colour = Config.Colors.ButtonsFaded;

        }
    }
}
