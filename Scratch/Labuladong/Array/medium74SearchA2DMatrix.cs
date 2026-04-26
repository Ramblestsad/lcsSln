/*
 * @lc app=leetcode id=74 lang=csharp
 * @lcpr version=30403
 *
 * [74] Search a 2D Matrix
 */

namespace Scratch.Labuladong.Algorithms.Search2DMatrix;

// @lc code=start
public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;
        // 把二维数组映射到一维
        var left = 0;
        var right = m * n;

        while (left < right)
        {
            var mid = left + ( right - left ) / 2;
            var midVal = _get(matrix, mid);
            if (midVal > target)
            {
                right = mid;
            }
            else if (midVal < target)
            {
                left = mid + 1;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    private int _get(int[][] matrix, int index)
    {
        var n = matrix[0].Length;

        var i = index / n;
        var j = index % n;

        return matrix[i][j];
    }
}
// @lc code=end

/*
// @lcpr case=start
// [[1,3,5,7],[10,11,16,20],[23,30,34,60]]\n3\n
// @lcpr case=end

// @lcpr case=start
// [[1,3,5,7],[10,11,16,20],[23,30,34,60]]\n13\n
// @lcpr case=end
 */
