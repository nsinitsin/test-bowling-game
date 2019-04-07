using System.Collections.Generic;
using Archangel.Tests.BowlingGame.Infrastructure.Models;

namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    internal interface IRollService
    {
        void Roll(IList<Frame> frames, short pins);
    }
}