using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static Colors Colors = new Colors();

        public static void PopulateConfig(string path)
        {
            try
            {
                LoadXML(path);
            }
            catch(XmlException ex)
            {
                //TODO: make this more descriptive.
                Console.WriteLine(ex + "\nError in config file.");
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
            {
                switch (element.Name.LocalName)
                {
                    case "background":
                        Colors.Background = ColorFromXElement(element);
                        break;
                    case "progressbar":
                        Colors.ProgressBar = ColorFromXElement(element);
                        break;
                    case "border":
                        Colors.Border = ColorFromXElement(element);
                        break;
                    case "text":
                        Colors.Text = ColorFromXElement(element);
                        break;
                    case "buttons":
                        Colors.Buttons = ColorFromXElement(element);
                        break;
                    case "buttonsfaded":
                        Colors.ButtonsFaded = ColorFromXElement(element);
                        break;
                    case "volume":
                        Colors.Volume = ColorFromXElement(element);
                        break;
                    case "shadow":
                        Colors.Shadow = ColorFromXElement(element);
                        break;
                }
            }
        }

        private static Color ColorFromXElement(XContainer elem)
        {
            byte r = Convert.ToByte(elem.Element("r").Value);
            byte g = Convert.ToByte(elem.Element("g").Value);
            byte b = Convert.ToByte(elem.Element("b").Value);
            byte a = Convert.ToByte(elem.Element("a").Value);

            return new Color(r,g,b,a);
        }
    }

    class Colors
    {
        public Color Background;
        public Color ProgressBar;
        public Color Border;
        public Color Text;
        public Color Buttons;
        public Color ButtonsFaded;
        public Color Volume;
        public Color Shadow;
    }
}
