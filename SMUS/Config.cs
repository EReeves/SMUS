using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SFML.Audio;
using SFML.Graphics;

namespace SMUS
{
    static class Config
    {
        public static List<string> musicDirectories = new List<string>();
        public static Dictionary<string,Color> Colors = new Dictionary<string, Color>();

        public static void PopulateConfig(string path)
        {
            try
            {
                LoadXML(path);
            }
            catch(XmlException ex)
            {
                Console.WriteLine(ex + "\nError parsing config file.");
            }
        }

        private static void LoadXML(string path)
        {
            XDocument config = XDocument.Load(path);
            var elem = config.Elements();
            foreach (XElement d in config.Descendants("musicdir").Descendants("dir"))
            {
                musicDirectories.Add(d.Value);
            }

            XElement colours = config.Descendants("colours").First();

            foreach (XElement element in colours.Elements())
                Colors.Add(element.Name.LocalName, ColorFromXElement(element));
        }

        private static Color ColorFromXElement(XContainer elem)
        {
            try
            {
                byte r = Convert.ToByte(elem.Element("r").Value);
                byte g = Convert.ToByte(elem.Element("g").Value);
                byte b = Convert.ToByte(elem.Element("b").Value);
                byte a = Convert.ToByte(elem.Element("a").Value);
                return new Color(r, g, b, a);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error in colour section of config file" + ex);
            }
        }
    }
}
