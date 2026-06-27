namespace Scratch.Labuladong.Algorithms.FindAllAnagramsInAString;

// 438. Find All Anagrams in a String (Medium)
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

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public IList<int> FindAnagrams(string s, string p)
    {
        var need = new Dictionary<char, int>();
        var window = new Dictionary<char, int>();
        foreach (var c in p)
        {
            if (need.TryGetValue(c, out var v)) need[c] = v + 1;
            else need[c] = 1;
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
            if (need.TryGetValue(c, out var cv))
            {
                if (window.TryGetValue(c, out var wv)) window[c] = wv + 1;
                else window[c] = 1;

                if (cv == window[c]) valid++;
            }

            // 判断左窗口是否要收缩
            while (( right - left ) >= p.Length)
            {
                // 当窗口符合条件时，把起始索引加入 res
                if (valid == need.Count)
                {
                    res.Add(left);
                }

                var d = s[left];
                left++;

                // 进行 window 数据的一系列更新
                if (need.TryGetValue(d, out var ndv))
                {
                    window.TryGetValue(d, out var wdv);
                    if (ndv == wdv) valid--;
                    window[d] = wdv - 1;
                }
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
