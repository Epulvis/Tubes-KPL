using System;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Presentation.Presenters;

namespace Tubes_KPL.src.Presentation.Views
{
    public class TaskView
    {
        private readonly TaskPresenter _presenter;

        public TaskView(TaskPresenter presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public void ShowMainMenu()
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
                        ShowAllTasks();
                        break;
                    case 2:
                        ShowTaskDetails();
                        break;
                    case 3:
                        AddNewTask();
                        break;
                    case 4:
                        UpdateTask();
                        break;
                    case 5:
                        UpdateTaskStatus();
                        break;
                    case 6:
                        DeleteTask();
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

        private void ShowAllTasks()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR TUGAS ===\n");
            
            string result = _presenter.GetAllTasks();
            Console.WriteLine(result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private void ShowTaskDetails()
        {
            Console.Clear();
            Console.WriteLine("=== DETAIL TUGAS ===\n");
            
            Console.Write("Masukkan ID Tugas: ");
            string idStr = Console.ReadLine();
            
            string result = _presenter.GetTaskDetails(idStr);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private void AddNewTask()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH TUGAS BARU ===\n");
            
            Console.Write("Judul Tugas: ");
            string judul = Console.ReadLine();
            
            Console.Write("Deadline (DD/MM/YYYY): ");
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
            
            string result = _presenter.CreateTask(judul, deadlineStr, kategoriIndex);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private void UpdateTask()
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
            
            string result = _presenter.UpdateTask(idStr, judul, deadlineStr, kategoriIndex);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private void UpdateTaskStatus()
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
            
            string result = _presenter.UpdateTaskStatus(idStr, statusIndex);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private void DeleteTask()
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
            
            string result = _presenter.DeleteTask(idStr);
            Console.WriteLine("\n" + result);
            
            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }
    }
} 