namespace Scratch.Labuladong.Algorithms.MinPathSum;

// 64. Minimum Path Sum (Medium)
//
// Given a m x n grid filled with non-negative numbers, find a path from top left to bottom right,
// which minimizes the sum of all numbers along its path.
//
// Note: You can only move either down or right at any point in time.
//
// Example 1:
//
// Input: grid = [[1,3,1],[1,5,1],[4,2,1]]
// Output: 7
// Explanation: Because the path 1 → 3 → 1 → 1 → 1 minimizes the sum.
//
// Example 2:
//
// Input: grid = [[1,2,3],[4,5,6]]
// Output: 12
//
// Constraints:
//
// - m == grid.length
//
// - n == grid[i].length
//
// - 1 <= m, n <= 200
//
// - 0 <= grid[i][j] <= 200
//
// Related Topics: Array, Dynamic Programming, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int MinPathSum(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        // 构造备忘录，初始值全部设为 -1
        memo = new int[m][];
        for (int i = 0; i < m; i++)
        {
            memo[i] = new int[n];
            Array.Fill(memo[i], -1);
        }

        return dp(grid, m - 1, n - 1);
    }

    private int[][] memo = [];

    // 定义：从左上角位置(0, 0) 走到位置(i, j) 的最小路径和为 dp(grid, i, j)
    int dp(int[][] grid, int i, int j)
    {
        // base case
        if (i == 0 && j == 0)
        {
            return grid[0][0];
        }

        if (i < 0 || j < 0)
        {
            return int.MaxValue;
        }

        // 避免重复计算
        if (memo[i][j] != -1)
        {
            return memo[i][j];
        }

        // 将计算结果记入备忘录
        memo[i][j] = Math.Min(
            dp(grid, i - 1, j),
            dp(grid, i, j - 1)
        ) + grid[i][j];

        return memo[i][j];
    }
}
//leetcode submit region end(Prohibit modification and deletion)
