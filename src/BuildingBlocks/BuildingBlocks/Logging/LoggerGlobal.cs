using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging;

public class LoggerGlobal<TRequest, TResponse>(ILogger<LoggerGlobal<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull, IRequest<TResponse>
where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request {Request} - response {Response} - request Data {RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);
        var timer = Stopwatch.StartNew();
        var response = await next(cancellationToken);
        timer.Stop();
        var timeTaken = timer.ElapsedMilliseconds;
        if(timer.ElapsedMilliseconds>3000)
        {
            logger.LogWarning("[SLOW] request {Request} and response {Response} in {Time}s", typeof(TRequest).Name, typeof(TResponse).Name, timeTaken/1000);
        }
        logger.LogInformation("[END] request {Request} and response {Response} in {Time}s", typeof(TRequest).Name, typeof(TResponse).Name, timeTaken/1000);
        return response;
    }
    
}