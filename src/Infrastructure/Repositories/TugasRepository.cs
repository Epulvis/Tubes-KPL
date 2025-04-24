using Tubes_KPL.src.Domain.Interfaces;
using Tubes_KPL.src.Domain.Models;

namespace Tubes_KPL.src.Infrastructure.Repositories
{
    public class TugasRepository : ITugasRepository
    {
        private readonly List<Tugas> _tugasDb = new(); // simulasi database

        public void Tambah(Tugas tugas)
        {
            tugas.Id = _tugasDb.Count + 1;
            _tugasDb.Add(tugas);
        }

        public void Perbarui(Tugas tugas)
        {
            var index = _tugasDb.FindIndex(t => t.Id == tugas.Id);
            if (index != -1)
                _tugasDb[index] = tugas;
        }

        public void Hapus(int id)
        {
            var tugas = AmbilById(id);
            if (tugas != null)
                _tugasDb.Remove(tugas);
        }

        public Tugas AmbilById(int id) =>
            _tugasDb.FirstOrDefault(t => t.Id == id);

        public IEnumerable<Tugas> AmbilSemua() =>
            _tugasDb;
    }
}
