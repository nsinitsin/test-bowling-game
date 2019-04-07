namespace Archangel.Tests.BowlingGame.Infrastructure
{
    public interface IGame
    {
        void Roll(int pins);
        int Score();
    }
}