using AutoMapper;
using BookStore.Contracts.BookStoreIslemleri.Command;
using BookStore.Contracts.KullaniciIslemleri.Command;
using FluentValidation;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.Configurations.Common.Validation;
using Shared.Core.Configurations.CQRS;
using Shared.Core.Repositories;
using Shared.Persistence.Models;

namespace BookStore.Application.Features.BookStoreIslemleri.Commands;
internal class SaveBookHandler(IBookListRepository bookListRepository, ITenantRepository tenantRepository, IMapper mapper) : ICommandHandler<SaveBookCommand, int>
{
    public async Task<Result<int>> Handle(SaveBookCommand request, CancellationToken cancellationToken)
    {

        var requestDto = request.SaveBookRequestDto;
        var tenantVarmi = await tenantRepository.GetByIdAsync(requestDto.TenantId, false); // bu methodlar geliştirlebilir en basit şekilde koydum any gibi sürecleri eklemedigim için nesneyi çektim Any de işimizi görürdü performans olarak daha iyi  (query istedigi kadar girebilir olması gerek vs.)
        if (tenantVarmi == null)
        {
            return Result<int>.Failure("Kiralayici Bilgisi Bulunamadı!"); // mesajlar tek biryeden yönetilmesi dil seçenekleri için daha iyi olurdu. Demo oldugu için böyle yazdım.
        }
        var requestMap = mapper.Map<BookList>(requestDto);
        if (requestDto.ID > 0)
        {
            await bookListRepository.UpdateAsync(requestMap);
            return Result<int>.SuccessResult(requestMap.Id);
        }
        await bookListRepository.AddAsync(requestMap);
        return Result<int>.SuccessResult(requestMap.Id);
    }

}
[ExcludeFromRegistration]
internal class SaveBookValidator : AbstractValidator<SaveBookCommand>
{
    internal SaveBookValidator()
    {
        RuleFor(x => x.SaveBookRequestDto.KitapAdi)
             .NotNull()
             .WithMessage(x => $"{nameof(x.SaveBookRequestDto.KitapAdi)} -> Alanı Zorunlu");

        RuleFor(x => x.SaveBookRequestDto.TenantId)
         .NotNull()
         .GreaterThan(0)
         .WithMessage(x => $"{nameof(x.SaveBookRequestDto.TenantId)} -> Alanı zorunlu ve 0'dan büyük olmalı.");

      
    }


}
