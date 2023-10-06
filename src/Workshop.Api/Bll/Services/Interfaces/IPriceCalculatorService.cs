using Workshop.Api.Bll.Models;

namespace Workshop.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    double CalculatePrice(GoodModel[] goods, double distance = 1);
    CalculatetionLogModel[] QueryLog(int take);

    void ClearHistory();
    
}