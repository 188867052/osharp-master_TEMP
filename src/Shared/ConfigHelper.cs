using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EFCore.Scaffolding.Extension.Models;

namespace EFCore.Scaffolding.Extension
{
    public static class ConfigHelper
    {
        private static readonly string file;

        static ConfigHelper()
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
            file = Directory.GetFiles(di.Parent.Parent.Parent.Parent.Parent.FullName, ".Scaffolding.xml", SearchOption.AllDirectories).FirstOrDefault();
        }

        public static ScaffoldConfig ScaffoldConfig => Deserialize(File.ReadAllText(file, Encoding.UTF8));

        private static ScaffoldConfig Deserialize(string xml)
        {
            using StringReader sr = new StringReader(xml);
            XmlSerializer xmldes = new XmlSerializer(typeof(ScaffoldConfig));
            return (ScaffoldConfig)xmldes.Deserialize(sr);
        }
    }
}
