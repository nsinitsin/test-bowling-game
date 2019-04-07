using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Archangel.Tests.BowlingGame.Common;
using Archangel.Tests.BowlingGame.Infrastructure.Models;

[assembly: InternalsVisibleTo("Archangel.Tests.BowlingGame.Tests")]
namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    internal class ScoreCalculationValidator : IScoreCalculationValidator
    {
        private readonly int _minimumFrames;

        public ScoreCalculationValidator(Configuration configuration)
        {
            _minimumFrames = configuration.MaxAmountOfFrames;
        }

        public (bool, string) Validate(IList<Frame> frames)
        {
            if (frames == null)
                return (false, $"{nameof(frames)} is not initialized");

            if (frames.Count != _minimumFrames)
                return (false, $"You are in frame #{frames.Count}. For ending game you need to have {_minimumFrames} frames.");

            for (var i = 0; i < frames.Count - 1; i++)
            {
                var frame = frames[i];
                if (frame.PinsStruck == null)
                    return (false, $"{nameof(frame.PinsStruck)} is not initialized.");

                if (frame.PinsStruck.Count == 0)
                    return (false, $"{nameof(frame)} #{i + 1} is not finished.");

                var firstRoundVal = frame.PinsStruck[0];
                if (firstRoundVal == 10 && frame.Type != TypeOfFrame.Strike)
                    return (false, $"{nameof(frame)} #{i + 1} is configured incorrectly");
                
                if (frame is Frame && frame.Type != TypeOfFrame.Strike)
                {
                    if (frame.PinsStruck.Count == 1)
                        return (false, $"{nameof(frame)} #{i + 1} is not finished.");

                    var secondRoundVal = frame.PinsStruck[1];
                    var sum = firstRoundVal + secondRoundVal;
                    if (sum == 10 && frame.Type != TypeOfFrame.Spare)
                        return (false, $"{nameof(frame)} #{i + 1} is configured incorrectly");
                }
            }

            var lastFrame = frames[frames.Count - 1];
            if (lastFrame.Type == TypeOfFrame.Strike || lastFrame.Type == TypeOfFrame.Spare)
            {
                if (lastFrame.PinsStruck.Count != 3)
                    return (false, $"Frame #{frames.Count} is not finished.");
            }
            else
            {
                if (lastFrame.PinsStruck.Count != 2)
                    return (false, $"Frame #{frames.Count} is configured incorrectly");
            }

            return (true, "");
        }
    }
}