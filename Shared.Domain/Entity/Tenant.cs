 
#nullable disable
using Shared.Domain.Common;
using System;
using System.Collections.Generic;

namespace Shared.Persistence.Models;

public partial class Tenant: BaseEntity
{
    public int Id { get; set; }

    public string Adi { get; set; }

    public string Number { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsRemove { get; set; }

    public virtual ICollection<Kullanici> Kullanicis { get; set; } = new List<Kullanici>();
}