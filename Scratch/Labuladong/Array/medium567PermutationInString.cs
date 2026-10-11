/*
 * @lc app=leetcode id=567 lang=csharp
 * @lcpr version=3.4.4
 */

namespace Scratch.Labuladong.Algorithms.PermutationInString;

// 567. Permutation in String (Medium)
//
// Given two strings s1 and s2, return true if s2 contains a permutation of s1, or false otherwise.
//
// In other words, return true if one of s1's permutations is the substring of s2.
//
// Example 1:
//
// Input: s1 = "ab", s2 = "eidbaooo"
// Output: true
// Explanation: s2 contains one permutation of s1 ("ba").
//
// Example 2:
//
// Input: s1 = "ab", s2 = "eidboaoo"
// Output: false
//
// Constraints:
//
// - 1 <= s1.length, s2.length <= 10^4
//
// - s1 and s2 consist of lowercase English letters.
//
// Related Topics: Hash Table, Two Pointers, String, Sliding Window

// @lc code=start
public class Solution
{
    // 判断 s2 是否包含 s1 的排列
    public bool CheckInclusion(string s1, string s2)
    {
        var need = new Dictionary<char, int>();
        var window = new Dictionary<char, int>();
        foreach (var c in s1)
        {
            need[c] = need.GetValueOrDefault(c) + 1;
        }

        int left = 0, right = 0;
        // valid 变量表示窗口中满足 need 条件的字符个数
        // 如果 valid 和 need.size 的大小相同，则说明窗口已满足条件，已经完全覆盖了串 T
        int valid = 0;

        while (right < s2.Length)
        {
            var c = s2[right];
            right++;
            // 进行 window 数据的一系列更新
            if (need.TryGetValue(c, out var cNeed))
            {
                window[c] = window.GetValueOrDefault(c) + 1;
                if (cNeed == window[c]) valid++;
            }

            // 判断左侧窗口是否要收缩
            while (( right - left ) >= s1.Length)
            {
                // 题目要s2是否包含s1的排列，所以窗口（right-left）大于等于s1的时候需要收缩。
                // 判断是否找到了合法的子串
                if (valid == need.Count) return true;
                var d = s2[left];
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

        return false;
    }
}
// @lc code=end

/*
// @lcpr case=start
// "ab"\n"eidbaooo"\n
// @lcpr case=end

// @lcpr case=start
// "ab"\n"eidboaoo"\n
// @lcpr case=end
 */
