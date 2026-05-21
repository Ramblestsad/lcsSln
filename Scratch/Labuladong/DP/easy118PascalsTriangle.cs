/*
 * @lc app=leetcode id=118 lang=csharp
 * @lcpr version=30403
 *
 * [118] Pascal's Triangle
 */

namespace Scratch.Labuladong.Algorithms.PascalTri;

// @lc code=start
public class Solution
{
    public IList<IList<int>> Generate(int numRows)
    {
        var res = new List<IList<int>>();

        if (numRows < 1) return res;
        res.Add([1]);

        // 开始一层一层生成，装入 res
        for (var i = 2; i <= numRows; i++)
        {
            var prevRow = res[^1];
            res.Add(generateNextRow(prevRow));
        }

        return res;
    }

    // 输入上一层的元素，生成并返回下一层的元素
    List<int> generateNextRow(IList<int> prevRow)
    {
        var curRow = new List<int>();
        curRow.Add(1);
        for (var i = 0; i < prevRow.Count - 1; i++)
        {
            curRow.Add(prevRow[i] + prevRow[i + 1]);
        }

        curRow.Add(1);
        return curRow;
    }
}
// @lc code=end

/*
// @lcpr case=start
// 5\n
// @lcpr case=end

// @lcpr case=start
// 1\n
// @lcpr case=end
 */
