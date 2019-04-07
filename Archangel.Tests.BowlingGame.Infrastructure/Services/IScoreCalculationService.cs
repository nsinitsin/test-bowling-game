using System.Collections.Generic;
using Archangel.Tests.BowlingGame.Infrastructure.Models;

namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    internal interface IScoreCalculationService
    {
        ServiceResult<short> Calculate(IList<Frame> frames);
    }
}