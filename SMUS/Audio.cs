using System;
using System.Windows.Forms;
using IrrKlang;
using SFML.Graphics;
using SMUS.Module;

namespace SMUS
{
    //Might as well be static considering it's on another thread.
    internal static class Audio
    {
        public static ISoundEngine Engine;
        public static ISound Current;
        public static Song CurrentSong;
        public static bool IsPlaying;
        public static State NextState;

        public enum State
        {
            Next,
            Shuffle,
            Repeat
        }

        public static void StartEngine()
        {
            try
            {
                Engine = new ISoundEngine();
                NextState = State.Next;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to find a working audio device. \n\n\nDetails: " + e.ToString());
            }
        }

        public static bool Play(Song song)
        {
            try
            {
                Stop();
                Current = Engine.Play2D(song.Path);              
                CurrentSong = song;
                IsPlaying = true;
                song.IsPlaying = true;
                song.Style = Text.Styles.Bold;
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public static bool Resume()
        {
            if (Current == null) return false;

            Engine.SetAllSoundsPaused(false);
            IsPlaying = true;
            return true;
        }

        public static void PlayNext(SongList sl, Song current)
        {
            if (Current == null || sl == null) return;

            switch (NextState)
            {
                case State.Next:
                    int index = sl.IndexOf(current);

                    if (sl.Count < index)
                        index = -1;

                    Play(sl[index + 1]);
                    break;

                case State.Shuffle:
                    var rand = new Random();
                    Play(sl[rand.Next(sl.Count)]);
                    break;

                case State.Repeat:
                    Play(current);
                    break;
            }
        }

        public static void Pause()
        {
            Engine.SetAllSoundsPaused(true);
            IsPlaying = false;
        }

        public static void Stop()
        {
            if (CurrentSong != null)
            {
                CurrentSong.IsPlaying = false;
                CurrentSong.Style = Text.Styles.Regular; 
            }

            IsPlaying = false;
            Engine.StopAllSounds();
        }
    }
}