using Tubes_KPL.src.Domain.Models;

namespace Tubes_KPL.src.Domain.Interfaces
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
