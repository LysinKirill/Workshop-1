namespace Workshop.Api.Responses.V3;

public record GetHistoryResponse(
    CargoResponse Cargo,
    double Price);