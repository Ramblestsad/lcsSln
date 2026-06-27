namespace Scratch.Labuladong.Algorithms.MaxAreaOfIsland;

// 695. Max Area of Island (Medium)
//
// You are given an m x n binary matrix grid. An island is a group of 1's (representing land)
// connected 4-directionally (horizontal or vertical.) You may assume all four edges of the grid
// are surrounded by water.
//
// The area of an island is the number of cells with a value 1 in the island.
//
// Return the maximum area of an island in grid. If there is no island, return 0.
//
// Example 1:
//
// Input: grid =
// [[0,0,1,0,0,0,0,1,0,0,0,0,0],[0,0,0,0,0,0,0,1,1,1,0,0,0],[0,1,1,0,1,0,0,0,0,0,0,0,0],[0,1,0,0,1,1,0,0,1,0,1,0,0],[0,1,0,0,1,1,0,0,1,1,1,0,0],[0,0,0,0,0,0,0,0,0,0,1,0,0],[0,0,0,0,0,0,0,1,1,1,0,0,0],[0,0,0,0,0,0,0,1,1,0,0,0,0]]
// Output: 6
// Explanation: The answer is not 11, because the island must be connected 4-directionally.
//
// Example 2:
//
// Input: grid = [[0,0,0,0,0,0,0,0]]
// Output: 0
//
// Constraints:
//
// - m == grid.length
//
// - n == grid[i].length
//
// - 1 <= m, n <= 50
//
// - grid[i][j] is either 0 or 1.
//
// Related Topics: Array, Depth-First Search, Breadth-First Search, Union-Find, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
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
//leetcode submit region end(Prohibit modification and deletion)
