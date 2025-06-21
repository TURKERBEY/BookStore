using Shared.Core.Repositories;
using Shared.Persistence.Models;
using Shared.Persistence.Repositories.Base;
 

namespace Shared.Persistence.Repositories;
public class KullaniciRepository : BaseRepository<Kullanici>, IKullaniciRepository
{
    public KullaniciRepository(BookStoreContext context) : base(context)
    {
    }
}