using AutoMapper;
using BookStore.Contracts.BookStoreIslemleri.Dtos;
using BookStore.Contracts.BookStoreIslemleri.Query;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.Configurations.CQRS;

using Shared.Core.Repositories;
 

namespace BookStore.Application.Features.BookStoreIslemleri.Query;
internal class GetAllBookHandler(IBookListRepository bookListRepository, IMapper mapper) : IQueryHandler<GetAllBookQuery, List<GetAllBookResponseDto>>
{
    public async Task<Result<List<GetAllBookResponseDto>>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {


     var kitaps= await  bookListRepository.GetAllAsync(); // kiralıycı ıd sine göre olması gerek normalde
           var responseMap = mapper.Map<List<GetAllBookResponseDto>>(kitaps);
   
        return Result<List<GetAllBookResponseDto>>.SuccessResult(responseMap);
    }

}