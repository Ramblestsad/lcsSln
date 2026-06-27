namespace Scratch.Labuladong.Algorithms.BestTimeToBuyAndSell;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MaxProfit(int[] prices)
    {
        var n = prices.Length;
        var dp = new int[n][];
        for (var i = 0; i < n; i++)
        {
            dp[i] = new int[2];
        }

        for (var i = 0; i < n; i++)
        {
            if (i == 0)
            {
                // base case
                dp[i][0] = 0;
                dp[i][1] = -prices[i];
                continue;
            }

            dp[i][0] = Math.Max(dp[i - 1][0], dp[i - 1][1] + prices[i]);
            dp[i][1] = Math.Max(dp[i - 1][1], -prices[i]);
        }

        return dp[n - 1][0];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
