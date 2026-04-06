/*
 * @lc app=leetcode id=712 lang=csharp
 * @lcpr version=30402
 *
 * [712] Minimum ASCII Delete Sum For Two Strings
 */

namespace Scratch.Labuladong.Algorithms.MinimumASCIIDeleteSumForTwoStrings;

//Given two strings s1 and s2, return the lowest ASCII sum of deleted
//characters to make two strings equal.
//
//
// Example 1:
//
//
//Input: s1 = "sea", s2 = "eat"
//Output: 231
//Explanation: Deleting "s" from "sea" adds the ASCII value of "s" (115) to the
//sum.
//Deleting "t" from "eat" adds 116 to the sum.
//At the end, both strings are equal, and 115 + 116 = 231 is the minimum sum
//possible to achieve this.
//
//
// Example 2:
//
//
//Input: s1 = "delete", s2 = "leet"
//Output: 403
//Explanation: Deleting "dee" from "delete" to turn the string into "let",
//adds 100[d] + 101[e] + 101[e] to the sum.
//Deleting "e" from "leet" adds 101[e] to the sum.
//At the end, both strings are equal to "let", and the answer is 100+101+101+101
// = 403.
//If instead we turned both strings into "lee" or "eet", we would get answers
//of 433 or 417, which are higher.
//
//
//
// Constraints:
//
//
// 1 <= s1.length, s2.length <= 1000
// s1 and s2 consist of lowercase English letters.
//
//
// Related TopicsString | Dynamic Programming
//
// 👍 4536, 👎 128bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    private int[][] memo = null!;

    public int MinimumDeleteSum(string s1, string s2)
    {
        var m = s1.Length;
        var n = s2.Length;

        memo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
            Array.Fill(memo[i], -1);
        }

        return dp(s1, 0, s2, 0);
    }

    // 定义：将 s1[i..] 和 s2[j..] 删除成相同字符串，
    // 最小的 ASCII 码之和为 dp(s1, i, s2, j)。
    private int dp(string s1, int i, string s2, int j)
    {
        var res = 0;
        // base case
        if (i == s1.Length)
        {
            // 如果 s1 到头了，那么 s2 剩下的都得删除
            for (; j < s2.Length; j++)
            {
                res += s2[j];
            }

            return res;
        }

        if (j == s2.Length)
        {
            // 如果 s2 到头了，那么 s1 剩下的都得删除
            for (; i < s1.Length; i++)
            {
                res += s1[i];
            }

            return res;
        }

        if (memo[i][j] != -1) return memo[i][j];

        if (s1[i] == s2[j])
        {
            // s1[i] 和 s2[j] 都是在 lcs 中的，不用删除
            memo[i][j] = dp(s1, i + 1, s2, j + 1);
        }
        else
        {
            // s1[i] 和 s2[j] 至少有一个不在 lcs 中，删一个
            memo[i][j] = Math.Min(
                s1[i] + dp(s1, i + 1, s2, j),
                s2[j] + dp(s1, i, s2, j + 1)
            );
        }

        return memo[i][j];
    }

    public int MinimumDeleteSumDpTable(string s1, string s2)
    {
        var m = s1.Length;
        var n = s2.Length;
        var dp = new int[m + 1][];
        for (int i = 0; i < m + 1; i++)
        {
            dp[i] = new int[n + 1];
        }

        for (int i = 1; i < m + 1; i++)
        {
            dp[i][0] = dp[i - 1][0] + s1[i - 1];
        }

        for (int j = 1; j < n + 1; j++)
        {
            dp[0][j] = dp[0][j - 1] + s2[j - 1];
        }

        for (int i = 1; i < m + 1; i++)
        {
            for (int j = 1; j < n + 1; j++)
            {
                if (s1[i - 1] == s2[j - 1])
                {
                    dp[i][j] = dp[i - 1][j - 1];
                }
                else
                {
                    dp[i][j] = Math.Min(dp[i - 1][j] + s1[i - 1], dp[i][j - 1] + s2[j - 1]);
                }
            }
        }

        return dp[m][n];
    }
}
// @lc code=end
