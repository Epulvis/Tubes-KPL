using Application.Models;

namespace Application.Services
{
    // State machine for managing task status transitions
    public class StatusStateMachine
    {
        // Enum for task status: NotStarted, InProgress, Completed, or Missed
        // Automata code by zuhri
        private readonly Dictionary<StatusTugas, StatusTugas[]> transitions = new()
        {
            { StatusTugas.BelumMulai, new[] { StatusTugas.SedangDikerjakan } },
            { StatusTugas.SedangDikerjakan, new[] { StatusTugas.Selesai, StatusTugas.Terlewat } },
            { StatusTugas.Selesai, new[] { StatusTugas.SedangDikerjakan } },
            { StatusTugas.Terlewat, new[] { StatusTugas.SedangDikerjakan } }
        };

        // Check if a transition from one status to another is valid
        public bool IsValidTransition(StatusTugas from, StatusTugas to)
            => transitions.ContainsKey(from) && transitions[from].Contains(to);

    }

}