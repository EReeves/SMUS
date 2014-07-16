using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
                text.Color = Config.Colors.Text;
                text.DisplayedString = "";
            };

            text.CharacterSize = 28;
            text.Style = Text.Styles.Bold;

            var tex = new Texture(Directory.GetCurrentDirectory() + "/Resources/Textures/blank.png");
            backgroundSprite = new Sprite(tex);
            backgroundSprite.Color = new Color(0,0,0, 180);
            backgroundSprite.Scale = new Vector2f(Program.Window.Size.X,Program.Window.Size.Y);
        }

        public override void Update()
        {
            if(!String.IsNullOrEmpty(text.DisplayedString))
                backgroundSprite.Draw(Program.Window,RenderStates.Default);

            var prev = text.Color;

            text.Position += new Vector2f(1,1);
            text.Color = Config.Colors.Shadow;
            text.Draw(Program.Window, RenderStates.Default);

            text.Color = prev;
            text.Position -= new Vector2f(1,1);
            text.Position = new Vector2f((int)(Program.Window.Size.X / 2 - text.GetLocalBounds().Width/2), Program.Window.Size.Y - text.GetLocalBounds().Height*2);
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
            inputText += e.Unicode.ToUpperInvariant();
            text.DisplayedString = inputText.ToLower();

            LetterScroll();
        }

        private void LetterScroll()
        {
            var song = songList.Find(s => s.Name.ToUpperInvariant().StartsWith(inputText));
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
