namespace Scratch.Labuladong.Algorithms.UniquePathsIII;

//leetcode submit region begin(Prohibit modification and deletion)
public class Solution
{
    private int[][] dirs = new int[][] { [0, 1], [0, -1], [1, 0], [-1, 0] };
    private int res = 0;
    private bool[][] visited = null!;
    private int visitedCount = 0;
    private int totalCount = 0;

    public int UniquePathsIII(int[][] grid)
    {
        var m = grid.Length;
        var n = grid[0].Length;
        visited = new bool[m][];
        for (int k = 0; k < m; k++)
        {
            visited[k] = new bool[n];
        }

        var startI = 0;
        var startJ = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == 1)
                {
                    startI = i;
                    startJ = j;
                }

                if (grid[i][j] == 1 || grid[i][j] == 0)
                {
                    totalCount++;
                }
            }
        }

        _dfs(grid, startI, startJ);

        return res;
    }

    private void _dfs(int[][] grid, int i, int j)
    {
        var m = grid.Length;
        var n = grid[0].Length;

        // 剪枝，索引越界
        if (i < 0 || i >= m || j < 0 || j >= n) return;
        // 剪枝，跳过起点、障碍物、已访问的格子
        if (grid[i][j] == -1 || visited[i][j]) return;

        // 到达终点
        if (grid[i][j] == 2)
        {
            // 必须走过所有0和1一次
            if (visitedCount == totalCount) res++;
            return;
        }

        visited[i][j] = true;
        visitedCount++;

        foreach (var dir in dirs)
        {
            _dfs(grid, i + dir[0], j + dir[1]);
        }

        visited[i][j] = false;
        visitedCount--;
    }
}
//leetcode submit region end(Prohibit modification and deletion)
