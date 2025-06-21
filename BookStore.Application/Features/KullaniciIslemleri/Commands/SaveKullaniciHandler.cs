using AutoMapper;
using BookStore.Contracts.KullaniciIslemleri.Command;
using FluentValidation;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.Configurations.Common.Validation;
using Shared.Core.Configurations.CQRS;
using Shared.Core.Configurations.Security.Hashing;
using Shared.Core.Repositories;
using Shared.Persistence.Models;


namespace BookStore.Application.Features.KullaniciIslemleri.Command;
internal class SaveKullaniciHandler(IKullaniciRepository kullaniciRepository, ITenantRepository tenantRepository, IMapper mapper) : ICommandHandler<SaveKullaniciCommand, int>
{
    public async Task<Result<int>> Handle(SaveKullaniciCommand request, CancellationToken cancellationToken)
    {

        var requestDto = request.SaveKullaniciRequestDto;
        var tenantVarmi = await tenantRepository.GetByIdAsync(requestDto.TenantId, false); // bu methodlar geliştirlebilir en basit şekilde koydum any gibi sürecleri eklemedigim için nesneyi çektim Any de işimizi görürdü performans olarak daha iyi  (query istedigi kadar girebilir olması gerek vs.)
        if (tenantVarmi == null)
        {
            return Result<int>.Failure("Kiralayici Bilgisi Bulunamadı!"); // mesajlar tek biryeden yönetilmesi dil seçenekleri için daha iyi olurdu. Demo oldugu için böyle yazdım.
        }
       
        var requestMap = mapper.Map<Kullanici>(requestDto);
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(requestDto.Password, out passwordHash, out passwordSalt);
        requestMap.PasswordHash = passwordHash;
        requestMap.PasswordSalt = passwordSalt;
        var kullaniciVarmi= await kullaniciRepository.GetSingleAsync(x => x.UserName == requestDto.UserName, false);
        if (requestDto.ID > 0)
        {
            if (kullaniciVarmi != null && kullaniciVarmi.Id != requestDto.ID)
            {
                return Result<int>.Failure("Bu Kullanıcı Adı Zaten Kullanılıyor!");
            }
            await kullaniciRepository.UpdateAsync(requestMap);
            return Result<int>.SuccessResult(requestMap.Id);
        }
        if (kullaniciVarmi != null)
        {
            return Result<int>.Failure("Bu Kullanıcı Adı Zaten Kullanılıyor!");
        }
        await kullaniciRepository.AddAsync(requestMap);
        return Result<int>.SuccessResult(requestMap.Id);
    }

}
[ExcludeFromRegistration]
internal class SaveKullaniciValidator : AbstractValidator<SaveKullaniciCommand>
{
    internal SaveKullaniciValidator()
    {
        RuleFor(x => x.SaveKullaniciRequestDto.UserName)
             .NotNull()
             .WithMessage(x => $"{nameof(x.SaveKullaniciRequestDto.UserName)} -> Alanı Zorunlu");

        RuleFor(x => x.SaveKullaniciRequestDto.TenantId)
         .NotNull()
         .GreaterThan(0)
         .WithMessage(x => $"{nameof(x.SaveKullaniciRequestDto.TenantId)} -> Alanı zorunlu ve 0'dan büyük olmalı.");

        RuleFor(x => x.SaveKullaniciRequestDto.AdiSoyadi)
            .NotNull()
            .WithMessage(x => $"{nameof(x.SaveKullaniciRequestDto.AdiSoyadi)} -> Alanı Zorunlu");

        RuleFor(x => x.SaveKullaniciRequestDto.CepTel)
           .NotNull()
           .WithMessage(x => $"{nameof(x.SaveKullaniciRequestDto.CepTel)} -> Alanı Zorunlu");

        RuleFor(x => x.SaveKullaniciRequestDto.Password)
           .NotNull()
           .WithMessage(x => $"{nameof(x.SaveKullaniciRequestDto.Password)} -> Alanı Zorunlu");
    }


}
