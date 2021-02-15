using Microsoft.Extensions.Localization;
using System.Reflection;

namespace iPassport.Application.Resources
{
    public class Resource
    {
        private readonly IStringLocalizer localizer;

        public Resource(IStringLocalizerFactory factory)
        {
            var type = typeof(Resource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            localizer = factory.Create("Resource", assemblyName.Name);
        }

        public string GetMessage(string resource)
        {
            return localizer[resource].Value;
        }
    }
}
