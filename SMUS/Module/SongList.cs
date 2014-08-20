﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private readonly object addLock = new object();

        public Font Font { get; set; }

        public SongList(Font font)
        {
            Font = font;
            Program.Window.MouseWheelMoved += (o, e) => ScrollText(e);
        }

        public void Update()
        {
            if (Count < 1) return;

            PixelSnap();

            if (!Program.WindowFocused)
                ScrollToCurrentSong();
            //Draw
            
            DrawSongText();

            //Update all songs
            foreach (Song song in this)
                song.Update();
        }

        public void LoadFromDirectory(string path)
        {
            var regx = new Regex(@".*\.(wav|ogg|mp3|flac|mod|it|s3d|xm)");
            var fileList = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).AsParallel()
                .Where(s => regx.IsMatch(s));

            Task<Song>[] tasks = fileList.Select(s => Task.Run(() =>
            {
                try
                {
                    lock (Font)
                    {
                        return new Song(s, Font);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Corrupted File: " + Path.GetFileName(s) + " - skipped.");
                    return null;
                }

            })).ToArray();

            Task.WaitAll(tasks);

            foreach (var song in tasks.Where(song => song.Result != null))
                Add(song.Result);
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

        public void ScrollToCurrentSong()
        {
            if(Audio.CurrentSong == null) return;

            if (Audio.CurrentSong.Position.Y < 0)
            {
                updateText = true;

                float amount = (Math.Abs(Audio.CurrentSong.Position.Y) - 3f + charHeight) / charHeight;
                yScroll += charHeight * (int)amount;
            }
            else if (Audio.CurrentSong.Position.Y > 6)
            {
                updateText = true;

                float amount = (Math.Abs(Audio.CurrentSong.Position.Y) - 3f + charHeight) / charHeight;
                yScroll -= charHeight * (int)amount;
            }

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