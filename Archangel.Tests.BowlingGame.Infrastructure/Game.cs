using Archangel.Tests.BowlingGame.Infrastructure.Models;
using System;
using System.Collections.Generic;
using Archangel.Tests.BowlingGame.Common;
using Archangel.Tests.BowlingGame.Infrastructure.Services;

namespace Archangel.Tests.BowlingGame.Infrastructure
{
    class Game : IGame
    {
        private readonly IRollService _rollService;
        private readonly IScoreCalculationService _scoreCalculationService;
        private readonly IList<Frame> Frames;

        public Game(Configuration configuration, IRollService rollService, IScoreCalculationService scoreCalculationService)
        {
            _rollService = rollService;
            _scoreCalculationService = scoreCalculationService;
            Frames = new List<Frame>(configuration.MaxAmountOfFrames);
        }

        public void Roll(int pins)
        {
            if (pins > short.MaxValue)
                throw  new Exception($"Amount of pins can't be more then {short.MaxValue}");

            _rollService.Roll(Frames, (short)pins);
        }

        public int Score()
        {
            var scoreResult = _scoreCalculationService.Calculate(Frames);
            if (!scoreResult.IsSuccess)
                throw new Exception(scoreResult.Error);

            return scoreResult.Result;
        }
    }
}
