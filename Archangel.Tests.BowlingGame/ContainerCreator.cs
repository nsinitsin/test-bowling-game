using System.IO;
using Archangel.Tests.BowlingGame.Common;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Archangel.Tests.BowlingGame
{
    public static class ContainerCreator
    {
        public static Container InitializeContainer()
        {
            var iocContainer = new Container();
            IoCContainerHelper.AddContainer(iocContainer);

            var projectDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));

            var builder = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var buildedConfig = builder.Build();
            var config = new Configuration();
            buildedConfig.Bind(config);
            iocContainer.RegisterSingleton(() => config);
            return iocContainer;
        }
    }
}