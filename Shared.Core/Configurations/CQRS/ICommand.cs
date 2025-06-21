using MediatR;
using Shared.Core.Configurations.Common.Result;
 

namespace Shared.Core.Configurations.CQRS;
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }