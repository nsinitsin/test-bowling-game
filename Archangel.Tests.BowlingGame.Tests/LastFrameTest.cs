using System;
using Archangel.Tests.BowlingGame.Infrastructure.Models;
using FluentAssertions;
using Xunit;

namespace Archangel.Tests.BowlingGame.Tests
{
    public class LastFrameTest
    {
        private readonly LastFrame _frame;

        public LastFrameTest()
        {
            _frame = new LastFrame();
        }

        [Theory]
        [InlineData(11)]
        public void TryToAddPins_IncorrectValues_FalseOnTryingToAdd(short value)
        {
            var result = _frame.TryToAddPins(value);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 11)]
        [InlineData(1, 10)]
        [InlineData(5, 6)]
        public void TryToAddPins_IncorrectValuesForSrcondRounds_ResultOfAddingFalse(short firstBall, short secondBall)
        {
            var result = _frame.TryToAddPins(firstBall);
            result.Should().BeTrue();
            result = _frame.TryToAddPins(secondBall);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(10, 10, 10, true, true)]
        [InlineData(1, 9, 10, true, true)]
        [InlineData(5, 5, 10, true, true)]
        [InlineData(10, 5, 5, true, true)]
        [InlineData(10, 10, 5, true, true)]
        [InlineData(2, 2, 5, true, false)]
        [InlineData(10, 5, 6, true, true)]
        [InlineData(5, 5, 6, true, true)]
        [InlineData(2, 9, 9, false, false)]
        [InlineData(2, 11, 9, false, false)]
        public void TryToAddPins_AddingUpToThreeballs_ResultOfAddingDifferent(short firstBall, short secondBall, short thirdBall, bool secondResult, bool thirdResult)
        {
            _frame.TryToAddPins(firstBall);

            if (secondBall >= 0)
            {
                var result = _frame.TryToAddPins(secondBall);
                result.Should().Be(secondResult);
            }

            if (thirdBall >= 0)
            {
                var result = _frame.TryToAddPins(thirdBall);
                result.Should().Be(thirdResult);
            }
        }

        [Theory]
        [InlineData(10, -1, -1, TypeOfFrame.Strike)]
        [InlineData(1, -1, -1, TypeOfFrame.Normal)]
        [InlineData(1, 9, -1, TypeOfFrame.Spare)]
        [InlineData(1, 3, -1, TypeOfFrame.Normal)]
        [InlineData(1, 9, 10, TypeOfFrame.Spare)]
        [InlineData(10, 1, 91, TypeOfFrame.Strike)]
        public void Type_AddingDifferentValidCombinations_CorrectTypes(short firstRound, short secondRound, short thirdRound,
            TypeOfFrame resultTypeOfFrame)
        {
            _frame.TryToAddPins(firstRound);

            if (secondRound >= 0)
                _frame.TryToAddPins(secondRound);

            if (thirdRound >= 0)
                _frame.TryToAddPins(thirdRound);

            _frame.Type.Should().Be(resultTypeOfFrame);
        }

        [Fact]
        public void Score_DoubleStrikeType_ShouldGet2NextBalls()
        {
            var frame = new Frame();
            var frame2 = new LastFrame();
            frame.TryToAddPins(10);
            frame2.TryToAddPins(10);
            frame2.TryToAddPins(1);
            frame2.TryToAddPins(5);
            frame.NextFrame = frame2;
            frame2.PreviousFrame = frame;
            frame.Score.Should().Be(21);
        }
    }
}