using MediatR;

namespace Done.Application.Abstraction;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}