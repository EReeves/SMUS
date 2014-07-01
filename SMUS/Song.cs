using System;
using System.IO;
using SFML.Graphics;
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
            Name = MetaData.Tag.FirstAlbumArtist + " - " + MetaData.Tag.Title;

            if (String.IsNullOrEmpty(MetaData.Tag.FirstAlbumArtist) || String.IsNullOrEmpty(MetaData.Tag.Title))
                Name = System.IO.Path.GetFileNameWithoutExtension(Path);

            this.DisplayedString = Name;

            //formatting
            this.CharacterSize = 14;
            this.Color = Color.Black;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(this);
        }

        public void Play()
        {
            //TODO: play song.
        }

    }
}
