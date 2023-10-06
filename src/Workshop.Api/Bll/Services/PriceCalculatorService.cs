using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Bll.Services;

public class PriceCalculatorService : IPriceCalculatorService
{
    private const double VolumeRatio = 3.27d;

    private readonly IStorageRepository _storageRepository;

    public PriceCalculatorService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }
    public double CalculatePrice(GoodModel[] goods)
    {
        if (!goods.Any())
        {
            throw new ArgumentException("список товаров пустой");
        }
        var volume = goods.Sum(x => x.Height * x.Length * x.Weight);
        var price = volume * VolumeRatio / 1000;
        
        _storageRepository.Save(new StorageEntity(volume, price, DateTime.UtcNow));
        return price;
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
                x.Price))
            .ToArray();
    }
}