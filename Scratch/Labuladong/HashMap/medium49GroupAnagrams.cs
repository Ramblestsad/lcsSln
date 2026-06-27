namespace Scratch.Labuladong.Algorithms.GroupAnagrams;

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
