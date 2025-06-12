namespace Application.Helpers
{
    public class InputValidator
    {
        public bool IsValidNonEmptyString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public bool IsValidTitle(string judul)
        {
            if (string.IsNullOrWhiteSpace(judul))
            {
                return false;
            }
            if (judul.Length > 100)
            {
                return false;
            }
            return true;
        }

        public bool IsValidDeadline(DateTime deadline)
        {
            return deadline.Date >= DateTime.Now.Date;
        }

        public bool TryParseId(string idInput, out int id)
        {
            if (int.TryParse(idInput, out id))
            {
                return id > 0;
            }
            return false;
        }

        public bool IsValidId(string idInput)
        {
            return TryParseId(idInput, out _);
        }

        public bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate.Date <= endDate.Date;
        }
    }
}
