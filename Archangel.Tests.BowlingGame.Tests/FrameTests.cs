using System;
using System.Collections.Generic;
using Archangel.Tests.BowlingGame.Infrastructure.Models;
using FluentAssertions;
using Xunit;

namespace Archangel.Tests.BowlingGame.Tests
{
    public class FrameTest
    {
        private Frame _frame;

        public FrameTest()
        {
            _frame = new Frame();
        }

        [Theory]
        [InlineData(11)]
        public void TryToAddPins_IncorrectValues_FalseOnTryingToAdd(short value)
        {
            var result = _frame.TryToAddPins(value);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(1,11)]
        [InlineData(1,10)]
        [InlineData(5,6)]
        public void TryToAddPins_IncorrectValuesForSrcondRounds_ResultOfAddingFalse(short firstBall, short secondBall)
        {
            var result = _frame.TryToAddPins(firstBall);
            result.Should().BeTrue();
            result = _frame.TryToAddPins(secondBall);
            result.Should().BeFalse();
        }

        [Fact]
        public void TryToAddPins_TryToAddThirdBall_ResultOfAddingFalse()
        {
            var result = _frame.TryToAddPins(9);
            result.Should().BeTrue();
            result = _frame.TryToAddPins(1);
            result.Should().BeTrue();
            result = _frame.TryToAddPins(5);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(10, -1, TypeOfFrame.Strike)]
        [InlineData(1, -1, TypeOfFrame.Normal)]
        [InlineData(1, 9, TypeOfFrame.Spare)]
        [InlineData(1, 3, TypeOfFrame.Normal)]
        public void Type_AddingDifferentValidCombinations_CorrectTypes(short firstRound, short secondRound,
            TypeOfFrame resultTypeOfFrame)
        {
            _frame.TryToAddPins(firstRound);

            if (secondRound >= 0)
                _frame.TryToAddPins(secondRound);

            _frame.Type.Should().Be(resultTypeOfFrame);
        }

        [Fact]
        public void Score_StrikeType_ShouldGet2NextBalls()
        {
            var frame = new Frame();
            var frame2 = new Frame();
            frame.TryToAddPins(10);
            frame2.TryToAddPins(1);
            frame2.TryToAddPins(9);
            frame.NextFrame = frame2;
            frame2.PreviousFrame = frame;
            frame.Score.Should().Be(20);
        }

        [Fact]
        public void Score_DoubleStrikeType_ShouldGet2NextBalls()
        {
            var frame = new Frame();
            var frame2 = new Frame();
            var frame3 = new Frame();
            frame.TryToAddPins(10);
            frame2.TryToAddPins(10);
            frame3.TryToAddPins(1);
            frame3.TryToAddPins(5);
            frame.NextFrame = frame2;
            frame2.PreviousFrame = frame;
            frame2.NextFrame = frame3;
            frame3.PreviousFrame = frame2;
            frame.Score.Should().Be(21);
        }
    }
}
