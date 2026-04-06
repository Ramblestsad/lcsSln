/*
 * @lc app=leetcode id=931 lang=csharp
 * @lcpr version=30402
 *
 * [931] Minimum Falling Path Sum
 */

namespace Scratch.Labuladong.Algorithms.MinimumFallingPathSum;

//Given an n x n array of integers matrix, return the minimum sum of any
//falling path through matrix.
//
// A falling path starts at any element in the first row and chooses the
//element in the next row that is either directly below or diagonally left/right.
//Specifically, the next element from position (row, col) will be (row + 1, col - 1), (
//row + 1, col), or (row + 1, col + 1).
//
//
// Example 1:
//
//
//Input: matrix = [[2,1,3],[6,5,4],[7,8,9]]
//Output: 13
//Explanation: There are two falling paths with a minimum sum as shown.
//
//
// Example 2:
//
//
//Input: matrix = [[-19,57],[-40,-5]]
//Output: -59
//Explanation: The falling path with a minimum sum is shown.
//
//
//
// Constraints:
//
//
// n == matrix.length == matrix[i].length
// 1 <= n <= 100
// -100 <= matrix[i][j] <= 100
//
//
// Related TopicsArray | Dynamic Programming | Matrix
//
// 👍 6975, 👎 174bug 反馈 | 使用指南 | 更多配套插件
//
//
//
//

// @lc code=start
public class Solution
{
    public int MinFallingPathSum(int[][] matrix)
    {
        var n = matrix.Length;
        var res = int.MaxValue;

        memo = new int[n][];
        for (int i = 0; i < n; i++)
        {
            memo[i] = new int[n];
            Array.Fill(memo[i], 66666);
        }

        // 终点可能在最后一行的任意一列，所以需要穷举一下j
        for (int j = 0; j < n; j++)
        {
            res = Math.Min(res, dp(matrix, n - 1, j));
        }

        return res;
    }

    private int[][] memo = null!;

    // 从第一行（matrix[0][..]）向下落，落到位置 matrix[i][j] 的最小路径和为 dp(matrix, i, j)。
    private int dp(int[][] matrix, int i, int j)
    {
        // 对于 matrix[i][j]，只有可能从
        // matrix[i-1][j], matrix[i-1][j-1], matrix[i-1][j+1]
        // 这三个位置转移过来。

        // 1、索引合法性检查
        if (i < 0 || j < 0 ||
            i >= matrix.Length ||
            j >= matrix[0].Length) return 99999;

        // 2、base case
        if (i == 0) return matrix[0][j];

        // 3、查找备忘录剪枝
        if (memo[i][j] != 66666) return memo[i][j];

        // 进行状态转移
        var minPreState = Math.Min(
            dp(matrix, i - 1, j),
            Math.Min(dp(matrix, i - 1, j - 1), dp(matrix, i - 1, j + 1)));
        memo[i][j] = matrix[i][j] + minPreState;

        return memo[i][j];
    }
}
// @lc code=end
