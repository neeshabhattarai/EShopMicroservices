using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHandler<TRequest,TResponse>:
    IRequestHandler<TRequest,TResponse>
    where TRequest:IQuery<TResponse>
    where TResponse:notnull
{
    
}