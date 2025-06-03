using System.Net.Http.Json;
using System.Text.Json;
using Application.Models;
using Application.Helpers;
using Application.Libraries;

namespace Application.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;
        private readonly InputValidator _validator;
        private readonly StatusStateMachine _statusStateMachine;
        private readonly string _apiBaseUrl;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            // Tambahkan konverter jika enum di API direpresentasikan sebagai string
            // Converters = { new JsonStringEnumConverter() } // Uncomment jika API menggunakan string untuk enum
        };

        // Constructor diubah untuk menerima HttpClient
        public TaskService(HttpClient httpClient, InputValidator validator, StatusStateMachine statusStateMachine, string apiBaseUrl = "http://localhost:4000/api/tugas")
        {
            _httpClient = httpClient;
            _validator = validator;
            _statusStateMachine = statusStateMachine;
            _apiBaseUrl = apiBaseUrl.TrimEnd('/');
        }

        public async Task<Result<List<Tugas>>> GetAllTasksAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/tasks");
                if (response.IsSuccessStatusCode)
                {
                    var tasks = await response.Content.ReadFromJsonAsync<List<Tugas>>(_jsonOptions);
                    return tasks != null ? Result<List<Tugas>>.Success(tasks) : Result<List<Tugas>>.Failure("Data tugas kosong atau format tidak sesuai.");
                }

                string errorContent = await response.Content.ReadAsStringAsync();
                return Result<List<Tugas>>.Failure($"Gagal mengambil tugas dari API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                // Secure Coding: Log exception, tampilkan pesan umum
                Console.WriteLine($"Network error in GetAllTasksAsync: {ex.Message}"); // Logging sisi klien
                return Result<List<Tugas>>.Failure($"Kesalahan jaringan saat menghubungi API: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error in GetAllTasksAsync: {ex.Message}");
                return Result<List<Tugas>>.Failure($"Format data dari API tidak valid: {ex.Message}");
            }
            catch (Exception ex) // General exception
            {
                Console.WriteLine($"Unexpected error in GetAllTasksAsync: {ex.Message}");
                return Result<List<Tugas>>.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }

        public async Task<Result<Tugas>> GetTaskByIdAsync(int id)
        {
            if (id <= 0) return Result<Tugas>.Failure("ID Tugas tidak valid.");

            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/tasks/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var task = await response.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                    return task != null ? Result<Tugas>.Success(task) : Result<Tugas>.Failure("Tugas tidak ditemukan atau format tidak sesuai.");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Result<Tugas>.Failure("Tugas tidak ditemukan di API.");
                }
                string errorContent = await response.Content.ReadAsStringAsync();
                return Result<Tugas>.Failure($"Gagal mengambil detail tugas dari API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error in GetTaskByIdAsync: {ex.Message}");
                return Result<Tugas>.Failure($"Kesalahan jaringan: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error in GetTaskByIdAsync: {ex.Message}");
                return Result<Tugas>.Failure($"Format data dari API tidak valid: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetTaskByIdAsync: {ex.Message}");
                return Result<Tugas>.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }

        public async Task<Result<Tugas>> CreateTaskAsync(string judul, DateTime deadline, int kategoriIndex)
        {
            // Validasi input sisi klien sebelum mengirim ke API
            if (!_validator.IsValidTitle(judul)) return Result<Tugas>.Failure("Judul tidak valid.");
            if (!_validator.IsValidDeadline(deadline)) return Result<Tugas>.Failure("Tanggal jatuh tempo tidak valid.");

            KategoriTugas kategori = kategoriIndex == 0 ? KategoriTugas.Akademik : KategoriTugas.NonAkademik;

            var newTask = new Tugas
            {
                Judul = judul,
                Deadline = deadline,
                Kategori = kategori,
                Status = StatusTugas.BelumMulai
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/tasks", newTask, _jsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    var createdTask = await response.Content.ReadFromJsonAsync<Tugas>(_jsonOptions);
                    return createdTask != null ? Result<Tugas>.Success(createdTask) : Result<Tugas>.Failure("Gagal membaca respons setelah membuat tugas.");
                }
                string errorContent = await response.Content.ReadAsStringAsync();
                return Result<Tugas>.Failure($"Gagal membuat tugas via API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error in CreateTaskAsync: {ex.Message}");
                return Result<Tugas>.Failure($"Kesalahan jaringan: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON processing error in CreateTaskAsync: {ex.Message}");
                return Result<Tugas>.Failure($"Format data dari API tidak valid atau masalah serialisasi: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in CreateTaskAsync: {ex.Message}");
                return Result<Tugas>.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }

        public async Task<Result> UpdateTaskStatusAsync(int id, StatusTugas newStatus)
        {
            if (id <= 0) return Result.Failure("ID Tugas tidak valid.");

            var existingTaskResult = await GetTaskByIdAsync(id);
            if (!existingTaskResult.IsSuccess)
            {
                return Result.Failure($"Gagal mendapatkan tugas untuk update: {existingTaskResult.Error}");
            }
            Tugas taskToUpdate = existingTaskResult.Value;

            if (!_statusStateMachine.IsValidTransition(taskToUpdate.Status, newStatus))
            {
                return Result.Failure($"Tidak dapat mengubah status dari {taskToUpdate.Status} ke {newStatus}.");
            }

            // 3. Update status dan UpdatedAt
            taskToUpdate.Status = newStatus;
            //taskToUpdate.UpdatedAt = DateTime.Now;

            // 4. Kirim seluruh objek tugas yang telah diperbarui ke API via PUT
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/tasks/{id}", taskToUpdate, _jsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success();
                }
                string errorContent = await response.Content.ReadAsStringAsync();
                return Result.Failure($"Gagal update status tugas via API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error in UpdateTaskStatusAsync: {ex.Message}");
                return Result.Failure($"Kesalahan jaringan: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON processing error in UpdateTaskStatusAsync: {ex.Message}");
                return Result.Failure($"Format data dari API tidak valid atau masalah serialisasi: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in UpdateTaskStatusAsync: {ex.Message}");
                return Result.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }

        public async Task<Result> DeleteTaskAsync(int id)
        {
            if (id <= 0) return Result.Failure("ID Tugas tidak valid.");
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/tasks/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Result.Failure("Tugas tidak ditemukan di API untuk dihapus.");
                }
                string errorContent = await response.Content.ReadAsStringAsync();
                return Result.Failure($"Gagal menghapus tugas via API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error in DeleteTaskAsync: {ex.Message}");
                return Result.Failure($"Kesalahan jaringan: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in DeleteTaskAsync: {ex.Message}");
                return Result.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }

        public async Task<Result<List<Tugas>>> GetTasksByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            if (!_validator.IsValidDateRange(startDate, endDate))
            {
                return Result<List<Tugas>>.Failure("Rentang tanggal tidak valid.");
            }

            string startDateString = startDate.ToString("yyyy-MM-dd");
            string endDateString = endDate.ToString("yyyy-MM-dd");

            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/tasks/filter?startDate={startDateString}&endDate={endDateString}");
                if (response.IsSuccessStatusCode)
                {
                    var tasks = await response.Content.ReadFromJsonAsync<List<Tugas>>(_jsonOptions);
                    return tasks != null ? Result<List<Tugas>>.Success(tasks) : Result<List<Tugas>>.Failure("Data tugas filter kosong atau format tidak sesuai.");
                }
                string errorContent = await response.Content.ReadAsStringAsync();
                return Result<List<Tugas>>.Failure($"Gagal filter tugas dari API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error in GetTasksByDateRangeAsync: {ex.Message}");
                return Result<List<Tugas>>.Failure($"Kesalahan jaringan: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error in GetTasksByDateRangeAsync: {ex.Message}");
                return Result<List<Tugas>>.Failure($"Format data dari API tidak valid: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetTasksByDateRangeAsync: {ex.Message}");
                return Result<List<Tugas>>.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }

        public async Task<Result> ExportTasksAsync(string format, string filePath)
        {
            if (string.IsNullOrWhiteSpace(format) || (format.ToLower() != "json" && format.ToLower() != "txt"))
            {
                return Result.Failure("Format ekspor tidak valid. Pilih 'json' atau 'txt'.");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/tasks/export?format={Uri.EscapeDataString(format)}&filePath={Uri.EscapeDataString(filePath)}");

                if (response.IsSuccessStatusCode)
                {
                    return Result.Success();
                }
                string errorContent = await response.Content.ReadAsStringAsync();
                return Result.Failure($"Gagal meminta ekspor tugas via API: {response.StatusCode} - {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error in ExportTasksAsync: {ex.Message}");
                return Result.Failure($"Kesalahan jaringan: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in ExportTasksAsync: {ex.Message}");
                return Result.Failure($"Terjadi kesalahan tak terduga: {ex.Message}");
            }
        }
    }
}
