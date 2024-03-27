namespace Todo.Application.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException()
    {
    }

    public ApplicationException(Dictionary<string, string> errors)
    {
        Errors = errors;
    }

    public ApplicationException(string? message) : base(message)
    {
    }

    public ApplicationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public Dictionary<string, string> Errors { get; } = [];
}

public class ValidationException : ApplicationException
{
    public override string Message => "Validation error.";

    public ValidationException(Dictionary<string, string> errors) : base(errors)
    {

    }
}

public class NotFoundException<TEntity> : ApplicationException
{
    public NotFoundException()
        : base($"{typeof(TEntity).Name} doesn't exist.")
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}