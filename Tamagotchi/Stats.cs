namespace Tamagotchi
{
    public record Stats
    {
        public string Name { get; set; } = string.Empty;
        public int Hunger { get; set; }
        public int Boredom { get; set; }
        public DateTime Created { get; set; }
    }
}
