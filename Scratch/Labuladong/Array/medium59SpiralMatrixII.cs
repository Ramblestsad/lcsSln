namespace Scratch.Labuladong.Algorithms.SpiralMatrixII;

// 59. Spiral Matrix II (Medium)
//
// Given a positive integer n, generate an n x n matrix filled with elements from 1 to n^2 in
// spiral order.
//
// Example 1:
//
// Input: n = 3
// Output: [[1,2,3],[8,9,4],[7,6,5]]
//
// Example 2:
//
// Input: n = 1
// Output: [[1]]
//
// Constraints:
//
// - 1 <= n <= 20
//
// Related Topics: Array, Matrix, Simulation

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[][] GenerateMatrix(int n)
    {
        var matrix = new int[n][];
        for (var i = 0; i < n; i++)
        {
            matrix[i] = new int[n];
        }

        var upperBoundary = 0;
        var lowerBoundary = n - 1;
        var leftBoundary = 0;
        var rightBoundary = n - 1;

        // 需要填入矩阵的数字
        var num = 1;

        while (num <= n * n)
        {
            if (upperBoundary <= lowerBoundary)
            {
                // 在顶部从左向右遍历
                for (var j = leftBoundary; j <= rightBoundary; j++)
                {
                    matrix[upperBoundary][j] = num++;
                }

                // 上边界下移
                upperBoundary++;
            }

            if (leftBoundary <= rightBoundary)
            {
                // 在右侧从上向下遍历
                for (var i = upperBoundary; i <= lowerBoundary; i++)
                {
                    matrix[i][rightBoundary] = num++;
                }

                // 右边界左移
                rightBoundary--;
            }

            if (upperBoundary <= lowerBoundary)
            {
                // 在底部从右向左遍历
                for (var j = rightBoundary; j >= leftBoundary; j--)
                {
                    matrix[lowerBoundary][j] = num++;
                }

                // 下边界上移
                lowerBoundary--;
            }

            if (leftBoundary <= rightBoundary)
            {
                // 在左侧从下向上遍历
                for (var i = lowerBoundary; i >= upperBoundary; i--)
                {
                    matrix[i][leftBoundary] = num++;
                }

                // 左边界右移
                leftBoundary++;
            }
        }

        return matrix;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
