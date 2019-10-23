using System;
using System.IO;
using System.Linq;
using EFCore.Scaffolding.Extension;

namespace OSharp.AspNetCore.CodeGeneration
{
    public static class Program
    {
        public static void Main()
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
            var scaffoldingFile = di.Parent.Parent.Parent.Parent.Parent.GetFiles(".Scaffolding.xml", SearchOption.AllDirectories).FirstOrDefault();
            var list = ScaffoldingHelper.Scaffolding("Entities", "OSharpDbContext", scaffoldingFile.Directory.FullName);
        }
    }
}
