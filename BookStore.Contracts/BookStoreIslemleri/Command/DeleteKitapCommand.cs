using Shared.Core.Configurations.CQRS;


namespace BookStore.Contracts.BookStoreIslemleri.Command;
public record class DeleteKitapCommand(int Id) : ICommand<bool>;