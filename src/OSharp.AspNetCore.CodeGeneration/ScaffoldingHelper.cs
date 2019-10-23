using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EFCore.Scaffolding.Extension.Models;

namespace EFCore.Scaffolding.Extension
{
    public static class ScaffoldingHelper
    {
        public static IEnumerable<string> Scaffolding(string @namespace, string contextName, string writeCodePath)
        {
            DbContextGenerator generator = new DbContextGenerator(@namespace, contextName, writeCodePath);
            generator.WriteTo();

            return generator.WriteAllTextModels.Select(o => o.Code);
        }
    }
}
