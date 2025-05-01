using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Tubes_KPL.src.Application.Libraries
{
    public static class JsonToTextConverter
    {
        /// <summary>
        /// Mengonversi file JSON menjadi file teks.
        /// </summary>
        /// <param name="jsonFilePath">Path file JSON yang akan dikonversi.</param>
        /// <param name="outputFilePath">Path file teks hasil konversi.</param>
        public static void ConvertJsonToText(string jsonFilePath, string outputFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"[ERROR] File JSON tidak ditemukan: {jsonFilePath}");
                throw new FileNotFoundException($"File JSON tidak ditemukan: {jsonFilePath}");
            }

            try
            {
                // Membaca isi file JSON
                var jsonContent = File.ReadAllText(jsonFilePath);

                // Coba parse JSON sebagai array
                if (JsonSerializer.Deserialize<object>(jsonContent) is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Array)
                {
                    var jsonArray = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonContent);
                    if (jsonArray == null)
                    {
                        throw new InvalidOperationException("File JSON kosong atau tidak valid.");
                    }

                    // Konversi setiap elemen array ke teks
                    var textContent = string.Empty;
                    foreach (var item in jsonArray)
                    {
                        textContent += ConvertDictionaryToText(item) + "\n";
                    }

                    // Menulis ke file teks
                    File.WriteAllText(outputFilePath, textContent);
                }
                else
                {
                    // Jika bukan array, parse sebagai Dictionary
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonContent);
                    if (jsonData == null)
                    {
                        throw new InvalidOperationException("File JSON kosong atau tidak valid.");
                    }

                    // Konversi ke teks
                    var textContent = ConvertDictionaryToText(jsonData);

                    // Menulis ke file teks
                    File.WriteAllText(outputFilePath, textContent);
                }

                Console.WriteLine($"[INFO] File JSON berhasil dikonversi ke file teks: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Gagal mengonversi file JSON ke teks: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Mengonversi Dictionary menjadi format teks.
        /// </summary>
        /// <param name="data">Dictionary yang akan dikonversi.</param>
        /// <returns>String dalam format teks.</returns>
        private static string ConvertDictionaryToText(Dictionary<string, object> data, int indentLevel = 0)
        {
            var text = string.Empty;
            var indent = new string(' ', indentLevel * 4); // Indentasi 4 spasi

            foreach (var kvp in data)
            {
                if (kvp.Value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
                {
                    // Jika value adalah objek JSON, rekursif
                    text += $"{indent}{kvp.Key}:\n";
                    var nestedData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonElement.GetRawText());
                    text += ConvertDictionaryToText(nestedData!, indentLevel + 1);
                }
                else
                {
                    // Jika value adalah tipe primitif
                    text += $"{indent}{kvp.Key}: {kvp.Value}\n";
                }
            }

            return text;
        }
    }
}