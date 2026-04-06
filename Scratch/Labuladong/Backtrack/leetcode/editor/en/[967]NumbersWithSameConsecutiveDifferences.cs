/*
 * @lc app=leetcode id=967 lang=csharp
 * @lcpr version=30402
 *
 * [967] Numbers With Same Consecutive Differences
 */

namespace Scratch.Labuladong.Algorithms.NumbersWithSameConsecutiveDifferences;

// @lc code=start
public class Solution
{
    public List<int> res = new List<int>();

    // 记录当前路径组成的数字的值
    int track = 0;

    // 记录当前数字的位数
    int digit = 0;

    public int[] NumsSameConsecDiff(int n, int k)
    {
        _backtrack(n, k);
        return res.ToArray();
    }

    void _backtrack(int n, int k)
    {
        // base case，到达叶子节点
        if (digit == n)
        {
            // 找到一个合法的 n 位数
            res.Add(track);
            return;
        }

        for (int i = 0; i <= 9; i++)
        {
            // 剪枝逻辑 1，第一个数字不能是 0
            if (digit == 0 && i == 0) continue;
            // 本题的剪枝逻辑 2，相邻两个数字的差的绝对值必须等于 k
            // digit > 0 意味着从第二位开始；
            // track % 10 是上一位数字；i 是当前数字
            if (digit > 0 && Math.Abs(i - track % 10) != k) continue;

            // 做选择，在 track 尾部追加数字 i
            digit++;
            track = 10 * track + i;
            _backtrack(n, k);
            // 取消选择
            track = track / 10;
            digit--;
        }
    }
}
// @lc code=end
