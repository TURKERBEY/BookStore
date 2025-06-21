using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.Exceptions.Types;
public class ValidationExceptionModel
{
    public string? Property { get; set; }

    public IEnumerable<string>? Errors { get; set; }
}