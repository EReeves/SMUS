using System;
using IrrKlang;

namespace SMUS
{
    class Audio
    {
        public static ISoundEngine Engine = new ISoundEngine();
        public static ISound Current;
        public static Song CurrentSong;
        public static bool IsPlaying;

        public static bool Play(Song song)
        {
            try
            {
                Stop();
                Current = Engine.Play2D(song.Path);
                CurrentSong = song;
                IsPlaying = true;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;    
        }

        public static void Stop()
        {
            IsPlaying = false;
            Engine.StopAllSounds();
        }
    }
}
