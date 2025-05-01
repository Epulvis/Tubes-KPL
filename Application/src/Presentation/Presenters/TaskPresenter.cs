using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tubes_KPL.src.Application.Helpers;
using Tubes_KPL.src.Application.Libraries;
using Tubes_KPL.src.Application.Services;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Infrastructure.Configuration;

namespace Tubes_KPL.src.Presentation.Presenters
{
    public class TaskPresenter
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:4000/api/tugas";
        
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IConfigProvider _configProvider;
        private readonly TaskService _taskService; // tambahkan ini

        public TaskPresenter(IConfigProvider configProvider, TaskService taskService)
        {
            _httpClient = new HttpClient();
            _configProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService)); // tambahkan ini
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }


        public async Task<string> CreateTask(string judul, string deadlineStr, int kategoriIndex)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(judul))
                {
                    var defaultTaskConfig = _configProvider.GetConfig<Dictionary<string, string>>("DefaultTask");
                    judul = defaultTaskConfig.ContainsKey("Judul") && !string.IsNullOrWhiteSpace(defaultTaskConfig["Judul"])
                        ? defaultTaskConfig["Judul"]
                        : "Tugas Default";
                }

                if (!DateHelper.TryParseDate(deadlineStr, out DateTime deadline))
                {
                    var defaultTaskConfig = _configProvider.GetConfig<Dictionary<string, string>>("DefaultTask");
                    int defaultDays = int.Parse(defaultTaskConfig["DeadlineDaysFromNow"]);
                    deadline = DateTime.Now.AddDays(defaultDays);
                }

                KategoriTugas kategori = kategoriIndex == 0 ? KategoriTugas.Akademik : KategoriTugas.NonAkademik;

                var newTugas = new Tugas
                {
                    Judul = judul,
                    Deadline = deadline,
                    Kategori = kategori,
                    Status = StatusTugas.BelumMulai
                };
                
                var response = await _httpClient.PostAsJsonAsync(BaseUrl, newTugas, _jsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    var createdTask = await response.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                    return $"Tugas berhasil dibuat dengan ID: {createdTask.Id}";
                }
                return $"Error: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> UpdateTaskStatus(string idStr, int statusIndex)
        {
            try
            {
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                if (statusIndex < 0 || statusIndex > 3)
                    return "Indeks status tidak valid! Gunakan 0-3.";

                StatusTugas newStatus = (StatusTugas)statusIndex;

                var getResponse = await _httpClient.GetAsync($"{BaseUrl}/{id}");
                if (!getResponse.IsSuccessStatusCode)
                    return "Tugas tidak ditemukan!";

                var task = await getResponse.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                task.Status = newStatus;

                var updateResponse = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", task, _jsonOptions);
                if (updateResponse.IsSuccessStatusCode)
                {
                    var updatedTask = await updateResponse.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                    return $"Status tugas '{updatedTask.Judul}' berhasil diubah menjadi {updatedTask.Status}";
                }
                return $"Error: {updateResponse.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> UpdateTask(string idStr, string judul, string deadlineStr, int kategoriIndex)
        {
            try
            {
                if (!InputValidator.IsValidJudul(judul))
                    return "Judul tugas tidak valid! Pastikan tidak kosong dan maksimal 100 karakter.";

                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                if (!DateHelper.TryParseDate(deadlineStr, out DateTime deadline))
                    return "Format tanggal tidak valid! Gunakan format DD/MM/YYYY.";

                if (!InputValidator.IsValidDeadline(deadline))
                    return "Deadline tidak dapat diatur di masa lalu.";

                KategoriTugas kategori = kategoriIndex == 0 ? KategoriTugas.Akademik : KategoriTugas.NonAkademik;

                var getResponse = await _httpClient.GetAsync($"{BaseUrl}/{id}");
                if (!getResponse.IsSuccessStatusCode)
                    return "Tugas tidak ditemukan!";

                var task = await getResponse.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                task.Judul = judul;
                task.Deadline = deadline;
                task.Kategori = kategori;
                
                var updateResponse = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", task, _jsonOptions);
                if (updateResponse.IsSuccessStatusCode)
                {
                    var updatedTask = await updateResponse.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                    return $"Tugas '{updatedTask.Judul}' berhasil diperbarui";
                }
                return $"Error: {updateResponse.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> DeleteTask(string idStr)
        {
            try
            {
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                var getResponse = await _httpClient.GetAsync($"{BaseUrl}/{id}");
                if (!getResponse.IsSuccessStatusCode)
                    return "Tugas tidak ditemukan!";

                var task = await getResponse.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                string judulTugas = task.Judul;

                var deleteResponse = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
                if (deleteResponse.IsSuccessStatusCode)
                {
                    return $"Tugas '{judulTugas}' berhasil dihapus";
                }
                return $"Error: {deleteResponse.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> GetTaskDetails(string idStr)
        {
            try
            {
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                    return "Tugas tidak ditemukan!";

                var tugas = await response.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);

                var reminderSettings = _configProvider.GetConfig<Dictionary<string, object>>("ReminderSettings");
                if (reminderSettings == null)
                    return "Pengaturan pengingat deadline tidak ditemukan!";

                int daysBeforeDeadline = ((JsonElement)reminderSettings["DaysBeforeDeadline"]).GetInt32();

                string statusWarning = "";
                if (DateHelper.DaysUntilDeadline(tugas.Deadline) <= daysBeforeDeadline && tugas.Status != StatusTugas.Selesai)
                {
                    statusWarning = $"\nPeringatan: Deadline {DateHelper.DaysUntilDeadline(tugas.Deadline)} hari lagi!";
                }

                return $"Detail Tugas #{tugas.Id}:\n" +
                       $"Judul: {tugas.Judul}\n" +
                       $"Kategori: {tugas.Kategori}\n" +
                       $"Status: {tugas.Status}\n" +
                       $"Deadline: {DateHelper.FormatDate(tugas.Deadline)}" +
                       statusWarning;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> GetAllTasks()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                if (!response.IsSuccessStatusCode)
                    return $"Error: {response.StatusCode}";

                var tasks = await response.Content.ReadFromJsonAsync<List<Tugas>>(_jsonOptions);
                if (!tasks.Any())
                    return "Tidak ada tugas yang tersedia.";

                string result = "Daftar Tugas:\n";
                result += "=================================================================================================\n";
                result += "| ID |            Judul            |   Kategori   |      Status      |       Deadline        |\n";
                result += "=================================================================================================\n";

                foreach (var t in tasks)
                {
                    string deadline = DateHelper.FormatDate(t.Deadline);
                    string status = t.Status.ToString();
                    
                    if (DateHelper.IsDeadlineApproaching(t.Deadline) && t.Status != StatusTugas.Selesai && t.Status != StatusTugas.Terlewat)
                    {
                        deadline += " ⚠️";
                    }

                    result += $"| {t.Id,-3}| {t.Judul,-28} | {t.Kategori,-12} | {status,-16} | {deadline,-21} |\n";
                }
                
                result += "=================================================================================================\n";
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        
        public async Task<string> PrintTasksToFilesFromApi(string jsonFilePath, string textFilePath)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Mengakses API di: {BaseUrl}");

                var response = await _httpClient.GetAsync(BaseUrl);
                if (!response.IsSuccessStatusCode)
                    return $"[ERROR] Gagal mengambil data dari API. Status code: {response.StatusCode}";

                var tasks = await response.Content.ReadFromJsonAsync<List<Tugas>>(_jsonOptions);
                if (tasks == null || !tasks.Any())
                    return "[INFO] Tidak ada tugas yang tersedia untuk dicetak.";

                var jsonContent = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFilePath, jsonContent);

                var textContent = JsonToTextConverter.ConvertTasksToText(tasks);
                File.WriteAllText(textFilePath, textContent);

                return $"[INFO] Daftar tugas berhasil dicetak ke file JSON: {jsonFilePath} dan file TXT: {textFilePath}.";
            }
            catch (Exception ex)
            {
                return $"[ERROR] Gagal mencetak daftar tugas: {ex.Message}";
            }
        }
    }
} 

