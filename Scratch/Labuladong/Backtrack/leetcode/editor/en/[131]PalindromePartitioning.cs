namespace Scratch.Labuladong.Algorithms.PalindromePartitioning;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    private List<IList<string>> res = new();
    private List<string> track = new();

    public IList<IList<string>> Partition(string s)
    {
        _backtrack(s, 0);
        return res;
    }

    private void _backtrack(string s, int start)
    {
        if (start == s.Length)
        {
            // base case: 走到叶子节点
            // 即整个 s 被成功分割为若干个回文子串，记下答案
            res.Add(new List<string>(track));
        }

        for (int i = start; i < s.Length; i++)
        {
            // s[start..i] 不是回文串，不能分割
            if (!_isPalindrome(s, start, i)) continue;

            // s[start..i] 是一个回文串，可以进行分割
            // 做选择，把 s[start..i] 放入路径列表中
            track.Add(s[start..( i + 1 )]);
            _backtrack(s, i + 1);
            track.RemoveAt(track.Count - 1);
        }
    }

    // 用双指针技巧判断 s[lo..hi] 是否是一个回文串
    private bool _isPalindrome(string s, int lo, int hi)
    {
        while (lo < hi)
        {
            if (s[lo] != s[hi]) return false;
            lo++;
            hi--;
        }

        return true;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
