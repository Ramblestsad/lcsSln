namespace Todo.WebApi.Services.Redis;

public record HotTodoScore(long TodoId, double Score);

public record CheckInClaimResult(bool Claimed, long TotalPoints, DateOnly CheckInDate);

public record MonthlyCheckInSummary(
    string UserKey,
    int Year,
    int Month,
    int TotalCheckedDays,
    IReadOnlyList<int> CheckedDays);

public record ApplySlotResult(bool Applied, bool Duplicate, long RemainingStock);
