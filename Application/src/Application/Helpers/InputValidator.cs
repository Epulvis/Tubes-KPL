using System;
using Tubes_KPL.src.Domain.Models;

namespace Tubes_KPL.src.Application.Helpers
{
    public static class InputValidator
    {
        public static bool IsValidJudul(string judul)
        {
            return !string.IsNullOrWhiteSpace(judul) && judul.Length <= 100;
        }

        // bintang : poin 4 Validasi Input (Pre/Postcondition) 
        public static bool IsValidDeadline(DateTime deadline)
        {
            // Precondition: Deadline harus valid
            if (deadline < DateTime.Now)
            {
                Console.WriteLine($"[ERROR] Deadline tidak valid. Input: {deadline}");
                return false;
            }

            return true;
        }
        public static bool IsValidStatusTransition(StatusTugas currentStatus, StatusTugas newStatus)
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
        public static bool TryParseId(string idInput, out int id)
        {
            return int.TryParse(idInput, out id) && id > 0;
        }
    }
} 