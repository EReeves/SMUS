using SFML.Graphics;
using SFML.Window;

namespace SFML.Tools
{
    public class BatchedSprite
    {
        public Vector2f Position { get; set; }
        public IntRect AtlasPosition { get; set; }
        public int ZOrder { get; set; }
        public Vertex[] Vertices { get; set; }
        public Color Colour { get; set; }

        public BatchedSprite(Vector2f pos, IntRect rect, int zOrder)
        {
            Position = pos;
            AtlasPosition = rect;
            ZOrder = zOrder;

            Colour = Color.White;

            Vertices = new Vertex[4];
        }

        public void UpdateVertices()
        {   
            //Verts
            Vertices[0].Position = Position;
            Vertices[1].Position = Position + new Vector2f(AtlasPosition.Width, 0);
            Vertices[2].Position = Position + new Vector2f(AtlasPosition.Width, AtlasPosition.Height);
            Vertices[3].Position = Position + new Vector2f(0, AtlasPosition.Height);
            //Texture coords
            Vertices[0].TexCoords = new Vector2f(AtlasPosition.Left, AtlasPosition.Top);
            Vertices[1].TexCoords = new Vector2f(AtlasPosition.Left + AtlasPosition.Width, AtlasPosition.Top);
            Vertices[2].TexCoords = new Vector2f(AtlasPosition.Left + AtlasPosition.Width, AtlasPosition.Top + AtlasPosition.Height);
            Vertices[3].TexCoords = new Vector2f(AtlasPosition.Left, AtlasPosition.Top + AtlasPosition.Height);
            //Colour
            for (int i = 0; i < 4; i++)
                Vertices[i].Color = Colour;
        }
    }
}
