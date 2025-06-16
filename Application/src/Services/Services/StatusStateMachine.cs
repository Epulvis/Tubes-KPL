using Tubes_KPL.src.Domain.Models;

namespace Tubes_KPL.src.Application.Services
{
    public static class StatusStateMachine
    {
        // Automata code by zuhri
        // State transition rules for task status
        private static readonly Dictionary<StatusTugas, StatusTugas[]> transitions = new()
        {
            { StatusTugas.BelumMulai, new[] { StatusTugas.SedangDikerjakan } },
            { StatusTugas.SedangDikerjakan, new[] { StatusTugas.Selesai, StatusTugas.Terlewat } },
            { StatusTugas.Selesai, new[] { StatusTugas.SedangDikerjakan } },
            { StatusTugas.Terlewat, new[] { StatusTugas.SedangDikerjakan } }
        };

        // Check if status transition is allowed
        public static bool CanTransition(StatusTugas from, StatusTugas to)
            => transitions.ContainsKey(from) && transitions[from].Contains(to);
    }

}