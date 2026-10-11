/*
 * @lc app=leetcode id=438 lang=csharp
 * @lcpr version=3.4.4
 */

namespace Scratch.Labuladong.Algorithms.FindAllAnagramsInAString;

// 438. Find All Anagrams(异位词) in a String (Medium)
//
// Given two strings s and p, return an array of all the start indices of p's anagrams in s. You
// may return the answer in any order.
//
// Example 1:
//
// Input: s = "cbaebabacd", p = "abc"
// Output: [0,6]
// Explanation:
// The substring with start index = 0 is "cba", which is an anagram of "abc".
// The substring with start index = 6 is "bac", which is an anagram of "abc".
//
// Example 2:
//
// Input: s = "abab", p = "ab"
// Output: [0,1,2]
// Explanation:
// The substring with start index = 0 is "ab", which is an anagram of "ab".
// The substring with start index = 1 is "ba", which is an anagram of "ab".
// The substring with start index = 2 is "ab", which is an anagram of "ab".
//
// Constraints:
//
// - 1 <= s.length, p.length <= 3 * 10^4
//
// - s and p consist of lowercase English letters.
//
// Related Topics: Hash Table, String, Sliding Window

// @lc code=start
public class Solution
{
    public IList<int> FindAnagrams(string s, string p)
    {
        var need = new Dictionary<char, int>();
        var window = new Dictionary<char, int>();
        foreach (var c in p)
        {
            need[c] = need.GetValueOrDefault(c) + 1;
        }

        int left = 0, right = 0, valid = 0;
        // valid 变量表示窗口中满足 need 条件的字符个数
        // 如果 valid 和 need.size 的大小相同，则说明窗口已满足条件，已经完全覆盖了串 T
        List<int> res = [];

        while (right < s.Length)
        {
            var c = s[right];
            right++;

            // 进行 window 数据的一系列更新
            if (need.TryGetValue(c, out var cNeed))
            {
                window[c] = window.GetValueOrDefault(c) + 1;
                if (cNeed == window[c]) valid++;
            }

            // 判断左窗口是否要收缩
            while (( right - left ) >= p.Length)
            {
                // 因为是anagram，窗口大小需要等于p的长度，所以窗口（right-left）大于等于p的时候需要收缩窗口。
                // 当窗口符合条件时，把起始索引加入 res
                if (valid == need.Count)
                {
                    res.Add(left);
                }

                var d = s[left];
                left++;

                // 进行 window 数据的一系列更新
                if (need.TryGetValue(d, out var dNeed))
                {
                    window.TryGetValue(d, out var dWindow);
                    if (dNeed == dWindow) valid--;
                    window[d] = dWindow - 1;
                }
            }
        }

        return res;
    }
}
// @lc code=end

/*
// @lcpr case=start
// "cbaebabacd"\n"abc"\n
// @lcpr case=end

// @lcpr case=start
// "abab"\n"ab"\n
// @lcpr case=end
 */
