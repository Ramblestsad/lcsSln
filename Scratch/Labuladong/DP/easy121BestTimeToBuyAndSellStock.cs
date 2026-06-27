namespace Scratch.Labuladong.Algorithms.BestTimeToBuyAndSell;

// 121. Best Time to Buy and Sell Stock (Easy)
//
// You are given an array prices where prices[i] is the price of a given stock on the i^th day.
//
// You want to maximize your profit by choosing a single day to buy one stock and choosing a
// different day in the future to sell that stock.
//
// Return the maximum profit you can achieve from this transaction. If you cannot achieve any
// profit, return 0.
//
// Example 1:
//
// Input: prices = [7,1,5,3,6,4]
// Output: 5
// Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
// Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you
// sell.
//
// Example 2:
//
// Input: prices = [7,6,4,3,1]
// Output: 0
// Explanation: In this case, no transactions are done and the max profit = 0.
//
// Constraints:
//
// - 1 <= prices.length <= 10^5
//
// - 0 <= prices[i] <= 10^4
//
// Related Topics: Array, Dynamic Programming

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
