using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    double CalculatePrice(GoodModel[] goods);
    CalculatetionLogModel[] QueryLog(int take);
}