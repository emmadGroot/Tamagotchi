using System.Text.Json;

namespace Tamagotchi
{
    public static class SaveUtilities
    {
        const string path = "save.json";

        private static readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true,
        };

        public static void SaveStats(Stats stats)
        {
            string json = JsonSerializer.Serialize(stats, options);
            File.WriteAllText(path, json);
        }
        public static Stats GetStats()
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Stats>(json, options);
        }

        public static void ResetStats()
        {
            File.Delete(path);
        }

    }
}
