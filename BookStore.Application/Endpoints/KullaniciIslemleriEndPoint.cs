
using BookStore.Contracts.KullaniciIslemleri.Command;
using BookStore.Contracts.KullaniciIslemleri.Dtos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Core.Configurations.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Endpoints;
public class KullaniciIslemleriEndPoint : CarterModule
{ 
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/Kullanici");

        api.WithTags("Kullanici");

        api.MapPost("SaveKullanici", async (SaveKullaniciRequestDto requestDto, ISender sender) =>
        {
            var response = await sender.Send(new SaveKullaniciCommand(requestDto));
            return Results.Ok(response);
        }).WithTags("Kullanici")
          .WithName("SaveKullanici").WithSummary("Bulut tabanlı olacagı düşünüldü aynı magazada birden fazla kullanıcı acılabilsin diye kiralıyıcı bilgisi eklendi (tokenden muaf bırakıldı!)")
          .Produces<Result<int>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound);

        api.MapPost("Login", async (LoginDto requestDto, ISender sender) =>
        {
            var response = await sender.Send(new LoginCommand(requestDto));
            return Results.Ok(response);
        }).WithTags("Kullanici")
         .WithName("Login").WithSummary("")
         .Produces<Result<string>>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound);


    }
}
