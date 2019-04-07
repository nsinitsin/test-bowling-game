using System;
using Archangel.Tests.BowlingGame.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Archangel.Tests.BowlingGame.Common;
using Archangel.Tests.BowlingGame.Infrastructure.Services;
using Archangel.Tests.BowlingGame.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Archangel.Tests.BowlingGame.Tests
{
    /// <summary>
    /// This is integration test.
    /// </summary>
    public class GameIntegrationTests
    {
        private Configuration config;
        private Game game;

        public GameIntegrationTests()
        {
            config = new Configuration { MaxAmountOfFrames = 10 };
            game = new Game(config, new RollService(config), new ScoreCalculationService(new ScoreCalculationValidator(config)));
        }
        
        [Fact]
        public void RollAndScore_ExternalIncomeForGame_DifferentResults()
        {
            var rows = CsvDataReadHelper.GetData("Data\\GameTestData.csv");

            foreach (var row in rows)
            {
                var result = 0;
                var isExpectError = false;
                if (row[0].ToString().ToLower() == "error")
                    isExpectError = true;
                else
                    result = Convert.ToInt16(row[0]);

                var rolls = row.Skip(1).Take(row.Length - 1).Select(s => Convert.ToInt16(s)).ToList();
                var tGame = new Game(config, new RollService(config), new ScoreCalculationService(new ScoreCalculationValidator(config)));
                Action act = () => rolls.ForEach(s => tGame.Roll(s));
                if (isExpectError)
                {
                    act.Should().Throw<Exception>();
                }
                else
                {
                    act();
                    tGame.Score().Should().Be(result);
                }
            }
        }

        [Fact]
        public void Score_AddResultForOneBall2AndGetScore_Error()
        {
            var rolls = new List<int> {2};
            rolls.ForEach(s => game.Roll(s));
            Action act = () => game.Score();
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Score_InputDataFromTest_Score133()
        {
            var rolls = new List<int> { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6 };
            rolls.ForEach(s => game.Roll(s));
            var score = game.Score();
            score.Should().Be(133);
        }
        /*
        [Theory]
        [InlineData()]
        public void Roll__()
        {

        }

        [Theory]
        [InlineData()]
        public void Roll__()
        {

        }

        [Theory]
        [InlineData()]
        public void Roll__()
        {

        }

        [Theory]
        [InlineData()]
        public void Roll__()
        {

        }*/
    }
}