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

        public Song(string _path, Font _font)
            : base("SongText", _font)
        {
            Path = _path;

            MetaData = MetaData.Create(_path);

            SetNameFromMetaData();

            this.DisplayedString = Name;

            //formatting
            this.CharacterSize = 14;
            this.Color = Color.Black;
            
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

        public void Draw(RenderWindow window)
        {
            this.Position += new Vector2f(0, 1);
            this.Color = Color.Black;
            window.Draw(this);
            this.Position -= new Vector2f(0, 1);
            this.Color = Color.White;
            window.Draw(this);
        }

        public void Play()
        {
            //TODO: play song.
        }

    }
}
