
using BookStore.Contracts.KullaniciIslemleri.Dtos;
using Shared.Core.Configurations.CQRS;
 

namespace BookStore.Contracts.KullaniciIslemleri.Command;
public record class SaveKullaniciCommand(SaveKullaniciRequestDto SaveKullaniciRequestDto) : ICommand<int>;