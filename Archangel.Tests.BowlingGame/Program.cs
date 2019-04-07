using Archangel.Tests.BowlingGame.Infrastructure;
using System;

namespace Archangel.Tests.BowlingGame
{
    class Program
    {
        private const string ExitCommand = "exit";
        private const string RollCommand = "roll";
        private const string ScoreCommand = "score";

        static void Main(string[] args)
        {
            var container = ContainerCreator.InitializeContainer();
            var game = container.GetInstance<IGame>();
            Console.WriteLine("Commands:");
            Console.WriteLine($"`{ExitCommand}` - for stoping execution and terminate application");
            Console.WriteLine($"`{RollCommand} 1` - for adding 1 pin in the frame");
            Console.WriteLine($"`{ScoreCommand}` - for showing score");

            while (true)
            {
                var userCommand = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userCommand))
                    continue;

                if (userCommand.ToLower() == ExitCommand)
                    return;
                try
                {
                    if (userCommand.ToLower().Contains(RollCommand))
                    {
                        var strArray = userCommand.Split(" ");
                        if (strArray.Length < 2 || !short.TryParse(strArray[1], out var pins))
                        {
                            Console.WriteLine($"{RollCommand} command is incorrect. Try again. Template is `{RollCommand} 1`");
                            continue;
                        }

                        game.Roll(pins);
                        continue;
                    }

                    if (userCommand.ToLower() == ScoreCommand)
                    {
                        Console.WriteLine($"Score: {game.Score()}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
