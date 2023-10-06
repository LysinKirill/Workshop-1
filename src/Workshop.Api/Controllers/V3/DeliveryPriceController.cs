using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Requests.V3;
using Workshop.Api.Responses.V3;

namespace Workshop.Api.Controllers.V3;

[ApiController]
[Route("v3/[controller]")]
public class DeliveryPriceController : ControllerBase
{
    private readonly IPriceCalculatorService _priceCalculatorService;
    private readonly IAnalyticsService _analyticsService;

    public DeliveryPriceController(IPriceCalculatorService priceCalculatorService, IAnalyticsService analyticsService)
    {
        _priceCalculatorService = priceCalculatorService;
        _analyticsService = analyticsService;
    }

    [HttpPost("calculate")]
    public CalculateResponse Calculate(CalculateRequest request)
    {
        var result =
            _priceCalculatorService.CalculatePrice(
                request.Goods
                    .Select(x => new GoodModel(
                        x.Height, 
                        x.Length, 
                        x.Width,
                        x.Weight))
                .ToArray(), request.Distance);
        return new CalculateResponse(result);
    }

    [HttpPost("get-history")]
    public GetHistoryResponse[] GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculatorService.QueryLog(request.Take);
        return log.Select(x => new GetHistoryResponse(
                new CargoResponse(x.Volume,
                    x.Weight),
                x.Price))
            .ToArray();
    }

    [HttpPost("delete-history")]
    public void DeleteHistory()
    {
        _priceCalculatorService.ClearHistory();
    }

    [HttpPost("analytics")]
    public ReportsResponse GetAnalytics()
    {
        return new ReportsResponse(
            _analyticsService.GetMaxVolume(),
            _analyticsService.GetMaxWeight(),
            _analyticsService.GetDistanceOfMaxVolume(),
            _analyticsService.GetDistanceOfMaxWeight(),
            _analyticsService.WeightedAverageCost()
        );
    }
}