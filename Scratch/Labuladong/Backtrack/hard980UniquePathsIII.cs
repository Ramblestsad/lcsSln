namespace Scratch.Labuladong.Algorithms.UniquePathsIII;

// 980. Unique Paths III (Hard)
//
// You are given an m x n integer array grid where grid[i][j] could be:
//
// - 1 representing the starting square. There is exactly one starting square.
//
// - 2 representing the ending square. There is exactly one ending square.
//
// - 0 representing empty squares we can walk over.
//
// - -1 representing obstacles that we cannot walk over.
//
// Return the number of 4-directional walks from the starting square to the ending square, that
// walk over every non-obstacle square exactly once.
//
// Example 1:
//
// Input: grid = [[1,0,0,0],[0,0,0,0],[0,0,2,-1]]
// Output: 2
// Explanation: We have the following two paths:
// 1. (0,0),(0,1),(0,2),(0,3),(1,3),(1,2),(1,1),(1,0),(2,0),(2,1),(2,2)
// 2. (0,0),(1,0),(2,0),(2,1),(1,1),(0,1),(0,2),(0,3),(1,3),(1,2),(2,2)
//
// Example 2:
//
// Input: grid = [[1,0,0,0],[0,0,0,0],[0,0,0,2]]
// Output: 4
// Explanation: We have the following four paths:
// 1. (0,0),(0,1),(0,2),(0,3),(1,3),(1,2),(1,1),(1,0),(2,0),(2,1),(2,2),(2,3)
// 2. (0,0),(0,1),(1,1),(1,0),(2,0),(2,1),(2,2),(1,2),(0,2),(0,3),(1,3),(2,3)
// 3. (0,0),(1,0),(2,0),(2,1),(2,2),(1,2),(1,1),(0,1),(0,2),(0,3),(1,3),(2,3)
// 4. (0,0),(1,0),(2,0),(2,1),(1,1),(0,1),(0,2),(0,3),(1,3),(1,2),(2,2),(2,3)
//
// Example 3:
//
// Input: grid = [[0,1],[2,0]]
// Output: 0
// Explanation: There is no path that walks over every empty square exactly once.
// Note that the starting and ending square can be anywhere in the grid.
//
// Constraints:
//
// - m == grid.length
//
// - n == grid[i].length
//
// - 1 <= m, n <= 20
//
// - 1 <= m * n <= 20
//
// - -1 <= grid[i][j] <= 2
//
// - There is exactly one starting cell and one ending cell.
//
// Related Topics: Array, Backtracking, Bit Manipulation, Matrix

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
