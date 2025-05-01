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
            //Console.WriteLine(JsonSerializer.Serialize(_configurations, new JsonSerializerOptions { WriteIndented = true }));
        }

        //bintang : poin 5 Logging Handler 
        public T GetConfig<T>(string key)
        {
            if (!_configurations.TryGetValue(key, out var value))
            {
                Console.WriteLine($"[WARNING] Konfigurasi dengan key '{key}' tidak ditemukan.");
                return default!;
            }

            Console.WriteLine($"[INFO] Mengambil konfigurasi dengan key '{key}'.");

            // Jika value sudah bertipe T, langsung kembalikan
            if (value is T typedValue)
                return typedValue;

            // Jika value adalah JsonElement, deserialisasi ulang ke tipe T
            if (value is JsonElement jsonElement)
            {
                try
                {
                    return jsonElement.Deserialize<T>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Gagal mendeserialisasi konfigurasi dengan key '{key}' ke tipe '{typeof(T)}': {ex.Message}");
                    throw;
                }
            }

            // Jika value adalah string, coba deserialisasi ke tipe T
            if (value is string stringValue)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(stringValue)!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Gagal mendeserialisasi string konfigurasi dengan key '{key}' ke tipe '{typeof(T)}': {ex.Message}");
                    throw;
                }
            }

            // Jika tidak dapat dikonversi, lempar error
            Console.WriteLine($"[ERROR] Tidak dapat mengonversi konfigurasi dengan key '{key}' ke tipe '{typeof(T)}'.");
            throw new InvalidCastException($"Tidak dapat mengonversi konfigurasi dengan key '{key}' ke tipe '{typeof(T)}'.");
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
