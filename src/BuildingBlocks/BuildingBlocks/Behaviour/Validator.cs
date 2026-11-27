using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviour;

public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResults = new ValidationContext<TRequest>(request);
        var validationErrors = await Task.WhenAll(validators.Select(v => v.ValidateAsync(validationResults, cancellationToken)));
        var failures = validationErrors.SelectMany(r => r.Errors).Where(f => f != null).ToList();
        if (failures.Count > 0)
        {
            throw new ValidationException(failures);
        }
        return await next(cancellationToken);
    }
}
