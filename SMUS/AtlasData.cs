using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SMUS
{
    class AtlasData : Dictionary<string, IntRect>
    {
        public Texture AtlasTexture { get; private set; }
    
        /// <summary>
        /// Contains Atlas Texture and Data.
        /// </summary>
        /// <param name="path">Path without extension.</param>
        public AtlasData(string path)
        {
            AtlasTexture = new Texture(path + ".png");
            string[] dataFile = File.ReadAllLines(path + ".txt");

            foreach (string[] split in dataFile.Select(s => s.Split(' ')))
            {
                Add(split[0], new IntRect(Convert.ToInt32(split[2]),
                    Convert.ToInt32(split[3]),
                    Convert.ToInt32(split[4]),
                    Convert.ToInt32(split[5])));
            }
        }
    }
}
