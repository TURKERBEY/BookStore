using MediatR;

using System.Transactions;

namespace Shared.Core.Configurations.Common.Pipelines.Transaction;
public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ITransactionalRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            TResponse response = await next();
            transactionScope.Complete();
            return response;
        }
        catch (Exception)
        {
            transactionScope.Dispose();
            throw;
        }
    }
}