namespace Scratch.Labuladong.Algorithms.LongestSubstringWithoutRepeatingCharacters;

// 3. Longest Substring Without Repeating Characters (Medium)
//
// Given a string s, find the length of the longest substring without duplicate characters.
//
// Example 1:
//
// Input: s = "abcabcbb"
// Output: 3
// Explanation: The answer is "abc", with the length of 3. Note that "bca" and "cab" are also
// correct answers.
//
// Example 2:
//
// Input: s = "bbbbb"
// Output: 1
// Explanation: The answer is "b", with the length of 1.
//
// Example 3:
//
// Input: s = "pwwkew"
// Output: 3
// Explanation: The answer is "wke", with the length of 3.
// Notice that the answer must be a substring, "pwke" is a subsequence and not a substring.
//
// Constraints:
//
// - 0 <= s.length <= 5 * 10^4
//
// - s consists of English letters, digits, symbols and spaces.
//
// Related Topics: Hash Table, String, Sliding Window

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
