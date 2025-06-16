namespace Application.Libraries
{
    // Kelas generic untuk merepresentasikan hasil operasi yang bisa sukses atau gagal
    public class Result<T>
    {
        // Nilai hasil jika operasi sukses
        public T Value { get; private set; }
        
        // Pesan error jika operasi gagal
        public string Error { get; private set; }

        // Menandakan apakah operasi berhasil
        public bool IsSuccess => Error == null;
        
        // Menandakan apakah operasi gagal
        public bool IsFailure => !IsSuccess;

        // Konstruktor privat untuk menghindari instansiasi langsung
        private Result(T value, string error)
        {
            Value = value;
            Error = error;
        }

        // Metode statis untuk membuat hasil sukses
        public static Result<T> Success(T value)
        {
            return new Result<T>(value, null);
        }

        // Metode statis untuk membuat hasil gagal
        public static Result<T> Failure(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentException("Pesan error tidak boleh kosong untuk Result.Failure.", nameof(error));
            }
            return new Result<T>(default(T), error);
        }

        // Metode untuk mengembalikan representasi string dari hasil
        public override string ToString() => IsSuccess ? Value?.ToString() : Error;
    }

// Kelas untuk merepresentasikan hasil operasi tanpa nilai
    public class Result
    {
        
        // Pesan error jika operasi gagal
        public string Error { get; private set; }

        // Menandakan apakah operasi berhasil
        public bool IsSuccess => Error == null;
        
        // Menandakan apakah operasi gagal
        public bool IsFailure => !IsSuccess;

        // Konstruktor privat untuk menghindari instansiasi langsung
        private Result(string error)
        {
            Error = error;
        }

        // Metode statis untuk membuat hasil sukses
        public static Result Success()
        {
            return new Result(null);
        }
        
        // Metode statis untuk membuat hasil gagal
        public static Result Failure(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentException("Pesan error tidak boleh kosong untuk Result.Failure.", nameof(error));
            }
            return new Result(error);
        }

        // Metode untuk mengembalikan representasi string dari hasil
        public override string ToString() => IsSuccess ? "Success" : Error;
    }
}