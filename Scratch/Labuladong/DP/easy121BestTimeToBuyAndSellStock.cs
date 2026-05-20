/*
 * @lc app=leetcode id=121 lang=csharp
 * @lcpr version=30403
 *
 * [121] Best Time to Buy and Sell Stock
 */

namespace Scratch.Labuladong.Algorithms.BestTimeToBuyAndSell;

// @lc code=start
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
// @lc code=end

/*
// @lcpr case=start
// [7,1,5,3,6,4]\n
// @lcpr case=end

// @lcpr case=start
// [7,6,4,3,1]\n
// @lcpr case=end
 */
