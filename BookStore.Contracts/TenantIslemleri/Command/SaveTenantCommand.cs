
using BookStore.Contracts.TenantIslemleri.Dtos;
using Shared.Core.Configurations.CQRS;
namespace BookStore.Contracts.TenantIslemleri.Command; 
public record class SaveTenantCommand(SaveTenantRequestDto SaveTenantRequestDto) : ICommand<int>;