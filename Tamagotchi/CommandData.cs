namespace Tamagotchi
{
    public class CommandData
    {
        public string CommandName { get; private set; }
        public Action<string[]> Action { get; private set; }
        public string Description { get; private set; }

        public CommandData(string commandName, Action<string[]> action, string description)
        {
            CommandName = commandName;
            Action = action;
            Description = description;
        }
    }
}
