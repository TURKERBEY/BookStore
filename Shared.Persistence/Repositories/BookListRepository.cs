using Shared.Core.Repositories;
using Shared.Persistence.Models;
using Shared.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Persistence.Repositories;
public class BookListRepository : BaseRepository<BookList>, IBookListRepository
{
    public BookListRepository(BookStoreContext context) : base(context)
    {
    }
}