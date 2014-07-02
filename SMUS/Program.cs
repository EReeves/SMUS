using System;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SMUS.Module;

namespace SMUS
{
    class Program
    {
        public static bool IsRunning = true;

        static void Main(string[] args)
        {
            var cs = new ContextSettings(32, 16, 2);
            RenderWindow window = new RenderWindow(new VideoMode(550, 104), "Smus", Styles.None, cs);
            window.SetFramerateLimit(60);

            Font font = new Font(Directory.GetCurrentDirectory() + "/Resources/Fonts/SourceSansPro-Regular.otf");

            ModuleContainer moduleContainer = new ModuleContainer();


            ProgressBar pBar = new ProgressBar(moduleContainer.Locks, window);
            SongList songList = new SongList(moduleContainer.Locks, window, font);
            DragWindow dragWindow = new DragWindow(moduleContainer.Locks, window);
            PlayButton playButton = new PlayButton(moduleContainer.Locks,window);
            Border border = new Border(moduleContainer.Locks, window);


            moduleContainer.AddModule(pBar);
            moduleContainer.AddModule(songList);
            moduleContainer.AddModule(dragWindow);
            moduleContainer.AddModule(playButton);
            moduleContainer.AddModule(border);

            songList.LoadFromDirectory("C:/Users/reeve_000/Desktop/Music");

            while (IsRunning)
            {
                window.DispatchEvents();
                window.Clear(new Color(70,50,40));

                moduleContainer.Update();

                window.Display();
            }
        }
    }
}
