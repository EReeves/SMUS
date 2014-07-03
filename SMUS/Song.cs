﻿using System;
using System.Text.RegularExpressions;
using SFML.Graphics;
using SFML.Window;
using SMUS.Module;
using MetaData = TagLib.File;

namespace SMUS
{
    //Contains both render and song data.
    internal class Song : Text
    {
        private readonly SongList songList;

        public bool IsPlaying = false;
        public string Name { get; private set; }
        public string Path { get; private set; }
        public MetaData MetaData { get; private set; }

        public Song(SongList _songList, string _path, Font _font)
            : base("SongText", _font)
        {
            Path = _path;
            songList = _songList;


            MetaData = MetaData.Create(_path);
            SetNameFromMetaData();
            DisplayedString = Name;

            //formatting
            CharacterSize = 14;
            Color = Color.Black;

            Program.Window.MouseButtonPressed += (o, e) => CollisionCheck(e);
        }

        public void Draw(RenderWindow window)
        {
            //Shadow
            Position += new Vector2f(0, 1);
            Color = Color.Black;
            window.Draw(this, RenderStates.Default);
            //Text
            Position -= new Vector2f(0, 1);
            Color = Config.Colors.Text;
            window.Draw(this, RenderStates.Default);
        }

        public void Update()
        {
            if (!IsPlaying)
            {
                Style = Styles.Regular;
                return;
            }

            if (Audio.CurrentSong != this)
            {
                IsPlaying = false;
                return;
            }

            if (Audio.Current.PlayPosition < Audio.Current.PlayLength - 100) return;
            PlayNext();
            IsPlaying = false;
        }

        public void Play()
        {
            Style = Styles.Bold;
            IsPlaying = true;
            Audio.Play(this);
        }

        public void PlayNext()
        {
            if (!Audio.Shuffle)
            {
                int index = songList.IndexOf(this);
                if (songList.Count < index)
                    index = -1;

                songList[index + 1].Play();
            }
            else
            {
                var rand = new Random();
                songList[rand.Next(songList.Count)].Play();

            }
        }

        private void SetNameFromMetaData()
        {
            bool title = !String.IsNullOrEmpty(MetaData.Tag.Title);
            bool artist = !String.IsNullOrEmpty(MetaData.Tag.FirstPerformer);

            if (title && artist)
            {
                Name = MetaData.Tag.FirstPerformer + " - " + MetaData.Tag.Title;
            }
            else if (!String.IsNullOrEmpty(MetaData.Tag.FirstAlbumArtist) && title)
            {
                Name = MetaData.Tag.FirstAlbumArtist + " - " + MetaData.Tag.Title;
            }
            else if (!title && artist)
            {
                Name = MetaData.Tag.FirstPerformer + " - " +
                       Regex.Replace(System.IO.Path.GetFileNameWithoutExtension(Path), @"[\d-]", "",
                           RegexOptions.Multiline)
                           .TrimStart();
            }
            else
            {
                Name = "Unknown Artist - " +              
                    Regex.Replace(System.IO.Path.GetFileNameWithoutExtension(Path), @"[\d-]", "",
                           RegexOptions.Multiline)
                           .TrimStart();
            }

            //Flacs seem to stay for some reason, manually remove.
            Name = Name.Replace(".flac", "");
            //Remove the brackets left over on duplicate files in Windows.
            Name = Name.Replace("()", "");
        }

        private void CollisionCheck(MouseButtonEventArgs e)
        {
            if (!Program.WindowFocused) return;
            if (e.Button != Mouse.Button.Left) return;
            if (!(Position.Y >= 0) || !(Position.Y <= Program.Window.Size.Y)) return;
            Vector2i pos = Mouse.GetPosition(Program.Window);
            if (pos.X >= Position.X && pos.Y >= Position.Y &&
                pos.X - Position.X <= GetLocalBounds().Width && pos.Y - Position.Y <= GetLocalBounds().Height)
                Play();
        }
    }
}