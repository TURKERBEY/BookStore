using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Core.Configurations.Common.Exceptions;
using Shared.Core.Configurations.Common.Result;
using Shared.Core.logService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Core.Configurations.Common.Middleware;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            LogService.ExceptionWrite(ex);

            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is ValidationExceptions validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            
            var Valideresponse = new
            {
                context.Response.StatusCode,
                Message = validationException.Errors.ToArray(),
            };
            return context.Response.WriteAsJsonAsync(Valideresponse);
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            context.Response.StatusCode,
            Message = "Sunucu hatası oluştu. Lütfen daha sonra tekrar deneyiniz. (\\wwwroot içerisinde Hata detayı loglandı Son kullanici görmesin diye)"
        };

        return context.Response.WriteAsJsonAsync(response);
    }

}

