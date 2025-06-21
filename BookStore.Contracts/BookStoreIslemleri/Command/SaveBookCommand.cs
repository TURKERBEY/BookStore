using BookStore.Contracts.BookStoreIslemleri.Dtos;
using Shared.Core.Configurations.CQRS;
 
namespace BookStore.Contracts.BookStoreIslemleri.Command;
public record class SaveBookCommand(SaveBookRequestDto SaveBookRequestDto) : ICommand<int>;