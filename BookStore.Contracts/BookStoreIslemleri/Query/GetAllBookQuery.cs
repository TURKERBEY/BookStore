using BookStore.Contracts.BookStoreIslemleri.Dtos;
using BookStore.Contracts.TenantIslemleri.Dtos;
using Shared.Core.Configurations.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.BookStoreIslemleri.Query;
public record class GetAllBookQuery() : IQuery<List<GetAllBookResponseDto>>;
