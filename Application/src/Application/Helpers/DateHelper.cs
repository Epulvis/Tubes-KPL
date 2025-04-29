using System;

namespace Tubes_KPL.src.Application.Helpers
{
    public static class DateHelper
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }
        public static string FormatDateTime(DateTime date)
        {
            return date.ToString("dd MMMM yyyy HH:mm");
        }
        public static int DaysUntilDeadline(DateTime deadline)
        {
            var today = DateTime.Today;
            return (int)Math.Ceiling((deadline.Date - today).TotalDays);
        }
        public static bool IsDeadlineApproaching(DateTime deadline)
        {
            var daysRemaining = DaysUntilDeadline(deadline);
            return daysRemaining >= 0 && daysRemaining <= 3;
        }
        public static bool IsDeadlinePassed(DateTime deadline)
        {
            return deadline < DateTime.Now;
        }
        public static bool TryParseDate(string input, out DateTime result)
        {
            return DateTime.TryParse(input, out result);
        }
    }
} 