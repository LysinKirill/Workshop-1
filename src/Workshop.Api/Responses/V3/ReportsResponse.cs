namespace Workshop.Api.Responses.V3;

public record ReportsResponse(double MaxVolume, double MaxWeight, double DistanceOfMaxVolume, double DistanceOfMaxWeight, double WeightedAverage);