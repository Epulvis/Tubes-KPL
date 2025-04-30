using System.Text.Json;

namespace Tubes_KPL.src.Infrastructure.Configuration
{
    class JsonConfigProvider : IConfigProvider
    {
        private readonly string _filePath;
        private Dictionary<string, object> _configurations;

        public JsonConfigProvider(string filePath)
        {
            _filePath = filePath;
            _configurations = JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(_filePath));

            // Debugging: Log isi _configurations
            Console.WriteLine(JsonSerializer.Serialize(_configurations, new JsonSerializerOptions { WriteIndented = true }));
        }

        public T GetConfig<T>(string key)
        {
            if (_configurations.TryGetValue(key, out var value))
            {
                // Jika value sudah bertipe T, langsung kembalikan
                if (value is T typedValue)
                    return typedValue;

                // Jika value adalah JsonElement, deserialisasi ulang ke tipe T
                if (value is JsonElement jsonElement)
                    return jsonElement.Deserialize<T>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

                // Jika value adalah string, coba deserialisasi ke tipe T
                if (value is string stringValue)
                    return JsonSerializer.Deserialize<T>(stringValue)!;
            }

            return default!;
        }

        public void SetConfig<T>(string key, T value)
        {
            _configurations[key] = value!;
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_configurations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
