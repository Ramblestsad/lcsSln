/*
 * @lc app=leetcode id=22 lang=csharp
 * @lcpr version=30403
 *
 * [22] Generate Parentheses
 */

namespace Scratch.Labuladong.Algorithms.GenParen;

// @lc code=start
using System.Text;

public class Solution
{
    private List<string> res = [];
    private StringBuilder track = new();

    public IList<string> GenerateParenthesis(int n)
    {
        if (n == 0) return [];

        // 可用的左括号和右括号数量初始化为n
        // left 记录还可以使用多少个左括号
        // right 记录还可以使用多少个右括号
        _backtrack(n, n);

        return res;
    }

    private void _backtrack(int left, int right)
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
        _backtrack(left - 1, right);
        // 撤销选择
        track.Remove(track.Length - 1, 1);

        // 尝试放一个右括号
        // 选择
        track.Append(')');
        _backtrack(left, right - 1);
        // 撤销选择
        track.Remove(track.Length - 1, 1);
    }
}
// @lc code=end

/*
// @lcpr case=start
// 3\n
// @lcpr case=end

// @lcpr case=start
// 1\n
// @lcpr case=end
 */
