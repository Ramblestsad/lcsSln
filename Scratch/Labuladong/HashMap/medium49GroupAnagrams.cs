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

//leetcode submit region begin(Prohibit modification and deletion)
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
        var cnt = new char[26];
        foreach (var c in s.ToCharArray())
        {
            var delta = c - 'a';
            cnt[delta]++;
        }

        return new string(cnt);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
