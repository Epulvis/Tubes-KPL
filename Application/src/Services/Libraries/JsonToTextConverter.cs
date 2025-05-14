using Tubes_KPL.src.Application.Helpers;
using Tubes_KPL.src.Domain.Models;

namespace Tubes_KPL.src.Application.Libraries
{
    class JsonToTextConverter
    {
        public static string ConvertTasksToText(List<Tugas> tasks)
        {
            var textContent = "Daftar Tugas:\n";
            textContent += "=================================================================================================\n";
            textContent += "| ID |            Judul            |   Kategori   |      Status      |       Deadline        |\n";
            textContent += "=================================================================================================\n";

            foreach (var t in tasks)
            {
                string deadline = DateHelper.FormatDate(t.Deadline);
                string status = t.Status.ToString();

                // Add warning symbol for approaching deadlines
                if (DateHelper.IsDeadlineApproaching(t.Deadline) && t.Status != StatusTugas.Selesai && t.Status != StatusTugas.Terlewat)
                {
                    deadline += " ⚠️";
                }

                textContent += $"| {t.Id,-3}| {t.Judul,-28} | {t.Kategori,-12} | {status,-16} | {deadline,-21} |\n";
            }
            textContent += "=================================================================================================\n";
            return textContent;
        }
    }
}