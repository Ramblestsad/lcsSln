namespace Scratch.Labuladong.Algorithms.Search2DMatrix;

// 74. Search a 2D Matrix (Medium)
//
// You are given an m x n integer matrix matrix with the following two properties:
//
// - Each row is sorted in non-decreasing order.
//
// - The first integer of each row is greater than the last integer of the previous row.
//
// Given an integer target, return true if target is in matrix or false otherwise.
//
// You must write a solution in O(log(m * n)) time complexity.
//
// Example 1:
//
// Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 3
// Output: true
//
// Example 2:
//
// Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 13
// Output: false
//
// Constraints:
//
// - m == matrix.length
//
// - n == matrix[i].length
//
// - 1 <= m, n <= 100
//
// - -10^4 <= matrix[i][j], target <= 10^4
//
// Related Topics: Array, Binary Search, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
