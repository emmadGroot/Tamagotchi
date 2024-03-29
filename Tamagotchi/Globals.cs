namespace Tamagotchi
{
    internal class Globals
    {
        public static readonly List<CommandData> commands = new()
        {
            new CommandData("help", Commands.Help, "Shows available commands - add parameter to get help on only one command"),
            new CommandData("stats", Commands.ShowStats, "Shows your Tamagotchi's stats"),
            new CommandData("feed", Commands.Feed, "Feed your Tamagotchi - add parameter to feed more or less"),
            new CommandData("play", Commands.Play, "Play with your Tamagotchi - add parameter to play more or less"),
            new CommandData("rename", Commands.Rename, "Rename your Tamagotchi - add parameter to skip prompt"),
            new CommandData("clear", Commands.Clear, "Clear the console"),
            new CommandData("kill", Commands.Kill, "Almost kill your Tamagothi (for debug purposes)"),
            new CommandData("exit", Commands.Exit, "Exit the application")
        };

        public static Stats? stats;
    }
}
