/*
 * @lc app=leetcode id=867 lang=csharp
 * @lcpr version=30402
 *
 * [867] Transpose Matrix
 */

namespace Scratch.Labuladong.Algorithms.TransposeMatrix;

// @lc code=start
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
// @lc code=end
