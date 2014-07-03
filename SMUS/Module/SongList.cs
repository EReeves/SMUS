using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SFML.Graphics;
using SFML.Window;

namespace SMUS.Module
{
    internal class SongList : List<Song>, IModule
    {
        private const float charHeight = 14;
        private readonly Vector2f basePosition = new Vector2f(5, 0);
        private bool updateText = true;
        private float yScroll;

        public Font Font { get; set; }

        public SongList(Font font)
        {
            Font = font;
            Program.Window.MouseWheelMoved += (o, e) => ScrollText(e);
        }

        public void Update()
        {
            PixelSnap();

            //Draw
            if (Count > 0)
                DrawSongText();

            //Update all songs
            foreach (Song song in this)
                song.Update();
        }

        public void LoadFromDirectory(string path)
        {
            var regx = new Regex(@".*\.(wav|ogg|mp3|flac|mod|it|s3d|xm)");
            string[] fileList = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => regx.IsMatch(s))
                .ToArray();
            foreach (string s in fileList)
            {
                try
                {
                    Add(new Song(this, s, Font));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Corrupted File: " + Path.GetFileName(s) + " - skipped.");
                }
            }
         
        }

        public void LoadFromMultipleDirectories(IEnumerable<string> paths)
        {
            foreach (string path in paths)
                LoadFromDirectory(path);
        }

        public void SortByArtist()
        {
            this.Sort((x, y) => System.String.Compare(x.Name,
                y.Name, System.StringComparison.OrdinalIgnoreCase));
        }

        private void DrawSongText()
        {
            for (int i = 0; i < Count; i++)
            {
                //Only update position when needed.
                if (updateText)
                {
                    this[i].Position = basePosition + new Vector2f(0, yScroll);
                    this[i].Position += new Vector2f(0, charHeight*i);
                }
                this[i].Draw(Program.Window);
            }

            if (updateText)
                updateText = false;
        }

        private bool BoundsDown()
        {
            //Able to scroll up?
            return this[Count - 1].Position.Y + 4 > Program.Window.Size.Y;
        }

        private bool BoundsUp()
        {
            //Able to scroll down?
            return this[0].Position.Y < 0;
        }

        private void PixelSnap()
        {
            //Shouldn't be needed but just in case snap it.
            float diff = yScroll % charHeight;
            if (diff > 0)
                yScroll -= 1;
            else if (diff < 0)
                yScroll += 1;
        }

        private void ScrollText(MouseWheelEventArgs e)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right)) return;
            updateText = true;
            switch (e.Delta)
            {
                case 1:
                    if (BoundsUp())
                        yScroll += charHeight * 2;
                    break;
                case -1:
                    if (BoundsDown())
                        yScroll -= charHeight * 2;
                    break;
            }
        }
    }
}