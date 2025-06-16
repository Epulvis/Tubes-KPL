namespace Tubes_KPL.src.Services.Libraries
{
    // Result class library by zuhri
    // Generic result wrapper for operation outcomes
    public class Result<T>
    {
        // Value if operation is successful
        public T? Value { get; private set; }
        
        // Error message if operation failed
        public string? Error { get; private set; }
        
        // Indicates if operation was successful
        public bool IsSuccess => Error == null;

        // Create a successful result
        public static Result<T> Success(T value)
        {
            // Special formatting for string type
            if (value is string str)
            {
                return new Result<T> { Value = (T)(object)$"[green]{str}[/]" };
            }
            return new Result<T> { Value = value };
        }

        // Create a failed result
        public static Result<T> Failure(string error)
        {
            return new Result<T> { Error = $"[red]{error}[/]" };
        }
        
        // String representation of result
        public override string? ToString() => IsSuccess ? Value.ToString() : Error;
    }
}