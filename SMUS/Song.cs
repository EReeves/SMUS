using SFML.Graphics;

namespace SMUS
{
    //Contains both render and song data.
    class Song : Text
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public MetaData MetaData { get; private set; }

        public Song(string _path, Font _font) : base("SongText", _font)
        {
            MetaData = new MetaData(_path);
            Path = _path;
            Name = MetaData.Title;
            this.DisplayedString = Name;
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
