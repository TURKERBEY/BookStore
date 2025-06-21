using MediatR;
using Shared.Core.Configurations.Common.Result;


namespace Shared.Core.Configurations.CQRS;
public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }