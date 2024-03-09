using MediatR;

namespace Done.Application.Common.Abstraction;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}