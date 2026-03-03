using StackExchange.Redis;

namespace Todo.WebApi.Services.Redis;

public class TodoEngagementRedisService: ITodoEngagementRedisService
{
    private const string TodoHotKey = "todo:hot:zset";
    private readonly IDatabase redisDatabase;
    private readonly ILogger<TodoEngagementRedisService> logger;

    private const string CheckInClaimLuaScript = """
                                                 local bitmapKey = KEYS[1]
                                                 local pointsKey = KEYS[2]
                                                 local offset = tonumber(ARGV[1])
                                                 local reward = tonumber(ARGV[2])
                                                 local bitmapExpireAtUnix = tonumber(ARGV[3])

                                                 local signed = redis.call('GETBIT', bitmapKey, offset)
                                                 if signed == 1 then
                                                     local existingPoints = tonumber(redis.call('GET', pointsKey) or '0')
                                                     return {0, existingPoints}
                                                 end

                                                 redis.call('SETBIT', bitmapKey, offset, 1)
                                                 if bitmapExpireAtUnix > 0 then
                                                     redis.call('EXPIREAT', bitmapKey, bitmapExpireAtUnix)
                                                 end

                                                 local totalPoints = redis.call('INCRBY', pointsKey, reward)
                                                 return {1, totalPoints}
                                                 """;

    private const string TodoSlotApplyLuaScript = """
                                                  local stockKey = KEYS[1]
                                                  local appliedSetKey = KEYS[2]
                                                  local userKey = ARGV[1]

                                                  if redis.call('SISMEMBER', appliedSetKey, userKey) == 1 then
                                                      local currentStock = tonumber(redis.call('GET', stockKey) or '-1')
                                                      return {2, currentStock}
                                                  end

                                                  local stock = tonumber(redis.call('GET', stockKey) or '-1')
                                                  if stock <= 0 then
                                                      return {0, stock}
                                                  end

                                                  local newStock = redis.call('DECR', stockKey)
                                                  redis.call('SADD', appliedSetKey, userKey)
                                                  return {1, newStock}
                                                  """;

    public TodoEngagementRedisService(
        IConnectionMultiplexer connectionMultiplexer,
        ILogger<TodoEngagementRedisService> logger)
    {
        redisDatabase = connectionMultiplexer.GetDatabase();
        this.logger = logger;
    }

    public async Task<double> IncrementTodoHotScoreAsync(long todoId, double score = 1, TimeSpan? expiry = null)
    {
        try
        {
            var newScore = await redisDatabase.SortedSetIncrementAsync(
                TodoHotKey,
                todoId.ToString(),
                score
            );
            if (expiry.HasValue)
            {
                await redisDatabase.KeyExpireAsync(TodoHotKey, expiry.Value);
            }

            logger.LogInformation(
                "Increment todo hot score success. TodoId: {TodoId}, Delta: {Delta}, NewScore: {NewScore}",
                todoId,
                score,
                newScore);
            return newScore;
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Increment todo hot score failed. TodoId: {TodoId}", todoId);
            throw;
        }
    }

    public async Task<IReadOnlyList<HotTodoScore>> GetTopHotTodoScoresAsync(int topN)
    {
        try
        {
            var values = await redisDatabase.SortedSetRangeByRankWithScoresAsync(
                TodoHotKey,
                0,
                Math.Max(topN - 1, 0),
                Order.Descending);

            var result = new List<HotTodoScore>(values.Length);
            foreach (var value in values)
            {
                if (!long.TryParse(value.Element.ToString(), out var todoId))
                {
                    continue;
                }

                result.Add(new HotTodoScore(todoId, value.Score));
            }

            return result;
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Get top hot todos failed. TopN: {TopN}", topN);
            throw;
        }
    }

    public async Task<CheckInClaimResult> ClaimDailyCheckInAsync(
        string userKey,
        DateOnly checkInDate,
        int rewardPoints)
    {
        try
        {
            var bitmapKey = GetCheckInBitmapKey(userKey, checkInDate.Year, checkInDate.Month);
            var pointsKey = GetPointsKey(userKey);
            var offset = checkInDate.Day - 1;

            var monthEnd = new DateTime(checkInDate.Year, checkInDate.Month,
                                        DateTime.DaysInMonth(checkInDate.Year, checkInDate.Month))
                .AddDays(45);
            var monthBitmapExpireAtUnix = new DateTimeOffset(monthEnd).ToUnixTimeSeconds();

            var evalResult = await redisDatabase.ScriptEvaluateAsync(
                CheckInClaimLuaScript,
                [bitmapKey, pointsKey],
                [offset, rewardPoints, monthBitmapExpireAtUnix]);

            var values = (RedisResult[])evalResult!;
            var claimed = ToInt64(values[0]) == 1;
            var totalPoints = ToInt64(values[1]);

            logger.LogInformation(
                "Check-in claim processed. User: {UserKey}, Date: {Date}, Claimed: {Claimed}, TotalPoints: {TotalPoints}",
                userKey,
                checkInDate,
                claimed,
                totalPoints);

            return new CheckInClaimResult(claimed, totalPoints, checkInDate);
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Check-in claim failed. User: {UserKey}, Date: {Date}", userKey, checkInDate);
            throw;
        }
    }

    public async Task<MonthlyCheckInSummary> GetMonthlyCheckInSummaryAsync(string userKey, int year, int month)
    {
        try
        {
            var bitmapKey = GetCheckInBitmapKey(userKey, year, month);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var checkedDays = new List<int>();

            for (var day = 1; day <= daysInMonth; day++)
            {
                var checkedIn = await redisDatabase.StringGetBitAsync(bitmapKey, day - 1);
                if (checkedIn)
                {
                    checkedDays.Add(day);
                }
            }

            return new MonthlyCheckInSummary(
                userKey,
                year,
                month,
                checkedDays.Count,
                checkedDays
            );
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Get monthly check-in summary failed. User: {UserKey}, YearMonth: {Year}-{Month}",
                            userKey, year, month);
            throw;
        }
    }

    public async Task<long> SetTodoApplyStockAsync(long todoId, int stock)
    {
        try
        {
            var stockKey = GetTodoApplyStockKey(todoId);
            await redisDatabase.StringSetAsync(stockKey, stock);
            logger.LogInformation("Set todo apply stock success. TodoId: {TodoId}, Stock: {Stock}", todoId, stock);
            return stock;
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Set todo apply stock failed. TodoId: {TodoId}", todoId);
            throw;
        }
    }

    public async Task<ApplySlotResult> TryApplyTodoSlotAsync(long todoId, string userKey)
    {
        try
        {
            var stockKey = GetTodoApplyStockKey(todoId);
            var appliedSetKey = GetTodoApplyUserSetKey(todoId);

            var evalResult = await redisDatabase.ScriptEvaluateAsync(
                TodoSlotApplyLuaScript,
                [stockKey, appliedSetKey],
                [userKey]);

            var values = (RedisResult[])evalResult!;
            var code = ToInt64(values[0]);
            var remainingStock = ToInt64(values[1]);

            var applied = code == 1;
            var duplicate = code == 2;

            logger.LogInformation(
                "Try apply todo slot processed. TodoId: {TodoId}, User: {UserKey}, Applied: {Applied}, Duplicate: {Duplicate}, RemainingStock: {RemainingStock}",
                todoId,
                userKey,
                applied,
                duplicate,
                remainingStock);

            return new ApplySlotResult(applied, duplicate, remainingStock);
        }
        catch (RedisException ex)
        {
            logger.LogError(ex, "Try apply todo slot failed. TodoId: {TodoId}, User: {UserKey}", todoId, userKey);
            throw;
        }
    }

    private static string GetCheckInBitmapKey(string userKey, int year, int month) =>
        $"todo:checkin:{userKey}:{year:D4}{month:D2}";

    private static string GetPointsKey(string userKey) => $"todo:points:{userKey}";

    private static string GetTodoApplyStockKey(long todoId) => $"todo:apply:stock:{todoId}";

    private static string GetTodoApplyUserSetKey(long todoId) => $"todo:apply:users:{todoId}";

    private static long ToInt64(RedisResult redisResult) =>
        long.TryParse(redisResult.ToString(), out var value) ? value : 0;
}
