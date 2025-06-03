using Application.Models;
using Application.Helpers;
using Application.Libraries;
using Application.View;
using Application.Services;

namespace Application.Controller
{
    public class TaskPresenter
    {
        private readonly ITaskView _view;
        private readonly TaskService _taskService;
        private readonly InputValidator _validator;

        public TaskPresenter(ITaskView view, TaskService taskService, InputValidator validator)
        {
            _view = view;
            _taskService = taskService;
            _validator = validator;

            // Subscribe ke event dari View
            _view.FormLoaded += OnFormLoaded;
            _view.AddTaskRequested += OnAddTaskRequested;
            _view.ViewTasksRequested += OnViewTasksRequested;
            _view.ViewTaskDetailsRequested += OnViewTaskDetailsRequested;
            _view.UpdateTaskStatusRequested += OnUpdateTaskStatusRequested;
            _view.DeleteTaskRequested += OnDeleteTaskRequested;
            _view.FilterTasksByDateRequested += OnFilterTasksByDateRequested;
            _view.ExportTasksRequested += OnExportTasksRequested;
        }

        private async void OnFormLoaded(object sender, EventArgs e) // Menjadi async void
        {
            await LoadTasksAsync(); // Memuat tugas saat form pertama kali ditampilkan
        }

        public async void OnAddTaskRequested(object sender, EventArgs e) // Menjadi async void
        {
            try
            {
                string title = _view.GetTaskTitleInput();
                DateTime dueDate = _view.GetTaskDueDateInput();
                // Asumsi ITaskView sekarang memiliki metode untuk mendapatkan KategoriIndex
                // Jika belum ada, Anda perlu menambahkannya ke ITaskView dan implementasinya di TaskManagementForm
                int kategoriIndex = _view.GetTaskKategoriIndexInput(); // Anda perlu implementasi ini di View

                // Validasi input
                if (!_validator.IsValidTitle(title)) // Menggunakan _validator yang di-inject
                {
                    _view.DisplayMessage("Judul tidak valid. Tidak boleh kosong dan maksimal 100 karakter.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }
                if (!_validator.IsValidDeadline(dueDate))
                {
                    _view.DisplayMessage("Tanggal jatuh tempo tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }
                // Anda bisa menambahkan validasi untuk kategoriIndex di sini jika perlu
                // if (kategoriIndex <= 0) { /* tampilkan error */ return; }

                // Memanggil metode async dari TaskService dengan parameter baru
                Result<Tugas> result = await _taskService.CreateTaskAsync(title, dueDate, kategoriIndex);

                if (result.IsSuccess)
                {
                    _view.DisplayMessage("Tugas berhasil ditambahkan.", "Sukses", MessageBoxIcon.Information);
                    _view.ClearInputs();
                    await LoadTasksAsync(); // Refresh daftar tugas
                }
                else
                {
                    // Menggunakan result.Error
                    _view.DisplayMessage($"Gagal menambahkan tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Secure Coding: Log exception dan tampilkan pesan umum
                Console.WriteLine($"[TaskPresenter:OnAddTaskRequested] Exception: {ex.Message}\n{ex.StackTrace}"); // Placeholder logging
                _view.DisplayMessage($"Terjadi kesalahan sistem saat menambahkan tugas: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        // Event handler untuk ViewTasksRequested (jika masih ada tombol refresh manual)
        // atau dipanggil secara internal.
        private async void OnViewTasksRequested(object? sender, EventArgs e) // Menjadi async void
        {
            await LoadTasksAsync();
        }

        // Metode helper untuk memuat tugas, bisa dipanggil dari beberapa tempat
        private async Task LoadTasksAsync()
        {
            try
            {
                Result<List<Tugas>> result = await _taskService.GetAllTasksAsync();
                if (result.IsSuccess && result.Value != null)
                {
                    _view.DisplayTasks(result.Value);
                }
                else
                {
                    // Menggunakan result.Error
                    _view.DisplayMessage($"Gagal mengambil daftar tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                    _view.DisplayTasks(new List<Tugas>()); // Tampilkan list kosong
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskPresenter:LoadTasksAsync] Exception: {ex.Message}\n{ex.StackTrace}");
                _view.DisplayMessage($"Terjadi kesalahan sistem saat mengambil tugas: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
                _view.DisplayTasks(new List<Tugas>()); // Tampilkan list kosong pada error
            }
        }


        public async void OnViewTaskDetailsRequested(object? sender, EventArgs e) // Menjadi async void
        {
            try
            {
                int taskId = _view.GetSelectedTaskId();
                if (taskId == -1)
                {
                    _view.DisplayMessage("Pilih tugas terlebih dahulu.", "Info", MessageBoxIcon.Information);
                    return;
                }

                // Asumsi _validator adalah instance, jika static gunakan InputValidator.IsValidId
                if (!_validator.IsValidId(taskId.ToString()))
                {
                    _view.DisplayMessage("ID tugas tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }

                Result<Tugas> result = await _taskService.GetTaskByIdAsync(taskId);
                if (result.IsSuccess && result.Value != null)
                {
                    _view.DisplayTaskDetails(result.Value);
                }
                else
                {
                    // Menggunakan result.Error
                    _view.DisplayMessage($"Gagal mengambil detail tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskPresenter:OnViewTaskDetailsRequested] Exception: {ex.Message}\n{ex.StackTrace}");
                _view.DisplayMessage($"Terjadi kesalahan sistem: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnUpdateTaskStatusRequested(object sender, EventArgs e) // Menjadi async void
        {
            try
            {
                int taskId = _view.GetSelectedTaskId();
                if (taskId == -1)
                {
                    _view.DisplayMessage("Pilih tugas yang akan diupdate.", "Info", MessageBoxIcon.Information);
                    return;
                }
                if (!_validator.IsValidId(taskId.ToString()))
                {
                    _view.DisplayMessage("ID tugas tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }

                StatusTugas newStatus = _view.GetNewTaskStatusInput();

                Result result = await _taskService.UpdateTaskStatusAsync(taskId, newStatus);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage("Status tugas berhasil diperbarui.", "Sukses", MessageBoxIcon.Information);
                    await LoadTasksAsync(); // Refresh list
                }
                else
                {
                    // Menggunakan result.Error
                    _view.DisplayMessage($"Gagal memperbarui status: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (OperationCanceledException)
            {
                _view.DisplayMessage("Update status dibatalkan.", "Info", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskPresenter:OnUpdateTaskStatusRequested] Exception: {ex.Message}\n{ex.StackTrace}");
                _view.DisplayMessage($"Terjadi kesalahan sistem: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnDeleteTaskRequested(object? sender, EventArgs e) // Menjadi async void
        {
            try
            {
                int taskId = _view.GetSelectedTaskId();
                if (taskId == -1)
                {
                    _view.DisplayMessage("Pilih tugas yang akan dihapus.", "Info", MessageBoxIcon.Information);
                    return;
                }
                if (!_validator.IsValidId(taskId.ToString()))
                {
                    _view.DisplayMessage("ID tugas tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }

                // Pertimbangkan untuk menambahkan dialog konfirmasi di View sebelum memanggil ini
                // if (!_view.ConfirmAction("Anda yakin ingin menghapus tugas ini?")) return;

                Result result = await _taskService.DeleteTaskAsync(taskId);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage("Tugas berhasil dihapus.", "Sukses", MessageBoxIcon.Information);
                    await LoadTasksAsync(); // Refresh list
                }
                else
                {
                    // Menggunakan result.Error
                    _view.DisplayMessage($"Gagal menghapus tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskPresenter:OnDeleteTaskRequested] Exception: {ex.Message}\n{ex.StackTrace}");
                _view.DisplayMessage($"Terjadi kesalahan sistem: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnFilterTasksByDateRequested(object? sender, EventArgs e) // Menjadi async void
        {
            try
            {
                DateTime startDate = _view.GetFilterStartDateInput();
                DateTime endDate = _view.GetFilterEndDateInput();

                // Validasi di presenter bisa menggunakan _validator instance
                if (!_validator.IsValidDateRange(startDate, endDate))
                {
                    _view.DisplayMessage("Rentang tanggal tidak valid (tanggal mulai tidak boleh setelah tanggal akhir).", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }

                Result<List<Tugas>> result = await _taskService.GetTasksByDateRangeAsync(startDate, endDate);
                if (result.IsSuccess && result.Value != null)
                {
                    _view.DisplayTasks(result.Value);
                    _view.DisplayMessage($"Menampilkan tugas dari {startDate:dd/MM/yyyy} hingga {endDate:dd/MM/yyyy}.", "Filter Diterapkan", MessageBoxIcon.Information);
                }
                else
                {
                    // Menggunakan result.Error
                    _view.DisplayMessage($"Gagal memfilter tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                    _view.DisplayTasks(new List<Tugas>()); // Tampilkan list kosong
                }
            }
            catch (OperationCanceledException)
            {
                _view.DisplayMessage("Filter berdasarkan tanggal dibatalkan.", "Info", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskPresenter:OnFilterTasksByDateRequested] Exception: {ex.Message}\n{ex.StackTrace}");
                _view.DisplayMessage($"Terjadi kesalahan sistem: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnExportTasksRequested(object sender, EventArgs e)
        {
            try
            {
                string format = _view.GetExportFormatInput()?.ToLower() ?? string.Empty;
                if (string.IsNullOrEmpty(format) || (format != "json" && format != "txt"))
                {
                    _view.DisplayMessage("Format ekspor tidak valid. Pilih 'json' atau 'txt'.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }

                string filePath = _view.GetExportFilePathInput(
                    $"TugasExport_{DateTime.Now:yyyyMMddHHmmss}" + (format == "json" ? ".json" : ".txt"),
                    format == "json" ? "JSON files (*.json)|*.json|All files (*.*)|*.*" : "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                );

                Result result = await _taskService.ExportTasksAsync(format, filePath);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage($"Permintaan ekspor tugas ke {filePath} berhasil dikirim ke server.", "Sukses", MessageBoxIcon.Information);
                }
                else
                {
                    _view.DisplayMessage($"Gagal mengekspor tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (OperationCanceledException)
            {
                _view.DisplayMessage("Ekspor tugas dibatalkan.", "Info", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskPresenter:OnExportTasksRequested] Exception: {ex.Message}\n{ex.StackTrace}");
                _view.DisplayMessage($"Terjadi kesalahan sistem saat mengekspor: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }
    }
}
