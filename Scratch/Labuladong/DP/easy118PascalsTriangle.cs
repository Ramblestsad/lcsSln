namespace Scratch.Labuladong.Algorithms.PascalTri;

// 118. Pascal's Triangle (Easy)
//
// Given an integer numRows, return the first numRows of Pascal's triangle.
//
// In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
//
// Example 1:
//
// Input: numRows = 5
// Output: [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
// Example 2:
//
// Input: numRows = 1
// Output: [[1]]
//
// Constraints:
//
// - 1 <= numRows <= 30
//
// Related Topics: Array, Dynamic Programming

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
