namespace Scratch.Labuladong.Algorithms.MinimumWindowSubstring;

// 76. Minimum Window Substring (Hard)
//
// Given two strings s and t of lengths m and n respectively, return the minimum window substring
// of s such that every character in t (including duplicates) is included in the window. If there
// is no such substring, return the empty string "".
//
// The testcases will be generated such that the answer is unique.
//
// Example 1:
//
// Input: s = "ADOBECODEBANC", t = "ABC"
// Output: "BANC"
// Explanation: The minimum window substring "BANC" includes 'A', 'B', and 'C' from string t.
//
// Example 2:
//
// Input: s = "a", t = "a"
// Output: "a"
// Explanation: The entire string s is the minimum window.
//
// Example 3:
//
// Input: s = "a", t = "aa"
// Output: ""
// Explanation: Both 'a's from t must be included in the window.
// Since the largest window of s only has one 'a', return empty string.
//
// Constraints:
//
// - m == s.length
//
// - n == t.length
//
// - 1 <= m, n <= 10^5
//
// - s and t consist of uppercase and lowercase English letters.
//
// Follow up: Could you find an algorithm that runs in O(m + n) time?
//
// Related Topics: Hash Table, String, Sliding Window

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public string MinWindow(string s, string t)
    {
        var window = new Dictionary<char, int>();
        var need = new Dictionary<char, int>();

        foreach (var c in t)
        {
            if (need.TryGetValue(c, out var v)) need[c] = v + 1;
            else need[c] = 1;
        }

        int left = 0, right = 0;
        // valid 变量表示窗口中满足 need 条件的字符个数
        // 如果 valid 和 need.size 的大小相同，则说明窗口已满足条件，已经完全覆盖了串 T
        int valid = 0;
        // 记录最小覆盖子串的起始索引及长度
        int start = 0, len = int.MaxValue;

        while (right < s.Length)
        {
            // c 是将移入窗口的字符
            var c = s[right];
            // 右移窗口
            right++;
            // 进行窗口内数据的一系列更新
            if (need.TryGetValue(c, out var cv))
            {
                if (window.TryGetValue(c, out var wv)) window[c] = wv + 1;
                else window[c] = 1;
                if (cv == window[c]) valid++;
            }

            // 一直循环外部循环知道window满足要求：包含t所有字符，即valid==need.Count
            while (valid == need.Count)
            {
                // 更新最小覆盖子串
                // right - left：计算当前合法窗口的长度。
                // len：记录之前找到的最小窗口长度（初始为 int.MaxValue）
                if (right - left < len)
                {
                    start = left;
                    len = right - left;
                }

                // d 是将移出窗口的字符
                var d = s[left];
                // 缩小窗口
                left++;
                // 进行窗口内数据的一系列更新
                if (need.TryGetValue(d, out var nd))
                {
                    window.TryGetValue(d, out var wvd);
                    if (nd == wvd) valid--;
                    window[d] = wvd - 1;
                }
            }
        }

        return len == int.MaxValue ? "" : s.Substring(start, len);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
