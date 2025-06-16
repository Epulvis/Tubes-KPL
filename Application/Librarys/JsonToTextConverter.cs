namespace TaskUtilities.Libraries
{
    public class JsonToTextConverter
    {
        private const int ApproachingDays = 3; // Batas hari deadline mendekat

        // Konversi daftar tugas ke format teks tabel
        public static string ConvertTasksToText(List<Tugas> tasks)
        {
            if (tasks == null) throw new ArgumentNullException(nameof(tasks));

            var textContent = "Daftar Tugas:\n";
            textContent += "=================================================================================================\n";
            textContent += "| ID |            Judul            |   Kategori   |      Status      |       Deadline        |\n";
            textContent += "=================================================================================================\n";

            foreach (var t in tasks)
            {
                if (t == null) continue; // Lewati jika data null

                string deadline = FormatDate(t.Deadline);
                string status = t.Status.ToString();

                // Tampilkan peringatan jika deadline mendekat
                if (IsDeadlineApproaching(t.Deadline) && t.Status != StatusTugas.Selesai && t.Status != StatusTugas.Terlewat)
                {
                    deadline += " ⚠️";
                }

                textContent += $"| {t.Id,-3}| {t.Judul,-28} | {t.Kategori,-12} | {status,-16} | {deadline,-21} |\n";
            }
            textContent += "=================================================================================================\n";
            return textContent;
        }

        // Hitung jumlah hari menuju deadline
        public static int DaysUntilDeadline(DateTime deadline)
        {
            var today = DateTime.Today;
            if (deadline.Date < today)
                throw new ArgumentException("Deadline tidak boleh di masa lalu", nameof(deadline));

            var days = (int)Math.Ceiling((deadline.Date - today).TotalDays);

            if (days < 0)
                throw new InvalidOperationException("Hasil perhitungan tidak valid.");

            return days;
        }

        // Cek apakah deadline mendekat
        public static bool IsDeadlineApproaching(DateTime deadline)
        {
            var daysRemaining = DaysUntilDeadline(deadline);
            return daysRemaining >= 0 && daysRemaining <= ApproachingDays;
        }

        // Format tanggal ke string
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }

        // Model data tugas
        public class Tugas
        {
            public int Id { get; set; }
            public string Judul { get; set; } = string.Empty; // Inisialisasi default
            public DateTime Deadline { get; set; }
            public StatusTugas Status { get; set; }
            public KategoriTugas Kategori { get; set; }
        }

        // Enum status tugas
        public enum StatusTugas
        {
            BelumMulai = 0,
            SedangDikerjakan = 1,
            Selesai = 2,
            Terlewat = 3
        }

        // Enum kategori tugas
        public enum KategoriTugas
        {
            Akademik,
            NonAkademik
        }
    }
}