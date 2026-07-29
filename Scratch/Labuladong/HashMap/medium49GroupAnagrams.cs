/*
 * @lc app=leetcode id=49 lang=csharp
 * @lcpr version=3.4.4
 */

namespace Scratch.Labuladong.Algorithms.GroupAnagrams;

// 49. Group Anagrams (Medium)
//
// Given an array of strings strs, group the anagrams together. You can return the answer in any
// order.
//
// Example 1:
//
// Input: strs = ["eat","tea","tan","ate","nat","bat"]
//
// Output: [["bat"],["nat","tan"],["ate","eat","tea"]]
//
// Explanation:
//
// - There is no string in strs that can be rearranged to form "bat".
//
// - The strings "nat" and "tan" are anagrams as they can be rearranged to form each other.
//
// - The strings "ate", "eat", and "tea" are anagrams as they can be rearranged to form each other.
//
// Example 2:
//
// Input: strs = [""]
//
// Output: [[""]]
//
// Example 3:
//
// Input: strs = ["a"]
//
// Output: [["a"]]
//
// Constraints:
//
// - 1 <= strs.length <= 10^4
//
// - 0 <= strs[i].length <= 100
//
// - strs[i] consists of lowercase English letters.
//
// Related Topics: Array, Hash Table, String, Sorting

// @lc code=start
public class Solution
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        var codeToGroup = new Dictionary<string, List<string>>();

        foreach (var str in strs)
        {
            var code = _encode(str);
            if (!codeToGroup.TryGetValue(code, out var list))
            {
                list = [];
                codeToGroup.Add(code, list);
            }

            list.Add(str);
        }

        var res = new List<IList<string>>();
        foreach (var group in codeToGroup.Values)
        {
            res.Add(group);
        }

        return res;
    }

    string _encode(string s)
    {
        var cnt = new int[26];
        foreach (var c in s.ToCharArray())
        {
            cnt[c - 'a']++;
        }

        return string.Join(",", cnt);
    }
}
// @lc code=end

/*
// @lcpr case=start
// ["eat","tea","tan","ate","nat","bat"]\n
// @lcpr case=end

// @lcpr case=start
// [""]\n
// @lcpr case=end

// @lcpr case=start
// ["a"]\n
// @lcpr case=end
 */
