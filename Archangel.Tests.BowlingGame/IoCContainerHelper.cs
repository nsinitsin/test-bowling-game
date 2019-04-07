using System;
using System.Collections.Generic;
using System.Text;
using Archangel.Tests.BowlingGame.Infrastructure;
using SimpleInjector;

namespace Archangel.Tests.BowlingGame
{
    public static class IoCContainerHelper
    {
        public static void AddContainer(Container container)
        {
            container.AddInfrastructure();
        }
    }
}
