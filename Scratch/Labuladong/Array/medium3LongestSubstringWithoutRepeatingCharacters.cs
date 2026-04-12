/*
 * @lc app=leetcode id=3 lang=csharp
 * @lcpr version=30402
 *
 * [3] Longest Substring Without Repeating Characters
 */

namespace Scratch.Labuladong.Algorithms.LongestSubstringWithoutRepeatingCharacters;

// @lc code=start
public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        var window = new Dictionary<char, int>();

        int left = 0, right = 0, res = 0;

        while (right < s.Length)
        {
            var c = s[right];
            right++;

            // 进行窗口内数据的一系列更新
            if (window.TryGetValue(c, out var cv)) window[c] = cv + 1;
            else window[c] = 1;

            // 判断左侧窗口是否要收缩
            while (window[c] > 1)
            {
                var d = s[left];
                left++;
                // 进行窗口内数据的一系列更新
                if (window.TryGetValue(d, out var wdv)) window[d] = wdv - 1;
            }

            // 更新答案
            res = Math.Max(res, right - left);
        }

        return res;
    }
}
// @lc code=end
