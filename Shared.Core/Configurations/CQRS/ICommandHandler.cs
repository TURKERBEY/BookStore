using MediatR;
using Shared.Core.Configurations.Common.Result;


namespace Shared.Core.Configurations.CQRS;
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{

}