namespace Workshop.Api.Bll.Services.Interfaces;

public interface IAnalyticsService
{
    double GetMaxVolume();

    double GetMaxWeight();

    double GetDistanceOfMaxVolume();

    double GetDistanceOfMaxWeight();

    double WeightedAverageCost();
}