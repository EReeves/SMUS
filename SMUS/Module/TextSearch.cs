using System;
using System.IO;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Timers;

namespace SMUS.Module
{
    class TextSearch : Module
    {
        private readonly SongList songList;
        private string inputText = "";
        private readonly Timer inputTimer;
        private readonly Text text;
        private readonly Sprite backgroundSprite;
        private readonly int charHeight;

        public TextSearch(SongList sl, Font font)
        {
            songList = sl;
            text = new Text("", font);
            Program.Window.TextEntered += (o, e) => TextEntered(e);

            //Timer for song search text reset.
            inputTimer = new Timer(1800);
            inputTimer.Elapsed += (o, e) =>
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Back)) return;
                inputText = "";
                text.Color = Config.Colors["text"];
                text.DisplayedString = "";
            };

            //Formatting.
            text.CharacterSize = 26;
            text.Font.GetTexture(26).Smooth = false;

            var tex = new Texture(Directory.GetCurrentDirectory() + "/Resources/Textures/blank.png");
            backgroundSprite = new Sprite(tex) {Color = new Color(0, 0, 0, 200)};

            text.DisplayedString = "a";
            charHeight = (int)text.GetLocalBounds().Height;
            text.DisplayedString = "";
            
        }

        public override void Update()
        {
            if (String.IsNullOrEmpty(text.DisplayedString)) return;
            
                var y = charHeight*2.2f;
                backgroundSprite.Position = new Vector2f(0, Program.Window.Size.Y - y);
                backgroundSprite.Scale = new Vector2f(Program.Window.Size.X, y);
                backgroundSprite.Draw(Program.Window, RenderStates.Default);
            

            var prev = text.Color;

            text.Position += new Vector2f(1,1);
            text.Color = Config.Colors["shadow"];
            text.Draw(Program.Window, RenderStates.Default);

            text.Color = prev;
            text.Position -= new Vector2f(1,1);
            text.Position = new Vector2f((int)(Program.Window.Size.X / 2f - text.GetLocalBounds().Width/2), Program.Window.Size.Y - charHeight*2.4f);
            text.Draw(Program.Window,RenderStates.Default);
        }

        private void TextEntered(TextEventArgs e)
        {
            if (e.Unicode == "\u0008" || Keyboard.IsKeyPressed(Keyboard.Key.Back))
            {
                Backspace();
                return;
            }

            inputTimer.Stop();
            inputTimer.Start();
            inputText += e.Unicode.ToLower();
            text.DisplayedString = inputText;

            LetterScroll();
        }

        private void LetterScroll()
        {
            if (inputText == "") return;

            Song song = null;
            Parallel.ForEach(songList, (s,p) =>
            {
                //Pretty intensive, this makes it a bit faster although not as accurate.
                var n = s.Name.ToLower();
                if (n[0] != inputText[0]) return;
                if (!n.StartsWith(inputText)) return;
                song = s;
                p.Break();
            });

            if (song == null)
            {
                text.Color = new Color(255,100,100);
                return;
            }
            text.Color = Color.White;
            song.Play();
            songList.ScrollToCurrentSong();
        }

        private void Backspace()
        {
            inputTimer.Stop();
            inputTimer.Start();
            
            if(String.IsNullOrEmpty(inputText))
                return;

            inputText = inputText.Substring(0, inputText.Length - 1);
            text.DisplayedString = inputText.ToLower();
        }
    }
}
