/*
 * @lc app=leetcode id=200 lang=csharp
 * @lcpr version=30402
 *
 * [200] Number Of Islands
 */

namespace Scratch.Labuladong.Algorithms.NumberOfIslands;

// @lc code=start
public class Solution
{
    public int NumIslands(char[][] grid)
    {
        var res = 0;
        var m = grid.Length;
        var n = grid[0].Length;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == '1')
                {
                    // 每发现一个岛屿，岛屿数量加一
                    res++;
                    // 然后使用 DFS 将岛屿淹了
                    // 为什么每次遇到岛屿，要用 DFS 算法把岛屿「淹了」？
                    // 主要是为了省事，避免维护 visited 数组。
                    _dfs(grid, i, j);
                }
            }
        }

        return res;
    }

    // 从 (i, j) 开始，将与之相邻的陆地都变成海水
    void _dfs(char[][] grid, int i, int j)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        if (i < 0 || i >= m || j < 0 || j >= n)
        {
            // out of index
            return;
        }

        if (grid[i][j] == '0') return; // 已经是海水了

        // 将 (i, j) 变成海水
        grid[i][j] = '0';
        // 淹没上下左右的陆地
        _dfs(grid, i + 1, j);
        _dfs(grid, i, j + 1);
        _dfs(grid, i - 1, j);
        _dfs(grid, i, j - 1);
    }
}
// @lc code=end
