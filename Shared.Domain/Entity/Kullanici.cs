 
#nullable disable
using Shared.Domain.Common;
 

namespace Shared.Persistence.Models;

public partial class Kullanici: BaseEntity
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string AdiSoyadi { get; set; }

    public string UserName { get; set; }

    public string CepTel { get; set; }

    public byte[] PasswordSalt { get; set; }

    public byte[] PasswordHash { get; set; }

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsRemove { get; set; }

    public virtual Tenant Tenant { get; set; }
}