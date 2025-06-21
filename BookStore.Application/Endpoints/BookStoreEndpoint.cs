using BookStore.Contracts.BookStoreIslemleri.Command;
using BookStore.Contracts.BookStoreIslemleri.Dtos;
using BookStore.Contracts.BookStoreIslemleri.Query;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Core.Configurations.Common.Result;
 

namespace BookStore.Application.Endpoints;
public class BookStoreEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/BookStore");

        api.WithTags("BookStore");
        api.RequireAuthorization();
        api.MapPost("SaveBook", async (SaveBookRequestDto requestDto, ISender sender)  =>
        {
            var response = await sender.Send(new SaveBookCommand(requestDto));
            return Results.Ok(response);
        }).WithTags("BookStore")
          .WithName("SaveBook").WithSummary("Kitap Kayıt/Update (ID dolu gelmesinde update sürecine dahil ediyorum)")
          .Produces<Result<int>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound);


        api.MapGet("GetAllBook", async ( ISender sender) =>
        {
            var response = await sender.Send(new GetAllBookQuery());
            return Results.Ok(response);
        }).WithTags("BookStore")
       .WithName("GetAllBook").WithSummary("Kitap Listesi")
       .Produces<Result<List<GetAllBookResponseDto>>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound);

        api.MapDelete("DeleteKitap", async (int Id,ISender sender) =>
        {
            var response = await sender.Send(new DeleteKitapCommand(Id));
            return Results.Ok(response);
        }).WithTags("BookStore")
        .WithName("DeleteKitap").WithSummary("Kitap Silme İşlemi Kitap Id beklemekte")
        .Produces<Result<bool>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);




    }
}