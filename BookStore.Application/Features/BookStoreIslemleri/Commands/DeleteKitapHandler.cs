using BookStore.Contracts.BookStoreIslemleri.Command;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.Configurations.CQRS;
using Shared.Core.Repositories;


namespace BookStore.Application.Features.BookStoreIslemleri.Commands;
internal class DeleteKitapHandler(IBookListRepository bookListRepository) : ICommandHandler<DeleteKitapCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteKitapCommand request, CancellationToken cancellationToken)
    {
        // kitabın baglı relationlari var ise kontrol edilmeli burayı bu kontrollerde rul ayrı kalsor ve classta  ayrılırsa daha kontrollü olur
        var kitap = await bookListRepository.GetSingleAsync(x => x.Id == request.Id, false);
        if (kitap==null)
        {
            return Result<bool>.Failure("Kayıt bulunamadı!");
        }
       await bookListRepository.DeleteAsync(kitap);
        return Result<bool>.SuccessResult(true);
    }

}