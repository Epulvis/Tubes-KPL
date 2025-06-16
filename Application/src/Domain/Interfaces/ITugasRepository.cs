using Tubes_KPL.src.Domain.Models;
using System.Collections.Generic;

namespace Tubes_KPL.src.Domain.Interfaces
{
    public interface ITugasRepository
    {
        // CRUD operations for Tugas
        void Tambah(Tugas tugas);

        // Update an existing task
        void Perbarui(Tugas tugas);

        // Delete a task by ID
        void Hapus(int id);

        // Retrieve a task by ID
        Tugas AmbilById(int id);

        // Retrieve all tasks
        IEnumerable<Tugas> AmbilSemua();
    }
}