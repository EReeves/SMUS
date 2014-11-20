using System.Threading.Tasks;
using IrrKlang;
using SFML.Window;

namespace SMUS.Module
{
    class HotKeys : Module
    {
        private readonly ProgressBar progressBar;
        private readonly SongList songList;
        private readonly VolumeControl volumeControl;

        public HotKeys(ProgressBar progressBar, SongList songList, VolumeControl volumeControl)
        {
            this.progressBar = progressBar;
            this.songList = songList;
            this.volumeControl = volumeControl;
            Program.Window.KeyPressed += (s,e) => ArrowKeys(e);
            
        }

        public override void Update()
        {
            //Global hotkeys.

            //Left
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt) ))
            {
                Audio.PlayPrev(Audio.CurrentSong);
            }

            //Right
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt)))
            {
                Audio.PlayNext(Audio.CurrentSong);
            }

            //Up (volume)
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt)))
            {
                volumeControl.Up(0.1f);
            }

            //Down (volume)
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) || Keyboard.IsKeyPressed(Keyboard.Key.RAlt)))
            {
                volumeControl.Down(0.1f);
            }

#if !MONO //I have no idea how to do this with mono, not too worried at the moment.
         
            //Media keys/WinAPI
            if (WINKeyboard.IsKeyDownOnce(0xB0)) //NextTrack VK
            {
                Audio.PlayNext(Audio.CurrentSong);
            }

            if (WINKeyboard.IsKeyDownOnce(0xB1)) //PrevTrack VK
            {
                Audio.PlayPrev(Audio.CurrentSong);
            }

            if (WINKeyboard.IsKeyDownOnce(0xB3)) //Play/Pause VK
            {
                if (Audio.IsPlaying)
                    Audio.Pause();
                else
                    Audio.Resume();
            }

#endif
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
                    Audio.PlayPrev(Audio.CurrentSong);
                    songList.ScrollToCurrentSong();
                    break;
                case Keyboard.Key.Down:
                    Audio.PlayNext(Audio.CurrentSong);
                    songList.ScrollToCurrentSong();
                    break;
            }

        }
    }
}
