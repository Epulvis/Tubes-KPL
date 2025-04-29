using System;
using System.Collections.Generic;
using System.Linq;
using Tubes_KPL.src.Application.Helpers;
using Tubes_KPL.src.Application.Services;
using Tubes_KPL.src.Domain.Models;

namespace Tubes_KPL.src.Presentation.Presenters
{
    public class TaskPresenter
    {
        private readonly TaskService _taskService;
        public TaskPresenter(TaskService taskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        public string CreateTask(string judul, string deadlineStr, int kategoriIndex)
        {
            try
            {
                // Validate input
                if (!InputValidator.IsValidJudul(judul))
                    return "Judul tugas tidak valid! Pastikan tidak kosong dan maksimal 100 karakter.";

                if (!DateHelper.TryParseDate(deadlineStr, out DateTime deadline))
                    return "Format tanggal tidak valid! Gunakan format DD/MM/YYYY.";

                if (!InputValidator.IsValidDeadline(deadline))
                    return "Deadline tidak dapat diatur di masa lalu.";

                // Convert kategori index to enum
                KategoriTugas kategori = kategoriIndex == 0 ? KategoriTugas.Akademik : KategoriTugas.NonAkademik;

                // Create task
                var tugas = _taskService.BuatTugas(judul, deadline, kategori);
                return $"Tugas berhasil dibuat dengan ID: {tugas.Id}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string UpdateTaskStatus(string idStr, int statusIndex)
        {
            try
            {
                // Validate ID
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                // Convert status index to enum
                if (statusIndex < 0 || statusIndex > 3)
                    return "Indeks status tidak valid! Gunakan 0-3.";

                StatusTugas newStatus = (StatusTugas)statusIndex;
                
                // Update status with automata validation
                var tugas = _taskService.UbahStatusTugas(id, newStatus);
                return $"Status tugas '{tugas.Judul}' berhasil diubah menjadi {tugas.Status}";
            }
            catch (KeyNotFoundException)
            {
                return "Tugas tidak ditemukan!";
            }
            catch (InvalidOperationException ex)
            {
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string UpdateTask(string idStr, string judul, string deadlineStr, int kategoriIndex)
        {
            try
            {
                // Validate input
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                if (!InputValidator.IsValidJudul(judul))
                    return "Judul tugas tidak valid! Pastikan tidak kosong dan maksimal 100 karakter.";

                if (!DateHelper.TryParseDate(deadlineStr, out DateTime deadline))
                    return "Format tanggal tidak valid! Gunakan format DD/MM/YYYY.";

                // Convert kategori index to enum
                KategoriTugas kategori = kategoriIndex == 0 ? KategoriTugas.Akademik : KategoriTugas.NonAkademik;

                // Update task
                var tugas = _taskService.PerbaruiTugas(id, judul, deadline, kategori);
                return $"Tugas '{tugas.Judul}' berhasil diperbarui";
            }
            catch (KeyNotFoundException)
            {
                return "Tugas tidak ditemukan!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string DeleteTask(string idStr)
        {
            try
            {
                // Validate ID
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                // Get task before deletion to show its title in confirmation
                var tugas = _taskService.AmbilTugasById(id);
                string judulTugas = tugas.Judul;

                // Delete task
                _taskService.HapusTugas(id);
                return $"Tugas '{judulTugas}' berhasil dihapus";
            }
            catch (KeyNotFoundException)
            {
                return "Tugas tidak ditemukan!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string GetTaskDetails(string idStr)
        {
            try
            {
                // Validate ID
                if (!InputValidator.TryParseId(idStr, out int id))
                    return "ID tugas tidak valid! Pastikan berupa angka positif.";

                // Get task
                var tugas = _taskService.AmbilTugasById(id);
                
                // Format task details
                string statusWarning = "";
                if (DateHelper.IsDeadlineApproaching(tugas.Deadline) && tugas.Status != StatusTugas.Selesai)
                {
                    int days = DateHelper.DaysUntilDeadline(tugas.Deadline);
                    statusWarning = $"\nPeringatan: Deadline {days} hari lagi!";
                }
                else if (DateHelper.IsDeadlinePassed(tugas.Deadline) && tugas.Status != StatusTugas.Terlewat && tugas.Status != StatusTugas.Selesai)
                {
                    statusWarning = "\nPeringatan: Deadline sudah terlewat!";
                }

                return $"Detail Tugas #{tugas.Id}:\n" +
                       $"Judul: {tugas.Judul}\n" +
                       $"Kategori: {tugas.Kategori}\n" +
                       $"Status: {tugas.Status}\n" +
                       $"Deadline: {DateHelper.FormatDate(tugas.Deadline)}" + 
                       statusWarning;
            }
            catch (KeyNotFoundException)
            {
                return "Tugas tidak ditemukan!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string GetAllTasks()
        {
            try
            {
                var tasks = _taskService.AmbilSemuaTugas();
                
                // Check if there are no tasks
                if (!tasks.Any())
                    return "Tidak ada tugas yang tersedia.";

                // Format tasks as table
                string result = "Daftar Tugas:\n";
                result += "=================================================================================================\n";
                result += "| ID |            Judul            |   Kategori   |      Status      |       Deadline        |\n";
                result += "=================================================================================================\n";

                foreach (var t in tasks)
                {
                    string deadline = DateHelper.FormatDate(t.Deadline);
                    string status = t.Status.ToString();
                    
                    // Add warning symbol for approaching deadlines
                    if (DateHelper.IsDeadlineApproaching(t.Deadline) && t.Status != StatusTugas.Selesai && t.Status != StatusTugas.Terlewat)
                    {
                        deadline += " ⚠️";
                    }

                    result += $"| {t.Id,-3}| {t.Judul,-28} | {t.Kategori,-12} | {status,-16} | {deadline,-21} |\n";
                }
                
                result += "=================================================================================================\n";
                
                // Update any tasks that should be marked as Terlewat
                _taskService.PerbaruiStatusTerlewat();
                
                return result;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
} 