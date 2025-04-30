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
            _configurations = File.Exists(_filePath)
                ? JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(_filePath)) ?? new()
                : new();
        }

        public T GetConfig<T>(string key)
        {
            return _configurations.TryGetValue(key, out var value) ? JsonSerializer.Deserialize<T>(value.ToString()!) : default!;
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
