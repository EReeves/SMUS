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
            RenderWindow window = new RenderWindow(new VideoMode(550, 100), "Smus", Styles.None);
            window.SetFramerateLimit(30);

            Font font = new Font(Directory.GetCurrentDirectory() + @"\Resources\Fonts\FiraSans-Regular.otf");

            ModuleContainer moduleContainer = new ModuleContainer();

            SongList songList = new SongList(window, font);
            DragWindow dragWindow = new DragWindow(window);

            moduleContainer.AddModule(songList);
            moduleContainer.AddModule(dragWindow);

            songList.LoadFromDirectory(@"C:\Users\reeve_000\Desktop\Music");

            while (IsRunning)
            {
                window.DispatchEvents();
                window.Clear(new Color(150,140,120));
                moduleContainer.Update();
                // list.Update(window);

                window.Display();
            }
        }
    }
}
