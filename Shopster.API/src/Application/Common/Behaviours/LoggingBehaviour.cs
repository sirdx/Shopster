using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Shopster.API.Application.Common.Interfaces;

namespace Shopster.API.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> 
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        Guid? userId = _currentUserService.Id;

        _logger.LogInformation("Shopster Request: {Name} {@UserId} {@Request}",
            requestName, userId.ToString(), request);

        return Task.CompletedTask;
    }
}
