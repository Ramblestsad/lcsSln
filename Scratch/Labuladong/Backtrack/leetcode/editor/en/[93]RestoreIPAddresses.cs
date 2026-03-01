namespace Scratch.Labuladong.Algorithms.RestoreIPAddresses;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    private List<string> res = new();
    private List<string> track = new();

    public IList<string> RestoreIpAddresses(string s)
    {
        _backtrack(s, 0);
        return res;
    }

    private void _backtrack(string s, int start)
    {
        if (start == s.Length && track.Count == 4)
        {
            // base case，走到叶子节点
            // 即整个 s 被成功分割为合法的四部分，记下答案
            res.Add(string.Join(".", track));
        }

        for (int i = start; i < s.Length; i++)
        {
            // s[start..i] 不是合法的 ip 数字，不能分割
            if (!_isValid(s, start, i)) continue;
            // 已经分解成 4 部分了，不能再分解了
            if (track.Count >= 4) break;

            // s[start..i] 是一个合法的 ip 数字，可以进行分割
            // 做选择，把 s[start..i] 放入路径列表中
            track.Add(s[start..( i + 1 )]);
            _backtrack(s, i + 1);
            // 撤销
            track.RemoveAt(track.Count - 1);
        }
    }

    // 判断 s[start..end] 是否是一个合法的 ip 段
    private bool _isValid(string s, int start, int end)
    {
        var length = end - start + 1;

        if (length == 0 || length > 3) return false;
        // 如果只有一位数字，肯定是合法的
        if (length == 1) return true;
        // 多于一位数字，但开头是 0，肯定不合法
        if (s[start] == '0') return false;
        // 排除了开头是 0 的情况，那么如果是两位数，怎么着都是合法的
        if (length <= 2) return true;
        // 现在输入的一定是三位数
        if (int.Parse(s[start..( start + length )]) > 255) // 不能大于255
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
