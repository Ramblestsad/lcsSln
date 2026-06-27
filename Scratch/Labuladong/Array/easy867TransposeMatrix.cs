namespace Scratch.Labuladong.Algorithms.TransposeMatrix;

// 867. Transpose Matrix (Easy)
//
// Given a 2D integer array matrix, return the transpose of matrix.
//
// The transpose of a matrix is the matrix flipped over its main diagonal, switching the matrix's
// row and column indices.
//
// Example 1:
//
// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
// Output: [[1,4,7],[2,5,8],[3,6,9]]
//
// Example 2:
//
// Input: matrix = [[1,2,3],[4,5,6]]
// Output: [[1,4],[2,5],[3,6]]
//
// Constraints:
//
// - m == matrix.length
//
// - n == matrix[i].length
//
// - 1 <= m, n <= 1000
//
// - 1 <= m * n <= 10^5
//
// - -10^9 <= matrix[i][j] <= 10^9
//
// Related Topics: Array, Matrix, Simulation

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int[][] Transpose(int[][] matrix)
    {
        int m = matrix.Length, n = matrix[0].Length;
        // 转置矩阵的长和宽颠倒
        var res = new int[n][];
        for (int i = 0; i < n; i++)
        {
            res[i] = new int[m];
            for (int j = 0; j < m; j++)
            {
                res[i][j] = matrix[j][i];
            }
        }

        return res;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
