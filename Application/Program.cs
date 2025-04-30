using System;
using System.Threading.Tasks;
using Tubes_KPL.src.Infrastructure.Configuration;
using Tubes_KPL.src.Presentation.Presenters;
using Tubes_KPL.src.Presentation.Views;

namespace Tubes_KPL
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                // Initialize configuration provider
                var configProvider = new JsonConfigProvider("src/Infrastructure/Configuration/config.json");

                // Initialize presenter with configuration provider
                var taskPresenter = new TaskPresenter(configProvider);

                // Initialize view with presenter dependency
                var taskView = new TaskView(taskPresenter, configProvider);

                // Start the application
                Console.WriteLine("Memulai aplikasi Manajemen Tugas Mahasiswa...");
                Console.WriteLine("Menghubungkan ke API...");

                // Show main menu
                await taskView.ShowMainMenu();

                Console.WriteLine("Aplikasi telah ditutup. Terima kasih!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Aplikasi mengalami masalah. Silakan coba lagi nanti.");
                Console.WriteLine("Tekan Enter untuk keluar...");
                Console.ReadLine();
            }
        }
    }
}
