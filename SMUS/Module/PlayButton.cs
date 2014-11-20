using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    internal class PlayButton : Module
    {
        private readonly Texture pauseTex;
        private readonly Texture playTex;
        private bool leftClick;
        private Sprite sprite;

        public PlayButton()
        {
            playTex = new Texture(Program.AssPath + "/Resources/Textures/play.png");
            pauseTex = new Texture(Program.AssPath + "/Resources/Textures/pause.png");
            //sprite is initialized here.
            SetTexture(playTex);

            Program.Window.MouseButtonPressed += (o, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                    leftClick = true;
            };
        }

        public void ClickCheck()
        {
            //Handles pausing/resuming.
            if (!leftClick) return;
            leftClick = false;

            if (!MouseInBounds()) return;

            if (Audio.IsPlaying)
                Audio.Pause();
            else
                Audio.Resume();
        }

        public void SpriteCheck()
        {
            SetTexture(Audio.IsPlaying ? pauseTex : playTex);
        }

        public override void Update()
        {
            SpriteCheck();
            ClickCheck();

            //Drop shadow.
            sprite.Position += new Vector2f(1, 1);
            sprite.Color = Config.Colors["shadow"];
            sprite.Draw(Program.Window, RenderStates.Default);

            //Main draw.
            sprite.Position -= new Vector2f(1, 1);
            sprite.Color = Config.Colors["buttons"];
            sprite.Draw(Program.Window, RenderStates.Default);
        }

        private void SetTexture(Texture tex)
        {
            sprite = new Sprite(tex)
            {
                Position = new Vector2f(Program.Window.Size.X - (tex.Size.X + tex.Size.X / 3),
                    (float)(Program.Window.Size.Y - tex.Size.Y) / 2)
            };
        }

        private bool MouseInBounds()
        {
            Vector2i e = Mouse.GetPosition(Program.Window);

            return e.X >= sprite.Position.X && e.Y >= sprite.Position.Y &&
                   e.X <= sprite.Position.X + sprite.Texture.Size.X &&
                   e.Y <= sprite.Position.Y + sprite.Texture.Size.Y;
        }
    }
}