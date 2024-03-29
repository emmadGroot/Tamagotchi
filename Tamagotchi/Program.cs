using Spectre.Console; // i recommend using https://learn.microsoft.com/en-us/windows/terminal/install to get the most out of Spectre Console
using System.Runtime.InteropServices;

namespace Tamagotchi
{
    internal class Program
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        private static readonly Random random = new();

        public static readonly List<CommandData> commands = new()
        {
            new CommandData("help", Commands.Help, "Shows available commands - add parameter to get help on only one command"),
            new CommandData("stats", Commands.ShowStats, "Shows your Tamagotchi's stats"),
            new CommandData("feed", Commands.Feed, "Feed your Tamagotchi - add parameter to feed more or less"),
            new CommandData("play", Commands.Play, "Play with your Tamagotchi - add parameter to play more or less"),
            new CommandData("rename", Commands.Rename, "Rename your Tamagotchi - add paramter to skip prompt"),
            new CommandData("clear", Commands.Clear, "Clear the console"),
            new CommandData("kill", Commands.Kill, "Almost kill your Tamagothi (for debug purposes)"),
            new CommandData("exit", Commands.Exit, "Exit the application")
        };

        private static bool alive = true;
        public static Stats? stats;

        static void Main()
        {
            // load stats
            if (File.Exists("save.json"))
            {
                stats = SaveUtilities.GetStats();
            }
            // create new tamagotchi
            else
            {
                stats = new()
                {
                    Hunger = 100,
                    Boredom = 0,
                    Created = DateTime.Now
                };
                string name = "";
                while (name == "")
                {
                    AnsiConsole.Markup("[blue]New Tamagotchi detected! Please enter a name:[/]\r\n> ");
                    name = Console.ReadLine();
                    Console.Clear();
                }
                stats.Name = name;
                SaveUtilities.SaveStats(stats);
            }

            AnsiConsole.Markup("run [green]help[/] to get started\r\n");
            StartLifeCycle();

            while (alive)
            {
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(" ");
                // stop running commands if the tamagotchi is dead
                if (alive)
                {
                    if (input[0] == "")
                    {
                        Console.WriteLine("Please enter a command");
                        continue;
                    }
                    List<CommandData> command = commands.Where(x => x.CommandName == input[0]).ToList();
                    if (command.Count > 0)
                    {
                        command[0].Action(input[1..]);
                    }
                    else
                    {
                        Console.WriteLine("Command not found");
                    }
                }
            }
            // keep the app open
            while (true);
        }

        private static void StartLifeCycle()
        {
            System.Timers.Timer statTimer;
            statTimer = new System.Timers.Timer(2 * 1000);
            // runs every 2 seconds
            statTimer.Elapsed += (s, e) =>
            {
                if (stats != null)
                {
                    // both hunger and boredom have a 1/5 chance of changing
                    if (random.Next(0, 4) == 0)
                        stats.Hunger -= 5;
                    if (random.Next(0, 4) == 0)
                        stats.Boredom += 5;
                    SaveUtilities.SaveStats(stats);
                    if (stats.Hunger < 0 || stats.Boredom > 100)
                    {
                        statTimer.Stop();
                        SaveUtilities.ResetStats();
                        alive = false;
                        // show a messagebox, then after, close the program
                        MessageBox(0, "Your Tamagotchi died!", "RIP", 0);
                        Environment.Exit(0);
                    }
                }
            };
            statTimer.Enabled = true;
        }
    }
}
