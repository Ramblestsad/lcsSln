using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Engagement.Contracts;

public record CheckInClaimRequest(
    [property: Required]
    [property: StringLength(128, MinimumLength = 1)]
    string UserKey,
    [property: Range(1, 1000)] int RewardPoints = 5,
    DateOnly? Date = null);

public record SetApplyStockRequest(
    [property: Range(0, 1_000_000)] int Stock);

public record ApplyTodoSlotRequest(
    [property: Required]
    [property: StringLength(128, MinimumLength = 1)]
    string UserKey);
