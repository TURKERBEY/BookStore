using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //private IMediator? _mediator;
        protected IMediator? Mediator => HttpContext.RequestServices.GetService<IMediator>()
                                          ?? throw new InvalidOperationException("Mediator service not registered.");
    }
}
