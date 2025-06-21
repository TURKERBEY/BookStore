using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Common;
public abstract class BaseEntity
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; } = null;

    public DateTime? UpdatedDate { get; set; } = null;

    public bool IsRemove { get; set; } = false;
}