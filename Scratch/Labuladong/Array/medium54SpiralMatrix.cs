namespace Scratch.Labuladong.Algorithms.SpiralMatrix;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public IList<int> SpiralOrder(int[][] matrix)
    {
        int m = matrix.Length, n = matrix[0].Length;
        var upperBoundary = 0;
        var lowerBoundary = m - 1;
        var leftBoundary = 0;
        var rightBoundary = n - 1;

        var res = new List<int>();

        while (res.Count < m * n)
        {
            if (upperBoundary <= lowerBoundary)
            {
                // 在顶部从左向右遍历
                for (var j = leftBoundary; j <= rightBoundary; j++)
                {
                    res.Add(matrix[upperBoundary][j]);
                }

                // 上边界下移
                upperBoundary++;
            }

            if (leftBoundary <= rightBoundary)
            {
                // 在右侧从上向下遍历
                for (var i = upperBoundary; i <= lowerBoundary; i++)
                {
                    res.Add(matrix[i][rightBoundary]);
                }

                // 右边界左移
                rightBoundary--;
            }

            if (upperBoundary <= lowerBoundary)
            {
                // 在底部从右向左遍历
                for (var j = rightBoundary; j >= leftBoundary; j--)
                {
                    res.Add(matrix[lowerBoundary][j]);
                }

                // 下边界上移
                lowerBoundary--;
            }

            if (leftBoundary <= rightBoundary)
            {
                // 在左侧从下向上遍历
                for (var i = lowerBoundary; i >= upperBoundary; i--)
                {
                    res.Add(matrix[i][leftBoundary]);
                }

                // 左边界右移
                leftBoundary++;
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
