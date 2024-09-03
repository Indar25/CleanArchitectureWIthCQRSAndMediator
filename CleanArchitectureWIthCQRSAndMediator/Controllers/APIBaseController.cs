using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWIthCQRSAndMediator.Controllers
{
    public class APIBaseController : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
