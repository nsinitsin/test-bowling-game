using SimpleInjector;

namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    static class IoCExtension
    {
        public static void AddServices(this Container container)
        {
            container.Register<IRollService, RollService>(Lifestyle.Transient);
            container.Register<IScoreCalculationValidator, ScoreCalculationValidator>(Lifestyle.Transient);
            container.Register<IScoreCalculationService, ScoreCalculationService>(Lifestyle.Transient);
        }
    }
}
