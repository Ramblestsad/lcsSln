namespace Scratch.Labuladong.Algorithms.CountSubIslands;

// 1905. Count Sub Islands (Medium)
//
// You are given two m x n binary matrices grid1 and grid2 containing only 0's (representing water)
// and 1's (representing land). An island is a group of 1's connected 4-directionally (horizontal
// or vertical). Any cells outside of the grid are considered water cells.
//
// An island in grid2 is considered a sub-island if there is an island in grid1 that contains all
// the cells that make up this island in grid2.
//
// Return the number of islands in grid2 that are considered sub-islands.
//
// Example 1:
//
// Input: grid1 = [[1,1,1,0,0],[0,1,1,1,1],[0,0,0,0,0],[1,0,0,0,0],[1,1,0,1,1]], grid2 =
// [[1,1,1,0,0],[0,0,1,1,1],[0,1,0,0,0],[1,0,1,1,0],[0,1,0,1,0]]
// Output: 3
// Explanation: In the picture above, the grid on the left is grid1 and the grid on the right is
// grid2.
// The 1s colored red in grid2 are those considered to be part of a sub-island. There are three
// sub-islands.
//
// Example 2:
//
// Input: grid1 = [[1,0,1,0,1],[1,1,1,1,1],[0,0,0,0,0],[1,1,1,1,1],[1,0,1,0,1]], grid2 =
// [[0,0,0,0,0],[1,1,1,1,1],[0,1,0,1,0],[0,1,0,1,0],[1,0,0,0,1]]
// Output: 2
// Explanation: In the picture above, the grid on the left is grid1 and the grid on the right is
// grid2.
// The 1s colored red in grid2 are those considered to be part of a sub-island. There are two
// sub-islands.
//
// Constraints:
//
// - m == grid1.length == grid2.length
//
// - n == grid1[i].length == grid2[i].length
//
// - 1 <= m, n <= 500
//
// - grid1[i][j] and grid2[i][j] are either 0 or 1.
//
// Related Topics: Array, Depth-First Search, Breadth-First Search, Union-Find, Matrix

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int CountSubIslands(int[][] grid1, int[][] grid2)
    {
        // 只要遍历 grid2 中的所有岛屿，把那些不可能是子岛的岛屿排除掉，剩下的就是子岛。
        var m = grid1.Length;
        var n = grid1[0].Length;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid1[i][j] == 0 && grid2[i][j] == 1)
                {
                    // 这个岛屿肯定不是子岛，淹掉
                    dfs(grid2, i, j);
                }
            }
        }

        // 现在 grid2 中剩下的岛屿都是子岛，计算岛屿数量
        var res = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid2[i][j] == 1)
                {
                    res++;
                    dfs(grid2, i, j);
                }
            }
        }

        return res;
    }

    // 从 (i, j) 开始，将与之相邻的陆地都变成海水
    void dfs(int[][] grid, int i, int j)
    {
        var m = grid.Length;
        var n = grid[0].Length;

        if (i < 0 || j < 0 || i >= m || j >= n) return;
        if (grid[i][j] == 0) return;

        grid[i][j] = 0;
        dfs(grid, i + 1, j);
        dfs(grid, i, j + 1);
        dfs(grid, i - 1, j);
        dfs(grid, i, j - 1);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
