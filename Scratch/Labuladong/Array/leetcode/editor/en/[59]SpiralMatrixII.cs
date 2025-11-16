namespace Scratch.Labuladong.Algorithms.SpiralMatrixII;

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
