using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private const double VolumeRatio = 3.27d;
    private const double WeightRatio = 1.34d;

    private readonly IStorageRepository _storageRepository;

    public PriceCalculatorService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }
    public double CalculatePrice(GoodModel[] goods, double distance = 1)
    {
        if (!goods.Any())
        {
            throw new ArgumentException("список товаров пустой");
        }
        var volumePrice = CalculatePriceByVolume(goods, out var volume);
        var weightPrice = CalculatePriceByWeight(goods, out var weight);

        var maxVolume = goods.Max(x => x.Height * x.Width * x.Length);
        var maxWeight = goods.Max(x => x.Weight);
        
        
        var resultPrice = Math.Max(volumePrice, weightPrice) * distance;
        _storageRepository.Save(new StorageEntity(volume, resultPrice, DateTime.UtcNow, weight, maxVolume, maxWeight, distance, goods.Length));

        return resultPrice;
    }

    private double CalculatePriceByVolume(GoodModel[] goods, out double volume)
    {
        volume = goods.Sum(x => x.Height * x.Length * x.Weight);
        var volumePrice = volume * VolumeRatio / 1000.0d;
        return volumePrice;
    }

    private static double CalculatePriceByWeight(GoodModel[] goods, out double weight)
    {
        weight = goods.Sum(x => x.Weight);
        var weightPrice = weight * WeightRatio / 1000.0d;
        return weightPrice;
    }
    public CalculatetionLogModel[] QueryLog(int take)
    {
        if (take < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take,"Take должно быть больше 0");
        }
        var log = _storageRepository.Query()
            .OrderByDescending(x => x.At)
            .Take(take)
            .ToArray();
        return log.Select(x => new CalculatetionLogModel(
                x.Volume,
                x.Price,
                x.Weight))
            .ToArray();
    }

    public void ClearHistory()
    {
        _storageRepository.ClearHistory();
    }


}