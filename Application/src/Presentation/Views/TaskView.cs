using Spectre.Console;
using Tubes_KPL.src.Application.Helpers;
using Tubes_KPL.src.Infrastructure.Configuration;
using Tubes_KPL.src.Presentation.Presenters;

namespace Tubes_KPL.src.Presentation.Views
{
    public class TaskView
    {
        private readonly TaskPresenter _presenter;
        private readonly IConfigProvider _configProvider;

        private readonly Dictionary<string, Func<Task>> _menuActions;

        public TaskView(TaskPresenter presenter, IConfigProvider configProvider)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _configProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));

            // Table-driven: mapping menu ke aksi
            _menuActions = new Dictionary<string, Func<Task>>
            {
                { "Lihat Daftar Tugas", ShowAllTasks },
                { "Lihat Tugas Berdasarkan Rentang Waktu", ShowTasksByDateRange },
                { "Lihat Detail Tugas", ShowTaskDetails },
                { "Tambah Tugas Baru", AddNewTask },
                { "Perbarui Tugas", UpdateTask },
                { "Ubah Status Tugas", UpdateTaskStatus },
                { "Hapus Tugas", DeleteTask },
                { "Cetak Daftar Tugas ke File JSON dan TXT", PrintTasksToFiles }
                // "Keluar" akan ditangani khusus
            };
        }

        public async Task ShowMainMenu()
        {
            var isRunning = true;

            while (isRunning)
            {
                AnsiConsole.Clear();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]=== APLIKASI MANAJEMEN TUGAS MAHASISWA ===[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Gunakan panah atas/bawah untuk memilih)[/]")
                        .AddChoices(_menuActions.Keys.Concat(new[] { "Keluar" })));

                if (choice == "Keluar")
                {
                    if (AnsiConsole.Confirm("Apakah Anda yakin ingin keluar?"))
                    {
                        isRunning = false;
                        AnsiConsole.MarkupLine("[green]Terima kasih! Program akan ditutup.[/]");
                        await Task.Delay(1000);
                    }
                }
                else if (_menuActions.TryGetValue(choice, out var action))
                {
                    await action();
                }
            }
        }
        private async Task ShowAllTasks()
        {
            Console.Clear();

            string result = await _presenter.GetAllTasks();
            Console.WriteLine(result);

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task ShowTasksByDateRange()
        {
            Console.Clear();
            Console.WriteLine("=== LIHAT TUGAS BERDASARKAN RENTANG WAKTU ===\n");

            Console.Write("Masukkan Tanggal Mulai (DD/MM/YYYY): ");
            string startDateStr = Console.ReadLine();

            Console.Write("Masukkan Tanggal Akhir (DD/MM/YYYY): ");
            string endDateStr = Console.ReadLine();

            string result = await _presenter.GetTasksByDateRange(startDateStr, endDateStr);
            if (!string.IsNullOrWhiteSpace(result))
                Console.WriteLine("\n" + result);

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task ShowTaskDetails()
        {
            Console.Clear();
            Console.WriteLine("=== LIHAT TUGAS BERDASARKAN RENTANG WAKTU ===\n");
            Console.Write("Masukkan Tanggal Mulai (DD/MM/YYYY): ");
            string startDateStr = Console.ReadLine();

            Console.Write("Masukkan Tanggal Akhir (DD/MM/YYYY): ");
            string endDateStr = Console.ReadLine();

            string result = await _presenter.GetTasksByDateRange(startDateStr, endDateStr);
            if (!string.IsNullOrWhiteSpace(result))
                Console.WriteLine("\n" + result);

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
            int kategori = InputHelper.GetValidatedInput("Pilih Kategori (0/1): ", s =>
                (int.TryParse(s, out int val) && (val == 0 || val == 1), val));

            //if (!int.TryParse(Console.ReadLine(), out int kategoriIndex) || kategoriIndex < 0 || kategoriIndex > 1)
            //{
            //    Console.WriteLine("Kategori tidak valid! Menggunakan default: Akademik");
            //    kategoriIndex = 0;
            //}

            string result = await _presenter.CreateTask(judul, deadline.ToString("dd/MM/yyyy"), kategori);
            Console.WriteLine("\n" + result);

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task UpdateTask()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold yellow]PERBARUI TUGAS[/]");

            string idStr = InputValidator.NonEmptyInput("Masukkan ID Tugas: ");

            string judul = InputValidator.NonEmptyInput("Judul Tugas Baru: ");

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

            var result = await _presenter.UpdateTask(idStr, judul, deadlineStr, kategoriIndex);
            AnsiConsole.MarkupLine("\n" + result);

            Console.WriteLine("\nTekan Enter untuk kembali ke menu utama...");
            Console.ReadLine();
        }

        private async Task UpdateTaskStatus()
        {
            Console.Clear();
            Console.WriteLine("=== UBAH STATUS TUGAS ===\n");

            string idStr = InputValidator.NonEmptyInput("Masukkan ID Tugas: ");

            var result = await _presenter.UpdateTaskStatus(idStr);

            AnsiConsole.MarkupLine($"\n{result}");
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
        
        //Library untuk mencetak daftar tugas ke file JSON dan TXT
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
    public static class InputHelper
    {
        /// <summary>
        /// Mengambil input dari user dan memvalidasi menggunakan parser.
        /// Precondition: prompt dan parser tidak boleh null/kosong.
        /// Postcondition: Mengembalikan nilai T yang valid sesuai parser.
        /// </summary>
        public static T GetValidatedInput<T>(string prompt, Func<string, (bool, T)> parser)
        {
            // Precondition
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt tidak boleh kosong.", nameof(prompt));
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                var (isValid, value) = parser(input);
                if (isValid)
                    return value;
                Console.WriteLine("Input tidak valid, coba lagi.");
            }
            // Postcondition: return value hanya jika isValid == true
        }
    }
}