using Tubes_KPL.src.Domain.Models;
using Tubes_KPL.src.Infrastructure.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        // Tes code Anggota 1
        var repo = new TugasRepository();

        // Tambah tugas
        var tugas1 = new Tugas
        {
            Judul = "Tugas KPL",
            Deadline = new DateTime(2025, 5, 10),
            Status = StatusTugas.BelumMulai,
            Kategori = KategoriTugas.Akademik
        };
        repo.Tambah(tugas1);

        var tugas2 = new Tugas
        {
            Judul = "Rapat Organisasi",
            Deadline = new DateTime(2025, 5, 5),
            Status = StatusTugas.SedangDikerjakan,
            Kategori = KategoriTugas.NonAkademik
        };
        repo.Tambah(tugas2);

        Console.WriteLine("Daftar Tugas:");
        foreach (var t in repo.AmbilSemua())
        {
            Console.WriteLine($"{t.Id}. {t.Judul} | {t.Kategori} | {t.Status} | Deadline: {t.Deadline.ToShortDateString()}");
        }

        // Perbarui tugas
        tugas1.Status = StatusTugas.Selesai;
        repo.Perbarui(tugas1);

        Console.WriteLine("\n Setelah diperbarui:");
        var updated = repo.AmbilById(tugas1.Id);
        Console.WriteLine($"{updated.Id}. {updated.Judul} | {updated.Kategori} | {updated.Status} | Deadline: {updated.Deadline.ToShortDateString()}");

        // Hapus tugas
        repo.Hapus(tugas2.Id);
        Console.WriteLine("\n Setelah dihapus:");
        foreach (var t in repo.AmbilSemua())
        {
            Console.WriteLine($"{t.Id}. {t.Judul} | {t.Kategori} | {t.Status} | Deadline: {t.Deadline.ToShortDateString()}");
        }

        Console.WriteLine("\n[Selesai]");
    }
}