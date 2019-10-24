using System.Collections.Generic;
using System.Linq;
using EFCore.Scaffolding.Extension;

namespace OSharp.AspNetCore.CodeGeneration
{
    public static class ScaffoldingHelper
    {
        public static IEnumerable<string> Scaffolding(string @namespace, string contextName, string writeCodePath)
        {
            var generator = new DbContextGenerator(@namespace, contextName, writeCodePath);
            generator.WriteTo();

            return generator.WriteAllTextModels.Select(o => o.Code);
        }
    }
}
