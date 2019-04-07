using System.Collections.Generic;
using System.Linq;
using Archangel.Tests.BowlingGame.Infrastructure.Models;
using Archangel.Tests.BowlingGame.Infrastructure.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Archangel.Tests.BowlingGame.Tests
{
    public class ScoreCalculationServiceTests
    {
        private Mock<IScoreCalculationValidator> validationMock;
        private ScoreCalculationService service;

        public ScoreCalculationServiceTests()
        {
            validationMock = new Mock<IScoreCalculationValidator>();
            validationMock.Setup(s => s.Validate(It.IsAny<IList<Frame>>()))
                .Returns(() => (true, ""));
            service = new ScoreCalculationService(validationMock.Object);
        }

        
    }
}