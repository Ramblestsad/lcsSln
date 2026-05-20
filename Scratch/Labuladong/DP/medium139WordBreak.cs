/*
 * @lc app=leetcode id=139 lang=csharp
 * @lcpr version=30403
 *
 * [139] Word Break
 */

namespace Scratch.Labuladong.Algorithms.WordBreak;

// @lc code=start
public class Solution
{
    private int[] memo = [];

    public bool WordBreak(string s, IList<string> wordDict)
    {
        // 备忘录，-1 代表未计算，0 代表 false，1 代表 true
        memo = new int[s.Length];
        Array.Fill(memo, -1);
        // 根据函数定义，判断 s[0..] 是否能够被拼出
        return dp(s, 0, wordDict);
    }

    // 定义：返回 s[i..] 是否能够被 wordDict 拼出
    bool dp(string s, int i, IList<string> wordDict)
    {
        // base case，整个 s 都被拼出来了
        if (i == s.Length)
        {
            return true;
        }

        // 防止冗余计算
        if (memo[i] != -1)
        {
            return memo[i] == 1;
        }

        // 遍历所有单词，尝试匹配 s[i..] 的前缀
        foreach (var word in wordDict)
        {
            var len = word.Length;
            if (i + len > s.Length)
            {
                continue;
            }

            var subStr = s[i..( i + len )];
            if (subStr != word)
            {
                continue;
            }

            // s[i..] 的前缀被匹配，去尝试匹配 s[i+len..]
            if (dp(s, i + len, wordDict))
            {
                // s[i..] 可以被拼出，将结果记入备忘录
                memo[i] = 1;
                return true;
            }
        }

        // s[i..] 不能被拼出，结果记入备忘录
        memo[i] = 0;
        return false;
    }
}
// @lc code=end

/*
// @lcpr case=start
// "leetcode"\n["leet","code"]\n
// @lcpr case=end

// @lcpr case=start
// "applepenapple"\n["apple","pen"]\n
// @lcpr case=end

// @lcpr case=start
// "catsandog"\n["cats","dog","sand","and","cat"]\n
// @lcpr case=end
 */
