using Shared.Core.Configurations.Common.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.Exceptions;
public class ValidationExceptions : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; }

    public ValidationExceptions()
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationExceptions(string? message)
        : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationExceptions(string? message, Exception? innerException)
        : base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationExceptions(IEnumerable<ValidationExceptionModel> errors)
        : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> values = errors.Select((ValidationExceptionModel x) => $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, x.Errors ?? Array.Empty<string>())}");
        return "Validation failed: " + string.Join(string.Empty, values);
    }
}