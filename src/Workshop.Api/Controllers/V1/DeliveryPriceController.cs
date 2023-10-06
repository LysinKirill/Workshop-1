using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Requests.V1;
using Workshop.Api.Responses.V1;

namespace Workshop.Api.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
public class DeliveryPriceController : ControllerBase
{
    [HttpPost]
    public CalculateResponse Calculate(CalculateRequest request)
    {
        throw new NotImplementedException();
    }
}
