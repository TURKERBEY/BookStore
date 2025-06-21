 
#nullable disable
using Shared.Domain.Common;
using System;
using System.Collections.Generic;

namespace Shared.Persistence.Models;

public partial class BookList: BaseEntity
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string KitapAdi { get; set; }

 
}