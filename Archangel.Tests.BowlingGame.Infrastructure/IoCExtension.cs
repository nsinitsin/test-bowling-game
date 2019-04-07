using Archangel.Tests.BowlingGame.Infrastructure.Services;
using SimpleInjector;

namespace Archangel.Tests.BowlingGame.Infrastructure
{
    public static class IoCExtension
    {
        public static void AddInfrastructure(this Container container)
        {
            container.AddServices();
            container.Register<IGame, Game>(Lifestyle.Transient);
        }
    }
}
