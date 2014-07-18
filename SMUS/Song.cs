using System;
using System.Text.RegularExpressions;
using SFML.Graphics;
using SFML.Window;
using SMUS.Module;
using TagLib;
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

        public Song(SongList _songList, string _path, Font _font)
            : base("SongText", _font)
        {
            Path = _path;
            songList = _songList;

            

            MetaData md = MetaData.Create(_path);
            SetNameFromMetaData(md);
            DisplayedString = Name;

            //formatting
            CharacterSize = 14;
            Color = Color.Black;

            Program.Window.MouseButtonPressed += (o, e) => CollisionCheck(e);

            md.Dispose();

        }

        public void Draw(RenderWindow window)
        {
            if(!IsVisible()) return;
            
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
            if (IsPlaying && FinishedPlaying())
            {
                Style = Styles.Regular;
                Audio.PlayNext(songList, this);
                IsPlaying = false;
            }
        }

        public void Play()
        {
            Style = Styles.Bold;
            IsPlaying = true;
            Audio.Play(this);
        }

        public bool FinishedPlaying()
        {
            return !Audio.Engine.IsCurrentlyPlaying(Path);
        }

        private void SetNameFromMetaData(MetaData md)
        {
            bool title = !String.IsNullOrEmpty(md.Tag.Title);
            bool artist = !String.IsNullOrEmpty(md.Tag.FirstPerformer);

            if (title && artist)
            {
                Name = md.Tag.FirstPerformer + " - " + md.Tag.Title;
            }
            else if (!String.IsNullOrEmpty(md.Tag.FirstAlbumArtist) && title)
            {
                Name = md.Tag.FirstAlbumArtist + " - " + md.Tag.Title;
            }
            else if (!title && artist)
            {
                Name = md.Tag.FirstPerformer + " - " +
                        // ReSharper disable once AssignNullToNotNullAttribute
                       Regex.Replace(input: System.IO.Path.GetFileNameWithoutExtension(Path), pattern: @"[\d-]", replacement: "", options: RegexOptions.Multiline)
                           .TrimStart();
            }
            else
            {
                Name = "Unknown Artist - " +              
                    // ReSharper disable once AssignNullToNotNullAttribute
                    Regex.Replace(input: System.IO.Path.GetFileNameWithoutExtension(Path), pattern: @"[\d-]", replacement: "", options: RegexOptions.Multiline)
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
            if (pos.X >= Program.Window.Size.X - Program.Window.Size.X/4) return;
            if (pos.X >= Position.X && pos.Y >= Position.Y &&
                pos.X - Position.X <= GetLocalBounds().Width && pos.Y - Position.Y <= GetLocalBounds().Height)
                Play();
        }

        private bool IsVisible()
        {
            return Position.Y > -GetLocalBounds().Height && Position.Y < Program.Window.Size.Y;
        }
    }
}