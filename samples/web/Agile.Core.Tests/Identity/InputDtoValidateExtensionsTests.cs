using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using Agile.Core.Identity.Dtos;
using Agile.Web.Startups;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using OSharp.Entity;
using Shouldly;
using Xunit;

namespace Agile.Core.Tests.Identity
{
    public class InputDtoValidateExtensionsTests
    {
        [Fact]
        public void UserValidate()
        {
            UserInputDto dto = new UserInputDto();
            ShouldThrowExtensions.ShouldThrow<ValidationException>(() => dto.Validate(), "fds");
            dto = new UserInputDto() { UserName = "1" };
            ShouldThrowExtensions.ShouldThrow<ValidationException>(() => dto.Validate(), "fds");
            dto = new UserInputDto() { UserName = "1234" };
            ShouldThrowExtensions.ShouldThrow<ValidationException>(() => dto.Validate(), "fds");
        }

        [Fact(Skip = "Not Complete.")]
        public void TestApiRouteGenerator()
        {
            var routeInfos = new TestSite("Agile.Web").GetAllRouteInfo();
            var json = JsonConvert.SerializeObject(routeInfos, Formatting.Indented);
            Console.WriteLine(json);
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);

            var file = Directory.GetFiles(di.Parent.Parent.Parent.Parent.FullName, "json.json", SearchOption.AllDirectories).FirstOrDefault();
            File.WriteAllText(file, json);
        }
    }

    public class TestSite
    {
        private readonly string projectName;

        public TestSite(string projectname)
        {
            this.projectName = projectname;
        }

        public IList<RouteInfo> GetAllRouteInfo()
        {
            var dllFile = Directory.GetFiles(Environment.CurrentDirectory, $"{this.projectName}.dll", SearchOption.AllDirectories).FirstOrDefault();
            if (string.IsNullOrEmpty(dllFile))
            {
                throw new ArgumentException($"No {this.projectName}.dll file found under the directory: {Environment.CurrentDirectory}.");
            }

            Console.WriteLine($"the project name:{this.projectName}.");
            Console.WriteLine($"find dll file:{dllFile}.");

            Assembly assembly = Assembly.LoadFile(dllFile);
            Type type = assembly.GetTypes().FirstOrDefault(o => o.Name == "Startup");

            if (type == null)
            {
                throw new ArgumentException($"No Startup.cs class found under the dll file: {dllFile}.");
            }

            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(AppContext.BaseDirectory)
                .UseStartup(type);

            TestServer server = new TestServer(builder);
            IRouteAnalyzer services = (IRouteAnalyzer)server.Host.Services.GetService(typeof(IRouteAnalyzer));
            var client = server.CreateClient();
            return services.GetAllRouteInfo();
        }
    }
}
