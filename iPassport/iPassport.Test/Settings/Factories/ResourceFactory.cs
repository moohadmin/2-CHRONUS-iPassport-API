using iPassport.Application.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace iPassport.Test.Settings.Factories
{
    public static class ResourceFactory
    {
        public static Resource Create()
        {
            var localizationOptions = new LocalizationOptions();
            localizationOptions.ResourcesPath = "Resources";
            var options = Options.Create(localizationOptions);
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            return new Resource(factory);
        }
    }
}
