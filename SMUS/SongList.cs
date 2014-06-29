using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace SMUS
{
    class SongList : List<Song>
    {
        private readonly Vector2f basePosition = new Vector2f(0, 0);
        private bool updateText = true;

        public SongList()
        {

        }

        public void Update(RenderWindow window)
        {
            if (this.Count > 0)
                DrawSongText(window);
        }

        private void DrawSongText(RenderWindow window)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (updateText)
                {
                    this[i].Position = basePosition;
                    float charHeight = this.First().GetLocalBounds().Height;
                    this[i].Position += new Vector2f(0, charHeight);
                }
                this[i].Draw(window);
            }

            if (updateText)
                updateText = false;
        }
    }
}
