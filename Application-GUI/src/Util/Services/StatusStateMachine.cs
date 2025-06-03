using Application.Models;

namespace Application.Services
{
    public class StatusStateMachine
    {
        // Automata code by zuhri
        private readonly Dictionary<StatusTugas, StatusTugas[]> transitions = new()
        {
            { StatusTugas.BelumMulai, new[] { StatusTugas.SedangDikerjakan } },
            { StatusTugas.SedangDikerjakan, new[] { StatusTugas.Selesai, StatusTugas.Terlewat } },
            { StatusTugas.Selesai, new[] { StatusTugas.SedangDikerjakan } },
            { StatusTugas.Terlewat, new[] { StatusTugas.SedangDikerjakan } }
        };

        public bool IsValidTransition(StatusTugas from, StatusTugas to)
            => transitions.ContainsKey(from) && transitions[from].Contains(to);

    }

}