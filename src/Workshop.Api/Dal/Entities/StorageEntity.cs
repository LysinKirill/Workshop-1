namespace Workshop.Api.Dal.Entities;

public record StorageEntity(
    double Volume,
    double Price,
    DateTime At,
    double Weight,
    double MaxVolume,
    double MaxWeight,
    double Distance,
    int Count);