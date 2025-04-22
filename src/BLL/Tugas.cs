namespace Tubes_KPL.src.BLL
{
    public enum KategoriTugas
    {
        Akademik,
        NonAkademik
    }

    public enum StatusTugas
    {
        BelumMulai,
        SedangDikerjakan,
        Selesai,
        Terlewat
    }

    public class Tugas
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTugas Status { get; set; }
        public KategoriTugas Kategori { get; set; }
    }
}

