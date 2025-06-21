using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Shared.Core.Configurations.Common.Exceptions;
using Shared.Core.Configurations.Common.Exceptions.Types;


namespace Shared.Core.Configurations.Common.Validation;
public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new ValidationContext<object>(request);
        List<ValidationExceptionModel> errors = (from f in _validators.Select((IValidator<TRequest> v) => v.Validate(context)).SelectMany((ValidationResult result) => result.Errors)
                                                 where f != null
                                                 select f into e
                                                 group e by e.PropertyName into g
                                                 select new ValidationExceptionModel
                                                 {
                                                     Property = g.Key,
                                                     Errors = g.Select((ValidationFailure x) => x.ErrorMessage)
                                                 }).ToList();
        if (errors.Any())
        {
            throw new ValidationExceptions(errors);
        }

        return await next();
    }
}