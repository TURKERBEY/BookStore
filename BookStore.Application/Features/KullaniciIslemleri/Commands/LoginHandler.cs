using BookStore.Contracts.KullaniciIslemleri.Command;
using FluentValidation;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.Configurations.Common.Validation;
using Shared.Core.Configurations.CQRS;
using Shared.Core.Configurations.Security.Hashing;
using Shared.Core.Configurations.Security.Jwt;
using Shared.Core.Repositories;

namespace BookStore.Application.Features.KullaniciIslemleri.Command;
internal class LoginHandler(IKullaniciRepository kullaniciRepository, ITokenHelper jwtTokenService) : ICommandHandler<LoginCommand, string>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        var requestDto = request.LoginDto;
        var user = await kullaniciRepository.GetSingleAsync(x => x.UserName == requestDto.Username, false);
        if (user == null)
        {
            return Result<string>.Failure("kullanici bulunamadı");
        }
        if (!HashingHelper.VerifyPasswordHash(requestDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Result<string>.Failure("Parola Hatalı");
        }
        if (user.IsActive==false)
        {
            return Result<string>.Failure("Kullaniciniz Pasife Alındı");
        }
        var token = jwtTokenService.CreateToken(user);
        return Result<string>.SuccessResult(token.Token);
    }

}
[ExcludeFromRegistration]
internal class LoginValidator : AbstractValidator<LoginCommand>
{
    internal LoginValidator()
    {
        RuleFor(x => x.LoginDto.Username)
             .NotNull()
             .WithMessage(x => $"{nameof(x.LoginDto.Username)} -> Alanı Zorunlu");

        RuleFor(x => x.LoginDto.Password)
         .NotNull()
         .WithMessage(x => $"{nameof(x.LoginDto.Password)} -> Alanı zorunlu ve 0'dan büyük olmalı.");


    }


}
