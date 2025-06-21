using AutoMapper;
using BookStore.Contracts.TenantIslemleri.Command;
using FluentValidation;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.Configurations.Common.Validation;
using Shared.Core.Configurations.CQRS;
using Shared.Core.Repositories;
using Shared.Persistence.Models;


namespace BookStore.Application.Features.TenantIslemleri.Command;
internal class SaveTenantHandler(ITenantRepository tenantRepository, IMapper mapper) : ICommandHandler<SaveTenantCommand, int>
{
    public async Task<Result<int>> Handle(SaveTenantCommand request, CancellationToken cancellationToken)
    {

        var requestDto = request.SaveTenantRequestDto;
        var requestMap = mapper.Map<Tenant>(requestDto);
        if (requestDto.ID > 0)
        {
            await tenantRepository.UpdateAsync(requestMap);
            return Result<int>.SuccessResult(requestMap.Id);
        }
        await tenantRepository.AddAsync(requestMap);
        return Result<int>.SuccessResult(requestMap.Id);
    }

}
[ExcludeFromRegistration]
internal class SaveTenantValidator : AbstractValidator<SaveTenantCommand>
{
    internal SaveTenantValidator()
    {
        RuleFor(x => x.SaveTenantRequestDto.Adi)
               .NotNull()
               .WithMessage(x => $"{nameof(x.SaveTenantRequestDto.Adi)} -> Alanı Zorunlu");

        RuleFor(x => x.SaveTenantRequestDto.Number)
            .NotNull()
            .WithMessage(x => $"{nameof(x.SaveTenantRequestDto.Number)} -> Alanı Zorunlu");
    }


}
