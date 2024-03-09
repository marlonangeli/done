using MediatR;

namespace Done.Application.Abstraction;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}