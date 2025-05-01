using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tubes_KPL.src.Application.Helpers;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Infrastructure.Configuration;
using Tubes_KPL.src.Presentation.Presenters;

namespace Tubes_KPL.src.Presentation.Views
{
    public class TaskView
    {
        private readonly TaskPresenter _presenter;
        private readonly IConfigProvider _configProvider;

        public TaskView(TaskPresenter presenter, IConfigProvider configProvider)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _configProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
        }

        public async Task ShowMainMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== APLIKASI MANAJEMEN TUGAS MAHASISWA ===");
                Console.WriteLine("1. Lihat Daftar Tugas");
                Console.WriteLine("2. Lihat Detail Tugas");
                Console.WriteLine("3. Tambah Tugas Baru");
                Console.WriteLine("4. Perbarui Tugas");
                Console.WriteLine("5. Ubah Status Tugas");
                Console.WriteLine("6. Hapus Tugas");
                Console.WriteLine("7. Cetak Daftar Tugas ke File JSON dan TXT");
                Console.WriteLine("0. Keluar");
                Console.Write("\nPilihan Anda: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Input tidak valid! Tekan Enter untuk melanjutkan...");
                    Console.ReadLine();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        await ShowAllTasks();
                        break;
                    case 2:
                        await ShowTaskDetails();
                        break;
                    case 3:
                        await AddNewTask();
                        break;
                    case 4:
                        await UpdateTask();
                        break;
                    case 5:
                        await UpdateTaskStatus();
                        break;
                    case 6:
                        await DeleteTask();
                        break;
                    case 7:
                        await PrintTasksToFiles();
                        break;
                    case 0:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid! Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                }
            }
        }
        private async Task ShowAllTasks()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR TUGAS ===\n");
            
            string result = await _presenter.GetAllTasks();
            Console.WriteLine(result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task ShowTaskDetails()
        {
            Console.Clear();
            Console.WriteLine("=== DETAIL TUGAS ===\n");

            Console.Write("Masukkan ID Tugas: ");
            string idStr = Console.ReadLine();

            string result = await _presenter.GetTaskDetails(idStr);

            // Add reminder logic
            //var reminderSettings = _configProvider.GetConfig<Dictionary<string, object>>("ReminderSettings");
            //if (reminderSettings != null && ((JsonElement)reminderSettings["EnableReminders"]).GetBoolean())
            //{
            //    Console.WriteLine("[Pengingat Aktif]");
            //}

            Console.WriteLine("\n" + result);

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task AddNewTask()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH TUGAS BARU ===\n");

            Console.Write("Judul Tugas: ");
            string judul = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(judul))
            {
                var defaultTaskConfig = _configProvider.GetConfig<Dictionary<string, object>>("DefaultTask");
                judul = defaultTaskConfig["Judul"].ToString();
                Console.WriteLine($"Judul default digunakan: {judul}");
            }

            Console.Write("Deadline (DD/MM/YYYY): ");
            string deadlineStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(deadlineStr) || !DateHelper.TryParseDate(deadlineStr, out DateTime deadline))
            {
                Console.WriteLine("Deadline tidak dimasukkan atau format tidak valid. Menggunakan tanggal hari ini sebagai default.");
                deadline = DateTime.Now;
            }

            Console.WriteLine("Kategori Tugas:");
            Console.WriteLine("0. Akademik");
            Console.WriteLine("1. Non-Akademik");
            Console.Write("Pilih Kategori (0/1): ");

            if (!int.TryParse(Console.ReadLine(), out int kategoriIndex) || kategoriIndex < 0 || kategoriIndex > 1)
            {
                Console.WriteLine("Kategori tidak valid! Menggunakan default: Akademik");
                kategoriIndex = 0;
            }

            string result = await _presenter.CreateTask(judul, deadline.ToString("dd/MM/yyyy"), kategoriIndex);
            Console.WriteLine("\n" + result);

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task UpdateTask()
        {
            Console.Clear();
            Console.WriteLine("=== PERBARUI TUGAS ===\n");
            
            Console.Write("Masukkan ID Tugas: ");
            string idStr = Console.ReadLine();
            
            Console.Write("Judul Tugas Baru: ");
            string judul = Console.ReadLine();
            
            Console.Write("Deadline Baru (DD/MM/YYYY): ");
            string deadlineStr = Console.ReadLine();
            
            Console.WriteLine("Kategori Tugas:");
            Console.WriteLine("0. Akademik");
            Console.WriteLine("1. Non-Akademik");
            Console.Write("Pilih Kategori (0/1): ");
            
            if (!int.TryParse(Console.ReadLine(), out int kategoriIndex) || kategoriIndex < 0 || kategoriIndex > 1)
            {
                Console.WriteLine("Kategori tidak valid! Menggunakan default: Akademik");
                kategoriIndex = 0;
            }
            
            string result = await _presenter.UpdateTask(idStr, judul, deadlineStr, kategoriIndex);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task UpdateTaskStatus()
        {
            Console.Clear();
            Console.WriteLine("=== UBAH STATUS TUGAS ===\n");
            
            Console.Write("Masukkan ID Tugas: ");
            string idStr = Console.ReadLine();
            
            Console.WriteLine("Status Tugas:");
            Console.WriteLine("0. Belum Mulai");
            Console.WriteLine("1. Sedang Dikerjakan");
            Console.WriteLine("2. Selesai");
            Console.WriteLine("3. Terlewat");
            Console.Write("Pilih Status (0-3): ");
            
            if (!int.TryParse(Console.ReadLine(), out int statusIndex) || statusIndex < 0 || statusIndex > 3)
            {
                Console.WriteLine("Status tidak valid! Operasi dibatalkan.");
                Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
                Console.ReadLine();
                return;
            }
            
            string result = await _presenter.UpdateTaskStatus(idStr, statusIndex);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("=== HAPUS TUGAS ===\n");
            
            Console.Write("Masukkan ID Tugas: ");
            string idStr = Console.ReadLine();
            
            Console.Write("Anda yakin ingin menghapus tugas ini? (y/n): ");
            string confirmation = Console.ReadLine()?.ToLower();
            
            if (confirmation != "y")
            {
                Console.WriteLine("Operasi dibatalkan.");
                Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
                Console.ReadLine();
                return;
            }
            
            string result = await _presenter.DeleteTask(idStr);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }
        private async Task PrintTasksToFiles()
        {
            Console.Clear();
            Console.WriteLine("=== CETAK DAFTAR TUGAS KE FILE ===\n");

            Console.Write("Masukkan path untuk file JSON: ");
            string jsonFilePath = Console.ReadLine();

            Console.Write("Masukkan path untuk file TXT: ");
            string textFilePath = Console.ReadLine();

            try
            {
                string result = await _presenter.PrintTasksToFilesFromApi(jsonFilePath, textFilePath);
                Console.WriteLine("\n" + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Terjadi kesalahan: {ex.Message}");
            }

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }
    }
} 

