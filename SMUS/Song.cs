using System;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using TagLib;
using MetaData = TagLib.File;

namespace SMUS
{
    //Contains both render and song data.
    class Song : Text
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public MetaData MetaData { get; private set; }
        public bool IsPlaying = false;

        private SongList songList;

        public Song(SongList _songList, Window window, string _path, Font _font)
            : base("SongText", _font)
        {
            Path = _path;
            songList = _songList;

            MetaData = MetaData.Create(_path);
            SetNameFromMetaData();
            this.DisplayedString = Name;

            //formatting
            this.CharacterSize = 14;
            this.Color = Color.Black;

            window.MouseButtonPressed += (o, e) => CollisionCheck(window, e);

        }

        private void SetNameFromMetaData()
        {
            bool title = !String.IsNullOrEmpty(MetaData.Tag.Title);
            bool artist = !String.IsNullOrEmpty(MetaData.Tag.FirstAlbumArtist);

            if (title && artist)
            {
                Name = MetaData.Tag.FirstAlbumArtist + " - " + MetaData.Tag.Title;
            }
            else
            {
                Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            }
        }

        private void CollisionCheck(Window window, MouseButtonEventArgs e)
        {
            if (e.Button != Mouse.Button.Left) return;
            if (!(Position.Y >= 0) || !(Position.Y <= window.Size.Y)) return;
            var pos = Mouse.GetPosition(window);
            if (pos.X >= Position.X && pos.Y >= Position.Y &&
                pos.X - Position.X <= GetLocalBounds().Width && pos.Y - Position.Y <= GetLocalBounds().Height)
            Play();
        }

        public void Draw(RenderWindow window)
        {
            this.Position += new Vector2f(0, 1);
            this.Color = Color.Black;
            window.Draw(this,RenderStates.Default);
            this.Position -= new Vector2f(0, 1);
            this.Color = Color.White;
            window.Draw(this,RenderStates.Default);
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

            if (Audio.Current.PlayPosition < Audio.Current.PlayLength - 10) return;
            IsPlaying = false;
            PlayNext();
        }

        public void Play()
        {
            Style = Styles.Bold;
            IsPlaying = true;
            Audio.Play(this);
        }

        public void PlayNext()
        {
            int index = songList.IndexOf(this);
            if (songList.Count < index)
                index = -1;

            songList[index+1].Play();
        }

    }
}
