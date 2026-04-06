/*
 * @lc app=leetcode id=72 lang=csharp
 * @lcpr version=30402
 *
 * [72] Edit Distance
 */

namespace Scratch.Labuladong.Algorithms.EditDistance;

//Given two strings word1 and word2, return the minimum number of operations
//required to convert word1 to word2.
//
// You have the following three operations permitted on a word:
//
//
// Insert a character
// Delete a character
// Replace a character
//
//
//
// Example 1:
//
//
//Input: word1 = "horse", word2 = "ros"
//Output: 3
//Explanation:
//horse -> rorse (replace 'h' with 'r')
//rorse -> rose (remove 'r')
//rose -> ros (remove 'e')
//
//
// Example 2:
//
//
//Input: word1 = "intention", word2 = "execution"
//Output: 5
//Explanation:
//intention -> inention (remove 't')
//inention -> enention (replace 'i' with 'e')
//enention -> exention (replace 'n' with 'x')
//exention -> exection (replace 'n' with 'c')
//exection -> execution (insert 'u')
//
//
//
// Constraints:
//
//
// 0 <= word1.length, word2.length <= 500
// word1 and word2 consist of lowercase English letters.
//
//
// Related TopicsString | Dynamic Programming
//
// 👍 16357, 👎 324bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int MinDistanceDPRecur(string word1, string word2)
    {
        var m = word1.Length;
        var n = word2.Length;

        // 备忘录初始化为特殊值，代表还未计算
        memo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
            Array.Fill(memo[i], -1);
        }

        // i，j 初始化指向最后一个索引
        return dp(word1, m - 1, word2, n - 1);
    }

    private int[][] memo = null!;

    // base case 是 i 走完 s1 或 j 走完 s2，可以直接返回另一个字符串剩下的长度。
    // 对于每对儿字符 s1[i] 和 s2[j]，可以有四种操作：
    // if s1[i] == s2[j]:
    //      啥都别做（skip）
    //      i, j 同时向前移动
    // else:
    //      三选一：
    //      插入（insert）
    //      删除（delete）
    //      替换（replace）
    private int dp(string s1, int i, string s2, int j)
    {
        // base case
        // s1[0..i] 已经是空串了，目标还剩下 s2[0..j]。
        // 要把空串变成长度为 j +1 的字符串，只能做 j +1 次插入
        if (i == -1) return j + 1;
        // 目标串已经空了，但 s1[0..i]还剩下字符。
        // 要把长度为 i +1 的字符串变成空串，只能做 i +1 次删除。
        if (j == -1) return i + 1;

        // 查备忘录，避免重叠子问题
        if (memo[i][j] != -1)
            return memo[i][j];

        if (s1[i] == s2[j])
        {
            memo[i][j] = dp(s1, i - 1, s2, j - 1);
        }
        else
        {
            memo[i][j] = _min(
                // 插入
                // 直接在 s1[i] 后面插入一个和 s2[j] 一样的字符
                // 那么 s2[j] 就被匹配了，前移 j，继续跟 i 对比
                // 别忘了操作数加一
                dp(s1, i, s2, j - 1) + 1,
                // 删除
                // 直接把 s[i] 这个字符删掉
                // 那么 s1[0..i-1] 和 s2[0..j] 的最小编辑距离就等于
                // 前移 i，继续跟 j 对比
                // 操作数加一
                dp(s1, i - 1, s2, j) + 1,
                // 替换
                // 我直接把 s1[i] 替换成 s2[j]，这样它俩就匹配了
                // 同时前移 i，j 继续对比
                // 操作数加一
                dp(s1, i - 1, s2, j - 1) + 1);
        }

        return memo[i][j];
    }

    private int _min(int a, int b, int c)
    {
        return Math.Min(a, Math.Min(b, c));
    }

    // DP Table
    public int MinDistance(string word1, string word2)
    {
        var m = word1.Length;
        var n = word2.Length;

        var dp = new int[m + 1][];
        for (int i = 0; i < m + 1; i++)
        {
            dp[i] = new int[n + 1];
        }

        // base case
        for (int i = 1; i <= m; i++)
            dp[i][0] = i;
        for (int j = 1; j <= n; j++)
            dp[0][j] = j;

        // 自底向上求解
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                    dp[i][j] = dp[i - 1][j - 1];
                else
                    dp[i][j] = _min(
                        // 删除
                        dp[i - 1][j] + 1,
                        // 插入
                        dp[i][j - 1] + 1,
                        // 替换
                        dp[i - 1][j - 1] + 1
                    );
            }
        }

        // 储存着整个 s1 和 s2 的最小编辑距离
        return dp[m][n];
    }
}
// @lc code=end
