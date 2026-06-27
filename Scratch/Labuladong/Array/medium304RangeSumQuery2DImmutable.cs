namespace Scratch.Labuladong.Algorithms.RangeSumQuery2DImmutable;

// 304. Range Sum Query 2D - Immutable (Medium)
//
// Given a 2D matrix matrix, handle multiple queries of the following type:
//
// - Calculate the sum of the elements of matrix inside the rectangle defined by its upper left
// corner (row1, col1) and lower right corner (row2, col2).
//
// Implement the NumMatrix class:
//
// - NumMatrix(int[][] matrix) Initializes the object with the integer matrix matrix.
//
// - int sumRegion(int row1, int col1, int row2, int col2) Returns the sum of the elements of
// matrix inside the rectangle defined by its upper left corner (row1, col1) and lower right corner
// (row2, col2).
//
// You must design an algorithm where sumRegion works on O(1) time complexity.
//
// Example 1:
//
// Input
// ["NumMatrix", "sumRegion", "sumRegion", "sumRegion"]
// [[[[3, 0, 1, 4, 2], [5, 6, 3, 2, 1], [1, 2, 0, 1, 5], [4, 1, 0, 1, 7], [1, 0, 3, 0, 5]]], [2, 1,
// 4, 3], [1, 1, 2, 2], [1, 2, 2, 4]]
// Output
// [null, 8, 11, 12]
//
// Explanation
// NumMatrix numMatrix = new NumMatrix([[3, 0, 1, 4, 2], [5, 6, 3, 2, 1], [1, 2, 0, 1, 5], [4, 1,
// 0, 1, 7], [1, 0, 3, 0, 5]]);
// numMatrix.sumRegion(2, 1, 4, 3); // return 8 (i.e sum of the red rectangle)
// numMatrix.sumRegion(1, 1, 2, 2); // return 11 (i.e sum of the green rectangle)
// numMatrix.sumRegion(1, 2, 2, 4); // return 12 (i.e sum of the blue rectangle)
//
// Constraints:
//
// - m == matrix.length
//
// - n == matrix[i].length
//
// - 1 <= m, n <= 200
//
// - -10^4 <= matrix[i][j] <= 10^4
//
// - 0 <= row1 <= row2 < m
//
// - 0 <= col1 <= col2 < n
//
// - At most 10^4 calls will be made to sumRegion.
//
// Related Topics: Array, Design, Matrix, Prefix Sum

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
