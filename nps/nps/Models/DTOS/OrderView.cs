namespace nps.Models.DTOS;

public record OrderView(
    long Id,
    string Number,
    DateTime OrderDate, 
    DateTime? DeliveryDate,
    string ClientEmail,
    bool HasSurvey);