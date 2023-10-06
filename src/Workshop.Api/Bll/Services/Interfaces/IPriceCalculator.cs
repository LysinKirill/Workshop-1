using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculator
{
    double CalculatePrice(GoodModel[] goods);
    CalculatetionLogModel[] QueryLog(int take);
}