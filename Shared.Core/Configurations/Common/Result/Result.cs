using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Configurations.Common.Result;
public class Result<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static Result<T> SuccessResult(T data, string? message = null)
    {
        return new Result<T> { Success = true, Data = data, Message = message };
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T> { Success = false, Message = message };
    }
}
