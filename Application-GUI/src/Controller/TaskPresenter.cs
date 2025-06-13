using Application.Models;
using Application.Services;
using Application.Helpers;
using Application.View;
using System.Text.Json;
using Application.Configuration;

namespace Application.Controller
{
    public class TaskPresenter
    {
        private readonly ITaskView _view;
        private readonly TaskService _taskService;
        private readonly InputValidator _validator;
        private readonly IConfigProvider _configProvider;

        public TaskPresenter(ITaskView view, TaskService taskService, InputValidator validator)
        {
            _view = view;
            _taskService = taskService;
            _validator = validator;

            // Subscribe ke event dari View
            _view.FormLoaded += OnFormLoaded;
            _view.AddTaskRequested += OnAddTaskRequested;
            //_view.ViewTasksRequested += OnViewTasksRequested;
            //_view.ViewTaskDetailsRequested += OnViewTaskDetailsRequested;
            _view.UpdateTaskStatusRequested += OnUpdateTaskStatusRequested;
            _view.DeleteTaskRequested += OnDeleteTaskRequested;
            _view.FilterTasksByDateRequested += OnFilterTasksByDateRequested;
            _view.ExportTasksRequested += OnExportTasksRequested;
        }

        private void OnFormLoaded()
        {
            OnViewTasksRequested();
        }

        public async void OnAddTaskRequested()
        {
            try
            {
                string title = _view.GetTaskTitleInput();
                DateTime dueDate = _view.GetTaskDueDateInput();
                int kategoriIndex = _view.GetKategoriIndexInput();

                if (!_validator.IsValidTitle(title) || !_validator.IsValidDeadline(dueDate))
                {
                    _view.DisplayMessage("Input tidak valid. Pastikan semua field terisi dengan benar.", "Error Validasi", MessageBoxIcon.Error);
                    return;
                }

                var result = await _taskService.CreateTaskAsync(title, dueDate, kategoriIndex);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage("Tugas berhasil ditambahkan.", "Sukses", MessageBoxIcon.Information);
                    OnViewTasksRequested();
                    _view.ClearInputs();
                }
                else
                {
                    _view.DisplayMessage($"Gagal menambahkan tugas: {result.Value}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Secure Coding: Log exception (tidak diimplementasikan di sini) dan tampilkan pesan umum
                _view.DisplayMessage($"Terjadi kesalahan: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        // get all task
        public async void OnViewTasksRequested()
        {
            try
            {
                var result = await _taskService.GetAllTasksAsync();
                //_view.DisplayMessage($"Gagal mengambil daftar tugas: {result}", "Error", MessageBoxIcon.Error);
                if (result.IsSuccess)
                {
                    _view.DisplayTasks(result.Value);
                }
                else
                {
                    _view.DisplayMessage($"Gagal mengambil daftar tugas: {result.Value}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _view.DisplayMessage($"Terjadi kesalahan saat mengambil tugas: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        // view task details
        public async void OnViewTaskDetailsRequested(int taskId)
        {
            try
            {
                // check valid task id
                if (taskId == -1)
                {
                    _view.DisplayMessage("Pilih tugas terlebih dahulu atau masukkan ID yang valid.", "Info", MessageBoxIcon.Information);
                    return;
                }

                //if (!_validator.IsValidId(taskId.ToString()))
                //{
                //    _view.DisplayMessage("ID tugas tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                //    return;
                //}
                var result = await _taskService.GetTaskByIdAsync(taskId);

                if (result.IsSuccess)
                {
                    // Secure Coding: Pastikan konfigurasi pengingat deadline ada
                    var reminderSettings = _configProvider.GetConfig<Dictionary<string, object>>("ReminderSettings");
                    if (reminderSettings == null)
                        _view.DisplayMessage("Pengaturan pengingat deadline tidak ditemukan!", "Error", MessageBoxIcon.Error);


                    // Secure Coding: Pastikan pengaturan 'DaysBeforeDeadline' ada dan valid
                    int daysBeforeDeadline = ((JsonElement)reminderSettings["DaysBeforeDeadline"]).GetInt32();

                    string statusWarning = "";
                    if (DateHelper.DaysUntilDeadline(result.Value.Deadline) <= daysBeforeDeadline && result.Value.Status != StatusTugas.Selesai)
                    {
                        statusWarning = $"\nPeringatan: Deadline {DateHelper.DaysUntilDeadline(result.Value.Deadline)} hari lagi!";
                    }

                    var task = $"Detail Tugas #{result.Value.Id}:\n" +
                                $"Judul: {result.Value.Judul}\n" +
                               $"Kategori: {result.Value.Kategori}\n" +
                               $"Status: {result.Value.Status}\n" +
                               $"Deadline: {DateHelper.FormatDate(result.Value.Deadline)}" +
                                statusWarning;

                    _view.DisplayTaskDetails(task);
                }
                else
                {
                    _view.DisplayMessage($"Gagal mengambil detail tugas: {result.Value}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _view.DisplayMessage($"Terjadi kesalahan: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnUpdateTaskStatusRequested()
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

                var result =  await _taskService.UpdateTaskStatusAsync(taskId, newStatus);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage("Status tugas berhasil diperbarui.", "Sukses", MessageBoxIcon.Information);
                    OnViewTasksRequested();
                }
                else
                {
                    _view.DisplayMessage($"Gagal memperbarui status: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (OperationCanceledException)
            {
                _view.DisplayMessage("Update status dibatalkan.", "Info", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _view.DisplayMessage($"Terjadi kesalahan: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnDeleteTaskRequested()
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

                // Konfirmasi sebelum menghapus
                // Dialog konfirmasi bisa jadi bagian dari ITaskView atau dibuat di presenter jika lebih kompleks
                // Untuk sekarang, asumsikan View yang menangani konfirmasi atau langsung hapus
                // Contoh sederhana jika Presenter menangani konfirmasi:
                // if (!_view.ConfirmAction("Apakah Anda yakin ingin menghapus tugas ini?")) return;


                var result = await _taskService.DeleteTaskAsync(taskId);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage("Tugas berhasil dihapus.", "Sukses", MessageBoxIcon.Information);
                    OnViewTasksRequested();
                }
                else
                {
                    _view.DisplayMessage($"Gagal menghapus tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _view.DisplayMessage($"Terjadi kesalahan: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnFilterTasksByDateRequested()
        {
            try
            {
                DateTime startDate = _view.GetFilterStartDateInput();
                DateTime endDate = _view.GetFilterEndDateInput();

                if (startDate > endDate)
                {
                    _view.DisplayMessage("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }
                // Validasi tanggal bisa ditambahkan di _validator
                if (!_validator.IsValidDateRange(startDate, endDate))
                {
                    _view.DisplayMessage("Rentang tanggal tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }


                var result = await _taskService.GetTasksByDateRangeAsync(startDate, endDate);
                if (result.IsSuccess)
                {
                    _view.DisplayTasks(result.Value);
                    _view.DisplayMessage($"Menampilkan tugas dari {startDate:dd/MM/yyyy} hingga {endDate:dd/MM/yyyy}.", "Filter Diterapkan", MessageBoxIcon.Information);
                }
                else
                {
                    _view.DisplayMessage($"Gagal memfilter tugas: {result.Error}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (OperationCanceledException)
            {
                _view.DisplayMessage("Filter berdasarkan tanggal dibatalkan.", "Info", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _view.DisplayMessage($"Terjadi kesalahan: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }

        public async void OnExportTasksRequested()
        {
            try
            {
                string format = _view.GetExportFormatInput()?.ToLower();
                if (string.IsNullOrEmpty(format) || (format != "json" && format != "txt"))
                {
                    _view.DisplayMessage("Format ekspor tidak valid. Pilih 'json' atau 'txt'.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }

                string defaultFileName = $"TugasExport_{DateTime.Now:yyyyMMddHHmmss}";
                string fileFilter = format == "json" ? "JSON files (*.json)|*.json" : "Text files (*.txt)|*.txt";
                defaultFileName += format == "json" ? ".json" : ".txt";

                string filePath = _view.GetExportFilePathInput(defaultFileName, fileFilter);

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    _view.DisplayMessage("Jalur file ekspor tidak valid.", "Error Validasi", MessageBoxIcon.Warning);
                    return;
                }


                var result = await _taskService.ExportTasksAsync(format, filePath);
                if (result.IsSuccess)
                {
                    _view.DisplayMessage($"Tugas berhasil diekspor ke {filePath}", "Sukses", MessageBoxIcon.Information);
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
                _view.DisplayMessage($"Terjadi kesalahan saat mengekspor: {ex.Message}", "Error Sistem", MessageBoxIcon.Error);
            }
        }
    }
}