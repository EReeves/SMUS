using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrrKlang;
using SFML.Window;

namespace SMUS.Module
{
    class HotKeys : Module
    {
        private readonly ProgressBar progressBar;
        private readonly SongList songList;

        public HotKeys(ProgressBar progressBar, SongList songList)
        {
            this.progressBar = progressBar;
            this.songList = songList;
            Program.Window.KeyPressed += (s,e) => ArrowKeys(e);
        }

        public override void Update()
        {
            
        }

        private void ArrowKeys(KeyEventArgs e)
        {
            if (Audio.CurrentSong == null) return;

            var pos = Audio.Current.PlayLength/100;

            switch (e.Code)
            {
                case Keyboard.Key.Left:
                    if(Audio.Current.PlayPosition > 0)
                        Audio.Current.PlayPosition -= pos;
                    break;
                case Keyboard.Key.Right:
                    if (Audio.Current.PlayPosition < Audio.Current.PlayLength)
                        Audio.Current.PlayPosition += pos;
                    break;
                case Keyboard.Key.Up:
                    Audio.PlayPrev(songList, Audio.CurrentSong);
                    songList.ScrollToCurrentSong();
                    break;
                case Keyboard.Key.Down:
                    Audio.PlayNext(songList, Audio.CurrentSong);
                    songList.ScrollToCurrentSong();
                    break;
            }

        }
    }
}
