using System.Text.Json.Serialization;
using System.Text.Json;
using Tubes_KPL.src.Domain.Models;

namespace API.Storage;

public class TugasStorage<T>
{

    // Mengambil daftar tugas dari file Tugas.json, jika file tidak ada maka mengembalikan list kosong
    private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Tugas.json");

    // Menyimpan data tugas ke file Tugas.json dalam format JSON yang rapi
    public static List<Tugas> GetTugas()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Tugas>();
        }

        var json = File.ReadAllText(FilePath);

        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };

        return JsonSerializer.Deserialize<List<Tugas>>(json, options) ?? new List<Tugas>();
    }

    public static void SaveTugas(T tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}