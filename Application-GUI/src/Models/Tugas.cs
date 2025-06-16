namespace Application.Models
{
    // Enum untuk kategori tugas: Akademik atau NonAkademik
    public enum KategoriTugas
    {
        Akademik,
        NonAkademik
    }

    // Enum untuk status tugas: BelumMulai, SedangDikerjakan, Selesai, atau Terlewat
    public enum StatusTugas
    {
        BelumMulai = 0,
        SedangDikerjakan = 1,
        Selesai = 2,
        Terlewat = 3
    }

    // Kelas model untuk merepresentasikan data tugas
    public class Tugas
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTugas Status { get; set; }
        public KategoriTugas Kategori { get; set; }
    }
}