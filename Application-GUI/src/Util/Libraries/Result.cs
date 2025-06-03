namespace Application.Libraries
{
    public class Result<T>
    {
        public T Value { get; private set; }

        public string Error { get; private set; }

        public bool IsSuccess => Error == null;

        public bool IsFailure => !IsSuccess;

        private Result(T value, string error)
        {
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, null);
        }

        public static Result<T> Failure(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentException("Pesan error tidak boleh kosong untuk Result.Failure.", nameof(error));
            }
            return new Result<T>(default(T), error);
        }

        public override string ToString() => IsSuccess ? Value?.ToString() : Error;
    }

    public class Result
    {

        public string Error { get; private set; }

        public bool IsSuccess => Error == null;
        public bool IsFailure => !IsSuccess;

        private Result(string error)
        {
            Error = error;
        }

        public static Result Success()
        {
            return new Result(null);
        }

        public static Result Failure(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                throw new ArgumentException("Pesan error tidak boleh kosong untuk Result.Failure.", nameof(error));
            }
            return new Result(error);
        }

        public override string ToString() => IsSuccess ? "Success" : Error;
    }
}
