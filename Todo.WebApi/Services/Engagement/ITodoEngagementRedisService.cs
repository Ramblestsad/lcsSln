namespace Todo.WebApi.Services.Engagement;

public interface ITodoEngagementRedisService
{
    Task<double> IncrementTodoHotScoreAsync(long todoId, double score = 1, TimeSpan? expiry = null);

    Task<IReadOnlyList<HotTodoScore>> GetTopHotTodoScoresAsync(int topN);

    Task<CheckInClaimResult> ClaimDailyCheckInAsync(
        string userKey,
        DateOnly checkInDate,
        int rewardPoints);

    Task<MonthlyCheckInSummary> GetMonthlyCheckInSummaryAsync(string userKey, int year, int month);

    Task<long> SetTodoApplyStockAsync(long todoId, int stock);

    Task<ApplySlotResult> TryApplyTodoSlotAsync(long todoId, string userKey);
}
