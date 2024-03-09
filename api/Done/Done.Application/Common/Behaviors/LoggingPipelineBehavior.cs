﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace Done.Application.Common.Behaviors;

public sealed class LoggingPipelineBehavior<TRequest, TResponse>(
        ILogger logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting request {@Request} at {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        try
        {
            var result = await next();

            return result;
        }
        catch (Exception e)
        {
            logger.LogError("Error handling request {@Request} at {@DateTimeUtc} with error {@Error}",
                typeof(TRequest).Name,
                DateTime.UtcNow,
                e.Message);
        }
        finally
        {
            logger.LogInformation("Finished request {@Request} at {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }

        return default!;
    }
}