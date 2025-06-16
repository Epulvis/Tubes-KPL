namespace Application.Helpers
{
    public class InputValidator
    {
        // Mengecek apakah input string tidak kosong dan bukan whitespace
        public bool IsValidNonEmptyString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        // Mengecek apakah judul valid (tidak kosong dan maksimal 100 karakter)
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
        
        // Mengecek apakah deadline tidak berada di masa lalu
        public bool IsValidDeadline(DateTime deadline)
        {
            return deadline.Date >= DateTime.Now.Date;
        }

        // Mencoba mengubah input string menjadi integer id yang valid (>0)
        public bool TryParseId(string idInput, out int id)
        {
            if (int.TryParse(idInput, out id))
            {
                return id > 0;
            }
            return false;
        }
        
        // Mengecek apakah input id valid (bisa diubah ke integer dan >0)
        public bool IsValidId(string idInput)
        {
            return TryParseId(idInput, out _);
        }

        // Mengecek apakah rentang tanggal valid (startDate <= endDate)
        public bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate.Date <= endDate.Date;
        }
    }
}
