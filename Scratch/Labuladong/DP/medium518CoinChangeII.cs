/*
 * @lc app=leetcode id=518 lang=csharp
 * @lcpr version=30402
 *
 * [518] Coin Change II
 */

namespace Scratch.Labuladong.Algorithms.CoinChangeII;

//You are given an integer array coins representing coins of different
//denominations and an integer amount representing a total amount of money.
//
// Return the number of combinations that make up that amount. If that amount
//of money cannot be made up by any combination of the coins, return 0.
//
// You may assume that you have an infinite number of each kind of coin.
//
// The answer is guaranteed to fit into a signed 32-bit integer.
//
//
// Example 1:
//
//
//Input: amount = 5, coins = [1,2,5]
//Output: 4
//Explanation: there are four ways to make up the amount:
//5=5
//5=2+2+1
//5=2+1+1+1
//5=1+1+1+1+1
//
//
// Example 2:
//
//
//Input: amount = 3, coins = [2]
//Output: 0
//Explanation: the amount of 3 cannot be made up just with coins of 2.
//
//
// Example 3:
//
//
//Input: amount = 10, coins = [10]
//Output: 1
//
//
//
// Constraints:
//
//
// 1 <= coins.length <= 300
// 1 <= coins[i] <= 5000
// All the values of coins are unique.
// 0 <= amount <= 5000
//
//
// Related TopicsArray | Dynamic Programming
//
// 👍 10193, 👎 241bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    // 转化为背包问题的描述形式：
    //      有一个背包，最大容量为 amount，有一系列物品 coins，每个物品的重量为 coins[i]，每个物品的数量无限。
    //      请问有多少种方法，能够把背包恰好装满？

    // 【状态】：「背包的容量」和「可选择的物品」
    // 【选择】：「装进背包」或者「不装进背包」

    // dp数组：
    //      若只使用前 i 个物品（可以重复使用），当背包容量为 j 时，有 dp[i][j] 种方法可以装满背包。
    //      base case:
    //          dp[0][..] = 0, dp[..][0] = 1。
    //          i = 0 代表不使用任何硬币面值，这种情况下显然无法凑出任何金额；
    //          j = 0 代表需要凑出的目标金额为 0，那么什么都不做就是唯一的一种凑法。

    // 状态转移
    //      1. 如果不把这第 i 个物品装入背包，也就是说不使用 coins[i-1] 这个面值的硬币，
    //          那么凑出面额 j 的方法数 dp[i][j] 应该等于 dp[i-1][j]，继承之前的结果。
    //      2. 如果你把这第 i 个物品装入了背包，也就是说你使用 coins[i-1] 这个面值的硬币，
    //          那么 dp[i][j] 应该等于 dp[i][j-coins[i-1]]。
    //      综上就是两种选择，而想求的 dp[i][j] 是「共有多少种凑法」，所以 dp[i][j] 的值应该是以上两种选择的结果之和。

    // FAQ
    //      那么如果我确定「使用第 i 个面值的硬币」，我怎么确定这个面值的硬币被使用了多少枚？
    //      简单的一个 dp[i][j-coins[i-1]] 可以包含重复使用第 i 个硬币的情况吗？
    //
    //      完全背包（零钱兑换 II）
    //      dp[i][j] = dp[i - 1][j] + dp[i][j - coins[i-1]];
    //                                   ^
    //                                  注意这里是 i，不是 i-1; 用了物品 i 后，仍然可以继续使用物品 i.
    //
    //      0-1 背包
    //      dp[i][j] = dp[i - 1][j] + dp[i - 1][j - weight[i-1]];
    //                                     ^
    //                                     这里是 i-1; 用了物品 i 后，不能再使用物品 i 了

    public int Change(int amount, int[] coins)
    {
        var n = coins.Length;
        var dp = new int[n + 1][];
        for (int i = 0; i < n + 1; i++)
        {
            dp[i] = new int[amount + 1];
        }

        // base case
        for (int i = 0; i <= n; i++)
        {
            dp[i][0] = 1;
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= amount; j++)
            {
                if (j - coins[i - 1] < 0)
                {
                    dp[i][j] = dp[i - 1][j];
                }
                else
                {
                    dp[i][j] = dp[i - 1][j] + dp[i][j - coins[i - 1]];
                }
            }
        }

        return dp[n][amount];
    }
}
// @lc code=end
