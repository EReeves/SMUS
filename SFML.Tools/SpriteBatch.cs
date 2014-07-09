using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace SFML.Tools
{
    public class SpriteBatch : List<BatchedSprite>
    {
        public Texture Atlas { get; set; }
        public Vertex[] Vertices { get; set; }
        public RenderStates State { get; set; }

        public SpriteBatch(Texture atlas)
        {
            Atlas = atlas;
            State = new RenderStates(Atlas);
        }

        public void Render(RenderTarget rt)
        {
            if(Vertices != null)
                rt.Draw(Vertices, PrimitiveType.Quads, State);     
        }

        public void SortZ()
        {
            this.Sort((a,b) => a.ZOrder.CompareTo(b.ZOrder));
        }

        public void CalculateVertices()
        {
            foreach (BatchedSprite bs in this)
                bs.UpdateVertices();

            Vertices = this.SelectMany(x => x.Vertices).ToArray();           
        }
    }
}
