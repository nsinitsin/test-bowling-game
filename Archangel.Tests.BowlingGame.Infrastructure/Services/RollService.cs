using System;
using System.Collections.Generic;
using System.Linq;
using Archangel.Tests.BowlingGame.Common;
using Archangel.Tests.BowlingGame.Infrastructure.Models;

namespace Archangel.Tests.BowlingGame.Infrastructure.Services
{
    internal class RollService : IRollService
    {
        private readonly int _maxAmountOfFrames;

        public RollService(Configuration configuration)
        {
            _maxAmountOfFrames = configuration.MaxAmountOfFrames;
        }

        public void Roll(IList<Frame> frames, short pins)
        {
            if (frames == null)
                throw new ArgumentNullException(nameof(frames));

            var frame = frames.LastOrDefault();
            if (frame == null)
            {
                frame = new Frame();
                frames.Add(frame);
            }
            else if (!frame.IsAvailableToAddMorePins && frames.Count < _maxAmountOfFrames)
            {
                var newFrame = frames.Count < _maxAmountOfFrames - 1 ? new Frame() : new LastFrame();
                newFrame.PreviousFrame = frame;

                frame.NextFrame = newFrame;
                frames.Add(newFrame);
                frame = newFrame;
            }

            if (frame.TryToAddPins(pins)) return;
            if (frame.IsAvailableToAddMorePins)
                throw new Exception($"You can't add to the system {pins} pins.");
            else
                throw new Exception($"Game is ended. You can't add more pins");
        }
    }
}