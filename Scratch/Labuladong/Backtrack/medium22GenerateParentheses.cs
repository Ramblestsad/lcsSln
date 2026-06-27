using System.Text;

namespace Scratch.Labuladong.Algorithms.GenParen;

//leetcode submit region begin(Prohibit modification and deletion)

public class Solution
{
    public IList<string> GenerateParenthesis(int n)
    {
        if (n == 0) return [];

        List<string> res = [];
        StringBuilder track = new();

        // 可用的左括号和右括号数量初始化为n
        // left 记录还可以使用多少个左括号
        // right 记录还可以使用多少个右括号
        _backtrack(n, n, res, track);

        return res;
    }

    private void _backtrack(int left, int right, List<string> res, StringBuilder track)
    {
        // 如果左括号剩下的多，则不合法
        if (left > right) return;
        // 数量小于0，则不合法
        if (left < 0 || right < 0) return;
        // 当所有括号用完时，得到合法组合
        if (left == 0 && right == 0)
        {
            res.Add(track.ToString());
            return;
        }

        // 尝试翻一个左括号
        // 选择
        track.Append('(');
        _backtrack(left - 1, right, res, track);
        // 撤销选择
        track.Remove(track.Length - 1, 1);

        // 尝试放一个右括号
        // 选择
        track.Append(')');
        _backtrack(left, right - 1, res, track);
        // 撤销选择
        track.Remove(track.Length - 1, 1);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
