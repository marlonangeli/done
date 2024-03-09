using MediatR;

namespace Done.Application.Common.Abstraction;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}