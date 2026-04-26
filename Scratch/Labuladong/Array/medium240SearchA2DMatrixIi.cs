/*
 * @lc app=leetcode id=240 lang=csharp
 * @lcpr version=30403
 *
 * [240] Search a 2D Matrix II
 */

namespace Scratch.Labuladong.Algorithms.Search2DMatrixII;

// @lc code=start
public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        // 初始化位置在右上角
        var i = 0;
        var j = n - 1;

        while (i < m && j >= 0)
        {
            if (matrix[i][j] < target)
            {
                // 需要大一点，向下移动
                i++;
            }
            else if (matrix[i][j] > target)
            {
                // 需要小一点，向左移动
                j--;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
// @lc code=end

/*
// @lcpr case=start
// [[1,4,7,11,15],[2,5,8,12,19],[3,6,9,16,22],[10,13,14,17,24],[18,21,23,26,30]]\n5\n
// @lcpr case=end

// @lcpr case=start
// [[1,4,7,11,15],[2,5,8,12,19],[3,6,9,16,22],[10,13,14,17,24],[18,21,23,26,30]]\n20\n
// @lcpr case=end
 */
