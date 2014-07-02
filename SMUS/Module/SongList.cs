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
        private readonly Vector2f basePosition = new Vector2f(2, 0);
        private bool updateText = true;
        private float yScroll = 0;
        
        public RenderWindow Window { get; set; }
        public Locks Locks { get; set; }
        public Font Font { get; set; }

        public SongList(Locks locks, RenderWindow window, Font font)
        {
            Locks = locks;
            Window = window;
            Font = font;

            Window.MouseWheelMoved += (o, e) =>
            {
                updateText = true;
                switch (e.Delta)
                {
                    case 1:
                        if(BoundsUp())
                            yScroll += 30;
                        break;
                    case -1:
                        if (BoundsDown())
                            yScroll -= 30;
                        break;
                }
            };

        }

        public bool BoundsDown()
        {
            //4 offset from bottom.
            return this[Count - 1].Position.Y + 4 > Window.Size.Y;
        }

        public bool BoundsUp()
        {
            return this[0].Position.Y < 0;
        }

        public void Update()
        {
            if (this.Count > 0)
                DrawSongText();
        }

        public void LoadFromDirectory(string path)
        {
            Regex regx = new Regex(@".*\.(wav|ogg|mp3|flac|mod|it|s3d|xm)");
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
                    this[i].Position = basePosition + new Vector2f(0,yScroll);
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
