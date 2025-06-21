using BookStore.Contracts.TenantIslemleri.Dtos;
using Shared.Core.Configurations.CQRS;

namespace BookStore.Contracts.TenantIslemleri.Queries;
public record class GetTenantQuery() : IQuery<List<GetAllTenantResponseDto>>;
