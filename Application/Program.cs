using Tubes_KPL.src.Application.Services;
using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Infrastructure.Repositories;
using Tubes_KPL.src.Presentation.Presenters;
using Tubes_KPL.src.Presentation.Views;

namespace Tubes_KPL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Initialize repository
            var tugasRepository = new TugasRepository();
            
            // Initialize service with repository dependency
            var taskService = new TaskService(tugasRepository);
            
            // Initialize presenter with service dependency
            var taskPresenter = new TaskPresenter(taskService);
            
            // Initialize view with presenter dependency
            var taskView = new TaskView(taskPresenter);
            
            // Start the application
            Console.WriteLine("Memulai aplikasi Manajemen Tugas Mahasiswa...");
            
            // Add some sample data for testing
            AddSampleData(taskService);
            
            // Show main menu
            taskView.ShowMainMenu();
            
            Console.WriteLine("Aplikasi telah ditutup. Terima kasih!");
        }
        
        private static void AddSampleData(TaskService taskService)
        {
            try 
            {
                // Add sample academic task
                taskService.BuatTugas(
                    "Tugas Besar KPL", 
                    DateTime.Now.AddDays(14), 
                    KategoriTugas.Akademik
                );
                
                // Add sample non-academic task with close deadline
                taskService.BuatTugas(
                    "Rapat Organisasi", 
                    DateTime.Now.AddDays(2), 
                    KategoriTugas.NonAkademik
                );
                
                // Add sample task that's already in progress
                var tugas = taskService.BuatTugas(
                    "Latihan UTS Matematika", 
                    DateTime.Now.AddDays(7), 
                    KategoriTugas.Akademik
                );
                taskService.UbahStatusTugas(tugas.Id, StatusTugas.SedangDikerjakan);
                
                // Add completed task
                var tugasSelesai = taskService.BuatTugas(
                    "Laporan Praktikum", 
                    DateTime.Now.AddDays(5), 
                    KategoriTugas.Akademik
                );
                taskService.UbahStatusTugas(tugasSelesai.Id, StatusTugas.Selesai);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding sample data: {ex.Message}");
            }
        }
    }
}