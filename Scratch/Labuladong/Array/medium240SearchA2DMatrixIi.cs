namespace Scratch.Labuladong.Algorithms.Search2DMatrixII;

// 240. Search a 2D Matrix II (Medium)
//
// Write an efficient algorithm that searches for a value target in an m x n integer matrix matrix.
// This matrix has the following properties:
//
// - Integers in each row are sorted in ascending from left to right.
//
// - Integers in each column are sorted in ascending from top to bottom.
//
// Example 1:
//
// Input: matrix = [[1,4,7,11,15],[2,5,8,12,19],[3,6,9,16,22],[10,13,14,17,24],[18,21,23,26,30]],
// target = 5
// Output: true
//
// Example 2:
//
// Input: matrix = [[1,4,7,11,15],[2,5,8,12,19],[3,6,9,16,22],[10,13,14,17,24],[18,21,23,26,30]],
// target = 20
// Output: false
//
// Constraints:
//
// - m == matrix.length
//
// - n == matrix[i].length
//
// - 1 <= n, m <= 300
//
// - -10^9 <= matrix[i][j] <= 10^9
//
// - All the integers in each row are sorted in ascending order.
//
// - All the integers in each column are sorted in ascending order.
//
// - -10^9 <= target <= 10^9
//
// Related Topics: Array, Binary Search, Divide and Conquer, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
