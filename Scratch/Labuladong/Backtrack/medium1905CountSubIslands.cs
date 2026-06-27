namespace Scratch.Labuladong.Algorithms.CountSubIslands;

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
