namespace Scratch.Labuladong.Algorithms.MatrixBlockSum;

// 1314. Matrix Block Sum (Medium)
//
// Given a m x n matrix mat and an integer k, return a matrix answer where each answer[i][j] is the
// sum of all elements mat[r][c] for:
//
// - i - k <= r <= i + k,
//
// - j - k <= c <= j + k, and
//
// - (r, c) is a valid position in the matrix.
//
// Example 1:
//
// Input: mat = [[1,2,3],[4,5,6],[7,8,9]], k = 1
// Output: [[12,21,16],[27,45,33],[24,39,28]]
//
// Example 2:
//
// Input: mat = [[1,2,3],[4,5,6],[7,8,9]], k = 2
// Output: [[45,45,45],[45,45,45],[45,45,45]]
//
// Constraints:
//
// - m == mat.length
//
// - n == mat[i].length
//
// - 1 <= m, n, k <= 100
//
// - 1 <= mat[i][j] <= 100
//
// Related Topics: Array, Matrix, Prefix Sum

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[][] MatrixBlockSum(int[][] mat, int k)
    {
        var m = mat.Length;
        var n = mat[0].Length;
        var numMat = new NumMatrix(mat);
        var answer = new int[m][];
        for (int i = 0; i < m; i++)
        {
            answer[i] = new int[n];
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // coordinates of the top-left corner
                var x1 = Math.Max(i - k, 0);
                var y1 = Math.Max(j - k, 0);
                // coordinates of the bottom-right corner
                var x2 = Math.Min(i + k, m - 1);
                var y2 = Math.Min(j + k, n - 1);

                answer[i][j] = numMat.SumRegion(x1, y1, x2, y2);
            }
        }

        return answer;
    }

    class NumMatrix
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
            // 为什么index + 1？因为preSum矩阵多了一圈
            return PreSum[row2 + 1, col2 + 1]
                   - PreSum[row1, col2 + 1] // 上方
                   - PreSum[row2 + 1, col1] // 左方
                   + PreSum[row1, col1]; // 左上方
        }
    }
}
//leetcode submit region end(Prohibit modification and deletion)
