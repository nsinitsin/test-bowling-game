using Archangel.Tests.BowlingGame.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Archangel.Tests.BowlingGame.Tests")]
namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    class ScoreCalculationService : IScoreCalculationService
    {
        private readonly IScoreCalculationValidator _validator;

        public ScoreCalculationService(IScoreCalculationValidator validator)
        {
            _validator = validator;
        }

        public ServiceResult<short> Calculate(IList<Frame> frames)
        {
            if (frames == null)
                return new ServiceResult<short>(false, $"{nameof(frames)} is not initialized.");

            var validationResult = _validator.Validate(frames);
            if (!validationResult.Item1)
                return new ServiceResult<short>(false, validationResult.Item2);

            var frame = frames.LastOrDefault();
            return new ServiceResult<short>(frame?.Score ?? 0);
        }
    }
}