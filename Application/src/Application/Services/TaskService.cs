using Tubes_KPL.src.Domain.Interfaces;
using Tubes_KPL.src.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tubes_KPL.src.Application.Services
{
    public class TaskService
    {
        private readonly ITugasRepository _repository;
        public TaskService(ITugasRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public Tugas BuatTugas(string judul, DateTime deadline, KategoriTugas kategori)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(judul))
                throw new ArgumentException("Judul tugas tidak boleh kosong", nameof(judul));

            if (deadline < DateTime.Now)
                throw new ArgumentException("Deadline tidak boleh di masa lalu", nameof(deadline));

            // Create task with default state "BelumMulai"
            var tugas = new Tugas
            {
                Judul = judul,
                Deadline = deadline,
                Status = StatusTugas.BelumMulai,
                Kategori = kategori
            };

            _repository.Tambah(tugas);
            return tugas;
        }
        public Tugas UbahStatusTugas(int id, StatusTugas status)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Tugas dengan ID {id} tidak ditemukan");

            // Apply automata logic for state transitions
            if (!IsValidStatusTransition(tugas.Status, status))
                throw new InvalidOperationException($"Transisi status dari {tugas.Status} ke {status} tidak valid");

            // If deadline has passed and status is not yet Terlewat, automatically set it
            if (tugas.Deadline < DateTime.Now && status != StatusTugas.Terlewat)
            {
                tugas.Status = StatusTugas.Terlewat;
            }
            else
            {
                tugas.Status = status;
            }

            _repository.Perbarui(tugas);
            return tugas;
        }
        private bool IsValidStatusTransition(StatusTugas currentStatus, StatusTugas newStatus)
        {
            switch (currentStatus)
            {
                case StatusTugas.BelumMulai:
                    // From BelumMulai can go to SedangDikerjakan, Selesai, or Terlewat
                    return newStatus == StatusTugas.SedangDikerjakan || 
                           newStatus == StatusTugas.Selesai || 
                           newStatus == StatusTugas.Terlewat;

                case StatusTugas.SedangDikerjakan:
                    // From SedangDikerjakan can go to Selesai or Terlewat
                    return newStatus == StatusTugas.Selesai || 
                           newStatus == StatusTugas.Terlewat;

                case StatusTugas.Selesai:
                    // From Selesai can only go to Terlewat (if deadline passed)
                    return newStatus == StatusTugas.Terlewat;

                case StatusTugas.Terlewat:
                    // Terlewat is a terminal state
                    return false;

                default:
                    return false;
            }
        }
        public Tugas PerbaruiTugas(int id, string judul, DateTime deadline, KategoriTugas kategori)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Tugas dengan ID {id} tidak ditemukan");

            // Validate input
            if (string.IsNullOrWhiteSpace(judul))
                throw new ArgumentException("Judul tugas tidak boleh kosong", nameof(judul));

            tugas.Judul = judul;
            tugas.Deadline = deadline;
            tugas.Kategori = kategori;

            // If deadline is in the past and task is not finished, mark as Terlewat
            if (deadline < DateTime.Now && tugas.Status != StatusTugas.Selesai)
            {
                tugas.Status = StatusTugas.Terlewat;
            }

            _repository.Perbarui(tugas);
            return tugas;
        }

        public void HapusTugas(int id)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Tugas dengan ID {id} tidak ditemukan");

            _repository.Hapus(id);
        }

        public IEnumerable<Tugas> AmbilSemuaTugas()
        {
            return _repository.AmbilSemua();
        }

        public Tugas AmbilTugasById(int id)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Tugas dengan ID {id} tidak ditemukan");

            return tugas;
        }

        public void PerbaruiStatusTerlewat()
        {
            var tugasList = _repository.AmbilSemua().ToList();
            var now = DateTime.Now;

            foreach (var tugas in tugasList)
            {
                if (tugas.Deadline < now && tugas.Status != StatusTugas.Terlewat && tugas.Status != StatusTugas.Selesai)
                {
                    tugas.Status = StatusTugas.Terlewat;
                    _repository.Perbarui(tugas);
                }
            }
        }
    }
} 