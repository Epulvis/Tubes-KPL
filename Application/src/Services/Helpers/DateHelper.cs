using System.Globalization;

namespace Tubes_KPL.src.Application.Helpers
{
    public static class DateHelper
    {
        // Format date to "dd MMMM yyyy"
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }

        // Format date and time to "dd MMMM yyyy HH:mm"
        public static string FormatDateTime(DateTime date)
        {
            return date.ToString("dd MMMM yyyy HH:mm");
        }

        // Calculate days until deadline with validation
        // Design by Contract: Precondition by bintang
        public static int DaysUntilDeadline(DateTime deadline)
        {
            //Preconditions
            if (deadline < DateTime.Today)
            {
                Console.WriteLine($"[ERROR] Deadline tidak valid. Input: {deadline}");
                throw new ArgumentException("Deadline tidak boleh di masa lalu", nameof(deadline));
            }

            var today = DateTime.Today;
            var days = (int)Math.Ceiling((deadline.Date - today).TotalDays);

            //Postconditions
            if (days < 0)
            {
                Console.WriteLine($"[ERROR] Hasil perhitungan tidak valid. Days: {days}");
                throw new InvalidOperationException("Hasil perhitungan tidak valid.");
            }
            return days;
        }

        // Check if deadline is within 3 days
        public static bool IsDeadlineApproaching(DateTime deadline)
        {
            var daysRemaining = DaysUntilDeadline(deadline);
            return daysRemaining >= 0 && daysRemaining <= 3;
        }
        
        // Check if deadline has passed
        public static bool IsDeadlinePassed(DateTime deadline)
        {
            return deadline < DateTime.Now;
        }
        
        // Try to parse date from string in "d/M/yyyy" format
        public static bool TryParseDate(string input, out DateTime result)
        {
            return DateTime.TryParseExact(input, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }
    }
}