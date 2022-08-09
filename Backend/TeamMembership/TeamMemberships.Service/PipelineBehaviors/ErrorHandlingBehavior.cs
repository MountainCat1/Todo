using MediatR;
using Microsoft.Extensions.Logging;
using TeamMemberships.Infrastructure.Exceptions;
using TeamMemberships.Service.Exceptions;

namespace TeamMemberships.Service.PipelineBehaviors;
public sealed class ErrorHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{

    private readonly ILogger<ErrorHandlingBehavior<TRequest,TResponse>> _logger;

    public ErrorHandlingBehavior(ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (ItemNotFoundException ex)
        {
            throw new NotFoundException(ex.Message, ex);
        }
    }
}