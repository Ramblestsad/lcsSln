namespace Scratch.Labuladong.Algorithms.NumberOfClosedIslands;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    public int ClosedIsland(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;

        // 提前把靠边的陆地都淹掉，然后算出来的就是封闭岛屿了。
        for (int j = 0; j < n; j++)
        {
            // 把靠上边的岛屿淹掉
            dfs(grid, 0, j);
            // 把靠下边的岛屿淹掉
            dfs(grid, m - 1, j);
        }

        for (int i = 0; i < m; i++)
        {
            // 把靠左边的岛屿淹掉
            dfs(grid, i, 0);
            // 把靠右边的岛屿淹掉
            dfs(grid, i, n - 1);
        }

        // 遍历 grid，剩下的岛屿都是封闭岛屿
        var res = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 0)
                {
                    res++;
                    dfs(grid, i, j);
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
        if (i < 0 || i >= m || j < 0 || j >= n)
        {
            // out of index
            return;
        }

        if (grid[i][j] == 1) return; // 已经是海水了

        // 将 (i, j) 变成海水
        grid[i][j] = 1;
        // 淹没上下左右的陆地
        dfs(grid, i + 1, j);
        dfs(grid, i, j + 1);
        dfs(grid, i - 1, j);
        dfs(grid, i, j - 1);
    }
}
//leetcode submit region end(Prohibit modification and deletion)
