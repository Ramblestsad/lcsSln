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

        // [3,0,1,4,2] [0,0,0,0,0,0]
        // [5,6,3,2,1] [0,3,0,0,0,0]
        // [1,2,0,1,5] [0,0,0,0,0,0]
        // [4,1,0,1,7] [0,0,0,0,0,0]
        // [1,0,3,0,5] [0,0,0,0,0,0]
        //             [0,0,0,0,0,0] row1 = 2, col 1 =1, row2 = 4, col2 = 3
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                // 计算每个矩阵 [0, 0, i, j] 的元素和
                PreSum[i, j] = PreSum[i - 1, j] + PreSum[i, j - 1] + matrix[i - 1][j - 1] - PreSum[i - 1, j - 1];
                // 每个 (PreSum[i,j]) 都可以只用 上方、左方、左上方 三个已经算好的值再加上当前格子值算出来
                // PreSum[i - 1, j] = 上方
                // PreSum[i, j - 1] = 左方
                // PreSum[i - 1, j - 1] = 左上方
                // 上方和左方两块相加时，左上角那块 (PreSum[i-1,j-1]) 被重复算了两次，所以要减去一次（容斥）。
                // 最后再加上当前新增的单元格 (matrix[i-1][j-1])
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        // 目标矩阵之和由四个相邻矩阵运算获得
        // 减去上方、减去左方、补回左上
        return PreSum[row2 + 1, col2 + 1]
               - PreSum[row1, col2 + 1] // 上方
               - PreSum[row2 + 1, col1] // 左方
               + PreSum[row1, col1]; // 左上方
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */
//leetcode submit region end(Prohibit modification and deletion)
