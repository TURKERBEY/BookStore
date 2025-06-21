
using BookStore.Contracts.TenantIslemleri.Command;
using BookStore.Contracts.TenantIslemleri.Dtos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Core.Configurations.Common.Result;


namespace BookStore.Application.Endpoints;
public class TenantIslemleriEndPoint : CarterModule
{
    private readonly IMediator _mediator;

    public TenantIslemleriEndPoint(IMediator mediator) : base()
    {
        _mediator = mediator;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/Tenant");
        
        api.WithTags("Tenant");

        api.MapPost("SaveTenant", async (SaveTenantRequestDto requestDto, ISender sender) =>
        {
            var response = await sender.Send(new SaveTenantCommand(requestDto));
            return Results.Ok(response);
        }).WithTags("Tenant")
          .WithName("SaveTenant").WithSummary("Bulut tabanlı olacagı düşünüldü aynı magazada birden fazla kullanıcı acılabilsin diye kiralıyıcı bilgisi eklendi (tokenden muaf bırakıldı!)")
          .Produces<Result<int>>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesProblem(StatusCodes.Status404NotFound);


    }
}
