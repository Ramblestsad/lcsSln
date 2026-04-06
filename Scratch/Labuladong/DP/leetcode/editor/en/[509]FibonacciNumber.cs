/*
 * @lc app=leetcode id=509 lang=csharp
 * @lcpr version=30402
 *
 * [509] Fibonacci Number
 */

namespace Scratch.Labuladong.Algorithms.FibonacciNumber;

//The Fibonacci numbers, commonly denoted F(n) form a sequence, called the
//Fibonacci sequence, such that each number is the sum of the two preceding ones,
//starting from 0 and 1. That is,
//
//
//F(0) = 0, F(1) = 1
//F(n) = F(n - 1) + F(n - 2), for n > 1.
//
//
// Given n, calculate F(n).
//
//
// Example 1:
//
//
//Input: n = 2
//Output: 1
//Explanation: F(2) = F(1) + F(0) = 1 + 0 = 1.
//
//
// Example 2:
//
//
//Input: n = 3
//Output: 2
//Explanation: F(3) = F(2) + F(1) = 1 + 1 = 2.
//
//
// Example 3:
//
//
//Input: n = 4
//Output: 3
//Explanation: F(4) = F(3) + F(2) = 2 + 1 = 3.
//
//
//
// Constraints:
//
//
// 0 <= n <= 30
//
//
// Related TopicsMath | Dynamic Programming | Recursion | Memoization
//
// 👍 9303, 👎 410bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int FibRecursive(int n)
    {
        // 备忘录全初始化为 -1
        // 因为斐波那契数肯定是非负整数，所以初始化为特殊值 -1 表示未计算
        var memo = new int[n + 1];
        Array.Fill(memo, -1);

        return dp(memo, n);
    }

    // top-down memo-recursive
    private int dp(int[] memo, int n)
    {
        // base case
        if (n == 0 || n == 1) return n;

        if (memo[n] != -1) return memo[n];

        // 在返回结果之前，存入备忘录
        memo[n] = dp(memo, n - 1) + dp(memo, n - 2);
        return memo[n];
    }

    // down-top iterate
    public int Fib(int n)
    {
        if (n is 0 or 1) return n;

        // base case
        int dp_i_1 = 1, dp_i_2 = 0;
        // 状态转移
        for (int i = 2; i <= n; i++)
        {
            var dp_i = dp_i_1 + dp_i_2;
            dp_i_2 = dp_i_1;
            dp_i_1 = dp_i;
        }

        return dp_i_1;
    }
}
// @lc code=end
