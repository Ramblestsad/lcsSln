/*
 * @lc app=leetcode id=1143 lang=csharp
 * @lcpr version=30402
 *
 * [1143] Longest Common Subsequence
 */

namespace Scratch.Labuladong.Algorithms.LongestCommonSubsequence;

//Given two strings text1 and text2, return the length of their longest common
//subsequence. If there is no common subsequence, return 0.
//
// A subsequence of a string is a new string generated from the original string
//with some characters (can be none) deleted without changing the relative order
//of the remaining characters.
//
//
// For example, "ace" is a subsequence of "abcde".
//
//
// A common subsequence of two strings is a subsequence that is common to both
//strings.
//
//
// Example 1:
//
//
//Input: text1 = "abcde", text2 = "ace"
//Output: 3
//Explanation: The longest common subsequence is "ace" and its length is 3.
//
//
// Example 2:
//
//
//Input: text1 = "abc", text2 = "abc"
//Output: 3
//Explanation: The longest common subsequence is "abc" and its length is 3.
//
//
// Example 3:
//
//
//Input: text1 = "abc", text2 = "def"
//Output: 0
//Explanation: There is no such common subsequence, so the result is 0.
//
//
//
// Constraints:
//
//
// 1 <= text1.length, text2.length <= 1000
// text1 and text2 consist of only lowercase English characters.
//
//
// Related TopicsString | Dynamic Programming
//
// 👍 15005, 👎 246bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    private int[][] memo = null!;

    public int LongestCommonSubsequence(string text1, string text2)
    {
        var m = text1.Length;
        var n = text2.Length;
        memo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
            Array.Fill(memo[i], -1);
        }

        return dp(text1, 0, text2, 0);
    }

    // 定义：计算 s1[i..] 和 s2[j..] 的最长公共子序列长度
    private int dp(string s1, int i, string s2, int j)
    {
        // base case
        if (i == s1.Length || j == s2.Length)
            return 0;

        if (memo[i][j] != -1)
            return memo[i][j];

        // s1[i] == s2[j]，说明这个字符一定在 LCS 中。
        if (s1[i] == s2[j])
        {
            // 加上 s1[i+1..] 和 s2[j+1..] 中的 lcs 长度，就是答案
            memo[i][j] = 1 + dp(s1, i + 1, s2, j + 1);
        }
        else
        {
            // s1[i] != s2[j] 意味着，s1[i] 和 s2[j] 中至少有一个字符不在 lcs 中：
            // https://labuladong.online/images/algo/LCS/3.jpeg
            // 穷举三种情况的结果，取其中的最大结果
            // dp(i +1, j +1) 一定不会比 max(dp(i +1, j), dp(i, j +1)) 更大，属于冗余情况，不需要考虑。
            memo[i][j] = Math.Max(
                dp(s1, i + 1, s2, j),
                dp(s1, i, s2, j + 1)
            );
        }

        return memo[i][j];
    }
}
// @lc code=end
