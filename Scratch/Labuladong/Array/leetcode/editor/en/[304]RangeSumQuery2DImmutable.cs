namespace Scratch.Labuladong.Algorithms.RangeSumQuery2DImmutable;

//leetcode submit region begin(Prohibit modification and deletion)
public class NumMatrix
{
    private int[,] PreSum { get; set; } = null!;

    public NumMatrix(int[][] matrix)
    {
        var m = matrix.Length;
        var n = matrix[0].Length;

        if (m == 0 || n == 0) return;

        PreSum = new int[m + 1, n + 1];

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                // 计算每个矩阵 [0, 0, i, j] 的元素和
                PreSum[i, j] = PreSum[i - 1, j] + PreSum[i, j - 1] + matrix[i - 1][j - 1] - PreSum[i - 1, j - 1];
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        // 目标矩阵之和由四个相邻矩阵运算获得
        return PreSum[row2 + 1, col2 + 1] - PreSum[row1, col2 + 1] - PreSum[row2 + 1, col1] + PreSum[row1, col1];
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */
//leetcode submit region end(Prohibit modification and deletion)
