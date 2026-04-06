/*
 * @lc app=leetcode id=695 lang=csharp
 * @lcpr version=30402
 *
 * [695] Max Area Of Island
 */

namespace Scratch.Labuladong.Algorithms.MaxAreaOfIsland;

// @lc code=start
public class Solution
{
    public int MaxAreaOfIsland(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        // 记录岛屿的最大面积
        var res = 0;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 1)
                {
                    // 淹没岛屿，并更新最大岛屿面积
                    res = Math.Max(res, dfs(grid, i, j));
                }
            }
        }

        return res;
    }

    // 淹没与 (i, j) 相邻的陆地，并返回淹没的陆地面积
    int dfs(int[][] grid, int i, int j)
    {
        var m = grid.Length;
        var n = grid[0].Length;

        if (i < 0 || j < 0 || i >= m || j >= n) return 0;
        if (grid[i][j] == 0) return 0;

        // 将 (i, j) 变成海水
        grid[i][j] = 0;

        return dfs(grid, i + 1, j)
               + dfs(grid, i, j + 1)
               + dfs(grid, i - 1, j)
               + dfs(grid, i, j - 1) + 1;
    }
}
// @lc code=end
