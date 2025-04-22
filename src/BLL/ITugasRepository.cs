namespace Tubes_KPL.src.BLL
{
    interface ITugasRepository
    {
        public interface ITugasRepository
        {
            void Tambah(Tugas tugas);
            void Perbarui(Tugas tugas);
            void Hapus(int id);
            Tugas AmbilById(int id);
            IEnumerable<Tugas> AmbilSemua();
        }
    }
}
