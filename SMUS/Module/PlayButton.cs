using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    class PlayButton : Module
    {
        private Sprite sprite;
        private readonly Texture playTex;
        private readonly Texture pauseTex;
        private bool click;

        public PlayButton(Locks locks, RenderWindow window) : base(locks, window)
        {
            playTex = new Texture(Directory.GetCurrentDirectory() + "/Resources/Textures/play.png");
            pauseTex = new Texture(Directory.GetCurrentDirectory() + "/Resources/Textures/pause.png");
            sprite = new Sprite(playTex)
            {
                Position = new Vector2f(Window.Size.X - (playTex.Size.X + playTex.Size.X / 4),
                    (Window.Size.Y - playTex.Size.Y) / 2),
                Color = new Color(255, 255, 255, 170)
            };
            Window.MouseButtonPressed += (o, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                    click = true;
            };
        }

        public void ClickCheck()
        {
            var e = Mouse.GetPosition(Window);



            if (!click) return;
            click = false;

            if (!(e.X >= sprite.Position.X) || !(e.Y >= sprite.Position.Y) ||
                !(e.X <= sprite.Position.X + sprite.Texture.Size.X) ||
                !(e.Y <= sprite.Position.Y + sprite.Texture.Size.Y)) return;

            if (Audio.IsPlaying)
                Audio.Pause();
            else
                Audio.Play();
        }

        public void SpriteCheck()
        {
            if (Audio.IsPlaying)
            {
                sprite = new Sprite(pauseTex)
                {
                    Position = new Vector2f(Window.Size.X - (pauseTex.Size.X + pauseTex.Size.X / 3),
                        (Window.Size.Y - pauseTex.Size.Y) / 2)
                };
            }
            else
            {
                sprite = new Sprite(playTex)
                {
                    Position = new Vector2f(Window.Size.X - (playTex.Size.X + playTex.Size.X / 3),
                        (Window.Size.Y - playTex.Size.Y) / 2)
                };
            }
        }

        public override void Update()
        {
            SpriteCheck();
            ClickCheck();

            sprite.Position += new Vector2f(1,1);
            sprite.Color = new Color(20, 20, 20, 100);
            sprite.Draw(Window, RenderStates.Default);

            sprite.Position -= new Vector2f(1, 1);
            sprite.Color = new Color(255, 255, 255, 190);
            sprite.Draw(Window,RenderStates.Default);   
        }
    }
}
