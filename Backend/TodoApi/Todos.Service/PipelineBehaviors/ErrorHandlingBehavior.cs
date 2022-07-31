using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Todos.Domain.Exceptions;
using Todos.Service.Abstractions;
using Todos.Service.Exceptions;
using ICommand = System.Windows.Input.ICommand;

namespace Todos.Service.PipelineBehaviors;


public sealed class ErrorHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
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