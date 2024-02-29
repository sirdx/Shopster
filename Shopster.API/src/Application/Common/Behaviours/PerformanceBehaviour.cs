using MediatR;
using Microsoft.Extensions.Logging;
using Shopster.API.Application.Common.Interfaces;
using System.Diagnostics;

namespace Shopster.API.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly Stopwatch _timer = new();

    public PerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        TResponse? response = await next();

        _timer.Stop();

        long elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500L)
        {
            string requestName = typeof(TRequest).Name;
            Guid? userId = _currentUserService.Id;

            _logger.LogWarning("Shopster Long Running Request: {Name} ({ElapsedMilliseconds} ms) {@UserId} {@Request}",
                requestName, elapsedMilliseconds, userId.ToString(), request);
        }

        return response;
    }
}
