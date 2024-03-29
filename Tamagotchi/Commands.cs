using Spectre.Console;

namespace Tamagotchi
{
    public static class Commands
    {
        public static void Help(string[] args)
        {
            if (args.Length == 0)
                foreach (CommandData command in Globals.commands)
                    AnsiConsole.MarkupInterpolated($"[green]{command.CommandName}[/] - {command.Description}\r\n");
            else
            {
                List<CommandData> command = Globals.commands.Where(x => x.CommandName == args[0]).ToList();
                if (command.Count > 0)
                    AnsiConsole.MarkupInterpolated($"[green]{command[0].CommandName}[/] - {command[0].Description}\r\n");
            }
        }

        public static void ShowStats(string[] args)
        {
            TimeSpan diff = DateTime.Now - Globals.stats.Created;
            double hours = Math.Round(diff.TotalHours, 2);
            AnsiConsole.MarkupInterpolated($"[purple]Name[/]: {Globals.stats.Name}\r\n[purple]Hunger[/]: {Globals.stats.Hunger}\r\n[purple]Boredom[/]: {Globals.stats.Boredom}\r\n[purple]Age[/]: {hours} hrs\r\n");
        }

        public static void Feed(string[] args)
        {
            int feed = 10;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out feed);
                if (feed < 0)
                {
                    AnsiConsole.MarkupInterpolated($"That's [red]rude...[/]\r\n");
                    return;
                }
                if (feed == 0)
                {
                    feed = 10;
                }
            }
            AnsiConsole.MarkupInterpolated($"Fed your Tamagotchi, restored [blue]{feed}[/] hunger points\r\n");
            Globals.stats.Hunger += feed;
            if (Globals.stats.Hunger > 100) Globals.stats.Hunger = 100;
            SaveUtilities.SaveStats(Globals.stats);
        }

        public static void Play(string[] args)
        {
            int play = 10;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out play);
                if (play < 0)
                {
                    AnsiConsole.MarkupInterpolated($"That's [red]rude...[/]\r\n");
                    return;
                }
                if (play == 0)
                {
                    play = 10;
                }
            }
            AnsiConsole.MarkupInterpolated($"Played with your Tamagotchi, removed [blue]{play}[/] boredom points\r\n");
            Globals.stats.Boredom -= play;
            if (Globals.stats.Boredom < 0) Globals.stats.Boredom = 0;
            SaveUtilities.SaveStats(Globals.stats);
        }

        public static void Rename(string[] args)
        {
            if (args.Length > 0)
            {
                Globals.stats.Name = string.Join(" ", args);
            }
            else
            {
                Globals.stats.Name = AnsiConsole.Ask<string>("[blue]New name:[/]");
            }
            SaveUtilities.SaveStats(Globals.stats);
        }

        public static void Clear(string[] args)
        {
            Console.Clear();
        }

        public static void Kill(string[] args)
        {
            AnsiConsole.Markup("[darkorange]You brought this upon yourself....[/]\r\n");
            Globals.stats.Hunger = 1;
            Globals.stats.Boredom = 99;
        }

        public static void Exit(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
