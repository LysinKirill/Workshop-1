using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly IStorageRepository _storageRepository;

    public AnalyticsService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }
    
    public double GetMaxVolume()
    {
        var storageEntities = _storageRepository.Query();
        return storageEntities.Max(x => x.MaxVolume);
    }

    public double GetMaxWeight()
    {
        var storageEntities = _storageRepository.Query();
        return storageEntities.Max(x => x.MaxWeight);
    }

    public double GetDistanceOfMaxVolume()
    {
        var storageEntities = _storageRepository.Query();
        var maxOverallVolume = storageEntities.Max(x => x.MaxVolume);
        return storageEntities.FirstOrDefault(x => x.MaxVolume == maxOverallVolume)?.Distance ?? 0;
    }

    public double GetDistanceOfMaxWeight()
    {
        var storageEntities = _storageRepository.Query();
        var maxOverallWeight = storageEntities.Max(x => x.MaxWeight);
        return storageEntities.FirstOrDefault(x => x.MaxWeight == maxOverallWeight)?.Distance ?? 0;
    }

    public double WeightedAverageCost()
    {
        var storageEntities = _storageRepository.Query();
        var totalPrice = storageEntities.Sum(x => x.Price);
        var totalCount = storageEntities.Sum(x => x.Count);
        return (totalPrice / totalCount);
    }
}