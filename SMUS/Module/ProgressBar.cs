using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class ProgressBar : Module
    {
        private readonly Sprite sprite;

        public ProgressBar(Locks locks, RenderWindow window) : base(locks, window)
        {
            Texture tex = new Texture(1,1);
            sprite = new Sprite(tex)
            {
                Position = new Vector2f(0,0),
                Color = new Color(30,20,10)
            };
        }

        public override void Update()
        {
            if (Audio.Current == null || Audio.Current.PlayPosition == 0) return;
            float percentage = (float)Audio.Current.PlayLength/(float)Audio.Current.PlayPosition+1;
            sprite.Scale = new Vector2f(Window.Size.X/percentage,Window.Size.Y);
            sprite.Draw(Window, RenderStates.Default);
        }
    }
}
