using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SFML.Graphics;
using SFML.Window;
using SMUS.Module;

namespace SMUS
{
    class SongList : List<Song>, IModule
    {
        private Vector2f basePosition = new Vector2f(0, 0);
        private bool updateText = true;
        private float yVelocity = 0;
        
        public RenderWindow Window { get; set; }
        public Font Font { get; set; }

        public SongList(RenderWindow _window, Font _font)
        {
            Window = _window;
            Font = _font;

            Window.MouseWheelMoved += (o, e) =>
            {
                updateText = true;
                switch (e.Delta)
                {
                    case 1:
                        yVelocity += 1;
                        break;
                    case -1:
                        yVelocity -= 1;
                        break;
                }
            };

        }

        public void Update()
        {
            if (this.Count > 0)
                DrawSongText();

            if (yVelocity > 0)
                yVelocity -= 0.5f;
            else if (yVelocity > 0)
                yVelocity += 0.5f;


            basePosition += new Vector2f(0,yVelocity);
        }

        public void LoadFromDirectory(string path)
        {
            Regex regx = new Regex(@".*\.(wav|ogg|mp3|flac|mod|mod|it|s3d|xm)");
            string[] fileList = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories)
                .Where(s => regx.IsMatch(s))
                .ToArray();
            foreach (string s in fileList)
            {
                Add(new Song(s, Font));
            }
        }


        private void DrawSongText()
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (updateText)
                {
                    this[i].Position = basePosition + new Vector2f(0,yVelocity);
                    float charHeight = this.First().GetLocalBounds().Height + 2;
                    this[i].Position += new Vector2f(0, charHeight*i);
                }
                this[i].Draw(Window);
            }

            if (updateText)
                updateText = false;
        }

    }
}
