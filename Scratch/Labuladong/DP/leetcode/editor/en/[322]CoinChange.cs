/*
 * @lc app=leetcode id=322 lang=csharp
 * @lcpr version=30402
 *
 * [322] Coin Change
 */

namespace Scratch.Labuladong.Algorithms.CoinChange;

//You are given an integer array coins representing coins of different
//denominations and an integer amount representing a total amount of money.
//
// Return the fewest number of coins that you need to make up that amount. If
//that amount of money cannot be made up by any combination of the coins, return -1.
//
//
// You may assume that you have an infinite number of each kind of coin.
//
//
// Example 1:
//
//
//Input: coins = [1,2,5], amount = 11
//Output: 3
//Explanation: 11 = 5 + 5 + 1
//
//
// Example 2:
//
//
//Input: coins = [2], amount = 3
//Output: -1
//
//
// Example 3:
//
//
//Input: coins = [1], amount = 0
//Output: 0
//
//
//
// Constraints:
//
//
// 1 <= coins.length <= 12
// 1 <= coins[i] <= 2³¹ - 1
// 0 <= amount <= 10⁴
//
//
// Related TopicsArray | Dynamic Programming | Breadth-First Search
//
// 👍 20845, 👎 542bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int CoinChange(int[] coins, int amount)
    {
        var dp = new int[amount + 1];
        // 为啥 dp 数组中的值都初始化为 amount + 1 呢，因为凑成 amount 金额的硬币数最多只可能等于 amount（全用 1 元面值的硬币），
        // 所以初始化为 amount + 1 就相当于初始化为正无穷，便于后续取最小值。
        // 为啥不直接初始化为 int 型的最大值 Integer.MAX_VALUE 呢？
        // 因为后面有 dp[i - coin] + 1，这就会导致整型溢出。
        Array.Fill(dp, amount + 1);

        // base case
        dp[0] = 0;
        // 外层 for 循环在遍历所有状态的所有取值
        for (int i = 0; i < dp.Length; i++)
        {
            // 内层 for 循环在求所有选择的最小值
            foreach (var coin in coins)
            {
                // 子问题无解，跳过
                if (i - coin < 0) continue;
                dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
            }
        }

        return ( dp[amount] == amount + 1 ) ? -1 : dp[amount];
    }

    private int[] memo = null!;

    public int CoinChangeRecur(int[] coins, int amount)
    {
        memo = new int[amount + 1];
        Array.Fill(memo, -666);

        return dp(coins, amount);
    }

    // 定义：要凑出目标金额 amount，至少要 dp(coins, amount) 个硬币
    int dp(int[] coins, int amount)
    {
        // base case
        if (amount == 0) return 0;
        if (amount < 0) return -1;

        // 查备忘录，防止重复计算
        if (memo[amount] != -666) return memo[amount];

        var res = int.MaxValue;
        foreach (var coin in coins)
        {
            // 计算子问题的结果
            var subProblem = dp(coins, amount - coin);
            // 子问题无解则跳过
            if (subProblem == -1) continue;
            // 在子问题中选择最优解，然后加一
            res = Math.Min(res, subProblem + 1);
        }

        // 把计算结果存入备忘录
        memo[amount] = ( res == int.MaxValue ) ? -1 : res;
        return memo[amount];
    }
}
// @lc code=end
