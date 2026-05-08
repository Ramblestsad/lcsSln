/*
 * @lc app=leetcode id=49 lang=csharp
 * @lcpr version=30403
 *
 * [49] Group Anagrams
 */

namespace Scratch.Labuladong.Algorithms.GroupAnagrams;

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
        var cnt = new char[26];
        foreach (var c in s.ToCharArray())
        {
            var delta = c - 'a';
            cnt[delta]++;
        }

        return new string(cnt);
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
