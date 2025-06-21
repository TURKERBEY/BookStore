using Shared.Core.Repositories;
using Shared.Persistence.Models;
using Shared.Persistence.Repositories.Base;
 

namespace Shared.Persistence.Repositories;
public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
{
    public TenantRepository(BookStoreContext context) : base(context)
    {
    }
}