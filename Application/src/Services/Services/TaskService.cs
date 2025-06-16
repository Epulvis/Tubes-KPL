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

        // Constructor with null check
        public TaskService(ITugasRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Create a new task with validation
        public Tugas BuatTugas(string judul, DateTime deadline, KategoriTugas kategori)
        {
            if (string.IsNullOrWhiteSpace(judul))
            {
                Console.WriteLine("[ERROR] Task title cannot be empty.");
                throw new ArgumentException("Task title cannot be empty.", nameof(judul));
            }

            if (deadline < DateTime.Now)
            {
                Console.WriteLine("[ERROR] Deadline cannot be in the past.");
                throw new ArgumentException("Deadline cannot be in the past.", nameof(deadline));
            }

            Console.WriteLine($"[INFO] Creating new task: '{judul}' with deadline '{deadline}'.");

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

        // Change task status with transition validation
        public Tugas UbahStatusTugas(int id, StatusTugas status)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            if (!IsValidStatusTransition(tugas.Status, status))
                throw new InvalidOperationException($"Invalid status transition from {tugas.Status} to {status}.");

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

        // Validate status transition logic
        // bintang : Design by Contract
        private bool IsValidStatusTransition(StatusTugas currentStatus, StatusTugas newStatus)
        {
            bool isValid = false;
            switch (currentStatus)
            {
                case StatusTugas.BelumMulai:
                    isValid = newStatus == StatusTugas.SedangDikerjakan ||
                              newStatus == StatusTugas.Selesai ||
                              newStatus == StatusTugas.Terlewat;
                    break;
                case StatusTugas.SedangDikerjakan:
                    isValid = newStatus == StatusTugas.Selesai ||
                              newStatus == StatusTugas.Terlewat;
                    break;
                case StatusTugas.Selesai:
                    isValid = newStatus == StatusTugas.Terlewat;
                    break;
                case StatusTugas.Terlewat:
                    isValid = false;
                    break;
            }
            Console.WriteLine($"[LOG] Status transition from {currentStatus} to {newStatus} is {(isValid ? "valid" : "invalid")}.");
            return isValid;
        }

        // Update task details with validation
        public Tugas PerbaruiTugas(int id, string judul, DateTime deadline, KategoriTugas kategori)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(judul))
                throw new ArgumentException("Task title cannot be empty.", nameof(judul));

            tugas.Judul = judul;
            tugas.Deadline = deadline;
            tugas.Kategori = kategori;

            if (deadline < DateTime.Now && tugas.Status != StatusTugas.Selesai)
            {
                tugas.Status = StatusTugas.Terlewat;
            }

            _repository.Perbarui(tugas);
            return tugas;
        }

        // Delete a task by ID
        public void HapusTugas(int id)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            _repository.Hapus(id);
        }

        // Get all tasks
        public IEnumerable<Tugas> AmbilSemuaTugas()
        {
            return _repository.AmbilSemua();
        }

        // Get a task by ID
        public Tugas AmbilTugasById(int id)
        {
            var tugas = _repository.AmbilById(id);
            if (tugas == null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            return tugas;
        }

        // Update status to 'Terlewat' for overdue tasks
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