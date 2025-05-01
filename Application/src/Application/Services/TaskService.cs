using Tubes_KPL.src.Domain.Interfaces;
using Tubes_KPL.src.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Tubes_KPL.src.Application.Libraries;

namespace Tubes_KPL.src.Application.Services
{
    public class TaskService
    {
        private readonly ITugasRepository _repository;
        public TaskService(ITugasRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        //Bintang : poin 1 Defensive Programming + Logging
        public Tugas BuatTugas(string judul, DateTime deadline, KategoriTugas kategori)
        {
            // Precondition: Validasi input
            if (string.IsNullOrWhiteSpace(judul))
            {
                Console.WriteLine($"[ERROR] Judul tugas tidak boleh kosong. Input: {judul}");
                throw new ArgumentException("Judul tugas tidak boleh kosong", nameof(judul));
            }

            if (deadline < DateTime.Now)
            {
                Console.WriteLine($"[ERROR] Deadline tidak boleh di masa lalu. Input: {deadline}");
                throw new ArgumentException("Deadline tidak boleh di masa lalu", nameof(deadline));
            }

            // Logging: Informasi tugas yang dibuat
            Console.WriteLine($"[INFO] Membuat tugas baru dengan judul '{judul}' dan deadline '{deadline}'.");

            // Postcondition: Pastikan tugas dibuat dengan status default
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
        
        // bintang : poin 3 automata
        private bool IsValidStatusTransition(StatusTugas currentStatus, StatusTugas newStatus)
        {
            bool isValid = false; // Variabel penanda apakah transisi status valid

            // Mengecek transisi yang diperbolehkan berdasarkan status saat ini
            switch (currentStatus)
            {
                case StatusTugas.BelumMulai:
                    // Jika status saat ini BelumMulai, hanya bisa ke SedangDikerjakan, Selesai, atau Terlewat
                    isValid = newStatus == StatusTugas.SedangDikerjakan ||
                              newStatus == StatusTugas.Selesai ||
                              newStatus == StatusTugas.Terlewat;
                    break;

                case StatusTugas.SedangDikerjakan:
                    // Jika SedangDikerjakan, hanya bisa ke Selesai atau Terlewat
                    isValid = newStatus == StatusTugas.Selesai ||
                              newStatus == StatusTugas.Terlewat;
                    break;

                case StatusTugas.Selesai:
                    // Jika Selesai, hanya bisa ke Terlewat (misalnya untuk validasi deadline)
                    isValid = newStatus == StatusTugas.Terlewat;
                    break;

                case StatusTugas.Terlewat:
                    // Terlewat adalah status akhir, tidak bisa transisi ke status lain
                    isValid = false;
                    break;
            }

            // Logging hasil validasi transisi status
            Console.WriteLine($"[LOG] Transisi dari {currentStatus} ke {newStatus} " +
                              (isValid ? "valid." : "tidak valid."));

            return isValid; // Mengembalikan hasil validasi
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
        public void CetakDaftarTugas(string jsonFilePath, string textFilePath)
        {
            // Ambil semua tugas dari repository
            var tugasList = _repository.AmbilSemua().ToList();

            if (!tugasList.Any())
            {
                Console.WriteLine("[INFO] Tidak ada tugas yang tersedia untuk dicetak.");
                return;
            }

            Console.WriteLine($"[DEBUG] Jumlah tugas yang ditemukan: {tugasList.Count}");

            // Serialize daftar tugas ke file JSON
            var jsonContent = JsonSerializer.Serialize(tugasList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, jsonContent);

            Console.WriteLine($"[INFO] Daftar tugas berhasil disimpan ke file JSON: {jsonFilePath}");

            // Konversi file JSON ke file teks
            JsonToTextConverter.ConvertJsonToText(jsonFilePath, textFilePath);

            Console.WriteLine($"[INFO] Daftar tugas berhasil dikonversi ke file teks: {textFilePath}");
        }
    }
} 

