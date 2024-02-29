namespace Shopster.API.Application.Common.Models;

public class Result
{
    public string[] Errors { get; }
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    protected internal Result(bool isSuccess, IEnumerable<string> errors)
    {
        if (isSuccess && errors.Any() ||
            !isSuccess && !errors.Any())
        {
            throw new ArgumentException("Invalid error");
        }

        IsSuccess = isSuccess;
        Errors = errors.ToArray();
    }

    public static Result Success() => new(true, []);
    public static Result Failure(IEnumerable<string> errors) => new(false, errors);
}
