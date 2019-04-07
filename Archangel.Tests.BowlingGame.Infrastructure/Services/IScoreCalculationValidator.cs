using System.Collections.Generic;
using Archangel.Tests.BowlingGame.Infrastructure.Models;

namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    public interface IScoreCalculationValidator
    {
        (bool, string) Validate(IList<Frame> frames);
    }
}