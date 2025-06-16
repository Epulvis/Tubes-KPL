namespace Tubes_KPL.src.Domain.Models
{
    // KategoriTugas enum to categorize tasks
    public enum KategoriTugas
    {
        Akademik,
        NonAkademik
    }

    // StatusTugas enum to represent the status of a task
    public enum StatusTugas
    {
        BelumMulai = 0,
        SedangDikerjakan = 1,
        Selesai = 2,
        Terlewat = 3
    }

    // Tugas class to represent a task with properties like Id, Judul, Deadline, Status, and Kategori
    public class Tugas
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTugas Status { get; set; }
        public KategoriTugas Kategori { get; set; }
    }
}