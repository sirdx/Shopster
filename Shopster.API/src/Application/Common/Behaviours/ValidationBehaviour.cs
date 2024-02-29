using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Shopster.API.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            List<ValidationFailure> failures = [];

            foreach (IValidator<TRequest> v in _validators)
            {
                var context = new ValidationContext<TRequest>(request);
                ValidationResult r = await v.ValidateAsync(context, cancellationToken);

                if (!r.IsValid)
                {
                    failures.AddRange(r.Errors);
                }
            }

            if (failures.Count != 0)
            {
                throw new Exceptions.ValidationException(failures);
            }
        }

        return await next();
    }
}
