using FluentValidation;
using MediatR;

namespace Todo.Application.Common;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = [];

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = _validators
            .Select(v => v.Validate(context));

        Dictionary<string, string> errorsDictionary = [];

        foreach (var result in validationResults)
        {
            if (result.IsValid) continue;

            var failure = result.Errors.First();
            errorsDictionary.Add(failure.PropertyName, failure.ErrorMessage);
        }

        if (errorsDictionary.Count != 0)
        {
            throw new Exceptions.ValidationException(errorsDictionary);
        }

        var response = await next();

        return response;
    }
}
