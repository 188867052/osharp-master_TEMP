using System.Collections.Generic;
using Newtonsoft.Json;

namespace Liuliu.Demo.Web
{
    public class RouteInfo
    {
        public RouteInfo()
        {
            this.Parameters = new List<ParameterInfo>();
            this.HttpMethods = "GET";
        }

        public string HttpMethods { get; set; }

        public string Area { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public IList<ParameterInfo> Parameters { get; set; }

        public string ParameterString => JsonConvert.SerializeObject(this.Parameters, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

        public string Path { get; set; }

        public string Namespace { get; set; }
    }

    public class ParameterInfo
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public string BinderType { get; set; }
    }
}