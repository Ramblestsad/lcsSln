/*
 * @lc app=leetcode id=64 lang=csharp
 * @lcpr version=30403
 *
 * [64] Minimum Path Sum
 */

namespace Scratch.Labuladong.Algorithms.MinPathSum;

// @lc code=start
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
// @lc code=end

/*
// @lcpr case=start
// [[1,3,1],[1,5,1],[4,2,1]]\n
// @lcpr case=end

// @lcpr case=start
// [[1,2,3],[4,5,6]]\n
// @lcpr case=end
 */
